using UnityEngine;
using System.Collections.Generic;
//using Events;


//HANDLES ACTUAL EVENTS - FIGHTING GOES TROUGHT THIS!
public class EventController : MonoBehaviour {

	public SoldierManager manager;
	public Campaing campaing;

	public Mission targetlocation;


	public void Fight (int Difficulty, Mission missionImput)
	{
		int[] indexes = {-1,-1,-1,-1};
		Fight (indexes, Difficulty, missionImput);
	}


	public void Fight(int [] indexes, int Difficulty, Mission missionImput)
{
		targetlocation = missionImput;
		string MissionName = campaing.GetNextMission();
		List<SoldierController> squad;

		//Get new squad. 
		if (indexes [0]==-1)
			squad = manager.GetSquad ();
		else
		{
			squad = manager.GetSquad(indexes);
		}
		Event_Battle Fight = new Event_Battle (MissionName);
		Event_Grenade Grenade = new Event_Grenade (MissionName);
		Event_EnemyEmplacement EnemyBunker = new Event_EnemyEmplacement();
		Event_Debrief MotherBase = new Event_Debrief();
		Event_Burial Grave = new Event_Burial();

		int soldierAmount = 0;
		int deadAmount = 0;
		
		int GreatestRank = 0;	// Highest rank soldier! Affects number of fight rounds but lessens difficulty!

		//Give Mission Number to all!



		foreach (SoldierController solttu in squad)
		{
			solttu.AddHistory("-MISSION-:"+campaing.missionNumber);
			solttu.AddEvent("\nTS:" + campaing.TimeStamp + ":" + MissionName +":\n");
			solttu.missions++;
			soldierAmount++;

			if (solttu.rank > GreatestRank)
				GreatestRank = solttu.rank;

			if (solttu.HasAttribute("cook"))
			{	
				if (solttu.callsign == "")
				{
				MotherBase.CookName = solttu.soldierFName +  " " + solttu.soldierLName;
				}
				else
				{
				MotherBase.CookName =  "'" + solttu.callsign +  "' " + solttu.soldierLName;
				}
			}

			this.CheckSoldierMorale(solttu);
			
		}

		if (GreatestRank == 1)		//troopers do not affect gameplay yet!
			GreatestRank = 0;

		//Enemies have NUMBER, successfull kills reduce this. Combat lasts until other side flees / is wiped out?

		bool MissionTargetDone = false;
		bool Retreat = false;

		// ENEMY NUMBERS ARE CALCULATED IN MISSION ITSELF!

		//while (missionImput.StillSomethingToKill() && (squad.Count > 0) && !Retreat)
		while (missionImput.StillSomethingToKill() == true && (squad.Count > 0) && (Retreat == false))
		{

			//randomising the soldier!

			int SoldierRandomiser = Mathf.FloorToInt(Random.Range(0,(squad.Count-1)));

			int i =	SoldierRandomiser;
			
			Debug.Log("Currently FIGHTING: " + squad[i].soldierID + "KILLS: " + missionImput.kills + "HOSTILES: " + missionImput.Hostiles + "\nCurrently SOMETHING TO KILL: " + missionImput.StillSomethingToKill() + "COUNT: " + (squad.Count > 0) + "RETREAT: " + Retreat);

			if (squad [i].alive == true){		//dead do not fight!
					int BattleEventRandomiser = Mathf.FloorToInt(Random.Range(0,100));
					
					//Other ideas : Enemy heavy  gun fire, OUR heavy gun fire?	CHANCE TO RETREAT!
					
					if (BattleEventRandomiser < 70)		// THE BASIC ATTACK
					{
						squad [i].AddKills(	missionImput.AddKills(	Fight.FightRound (squad [i], Difficulty + (Mathf.FloorToInt (Random.Range (-10, 10))) - GreatestRank)));
					}					
//					else if (targetlocation.type == "Assault" && BattleEventRandomiser > 90 && !MissionTargetDone)
//					{
//						//The HQ attack special here?
//						
//						// Only one chance to do it properly?
//						
//					}
					else 
					{
						if (Random.Range(0, 100) < 50){
							squad [i].AddKills(	missionImput.AddKills(	EnemyBunker.Encounter(squad [i], Difficulty + (Mathf.FloorToInt (Random.Range (-10, 10))) - GreatestRank)));
						}
						else{
							Grenade.CheckGrenade (squad [i], Difficulty + (Mathf.FloorToInt (Random.Range (-10, 10))) - GreatestRank);
						}
					}
				}
			else
				Retreat = true; // needs event of its own but works for now!
				
				
				
				//Everyone has enough processing power anyways.
			manager.MoveDeadsAway ();

		}


		//Extra angst if only survivor
		foreach (SoldierController solttu in squad)
		{
			if (solttu.alive == false)
			{
				deadAmount++;
			}
		}
		if ((soldierAmount - deadAmount) == 1)
		{
			HandleOnlySurvivor(squad);
		}


		// MISSION CALCULATES RESULTS - Was it victorius?

		bool Victory;

		if (Retreat == true)
			Victory = false;
		else
			bool Victory = missionImput.IsVictory();




		//DEBRIEFING FOR EACH!

		bool AwardBraveryMedal = false;

		if (targetlocation.type == "Assault" && Victory == true)
		{
			AwardBraveryMedal = true;		//SHOULD BE GIVEN TO ONLY SUCCEFFUL MISSIONS BUT AS OF NOW CANNOT BE CHECKED PROPERLY!
		}

		foreach (SoldierController solttu in squad)
		{
			if (solttu.alive == true)	
			{
				MotherBase.Handle(solttu, Victory, AwardBraveryMedal);
			}
		}
		foreach (SoldierController solttu in squad)
		{
			if (solttu.alive == false)	
			{
				Grave.Bury(solttu,manager, AwardBraveryMedal);
			}
		}

		manager.MoveDeadsAway ();

	}











	public void Vacate (int Difficulty)
	{
		int[] indexes = {-1,-1,-1,-1};
		Vacate (indexes, Difficulty);
	}

	public void Vacate(int [] indexes, int Difficulty)			//VACATION!
	{
		string MissionName = campaing.GetNextMission();
		List<SoldierController> squad;
		
		//Get new squad. 
		if (indexes [0]==-1)
			squad = manager.GetSquad ();
		else
		{
			squad = manager.GetSquad(indexes);
		}
				
		int soldierAmount = 0;
		int deadAmount = 0;
		int GreatestRank = 0;

		Event_Vacation Party = new Event_Vacation (MissionName);
		Event_Burial Grave = new Event_Burial();

		foreach (SoldierController solttu in squad)
		{
			solttu.AddEvent("\nTS:" + campaing.TimeStamp + ": VACATION\n");
			solttu.missions++;
			soldierAmount++;

			if (solttu.rank > GreatestRank)
				GreatestRank = solttu.rank;
		}


		foreach (SoldierController solttu in squad)
		{
			Party.Handle(solttu, Difficulty, GreatestRank);
		}

		/*Wild Party takes n rounds. 
		for (int j =0; j<(Mathf.FloorToInt(Random.Range((2),(10-GreatestRank)))); j++) {
			for (int i =0; i<squad.Count; i++) {
				
				if (squad [i].alive == true){		//dead do not party!!

					Party.Handle(squad [i], Difficulty, GreatestRank);

				}
								
				//Everyone has enough processing power anyways.
				manager.MoveDeadsAway ();
			}
			
			
		}*/

		
		//Extra angst if only survivor
		foreach (SoldierController solttu in squad)
		{
			if (solttu.alive == false)
			{
				deadAmount++;
			}
		}

		if ((soldierAmount - deadAmount) == 1)
		{
			HandleOnlySurvivor(squad);
		}

		foreach (SoldierController solttu in squad)
		{
			if (solttu.alive == false)	
			{
				Grave.Bury(solttu,manager, false);
			}
		}

		
		manager.MoveDeadsAway ();
		
	}

	private void CheckSoldierMorale(SoldierController solttu)	//How this soldier likes this mission?

	{

		
		int MoraleRoll = Random.Range(0, 100);
	
		if (solttu.morale > MoraleRoll)
		{
			if (solttu.HasAttribute("veteran") && solttu.HasAttribute("heroic"))
		{
			solttu.AddEvent("Come on you apes, who wants to live forever?\n");
			solttu.ChangeMorale(20);
		}
		else if ((solttu.HasAttribute("newbie") || solttu.HasAttribute("coward")) && !solttu.HasAttribute("heroic"))
		{
			if (Random.Range (0,10) > 5)
			{
				solttu.AddEvent(solttu.soldierLName + " was afraid to leave the base\n");
				solttu.ChangeMorale(-10);
			}
			else if (Random.Range (0,10) > 3)
			{
				solttu.AddEvent("This is not Kansas anymore..\n");
				solttu.ChangeMorale(-20);
			}
			else
			{
				solttu.AddEvent(solttu.GetRank() + " missed home!\n");
				solttu.ChangeMorale(-20);
			}
		}
		else if (solttu.HasAttribute("heroic") && (Random.Range (0,10) > 3))
		{
			solttu.AddEvent("Onward to glory!\n");
			solttu.ChangeMorale(10);
		}
		else if (Random.Range (0,10) > 7)
		{
			solttu.AddEvent(solttu.soldierLName + " looked forward to the mission!\n");
		}
		else if (Random.Range (0,10) > 7)
		{
			solttu.AddEvent("This one will be OK\n");
		}
		else
		{
			solttu.AddEvent("Lets do this!\n");
		}
		}
		else if (solttu.morale-20 < MoraleRoll) // minor fail
		{
			if (solttu.HasAttribute("veteran") && (Random.Range (0,10) > 3) )
			{
				solttu.AddEvent("One more.\n");
			}
			else if (solttu.missions > 6 && solttu.HasAttribute("veteran") && (Random.Range (0,10) > 6))
			{
				solttu.AddEvent("Still more missions.\n");
				solttu.ChangeMorale(-10);
			}
			else if (solttu.missions > 3 && !solttu.HasAttribute("veteran") && (Random.Range (0,10) > 6))
			{
				solttu.AddEvent("Urgh, another mission.\n");
				solttu.ChangeMorale(-10);
			}
			else if ((solttu.HasAttribute("newbie") || solttu.HasAttribute("coward")) && !solttu.HasAttribute("heroic"))
			{
				solttu.AddEvent("It was hard to leave the base.\n");
				solttu.ChangeMorale(-25);
			}
			else
			{
				solttu.AddEvent("It was not fun to go to mission.\n");
				solttu.ChangeMorale(-20);
			}
		}
		else if (solttu.morale < MoraleRoll) // minor fail
		{
			if (solttu.HasAttribute("veteran"))
			{
				solttu.AddEvent("One more. Who dies this time?\n");
				solttu.ChangeMorale(-10);
			}
			else if ((solttu.HasAttribute("coward") || solttu.HasAttribute("newbie")) && !solttu.HasAttribute("heroic"))
			{
				solttu.AddEvent("No! I am gona die out there! \n");
				solttu.ChangeMorale(-40);
			}
			else {
				solttu.AddEvent("Leaving for the mission was hell.\n");
				solttu.ChangeMorale(-30);
			}
		}			
}

	private void HandleOnlySurvivor(List<SoldierController> squad){

		foreach (SoldierController solttu in squad)
		{
			if (solttu.alive == true)	//find the one alive
			{
				string Sexdiff = "";
				if (solttu.sex == 'm')
				{
					Sexdiff = "He";
				}
				else 
				{
					Sexdiff = "She";
				}
				
				if (solttu.HasAttribute("loner") && solttu.HasAttribute("coward"))
				{
					solttu.AddEvent(Sexdiff + " was the only survivor. It was not good at all.\n");
					solttu.ChangeMorale(-10);
				}
				else if (solttu.HasAttribute("loner") == true)
				{
					solttu.AddEvent(Sexdiff + " was the only survivor. It did not matter at all.\n");
				}
				else if (solttu.HasAttribute("coward") == true)
				{
					solttu.AddEvent(Sexdiff + " was the only survivor. It was completely horrible!\n");
					solttu.ChangeMorale(-50);
				}
				else if ((Random.Range(0, 100) > 80))
				{
					solttu.AddEvent(solttu.getCallsignOrFirstname() + " was the only survivor. " + Sexdiff +" did not care any more.\n");
					solttu.AddAttribute("loner");
					solttu.skill++;
				}
				else if ((Random.Range(0, 100) > 80) && !solttu.HasAttribute("heroic"))
				{
					solttu.AddEvent(solttu.getCallsignOrFirstname() + " was the only survivor. Mental scars will not heal.\n");
					solttu.AddAttribute("coward");
					solttu.ChangeMorale(-30);
				}
				else
				{
					solttu.AddEvent(Sexdiff + " was the only survivor. It was depressing.\n");

					solttu.ChangeMorale(-20);
					if (Random.Range (0,0) > 8)
					{
						solttu.AddAttribute("depressed");
						solttu.AddEvent("Really depressing!\n");
					}
				}
				
			}
		}

	}


	/*void OnGUI () {
		if (GUI.Button (new Rect (30,250,250,20), "!!!TAPPELU!!!")) {
			this.Fight();
			
		}*/
		/*if (GUI.Button (new Rect (50,250,200,40), "CHECK DEAD")) {
			this.MoveDeadsAway();
			
		}*/
	//}


}

