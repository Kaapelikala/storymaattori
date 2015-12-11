using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ReportController : MonoBehaviour {

	public Campaing campaing;

	public int ReportNumber = 127;
	
	public GameObject mainViewCanvas;
	public GameObject NewsView;	//news popup 
	public Transform HereNews;

	public bool ShowNewReports = true;
	public bool ShowDeadReports = true;

	//BOOLEANS TO check does most normal popups appear - Deaths and new dudes !


	public int NextReportNumber()
	{
		this.ReportNumber += UnityEngine.Random.Range(1,8)+ UnityEngine.Random.Range(1,8);

		return ReportNumber;

	}


	public void CreateNewsPopup(string NewsImput){
		//One for header and other for main text? not now, later!

		int ThisNewsNumber = NextReportNumber();

		string HeaderReturn ="Report: TS:" + campaing.TimeStamp + "/" + ThisNewsNumber + "\n"; 

		string MainTextReturn = NewsImput;

		GameObject NewNews = (GameObject)Instantiate(NewsView, new Vector3(0f,0f,0f),Quaternion.identity);

		NewNews.name = "News: " + campaing.TimeStamp + "/" + ThisNewsNumber;

		NewsPanel OurNewText = NewNews.GetComponentInChildren<NewsPanel>();
		
		if (OurNewText != null)
		{
			OurNewText.UpdatePanel(MainTextReturn, HeaderReturn);
		}
		else
			Debug.Log("NewsPaper does not find its Text!: " + NewsImput);
		
		NewNews.transform.parent = HereNews.transform;
		
		NewNews.transform.localPosition = new Vector3(0f,0f,0f);		// so in correct location
		
	}




	public void CreateNewSoldierPopup(SoldierController Recruit)
	{
		if (this.ShowNewReports == true)
		{
			string ToReturn = "--New Soldier--\n" + Recruit.AllNamesNoRANK().ToUpper()+"\n";

			ToReturn += "ID: " + campaing.SquadID + "/" + Recruit.soldierID + "\n";
			ToReturn += "Skill: " + StatToShort(Recruit.skill) +"\n";
			ToReturn += "Health: " + StatToShort(Recruit.health) +"\n";
			ToReturn += "Morale: " + StatToShort(Recruit.morale) +"\n";

			this.CreateNewsPopup(ToReturn);
		}
		
	}

	public void CreateEmergencySoldierPopup(SoldierController Recruit)
	{
		if (this.ShowNewReports == true)
		{
			string ToReturn = "--Emergency Recruit--\n" + Recruit.AllNamesNoRANK().ToUpper()+"\n";
			
			ToReturn += "ID: " + campaing.SquadID + "/" + Recruit.soldierID + "\n";
			ToReturn += "Skill: " + StatToShort(Recruit.skill) +"\n";
			ToReturn += "Health: " + StatToShort(Recruit.health) +"\n";
			ToReturn += "Morale: " + StatToShort(Recruit.morale) +"\n";
			
			this.CreateNewsPopup(ToReturn);
		}
		
	}

	public void CreateReinforcementsPopUp(List<SoldierController> Reinforcements)
	{
		if (Reinforcements.Count == 1)
		{
			bool SavingFormerStatus = this.ShowNewReports;	// save it - when many come at the same time no invidinual news!
			campaing.ReportCont.ShowNewReports = true;

			this.CreateNewSoldierPopup(Reinforcements[0]);
			
			this.ShowNewReports = SavingFormerStatus;	// save it - when many come at the same time no invidinual news!
		}
		else
		{
			string ToReturn = "--Reinforcements--\n";

			foreach (SoldierController solttu in Reinforcements)
			{
				ToReturn += solttu.AllNamesNoRANK() + "\n";
			}
				
			this.CreateNewsPopup(ToReturn);
		}
	}


	
	public void CreateDEADSoldierPopup(SoldierController Corpse)
	{
		if (this.ShowDeadReports == true)
		{
			string ToReturn = "--Casualty--\n" + Corpse.GetRank() +"\n"+ Corpse.AllNamesNoRANK().ToUpper() +"\n" + Corpse.HowDied+"\n";

			ToReturn += "Missions: "+Corpse.missions +" | Kills: " +Corpse.kills+"\n";

			if (Corpse.awards.Count > 0)
				ToReturn += "Medals: "+ Corpse.GetAwardsShort();

			this.CreateNewsPopup(ToReturn);
		}
		
	}

	public void ToggleShowNewReports()
	{
		if (ShowNewReports == true)
			ShowNewReports = false;
		else
			ShowNewReports = true;
	}
	public void ToggleShowDeadReports()
	{
		if (ShowDeadReports == true)
			ShowDeadReports = false;
		else
			ShowDeadReports = true;

	}

	private string StatToShort(int Stat)
	{
		if (Stat > 110)
			return "++";
		else if (Stat > 105)
			return "+";
		else if (Stat < 90)
			return "--";
		else if (Stat < 95)
			return "-";
		
		return "Avg";
		
	}

}
