using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mission : MonoBehaviour {

	public string MissionName = "";
	public string location;
	public List<SoldierController> squad;
	public string type;
	public int difficulty;
	int killsStart=0;

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
			returned += MissionName;
			returned += "--Location: " + location + "\n";
			returned += "--Operation: " + type + "\n";
			returned += "--Members:\n";
			bool[] dead = new bool[4];
			foreach (SoldierController soldier in squad) {
				returned += soldier.soldierFName + " '" + soldier.callsign + "' " + soldier.soldierLName + "\n";
			}
			returned += "--During the mission soldiers killed: ";
			int killsNow = 0;
			foreach (SoldierController soldier in squad) {
				killsNow += soldier.kills;
			}
			returned += killsNow - killsStart + "\n";
			bool wastedPrinted = false;
			int soldiersDead = 0;
			foreach (SoldierController soldier in squad) {
				if (!soldier.alive) {
					if (!wastedPrinted) {
						returned += "--During the mission died: \n";
						wastedPrinted=true;
					}
					soldiersDead++;
					returned += soldier.soldierFName + " '" + soldier.callsign + "' " + soldier.soldierLName + "\n";
				}
			}
			if (squad.Count == soldiersDead)
			{
				returned += "Mission was a DEFEAT!\n";
				this.victory = false;
			}
			else if (killsNow < soldiersDead)
			{
				returned += "Mission was an AMBARRASMENT!\n";
				this.victory = false;
			}
			else
			{
				returned += "Mission was A VICTORY!\n";
				this.victory = true;
			}

			return returned;
		}
}
}