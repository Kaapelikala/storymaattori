using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mission : MonoBehaviour {

	public string location;
	public List<SoldierController> squad;
	public string type;
	public int difficulty;
	int killsStart=0;

	public Mission (string location, string type, int difficulty)
	{
		this.location=location;
		this.type=type;
		this.difficulty=difficulty;
	}
	public void AddSquad(List<SoldierController> squad)
	{	
		this.squad=squad;
		foreach (SoldierController s in squad) {
			killsStart+=s.kills;
		}
	}

	override public string ToString()
	{
		string returned = "";
		returned += "Location: " + location + "\n";
		returned += "Operation: " + type + "\n";
		returned += "Members:\n";
		bool[]dead = new bool[4];
		foreach (SoldierController soldier in squad) {
			returned +=soldier.soldierFName+" '"+soldier.callsign+"' "+soldier.soldierLName+"\n";
		}
		returned += "During the mission soldiers wasted: ";
		int killsNow = 0;
		foreach (SoldierController soldier in squad) {
			killsNow+=soldier.kills;
		}
		returned += killsNow - killsStart+"\n";
		bool wastedPrinted = false;
		foreach(SoldierController soldier in squad)
		{
			if (!soldier.alive)
			{
				if (!wastedPrinted)
				{
					returned+="During the mission died: \n";
				}
				returned+=soldier.soldierFName+" '"+soldier.callsign+"' "+soldier.soldierLName+"\n";
			}
		}
		return returned;
	}
}
