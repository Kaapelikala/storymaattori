using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class SoldierManager : MonoBehaviour {

	public int SoldierID = 42001;
	public int[] squadIds=new int[4];
	public int inSquadCurrently=0;

	//public SoldierController[] soldiers = new SoldierController[25];
	public List<SoldierController> soldiers = new List<SoldierController> (0);
	public List<SoldierController> dead = new List<SoldierController> (0);

	void Start () {
		//for testing. DELETE PRIOR TO LAUNCH
		//

	}

	public bool SetToMission (int i)
	{
		Debug.Log ("Setting to mission: " + i);
		if (i >= soldiers.Count)
			return false;
		if (inSquadCurrently < 4) {
			bool ok=true;
			for (int j=0;j<inSquadCurrently;j++)
			{if (squadIds[j]==i)
				ok=false;
			}
			if (ok)
			{
				squadIds [inSquadCurrently] = i;
				inSquadCurrently++;
				return true;
			}
			Debug.Log ("Currently in squad: "+inSquadCurrently);
		}
		return false;
	}
	public List<SoldierController> GetSquad ()
	{
		//return GetSquad (squadIds);
		//complicated

		//for testing, ALL SOLDIERS ARE SENT
		return soldiers;
	}



	//gets a specific squad using given indexes
	public List<SoldierController> GetSquad (int[] indexes)
	{
		if (soldiers.Count <4)
			return null;

		List<SoldierController> returned= new List<SoldierController> ();
		for (int i =0;i<4;i++)
		{
			returned.Add(soldiers[indexes[i]]);
		}
		return returned;
	}

	//gets a random squad 
	public List<SoldierController> GetSquad(bool random)
	{
		List<int> listOfNumbers=new List<int>();
		while (listOfNumbers.Count!=4&&listOfNumbers.Count<soldiers.Count)
		{
			int temp = Mathf.FloorToInt(Random.Range(0,soldiers.Count));
			if (!listOfNumbers.Contains(temp))
			{
				listOfNumbers.Add(temp);
			}
		}
		List <SoldierController> temporary = new List<SoldierController>();
		for (int i = 0;i<listOfNumbers.Count;i++)
		{
			temporary.Add(soldiers[listOfNumbers[i]]);
		}
		return temporary;

	}

	public void CreateNewSoldier() //creates new soldier!!
	{
		soldiers.Add(new SoldierController(SoldierID));
		Debug.Log ("Added soldier " + SoldierID + ". Size now " + soldiers.Count);
		SoldierID++;
		this.MoveDeadsAway();
	
	}

	public void MoveDeadsAway() //Moves dead Soldiers to Dead line
	{

		//foreach (SoldierController sotilas in soldiers)

		for(int i = 0; i < soldiers.Count ; i++)
		{
			if (soldiers[i] != null){

				if(soldiers[i].alive == false)		//first we copy dead
				{
					dead.Add (soldiers[i]);
					soldiers.RemoveAt(i);

				}
			}

		}

		//this.checkSizes();

	}



/*    public void CreateRandomSoldiers(int num) {
        for (int i = 0; i < num; i++)
        {
           /* TestSoldier soldier = new TestSoldier();
            soldier.id = i;
            soldier.callsign = "sign" + i;
            soldier.experience = Random.Range(1, 10);
            soldier.traits = i * 3;
            soldiers.Add(soldier);*/
    /*    }

    }

*/
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
