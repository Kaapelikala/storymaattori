using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;



/// <summary>
/// Manages Soldiers. Creates new for each dead (begins at 6).
/// </summary>
public class SoldierManager : MonoBehaviour {

	public int SoldierID = 42001;
	public int[] squadIds=new int[4]{-2,-2,-2,-2};
	public int inSquadCurrently=0;

	public Campaing campaing;

	//public SoldierController[] soldiers = new SoldierController[25];
	public List<SoldierController> soldiers = new List<SoldierController> (0);
	public List<SoldierController> dead = new List<SoldierController> (0);

	void Start () {
		squadIds=new int[4]{-2,-2,-2,-2};
		//At start: six soldiers!
		this.CreateNewSoldier();
		this.CreateNewSoldier();

		this.CreateNewSoldier();
		this.CreateNewSoldier();

		this.CreateNewSoldier();
		this.CreateNewSoldier();

	}

	public void EmptySquad()
	{
		for (int i = 0; i<4; i++) {
			squadIds [i] = -1;
		}
		inSquadCurrently = 0;
	}

	public bool SetToMission (int i)
	{
		Array.Sort (squadIds);

		//If we are trying to add a soldier that does not exit.
		if (i >= soldiers.Count)
			return false;
		//does this one belong to the squad already?
		//If yes, remove from squad.
		bool found = false;
		int amount = 0;
		for (int k=0;k<4;k++)
		{
			if (squadIds[k]==i)
			{
				Debug.Log ("Deleting "+i+" from "+k);
				amount++;
				Debug.Log ("Amount of found: "+amount);
				found=true;
				squadIds[k]=-2;

			}
		}
		if (found) {
			if (amount>1)
			{
				inSquadCurrently-=amount;
			}
			else
			{
				inSquadCurrently--;
			}

			return false;
		}
		Debug.Log ("checking room...");

		//If there is room for soldiers.
		if (inSquadCurrently < 4) {
			Debug.Log("Adding "+i+" to "+inSquadCurrently);
			squadIds [0] = i;
			inSquadCurrently++;
			Debug.Log ("Currently in squad: "+inSquadCurrently);
			return true;
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
			Debug.Log("i: "+i+", index: "+indexes[i]);
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
			int temp = Mathf.FloorToInt(UnityEngine.Random.Range(0,soldiers.Count));
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
		SoldierController RECRUIT = new SoldierController(SoldierID);
			
		string JoiningEvent = "TS:" + campaing.TimeStamp + ": Joined " + campaing.SquadName + "\n";

		RECRUIT.AddEvent(JoiningEvent);

		soldiers.Add(RECRUIT);


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

					this.CreateNewSoldier();		//YES, NEW SOLDIER PER EACH DEAD!

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

	/*void OnGUI () {			//FOR QUICK GENERATION OF SOLDIERS
		if (GUI.Button (new Rect (0,0,200,20), "NEW SOLTTU")) {
			this.CreateNewSoldier();

		}
	}*/


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
