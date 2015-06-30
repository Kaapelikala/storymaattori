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
		Event_Debrief MotherBase = new Event_Debrief();

		int soldierAmount = 0;
		int deadAmount = 0;
		
		int GreatestRank = 0;	// Highest rank soldier! Affects number of fight rounds but lessens difficulty!

		//Give Mission Number to all!



		foreach (SoldierController solttu in squad)
		{
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
			
		}

		if (GreatestRank == 1)		//troopers do not affect gameplay yet!
			GreatestRank = 0;

		//IDEA not implemented: Enemies have NUMBER, successfull kills reduce this. Combat lasts until other side flees / is wiped out?

		bool MissionTargetDone = false;

		//Combat takes n rounds. 
		for (int j =0; j<(Mathf.FloorToInt(Random.Range((1+GreatestRank),(6+GreatestRank)))); j++) {
			for (int i =0; i<squad.Count; i++) {

				if (squad [i].alive == true){		//dead do not fight!
					int BattleEventRandomiser = Mathf.FloorToInt(Random.Range(0,100));

					//Other ideas : Enemy heavy  gun fire, OUR heavy gun fire?

					if (BattleEventRandomiser < 80)
						Fight.FightRound (squad [i], Difficulty + (Mathf.FloorToInt (Random.Range (-10, 10))) - GreatestRank);
					else if (targetlocation.type == "Assault" && BattleEventRandomiser > 90 && !MissionTargetDone)
					{
						//The HQ attack special here?

						// Only one chance to do it properly?

					}
					else 
					{
						Grenade.CheckGrenade (squad [i], Difficulty + (Mathf.FloorToInt (Random.Range (-10, 10))) - GreatestRank);
					}
				}
			
				//squad [i].AddEvent("Missed a lot\n");


				//Everyone has enough processing power anyways.
				manager.MoveDeadsAway ();
			}


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



		//DEBRIEFING FOR ALIVES!

		foreach (SoldierController solttu in squad)
		{
			if (solttu.alive == true)
			{
			MotherBase.Handle(solttu);
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
		

		
		manager.MoveDeadsAway ();
		
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
					solttu.AddEvent(solttu.callsign + " was the only survivor. " + Sexdiff +" did not care any more.\n");
					solttu.AddAttribute("loner");
					solttu.skill++;
				}
				else if ((Random.Range(0, 100) > 80))
				{
					solttu.AddEvent(solttu.callsign + " was the only survivor. Mental scars will not heal.\n");
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

