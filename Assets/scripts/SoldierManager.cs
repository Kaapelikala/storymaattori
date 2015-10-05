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

		bool NoSameName = true;

		//NO TWO SOLDIERS WITH THE SAME NAME
		while (NoSameName)
		{
			NoSameName = false;
			foreach (SoldierController solttu in soldiers)
			{
				if ((solttu.soldierFName == RECRUIT.soldierFName) && (solttu.soldierLName == RECRUIT.soldierLName))
				{
					NoSameName = true;
					RECRUIT = new SoldierController(SoldierID);
				}
			}
		}

			
		string JoiningEvent = "TS:" + campaing.TimeStamp + ": Joined " + campaing.SquadName + "\n";

		RECRUIT.AddEvent(JoiningEvent);

		int traitRandomiser = UnityEngine.Random.Range(0, 12);

		string SoldierStatsAnalyser = "Bootcamp report of RCT " + RECRUIT.soldierFName + ":\n Condition as ";

		if (RECRUIT.health <= 90)
		{
			SoldierStatsAnalyser += "poor";
		}
		else if (RECRUIT.health >= 110)
		{
			SoldierStatsAnalyser += "good";
		}
		else
		{
			SoldierStatsAnalyser += "OK";
		}


		SoldierStatsAnalyser += ",\n Fighting spirit as ";

		if (RECRUIT.morale <= 90)
		{
			SoldierStatsAnalyser += "poor";
		}
		else if (RECRUIT.morale >= 110)
		{
			SoldierStatsAnalyser += "good";
		}
		else
		{
			SoldierStatsAnalyser += "OK";
		}

		SoldierStatsAnalyser += ",\n Weapons Skills as ";

		if (RECRUIT.skill <= 95)
		{
			SoldierStatsAnalyser += "poor";
		}
		else if (RECRUIT.skill >= 105)
		{
			SoldierStatsAnalyser += "good";
		}
		else
		{
			SoldierStatsAnalyser += "OK";
		}
		
		RECRUIT.AddEvent(SoldierStatsAnalyser + ".\n");




		string sexdiff = "";
		
		if (RECRUIT.sex == 'm')
		{
			sexdiff = "his";
		}
		else
			sexdiff = "her";
		
		switch(traitRandomiser)
		{
		case 0:
			RECRUIT.AddAttribute("heroic");
			RECRUIT.AddEvent("Recruit seems to have bit of heroic blood!\n");
			break;
		case 1:
			RECRUIT.AddAttribute("accurate");
			RECRUIT.AddEvent("Recruit has good eyesight!\n");
			break;
		case 2:
			RECRUIT.AddAttribute("inaccurate");
			RECRUIT.AddEvent("Recruit scored poorly on weapon tests!\n");
			break;
		case 3:
			RECRUIT.AddAttribute("idiot");
			RECRUIT.AddEvent("Recruit was not the smartest of " + sexdiff+ " class!\n");
			break;
		case 4:
			RECRUIT.AddAttribute("loner");
			RECRUIT.AddEvent("Recruit enjoys time alone.\n");
			break;
		case 5:
			RECRUIT.AddAttribute("cook");
			RECRUIT.AddEvent("Recruit can create wonderful dinners from barely no ingridients!\n");
			break;
		case 6:
			RECRUIT.AddAttribute("tough");
			RECRUIT.AddEvent("Recruit almost enjoyed the hardships of the bootcamp!\n");
			break;
		case 7:
			RECRUIT.AddAttribute("young");
			RECRUIT.AddEvent("Recruit looks like a teenager!\n");
			break;
		case 8:
			RECRUIT.AddAttribute ("drunkard");
			RECRUIT.AddEvent("Recruit likes the bottle bit too much..\n");
			break;
		case 9:
			RECRUIT.AddAttribute("coward");
			RECRUIT.AddEvent("Recruit is REALLY easy to spook!\n");
			break;
		case 10:
			RECRUIT.AddAttribute("techie");
			RECRUIT.AddEvent("Recruit likes machines more than people!\n");
			break;
		default:
			RECRUIT.AddAttribute("lucky");
			RECRUIT.AddEvent("Nothing bad ever happens to this recruit!\n");
			break;
		}

		// IDEA: BOOTCAMP??

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
