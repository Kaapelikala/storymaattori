using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class SoldierManager : MonoBehaviour {

	public SoldierController[] soldiers = new SoldierController[25]; 
	public int NumberAlive = 0;

	public SoldierController[] dead = new SoldierController[25]; 
	public int NumberDead = 0;

	// Use this for initialization
	void Start () {

		//for testing. DELETE PRIOR TO LAUNCH
		this.CreateNewSoldier();

	}

	public void CreateNewSoldier() //creates new soldier!!
	{
		soldiers[NumberAlive] = new SoldierController();
		NumberAlive++;

	}



    public void CreateRandomSoldiers(int num) {
        for (int i = 0; i < num; i++)
        {
           /* TestSoldier soldier = new TestSoldier();
            soldier.id = i;
            soldier.callsign = "sign" + i;
            soldier.experience = Random.Range(1, 10);
            soldier.traits = i * 3;
            soldiers.Add(soldier);*/
        }

    }


	/* ArrayList<SoldierController> GetSoldiers() {

		//this.soldiers.
        return this.soldiers;
    }*/

	void OnGUI () {
		if (GUI.Button (new Rect (30,200,200,20), "NEW SOLTTU")) {
			this.CreateNewSoldier();

		}
	}
}
