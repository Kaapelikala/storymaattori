using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;


public class SoldierSelector : MonoBehaviour {

	public SoldierManager manager;
	public Text soldierName;
	public Text onMission;

	void Update()
	{
		soldierName.text = manager.soldiers[Int32.Parse (this.gameObject.name)].soldierFName+manager.soldiers[Int32.Parse (this.gameObject.name)].callsign+manager.soldiers[Int32.Parse (this.gameObject.name)].soldierLName;
	}

	public void SetToMission()
	{
		Debug.Log ("This name: "+this.gameObject.name);
		int temp;
		temp = Int32.Parse (this.gameObject.name);
		Debug.Log ("This parsed name: " + temp);
		if (manager.SetToMission (temp))
			onMission.text="On mission";

	}



}
