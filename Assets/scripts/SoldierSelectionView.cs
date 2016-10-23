using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class SoldierSelectionView : MonoBehaviour {

	public MissionLog log;

	public Mission Current;

	public Text MissionInfoText;
	public Text MissionDifText;
	public Text BattleRangeText;

	void Update () {
		ShowSoldierAmount ();

		String Returnoitava = "";

		Returnoitava += log.mission.type + " " + log.mission.location + "\n";

		if (log.mission.type == "Patrol")
		{
			Returnoitava += "  Recon the area.\n  Potential hostiles.";
		}
		else if (log.mission.type == "Vacation")
		{
			Returnoitava += "  Long deserved R&R!";
		}
		else if (log.mission.type == "Assault")
		{
			Returnoitava += "  Priority target!\n  Expect heavy casualties!";
		}
		else 
		{
			Returnoitava += "  Neutralise all hostiles.";
		}


		if (log.mission.type == "Patrol")
		{
			BattleRangeText.text = "???";
		}
		else if (log.mission.type == "Vacation")
		{
			BattleRangeText.text = "";
		}
		else
		{
			BattleRangeText.text = log.mission.getExpEncounterRange();
		}

		MissionInfoText.text = Returnoitava;

		if (log.mission.type == "Patrol")
		{
			MissionDifText.text = "???";
		}
		else if (log.mission.type == "Vacation")
		{
			MissionDifText.text = "None";
		}
		else if (log.mission.difficulty < 90)
		{
			MissionDifText.text = "Easy";
		}
		else if (log.mission.difficulty > 110)
		{
			MissionDifText.text = "Hard";
		}
		else
		{
			MissionDifText.text = "Normal";
		}
	}

	public Text textField;
	public SoldierManager manager;



	public void ShowSoldierAmount()
	{
		textField.text =   manager.inSquadCurrently.ToString();
	}

}
