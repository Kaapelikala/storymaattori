using UnityEngine;
using System.Collections;

public class ButtonController : MonoBehaviour {

	public GameObject soldierView;
	public GameObject missionView;
	public GameObject deadSoldierView;
	public GameObject mainView;


	public void ActivateSoldierViewButton(){
		soldierView.SetActive (true);
		mainView.SetActive (false);
	}

	public void ActivateDeadSoldierViewButton(){
		deadSoldierView.SetActive (true);
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
