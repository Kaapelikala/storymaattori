using UnityEngine;
using System.Collections.Generic;
//using Events;

public class EventController : MonoBehaviour {

	public SoldierManager manager;
	
	public void Fight()
{
		List<SoldierController> squad = manager.GetSquad ();
		Event_Battle temp = new Event_Battle ();
		//Combat takes n rounds. 
		for (int j =0; j<(Mathf.FloorToInt(Random.Range(3,10))); j++) {
			for (int i =0; i<squad.Count; i++) {
				temp.FightRound (squad [i], 90 + (Mathf.FloorToInt (Random.Range (0, 20))));
				if (squad [i].health <= 0) {
					//kill the dood.
					manager.MoveDeadsAway ();
				}
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

