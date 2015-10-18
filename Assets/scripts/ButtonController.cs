using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

//Handles the many parts of UI.
public class ButtonController : MonoBehaviour {
	
	public GameObject soldierView;
	public GameObject missionView;

	public GameObject deadSoldierView;
		public GameObject soldierSelectorView;

	public GameObject mainView;

	public SoldierManager manager;
	public MissionLog missions;

	public SoldierView DeadSoldierViewer;
	public SoldierView SoldierViewer;

	public AudioSource BOOM;

	public AudioSource NO;

	public void Start()
	{
		missions.AddMission();	//we always have one mission!

	}

	public void CheckDeactivateSoldierSelectionView()		//This is the GO signal, sends Soldiers to MISSION!
	{

		if (manager.inSquadCurrently == 4) {

			BOOM.Play ();

			missions.AddSquad();
			
			this.ActivateMissionViewButton();
			DeactivateSoldierSelectorView();
			mainView.SetActive(false);
		}
		else

		{

			NO.Play();
		}


	}
	
	public void ActivateSoldierSelectorView()
	{
		soldierSelectorView.SetActive (true);
		mainView.SetActive (false);
		missionView.SetActive (false);
	}
	
	public void DeactivateSoldierSelectorView()
	{
		soldierSelectorView.SetActive (false);
		mainView.SetActive (true);
	}
	
	
	public void ActivateSoldierViewButton(){
		soldierView.SetActive (true);

		SoldierViewer.CheckAliveMessage();
		//soldierView.transform.GetChild(0).transform.FindChild ("SoldierView").SendMessage ("CheckAliveMessage");
		mainView.SetActive (false);
	}
	
	public void ActivateDeadSoldierViewButton(){
		deadSoldierView.SetActive (true);
		DeadSoldierViewer.CheckAliveMessage();
		//deadSoldierView.transform.GetChild(0).transform.FindChild ("SoldierView").SendMessage ("CheckAliveMessage");
		mainView.SetActive (false);
	}
	
	public void ActivateMissionViewButton(){
		missionView.SetActive (true);
		mainView.SetActive (false);
	}
	
	public void DeActivateSoldierViewButton(){
		soldierView.SetActive (false);
		mainView.SetActive (true);
	}
	
	public void DeActivateDeadSoldierViewButton(){
		deadSoldierView.SetActive (false);
		mainView.SetActive (true);
	}
	
	public void DeActivateMissionViewButton(){
		missionView.SetActive (false);
		mainView.SetActive (true);
	}
	
}
