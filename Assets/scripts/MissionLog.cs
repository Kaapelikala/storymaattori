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
<<<<<<< HEAD
		currentlyAdded = -1;
		missions = new List<Mission>();
		missionText.text="";
=======
		//missions = new List<Mission>;
		//missionText.text="";
>>>>>>> d9dcb7a3ba915ef9a7d61171742594ef01f19c00
	}

	public void UpdataLog()
	{
		currentlyAdded++;
		missionText.text = missionText.text + missions [0].ToString;
	}
}
