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

		//Give Mission Number to all!

		foreach (SoldierController solttu in squad)
		{
			solttu.AddEvent("\nTS:" + campaing.TimeStamp + ":" + MissionName +":\n");
			solttu.missions++;

			if (solttu.HasAttribute("cook"))
				MotherBase.CookName =  "'" + solttu.callsign +  "' " + solttu.soldierLName;

			
		}

		//Combat takes n rounds. 
		for (int j =0; j<(Mathf.FloorToInt(Random.Range(1,5))); j++) {
			for (int i =0; i<squad.Count; i++) {

				int BattleEventRandomiser = Mathf.FloorToInt(Random.Range(0,100));
				
				if (BattleEventRandomiser < 80)
					Fight.FightRound (squad [i], 90 + (Mathf.FloorToInt (Random.Range (0, 20))));
				else {
				Grenade.CheckGrenade (squad [i], 90 + (Mathf.FloorToInt (Random.Range (0, 20))));
				}
			
				//squad [i].AddEvent("Missed a lot\n");


				//Everyone has enough processing power anyways.
				manager.MoveDeadsAway ();
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

