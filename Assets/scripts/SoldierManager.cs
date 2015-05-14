using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class SoldierManager : MonoBehaviour {

	public int SoldierID = 42001;

	public SoldierController[] soldiers = new SoldierController[25]; 
	//public ArrayList soldiers = new ArrayList();   // too hard!
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
		soldiers[NumberAlive] = new SoldierController(SoldierID);
		NumberAlive++;

		SoldierID++;
	
	}

	public void MoveDeadsAway() //Moves dead Soldiers to Dead line
	{
		int NewDeads = 0;

		//foreach (SoldierController sotilas in soldiers)

		for(int i = 0; i < soldiers.Length ; i++)
		{
			if (soldiers[i] != null){

				if(soldiers[i].alive == false)		//first we copy dead
				{
					dead[NumberDead] = soldiers[i];
					Debug.Log(soldiers[i].soldierID + " is deposited correctly!");

					NewDeads++;
					NumberDead++;
				}
			}

		}


		for(int i = 0; i < soldiers.Length ; i++)
		{		
			if (soldiers[i] != null){
				
				if(soldiers[i].alive == false)		//then we remove them at the location! WERY CLUMBERSOME AT THIS MOMENT!
				{
					System.Collections.Generic.List<SoldierController> list = new System.Collections.Generic.List<SoldierController>(soldiers);
					
					list.Remove(soldiers[i]);
					list.Add (null);
					soldiers = list.ToArray();

					i--;
					NumberAlive--;

					
				}
			}
		}

		//this.checkSizes();

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
		if (GUI.Button (new Rect (50,250,200,40), "CHECK DEAD")) {
			this.MoveDeadsAway();
			
		}
	}


	/*public void checkSizes() {		//pitäs auttaa siin ettei mennä yli jos tulee paljon sotilaita
		if (NumberAlive > this.soldiers.Length/2) {
			SoldierController[] NEWARRAY = new SoldierController[this.soldiers.Length*2]; 

			for(int i = 0; i < soldiers.Length ; i++)
			{
				this.soldiers.CopyTo(NEWARRAY, i);
			}

			this.soldiers = NEWARRAY;
		}
		if (NumberDead > this.dead.Length/2) {
			SoldierController[] NEWARRAY = new SoldierController[this.dead.Length*2]; 
			
			
			for(int i = 0; i < dead.Length ; i++)
			{
				this.dead.CopyTo(NEWARRAY, i);
			}
			
			this.dead = NEWARRAY;
		}
		
		
	}*/
	
	
}
