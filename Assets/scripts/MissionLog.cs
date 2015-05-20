using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class MissionLog : MonoBehaviour {
	
	public Text missionText;
	public List<Mission> missions;
	public int currentlyAdded;
	public SoldierManager manager;
	
	// Use this for initialization
	void Start () {
		currentlyAdded = -1;
		missions = new List<Mission>();
		missionText.text="";
	}

	public void AddMission ()
	{
		Mission mission = new Mission ("jungle", "raid", 0);
		missions.Add(mission);
	}
	public void AddSquad(int[] ids)
	{
		missions [currentlyAdded + 1].AddSquad (manager.GetSquad(ids));
	}
	
	public void UpdataLog()
	{
		currentlyAdded++;
		missionText.text+= missions [currentlyAdded].ToString();
	}
}
