using UnityEngine;
using System.Collections.Generic;
//using Events;

public class EventController : MonoBehaviour {

	public SoldierManager manager;
	public Campaing campaing;
	
	public void Fight()
{
		string MissionName = campaing.GetNextMission();

		//Get new squad. 
		List<SoldierController> squad = manager.GetSquad ();
		Event_Battle temp = new Event_Battle (MissionName);

		//Give Mission Number to all!

		foreach (SoldierController solttu in squad)
		{
			solttu.AddEvent("\n" + MissionName + ":\n");
			
		}

		//Combat takes n rounds. 
		for (int j =0; j<(Mathf.FloorToInt(Random.Range(3,10))); j++) {
			for (int i =0; i<squad.Count; i++) {
				temp.FightRound (squad [i], 90 + (Mathf.FloorToInt (Random.Range (0, 20))));
				//Everyone has enough processing power anyways.
				manager.MoveDeadsAway ();
			}
		}
	}



	void OnGUI () {
		if (GUI.Button (new Rect (30,250,250,20), "!!!TAPPELU!!!")) {
			this.Fight();
			
		}
		/*if (GUI.Button (new Rect (50,250,200,40), "CHECK DEAD")) {
			this.MoveDeadsAway();
			
		}*/
	}


}

