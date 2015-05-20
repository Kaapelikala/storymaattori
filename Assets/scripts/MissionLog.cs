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
	public EventController control;

	public void AddMission ()
	{if (manager.soldiers.Count > 3) {
			Debug.Log ("Adding a mission...");

			string target = "";

			int targetSelect = Random.Range(0, 100);

			if (targetSelect< 50)
			{
				target = "Canyon #" + Random.Range(101, 999);
			}
			else if (targetSelect< 75)
			{
				target = "Farm #" + Random.Range(101, 999);
			}
			else
			{
				target = "Cavern #" + Random.Range(101, 999);
			}

			string missionSelect = "";

			int missionRoll = Random.Range(0, 100);
			
			if (missionRoll< 50)
			{
				missionSelect = "liberation";
			}
			else if (missionRoll< 75)
			{
				missionSelect = "attack";
			}
			else
			{
				missionSelect = "raid";
			}



			mission = new Mission (target, missionSelect, 0);
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
		Debug.Log ("Fighting....");
		control.Fight (manager.squadIds);
		
		Debug.Log ("writing to log...");
		UpdateLog ();
		manager.squadIds = new int[4]{-2,-2,-2,-2};
		manager.inSquadCurrently = 0;
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
