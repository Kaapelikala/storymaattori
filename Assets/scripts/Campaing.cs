using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


// Large and high up in hierarchy: it is supposed to be deposit of lore & Connectivity between scripts!!!
// Now used mostly for timestamps and mission names
public class Campaing : MonoBehaviour {

	public int CampaingYear = 0;

	public int SquadID = 4118;
	public string SquadName = "Raccoon Squad";

	public string PlanetName = "Nucheron IV";
	public string FriendName = "Human Empire";
	public string EnemyName = "Maulers of Ghan";

	public bool HumanAttacker = true;

	public string CauseOfWar = "";

	public string PlanetType = "arid";
	public string PlanetAdje = "dry";

	//for notes
	public int missionNumber = 0;
	public int TimeStamp = 0;
	public int TotalKills = 0;
	public int TotalDead = 0;

	public int Campaing_Difficulty = 100; //used to determine current front difficulty. AFFECTS ALL BATTLES

	public Text BeginText;

	public Text WarLog;

	public string alkuteksti = "";

	public SoldierManager Soldiers;
	public ButtonController ButtonCont;
	public ReportController ReportCont;

	// Use this for initialization
	void Start () {

		BeginText.text = this.Begin();
	
	}


	public string GetNextMission(){

		missionNumber++;
		TimeStamp += Mathf.RoundToInt((Random.Range(4, 8))+ (Random.Range(4, 8)));

		return "M"+ missionNumber;

	}

	// Update is called once per frame
	void Update () {

		WarLog.text = 
				"TimeStamp:" + "\n" + TimeStamp + "\n" +
				"Missions:" + "\n" + missionNumber + "\n" +
				"Total Kills:" + "\n" + TotalKills + "\n" +
				"Total Deaths:" + "\n" + TotalDead+ "\n" +
				"Kills/Deaths:" + "\n";	

		if (TotalDead == 0)
		{
			WarLog.text += "?!?";
		}
		else
		{
			WarLog.text += ""+ TotalKills/TotalDead;
		}
	}
	

	public string Begin(){

		alkuteksti = "In the year ";

	 	CampaingYear =	Random.Range(4100, 4998);
		SquadID = Random.Range(1901, 2299);
		
		alkuteksti += CampaingYear.ToString() + ", the ";
				
		//WHICH ARE ATTACKERS
		int WarAttackerRandomiser = Random.Range(0, 1);

		switch (WarAttackerRandomiser)
		{
		case 0:
			HumanAttacker = true;
			break;
		case 1:
			HumanAttacker = false;
			break;
		}

		if (HumanAttacker)
			alkuteksti +=FriendName;
		else
			alkuteksti += EnemyName;

		//WHY
		int WarReasonRandomiser = Random.Range(0, 2);

		switch (WarReasonRandomiser)
		{
		case 0:
			this.CauseOfWar = "Attack";
			break;
		case 1:
			this.CauseOfWar = "Suprice";
			break;
		default:
			this.CauseOfWar = "Betrayal";
			break;
		}

		if (CauseOfWar == "Suprice"){
			alkuteksti += " supriced the ";
		}
		else if (CauseOfWar == "Betrayal"){
			alkuteksti += " betrayed the ";
		}
		else {
			alkuteksti += " attacked the ";
		}

		if (HumanAttacker)
			alkuteksti += EnemyName + " on the ";
		else
			alkuteksti += FriendName + " on the ";

		alkuteksti += PlanetType + " world of " + PlanetName + ".";

		alkuteksti += "\n The following is the story of " + FriendName + "s squad " + SquadID + " called the " + SquadName + ".";

		Debug.Log (alkuteksti);

		return alkuteksti;
	}

}
