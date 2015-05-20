using UnityEngine;
using System.Collections.Generic;
//using Events;

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
		Event_Battle temp = new Event_Battle (MissionName);

		//Give Mission Number to all!
		for (int i =0; i<squad.Count; i++) {
			squad [i].AddEvent("\n" + MissionName + ":\n");
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

