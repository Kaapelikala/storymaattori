using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Mission : MonoBehaviour {

	public string location;
	public List<SoldierController> squad;
	public string type;
	public int difficulty;


	override public string ToString()
	{
		string returned = "";
		returned += "Location: " + location + "\n";
		returned += "Operation: " + type + "\n";
		returned += "Members:\n";
		foreach (SoldierController soldier in squad) {
			returned +=soldier.soldierFName+" '"+soldier.callsign+"' "+soldier.soldierLName+"\n";
		}
		returned += "During the mission: ";
		foreach (SoldierController soldier in squad) {
			returned +=soldier.soldierFName+" '"+soldier.callsign+"' "+soldier.soldierLName+" managed to:\n";
			returned+=soldier.GetEvents
		}
		return returned;
	}
}
