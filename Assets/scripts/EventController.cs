using UnityEngine;
using System.Collections.Generic;
//using Events;


//HANDLES ACTUAL EVENTS - FIGHTING GOES TROUGHT THIS!
public class EventController : MonoBehaviour {

	public SoldierManager manager;
	public Campaing campaing;



	public void Fight ()
	{
		int[] indexes = {-1,-1,-1,-1};
		Fight (indexes);
	}


	public void Fight(int [] indexes)
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
				MotherBase.CookName =  "'" + solttu.callsign +  "' " + solttu.soldierLName;

			
		}

		if (GreatestRank == 1)		//troopers do not affect gameplay yet!
			GreatestRank = 0;


		//Combat takes n rounds. 
		for (int j =0; j<(Mathf.FloorToInt(Random.Range(1,(6+GreatestRank)))); j++) {
			for (int i =0; i<squad.Count; i++) {

				if (squad [i].alive == true){		//dead do not fight!
					int BattleEventRandomiser = Mathf.FloorToInt(Random.Range(0,100));
					
					if (BattleEventRandomiser < 80)
						Fight.FightRound (squad [i], 90 + (Mathf.FloorToInt (Random.Range (0, 20))) - GreatestRank);
					else 
					{
						Grenade.CheckGrenade (squad [i], 90 + (Mathf.FloorToInt (Random.Range (0, 20))) - GreatestRank);
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
					if (solttu.HasAttribute("loner") == true)
					{
						solttu.AddEvent(Sexdiff + " was the only survivor. It did not matter at all.\n");
					}
					else if ((Random.Range(0, 100) > 80))
					{
						solttu.AddEvent(solttu.callsign + " was the only survivor. " + Sexdiff +" did not care any more.\n");
						solttu.AddAttribute("loner");
						solttu.skill++;
					}
					else
					{
						solttu.AddEvent(Sexdiff + " was the only survivor. It was depressing.\n");
						solttu.ChangeMorale(-20);
					}

				}
			}
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



	/*void OnGUI () {
		if (GUI.Button (new Rect (30,250,250,20), "!!!TAPPELU!!!")) {
			this.Fight();
			
		}*/
		/*if (GUI.Button (new Rect (50,250,200,40), "CHECK DEAD")) {
			this.MoveDeadsAway();
			
		}*/
	//}


}

