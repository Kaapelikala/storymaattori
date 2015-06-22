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

	public bool victory;

	public Mission (string location, string type, int difficulty)
	{
		this.location=location;
		this.type=type;
		this.difficulty=difficulty;
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
			string returned = "";
			returned += MissionName + "\n";
			returned += "--Location: " + location + "\n";
			returned += "--Operation: " + type + "\n";
			returned += "--Members:\n";
			bool[] dead = new bool[4];
			foreach (SoldierController soldier in squad) {
				if (soldier.callsign != "")
				{
					returned += soldier.GetRank() + " " + soldier.soldierFName + " '" + soldier.callsign + "' " + soldier.soldierLName + "\n";
				}
				else
				{
					returned += soldier.GetRank() + " " + soldier.soldierFName + " " + soldier.soldierLName + "\n";
				}
			}
			returned += "--During the mission soldiers killed: ";
			int killsNow = 0;
			foreach (SoldierController soldier in squad) {
				killsNow += soldier.kills;
			}
			
			thisMissionKills = killsNow - killsStart;

			ReportToCampaing.TotalKills += thisMissionKills;		//reports to Campaing the kills!

			returned += thisMissionKills + "\n";
			bool wastedPrinted = false;
			int soldiersDead = 0;
			foreach (SoldierController soldier in squad) {
				if (!soldier.alive) {
					if (!wastedPrinted) {
						returned += "--During the mission died: \n";
						wastedPrinted=true;
					}
					soldiersDead++;
					ReportToCampaing.TotalDead++;		//reports to Campaing the deads!!
					if (soldier.callsign != "")
					{
						returned += soldier.GetRank() + " " + soldier.soldierFName + " '" + soldier.callsign + "' " + soldier.soldierLName + "\n";
					}
					else
					{
						returned += soldier.GetRank() + " " + soldier.soldierFName + " " + soldier.soldierLName + "\n";
					}
				}
			}
			if (squad.Count == soldiersDead)	//all are dead
			{
				returned += "--Mission was a DEFEAT!\n";
				this.victory = false;
			}
			else if (thisMissionKills < soldiersDead)
			{
				returned += "--Mission was an AMBARRASMENT!\n";
				this.victory = false;
			}
			else if (thisMissionKills == soldiersDead)
			{
				returned += "--Mission was a DRAW!\n";
				this.victory = false;
			}
			else
			{
				returned += "--Mission was A VICTORY!\n";
				this.victory = true;
			}

			return returned;
		}
}
}