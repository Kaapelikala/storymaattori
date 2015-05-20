using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class MissionLog : MonoBehaviour {
	
	public Text missionText;
	public List<Mission> missions = new List<Mission>();
	public int currentlyAdded=0;
	public SoldierManager manager;
	private Mission mission;


	public void AddMission ()
	{if (manager.soldiers.Count > 3) {
			Debug.Log ("Adding a mission...");
			mission = new Mission ("jungle", "raid", 0);
			missions.Add (mission);
			Debug.Log(missions.IndexOf(mission));
		}
	}
	public void AddSquad()
	{
		Debug.Log ("Adding squad...");
		Debug.Log (missions.IndexOf(mission));
		Debug.Log (currentlyAdded);
 		Debug.Log ("mission @ "+missions[currentlyAdded]);
		missions[currentlyAdded].AddSquad (manager.GetSquad(manager.squadIds));
		Debug.Log ("writing to log...");
		UpdateLog ();
	}
	
	public void UpdateLog()
	{
		string temp=missions [currentlyAdded].ToString();
		missionText.text = missionText.text+temp;
		missionText.text += "\n\n";
		Debug.Log (missionText.text);
		Debug.Log (temp);
		
		currentlyAdded++;
	}
}
