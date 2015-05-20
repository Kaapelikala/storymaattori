using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class MissionLog : MonoBehaviour {

	public Text missionText;
	public List<Mission> missions;
	public int currentlyAdded;


	// Use this for initialization
	void Start () {
		currentlyAdded = -1;
		missions = new List<Mission>();
		missionText.text="";
	}

	public void UpdataLog()
	{
		currentlyAdded++;
		missionText.text = missionText.text + missions [0].ToString;
	}
}
