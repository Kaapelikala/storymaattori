using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;


//Creates missions and handles them : MISSION SEND IS IN HERE!

public class MissionLog : MonoBehaviour {

	public Text missionText;
	public List<Mission> missions = new List<Mission>();
	public int currentlyAdded=0;
	public SoldierManager manager;
	public Mission mission;
	public EventController control;

	public RectTransform BG;


	public void AddMission ()		
	{
		if (manager.soldiers.Count > 3) {
			Debug.Log ("Adding a mission...");

			string target = "";
			string missionSelect = "";
			int Mission_DIFF = Random.Range((control.campaing.Campaing_Difficulty - 20), (control.campaing.Campaing_Difficulty + 20));

			int MissionTypeChance = Random.Range(0, 100); 

			if (MissionTypeChance > 90)	//Partyparty!
			{
				int targetSelect = Random.Range(0, 100);
				
				if (targetSelect< 50)
				{
					target = "Beach #" + Random.Range(101, 999);
				}
				else if (targetSelect< 75)
				{
					target = "Base #" + Random.Range(101, 999);
				}
				else
				{
					target = "Bar #" + Random.Range(101, 999);
				}

				missionSelect = "Vacation";


			}
			else if (MissionTypeChance < 10)		// ENEMY HQ ASSAULT MISSION! 
			{
				int targetSelect = Random.Range(0, 100);
				
				if (targetSelect< 25)
				{
					target = "Enemy HQ #" + Random.Range(101, 999);
				}
				else if (targetSelect< 50)
				{
					target = "Hill #" + Random.Range(101, 999);
				}
				else if (targetSelect< 75)
				{
					target = "Fuel Depot #" + Random.Range(101, 999);
				}
				else
				{
					target = "Bunker #" + Random.Range(101, 999);
				}
				
				missionSelect = "Assault";
			}
			else  // Normal mission aka battlemission!
			{

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

				

				int missionRoll = Random.Range(0, 100);
				
				if (missionRoll< 50)
				{
					missionSelect = "liberation";
					Mission_DIFF += 5;
				}
				else if (missionRoll< 75)
				{
					missionSelect = "attack";
				}
				else
				{
					missionSelect = "raid";
				}
			}




			this.mission = new Mission (target, missionSelect, Mission_DIFF, control.campaing);
			this.mission.MissionName = "Mission " + (control.campaing.missionNumber+1) + "";
		}
	}
	public void AddSquad()		//LADS GO TO BATTLE!BATTLE!
	{
		if (manager.inSquadCurrently == 4) {

			missions.Add (this.mission);
			Debug.Log(missions.IndexOf(mission));

			bool VictoryMatters = false;

			Debug.Log ("Adding squad...");
			Debug.Log (missions.IndexOf (mission));
			Debug.Log (currentlyAdded);
			Debug.Log ("mission @ " + missions [currentlyAdded]);
			missions [currentlyAdded].AddSquad (manager.GetSquad (manager.squadIds));		// Actual BATTLE
			Debug.Log ("Fighting....");

			if (this.mission.type == "Vacation")
			{
				control.Vacate (manager.squadIds, mission.difficulty);

			}
			else if (this.mission.type == "Assault")
			{
				control.Fight (manager.squadIds, mission.difficulty, missions [currentlyAdded]);	//needs its own, not yet implemented!
				VictoryMatters = true;
			}
			else  {
				control.Fight (manager.squadIds, mission.difficulty, missions [currentlyAdded]);
			}

			Debug.Log ("writing to log...");
			UpdateLog ();

			if (VictoryMatters)
			{
				if (missions [currentlyAdded-1].victory == true)
				{
					control.campaing.Campaing_Difficulty--;
					Debug.Log ("mission "+ mission.MissionName +"was VICTORY!");
				}
				else
				{
					control.campaing.Campaing_Difficulty++;
					Debug.Log ("mission "+ mission.MissionName +" was defeat!");
				}
			}

			manager.squadIds = new int[4]{-2,-2,-2,-2};
			manager.inSquadCurrently = 0;

			this.AddMission();		// NEW MISSION IS CREATED
		}
	}
	
	public void UpdateLog()
	{
		string temp=missions [currentlyAdded].ToString();
		missionText.text = missionText.text+temp;

		this.missionText.rectTransform.sizeDelta = new Vector2(359, missionText.text.Length*2);

		this.BG.sizeDelta = new Vector2( 0 , missionText.text.Length*2);

		missionText.text += "\n\n";

		Debug.Log ("All Missions:" + missionText.text);
		Debug.Log ("Current:" + temp);
		
		currentlyAdded++;
	}

	public void EXPORT()
	{
		string returnoitava = "";

		returnoitava += "Total Kills: " + control.campaing.TotalKills;
		returnoitava += "Total Deaths: " + control.campaing.TotalDead;
		returnoitava += "Total Missions: " + control.campaing.missionNumber;

		returnoitava += control.campaing.alkuteksti;

		returnoitava += "\n\n\n +++MISSIONS+++\n";

		returnoitava += missionText.text;

		returnoitava += "\n\n\n +++ALIVE SOLDIERS+++\n";

		foreach (SoldierController solttu in control.manager.soldiers)
		{
			returnoitava += "\n\n";
			returnoitava += "--" + solttu.soldierID + "\n" + solttu.toString();
		}
		
		returnoitava += "\n\n\n +++DEAD SOLDIERS+++\n";

		foreach (SoldierController solttu in control.manager.dead)
		{
			returnoitava += "\n\n";
			returnoitava += "--" + solttu.soldierID + "\n" + solttu.toString();
		}

		returnoitava = returnoitava.Replace("\n", System.Environment.NewLine);

		System.IO.File.WriteAllText(@"D:\Storymaattori_HistoryExport_" + control.campaing.CampaingYear + "+" + control.campaing.TimeStamp + ".txt", returnoitava);


	}
}
