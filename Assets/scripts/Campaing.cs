using UnityEngine;
using System.Collections;

public class Campaing : MonoBehaviour {

	public int CampaingYear = 0;

	public int SquadID = 4118;
	public string SquadName = "Raccon Squad";

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

	// Use this for initialization
	void Start () {

		this.Begin();
	
	}


	public string GetNextMission(){

		missionNumber++;
		TimeStamp += 10;

		return "M"+ missionNumber;

	}

	// Update is called once per frame
	void Update () {
	
	}
	

	public string Begin(){

		string alkuteksti = "In the year ";

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
