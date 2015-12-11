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

	string [] SecondNames = new string[] {
		"A.",
		"B.",
		"C.",
		"D.",
		"E.",
		"F.",
		"G.",
		"H.",
		"J.",
		"K.",
		"L.",
		"M.",
		"N.",
		"O.",
		"P.",
		"R.",
		"V.",
		"U.",
		"T.",
		"Z.",
		"X.",
		"Y.",
		"DC.",
		"Jr.",
		"II",
		"III",
		"IV"
	};


	void Start () {
		squadIds=new int[4]{-2,-2,-2,-2};
		//At start: six soldiers!
		this.CreateNewSoldier();
		this.CreateNewSoldier();

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

	public SoldierController CreateNewSoldier() //creates new soldier!!
	{
		
		if (soldiers.Count >19)	//cant have more than 19!
		{
			return null;
		}

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

		foreach (SoldierController solttu in soldiers) // if same first name OR random chance
		{
			if ((solttu.soldierFName == RECRUIT.soldierFName))
			{
				RECRUIT.soldierMName = SecondNames[(Mathf.RoundToInt(UnityEngine.Random.value*(SecondNames.GetLength(0)-1)))];
			}

		}
		if (RECRUIT.soldierMName == "" && UnityEngine.Random.Range (0,10) > 7)	//Because middle names are fun but not everyone needs one!
		{
			RECRUIT.soldierMName = SecondNames[(Mathf.RoundToInt(UnityEngine.Random.value*(SecondNames.GetLength(0)-1)))];
		}



			
		string JoiningEvent = "TS:" + campaing.TimeStamp + ": Joined " + campaing.SquadName + "\n";

		RECRUIT.AddHistory("-JOIN-: " + campaing.TimeStamp);

		RECRUIT.AddEvent(JoiningEvent);

		int traitRandomiser = UnityEngine.Random.Range(0, 12);

		string SoldierStatsAnalyser = "Bootcamp report of RCT " + RECRUIT.soldierFName + ":\n Condition as ";

		SoldierStatsAnalyser += StatToShort(RECRUIT.health);


		SoldierStatsAnalyser += ",\n Fighting spirit as ";

		SoldierStatsAnalyser += StatToShort(RECRUIT.morale);


		SoldierStatsAnalyser += ",\n Weapons Skills as ";

		SoldierStatsAnalyser += StatToShort(RECRUIT.skill);

		
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
		//if (RECRUIT.soldierID > 42008)	// not to the Soldiers created at the beginning!
			//campaing.ReportCont.CreateNewSoldierPopup(RECRUIT);

		Debug.Log ("Added soldier " + SoldierID + ". Size now " + soldiers.Count);
		SoldierID++;
		//this.MoveDeadsAway();

		return RECRUIT;

	
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
	
	/// <summary>
	/// Another soldier is dead. Checks if new recruits are needed! If Squad has more high-ranked soldiers more are assigned to it!
	/// </summary>
	/// <param name="Dead">dead soldier.</param>
	public void DeadSoldierNOTE(SoldierController Dead)
	{

		campaing.ReportCont.CreateDEADSoldierPopup(Dead);// this needs to be last so it is first thing to see!

	}

	public void CheckForNewSoldiers()
	{
		this.MoveDeadsAway();

		if (soldiers.Count >= 12)		// 12 is number of max soldiers!
		{}
		else if (campaing.MissionsToReinforcements <= 0)
		{
			campaing.MissionsToReinforcements = UnityEngine.Random.Range(3,7);

			float RankCalculator = 0;
			
			foreach (SoldierController soldier in soldiers)
			{
				RankCalculator += soldier.rank;
			}
			
			float NewAmountOfSoldiers = Math.Min((11 - soldiers.Count), 2+RankCalculator);		//MIN 2 , MAX 12 soldiers!

			bool SavingFormerStatus = campaing.ReportCont.ShowNewReports;	// save it - when many come at the same time no invidinual news!
			campaing.ReportCont.ShowNewReports = false;

			List<SoldierController> NewSoldiers = new List<SoldierController> (0);

			for ( int i = 0; i < NewAmountOfSoldiers; i++)
			{
				NewSoldiers.Add(this.CreateNewSoldier());
			}

			campaing.ReportCont.CreateReinforcementsPopUp(NewSoldiers);

			campaing.ReportCont.ShowNewReports = SavingFormerStatus;

		
//			float RankCalculator = 0;
//			
//			foreach (SoldierController soldier in soldiers)
//			{
//				if (!soldier.HasAttribute("newbie"))
//					RankCalculator += 0.25f;
//				RankCalculator += soldier.rank;
//			}
//			
//			float NewAmountOfSoldiers = Math.Min(19, (Math.Max(RankCalculator, 5.0f)));		//MIN 5 soldiers!
//			
//			for ( int i = soldiers.Count; i < NewAmountOfSoldiers; i++)
//			{
//				this.CreateNewSoldier();		//YES, NEW SOLDIER PER EACH DEAD!
//			}
		}

		if (soldiers.Count < 4)		// EMERGENCYREINFORCEMENT - always checked!
		{
			campaing.ReportCont.CreateEmergencySoldierPopup(this.CreateNewSoldier());	// should have different stats due emerg but whatever
			this.CheckForNewSoldiers();
		}
		
	}

	private string StatToShort(int Stat)
	{
		if (Stat > 110)
			return "very good";
		else if (Stat > 105)
			return "good";
		else if (Stat < 90)
			return "very poor";
		else if (Stat < 95)
			return "poor";
		
		return "OK";
		
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
