using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mission : MonoBehaviour {

	public string MissionName = "";
	public string location;
	public List<SoldierController> squad;
	public string type;		//what kind of mission this is!
	public int difficulty;
	int killsStart=0;
	int thisMissionKills = 0;

	public Campaing ReportToCampaing;

	int soldiersDead = 0;

	public bool victory = false;
	public bool LOCKED = false;		//LOCKS SO IT IS NOT CALCULATED AGAIN!

	public Mission (string location, string type, int difficulty)
	{
		this.location=location;
		this.type=type;
		this.difficulty=difficulty;

		//this.name = ReportToCampaing.missionNumber + "";
	}
	public void AddSquad(List<SoldierController> squad)
	{	
		Debug.Log ("Squad Size: "+squad.Count);
		this.squad=squad;
		foreach (SoldierController s in squad) {
			killsStart+=s.kills;
			Debug.Log(killsStart);
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
			bool[] dead = new bool[4];
			foreach (SoldierController soldier in squad) {

				returned += soldier.AllNames() + "\n";

			}

			if (this.type != "Vacation")
			{
				returned += "--During the mission soldiers killed: ";

				returned += thisMissionKills + "\n";

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
				
				if (squad.Count == soldiersDead)	//all are dead
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
				else
				{
					returned += "--Mission was A VICTORY!\n";
				}
			}




			return returned;

		}


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
			
				

				
				if (thisMissionKills < soldiersDead)	//Victory is simple: Did they kill more than lost?
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

		


}