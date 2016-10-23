using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mission : MonoBehaviour {

	public string MissionName = "";
	public string location;
	public List<SoldierController> squad;
	public string type;		//what kind of mission this is!
	public int difficulty;
	public int encounterRange = 0;	//How far or near the battle happens: 0 is average, -1 is CLOSE and +1 is FAR
	public int ExpectedEncounterRange = 0;	//What Command Expects is to be!
	int killsStart=0;
	int thisMissionKills = 0;

	public int Hostiles = 0;
	public int kills = 0;

	public Campaing ReportToCampaing;

	int soldiersDead = 0;
		
		// outcome is a String would be better!
	public bool retreat = false;	
	public bool Enemyretreat = false;	
	public bool victory = false;
	public bool LOCKED = false;		//LOCKS SO IT IS NOT CALCULATED AGAIN!

	public Mission (string location, string type, int difficulty, Campaing CampaingImput)
	{
		this.location=location;
		this.type=type;
		this.difficulty=difficulty;
		this.ReportToCampaing = CampaingImput;
		this.Hostiles= Mathf.FloorToInt(Random.Range(1,3) + Random.Range(1,3));
		this.encounterRange = Mathf.FloorToInt(Random.Range(-1,1));
		this.ExpectedEncounterRange = encounterRange + Mathf.FloorToInt(Random.Range(-1,1));	

		//int EnemyNumber = 4; // for easier testing!
		
		if (Mathf.FloorToInt(ReportToCampaing.Campaing_Difficulty/20) >= 5) // if Campaing difficulty is larger than 100 more enemies!
			this.Hostiles ++;

		if (this.type=="Assault")	// yes, it is harder!
		{
			this.Hostiles += Mathf.FloorToInt(Random.Range(1,4));
		}
		if (this.type=="Patrol")	// Chance to encounter enemies is randomised HERE instead of actual play!
		{
			int EnemyEncounterChance = this.difficulty - 50;	// often will be about 50ish

			if (Random.Range (0,100) > EnemyEncounterChance) // NO ENEMIES
			{
				this.Hostiles = 0;
				this.victory = true; // no enemies = victory is assured?? (for now at least)
			}
		}
		else if (this.type=="Vacation")
			this.Hostiles = 0;

		//this.name = "M:" + this.ReportToCampaing.missionNumber + "";
		//this.name = "";
	}
	public void AddSquad(List<SoldierController> squad)
	{	
		Debug.Log ("Squad Size: "+squad.Count);
		this.squad=squad;
		foreach (SoldierController s in squad) {
			killsStart+=s.kills;
			//Debug.Log(killsStart);
		}
	}

	override public string ToString()
	{
		if (squad == null) {
			Debug.Log ("NULL!!!");
		return "";
		}
			else{

			if (LOCKED == false)
				this.IsVictory();		//Calculates actual NUMBERS behind all! this just PRINTS stuff

			string returned = "";
			returned += MissionName + "\n";
			returned += "--Location: " + location + "\n";
			returned += "--Operation: " + type + "\n";
			returned += "--Members:\n";
			//bool[] dead = new bool[4];
			foreach (SoldierController soldier in squad) {

				returned += soldier.AllNames() + "\n";

			}




			if ((this.type != "Vacation")  )		//Show battle notification if NOT vacation or NOT Patrol with no encounter
			{
				if (this.type == "Patrol" && this.Hostiles > 0)
				{
					if (this.type == "Patrol") // if partrol where is enemies print this special addition
					{
						returned += "--The location contained enemies!\n";
					}
					returned += ("--The battle took place in " + this.getEncounterRange() + " range.\n");
					
					if (Hostiles > squad.Count*1.5)
						returned += "--The Squad was greatly outnumbered!\n";
					else if (Hostiles > squad.Count+Random.Range(1,2))
						returned += "--The Squad was outnumbered!\n";
					else if (Hostiles < squad.Count-Random.Range(1,2))
						returned += "--The Squad outnumbered the enemy!\n";
	//				else
	//					returned += "--The Squad and enemies were about even.\n";


					returned += "--During the mission soldiers killed: ";

					returned += thisMissionKills + "\n";
				}
				else if (this.type == "Patrol" && this.Hostiles == 0)	//special bit for non-combat patrol missions
				{
					returned += "--The location was clear of enemy activity!\n";

				}
			}

			if (Enemyretreat == true)
			{
				returned += ("--Rest enemies retreated.\n");
			}

			bool wastedPrinted = false;



			foreach (SoldierController soldier in squad) {
				if (!soldier.alive) {
					if (!wastedPrinted) {
						returned += "--During the mission died: \n";
						wastedPrinted=true;
					}

					returned += soldier.AllNames() + "\n";
				}
			}

			if (this.type != "Vacation")
			{
				if (this.type == "Patrol")
				{
					if (this.Hostiles == 0 && retreat == false)
					{
						returned += "--Mission was a SUCCESS!\n";
					}
					else
					{
						returned += this.StandardMissionResults();
					}
				}
				else
				{
					returned += this.StandardMissionResults();
				}
			}




			return returned;

		}


		}

	private string StandardMissionResults ()
	{

		string returned = "";

		if (retreat == true)	//all are dead
		{
			returned += "--Mission ended in RETREAT!\n";
		}
		else if (squad.Count == soldiersDead)	//all are dead
		{
			returned += "--Mission was a TOTAL DEFEAT!\n";
		}
		else if (thisMissionKills < soldiersDead)
		{
			returned += "--Mission was a FAILURE!\n";
		}
		else if (thisMissionKills == soldiersDead)
		{
			returned += "--Mission was a DRAW!\n";
		}
		else if (thisMissionKills > squad.Count*2)
		{
			returned += "--Mission was A MAJOR VICTORY!\n";
		}
		else
		{
			returned += "--Mission was A VICTORY!\n";
		}

		return returned;
	}

	//IS THIS MISSION VICTORY?
	public bool IsVictory()
		{
		
			if (LOCKED == true)
				return this.victory;

			if (this.type != "Vacation")
			{
				
				int killsNow = 0;
				foreach (SoldierController soldier in squad) {
					killsNow += soldier.kills;
				}


				thisMissionKills = killsNow - killsStart;
				
				ReportToCampaing.TotalKills += thisMissionKills;		//reports to Campaing the kills!

				foreach (SoldierController soldier in squad) {
						if (!soldier.alive) {
							this.soldiersDead++;
							ReportToCampaing.TotalDead++;		//reports to Campaing the deads!!
						}
					}
									

				if (this.retreat == true)
				{
					this.victory = false;
				}	
				else if (thisMissionKills < soldiersDead)	//Victory is simple: Did they kill more than lost?
				{
					this.victory = false;
				}
				else
				{
					this.victory = true;
				}


			}
			else
			{
				this.victory = true;	//Party is always victory (or at least now)
			}

			this.LOCKED = true;
			return this.victory;
		}

	public int AddKills(int HowMany)
	{

		//return HowMany;

		if (HowMany <= 0)
			return 0;
		else if (this.kills >= this.Hostiles)
		{
			this.kills = this.Hostiles;
			return 0;
		}
		else if (this.kills + HowMany >= this.Hostiles)	// goes over, remaining are killed!
		{
			int calculation = this.Hostiles - this.kills;
			kills += calculation;
			return calculation;
		}
		else if (HowMany <= this.Hostiles)
		{
			kills += HowMany;
			return HowMany;
		}


		return 0; 
	}

	public string getEncounterRange()
	{
		if (encounterRange > 0)
		{
			return "Far";
		}
		else if (encounterRange < 0)
		{
			return "Close";
		}

		return "Near";

	}
	public string getExpEncounterRange()
	{
		if (ExpectedEncounterRange > 0)
		{
			return "Far";
		}
		else if (ExpectedEncounterRange < 0)
		{
			return "Close";
		}
		
		return "Near";
		
	}

	public bool StillSomethingToKill()
	{
		if (kills < Hostiles)
		{
			return true;
		}
		return false;
	}

		


}