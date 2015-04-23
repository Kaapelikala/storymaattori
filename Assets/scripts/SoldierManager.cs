using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class SoldierManager : MonoBehaviour {

    List<TestSoldier> soldiers; 

	// Use this for initialization
	void Start () {
	    this.soldiers = new List<TestSoldier>();

        // testing
        this.CreateRandomSoldiers(5);
	}
	
	

    public void CreateRandomSoldiers(int num) {
        for (int i = 0; i < num; i++)
        {
            TestSoldier soldier = new TestSoldier();
            soldier.id = i;
            soldier.callsign = "sign" + i;
            soldier.experience = Random.Range(1, 10);
            soldier.traits = i * 3;
            soldiers.Add(soldier);
        }

    }


    public List<TestSoldier> GetSoldiers() {
        return this.soldiers;
    }

}
