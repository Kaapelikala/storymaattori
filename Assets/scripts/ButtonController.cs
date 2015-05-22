﻿using UnityEngine;
using System.Collections;


//Handles the many parts of UI.
public class ButtonController : MonoBehaviour {
	
	public GameObject soldierView;
	public GameObject missionView;
	public GameObject deadSoldierView;
	public GameObject mainView;
	public GameObject soldierSelectorView;
	public SoldierManager manager;
	public MissionLog missions;

	public AudioSource BOOM;

	public AudioSource NO;
	
	public void CheckDeactivateSoldierSelectionView()
	{

		if (manager.inSquadCurrently == 4) {

			BOOM.Play ();

			missions.AddMission();
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
		soldierView.transform.GetChild(0).transform.FindChild ("SoldierView").SendMessage ("CheckAliveMessage");
		mainView.SetActive (false);
	}
	
	public void ActivateDeadSoldierViewButton(){
		deadSoldierView.SetActive (true);
		deadSoldierView.transform.GetChild(0).transform.FindChild ("DeadView").SendMessage ("CheckAliveMessage");
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
