using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;


public class SoldierSelector : MonoBehaviour {

	public SoldierManager manager;
	public Text soldierName;
	public Text onMission;
	public MissionLog missions;


	void Update()
	{

		if (Int32.Parse(this.gameObject.name)<manager.soldiers.Count)
		soldierName.text = manager.soldiers[Int32.Parse (this.gameObject.name)].soldierFName+manager.soldiers[Int32.Parse (this.gameObject.name)].callsign+manager.soldiers[Int32.Parse (this.gameObject.name)].soldierLName;
		Array.Sort (manager.squadIds);
		if (Array.BinarySearch(manager.squadIds,Int32.Parse(this.gameObject.name))<0)
		{
			onMission.text="";
		}
	}


	public void SetToMission()
	{
		Debug.Log ("This name: "+this.gameObject.name);
		int temp;
		temp = Int32.Parse (this.gameObject.name);
		Debug.Log ("This parsed name: " + temp);


		Debug.Log ("Setting to mission...");
		//SetDebug.Log ("Setting...");
			if (manager.SetToMission (temp))
				onMission.text = "On mission";
			else 
				onMission.text = "";




	}



}
