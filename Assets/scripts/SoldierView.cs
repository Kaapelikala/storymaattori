using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


//Shows details of Soldier at SQUADVIEW. 

public class SoldierView : MonoBehaviour {

	public SoldierManager WhereToDrawSoldiers;
	public bool Alives; //whether DEAD or alive soldiers!
	public List<SoldierController> SOLDIERS;
	public int Current = 1;		// keeps track of which soldier we are looking at.
	public SoldierController Target;

	public Text View_Name; 			//just the name
	public Text View_Details; 		//rank, name etc
	public Text View_Traits; 		//traits
	public Text View_Numbers; 		//Kills, missions, etc

	public Text View_Alive;			//is Soldier alive or dead?

	public Text History;			// All the history!

	public SoldierHeadImage IMAGE;

	public GameObject NoAlive;
	public GameObject SomeAlive;

	public InputField CallsignChanger;

	public GUIStyle Style;

	public Vector2 scrollPosition;

	void Start () {
	
		if (Alives == true)
			this.SOLDIERS = WhereToDrawSoldiers.soldiers;
		else
			this.SOLDIERS = WhereToDrawSoldiers.dead;
	
	}

		
//	//public Vector2 scrollPosition = Vector2.zero;
//	void OnGUI() {
//
//
//		//GUI.TextArea(new Rect(10, 300, 350, 100), History, Style);
//
//
//		scrollPosition = GUI.BeginScrollView(new Rect(10, 290, 350, 100), scrollPosition, new Rect(0, 0, 220, Target.events.Count*20));
//	
//		GUILayout.Label(History, Style);
//
//		GUI.EndScrollView();
//	}

	/*
	 * CheckAliveStatus checks if there are alive soldiers
	 * If yes, returns true and sets "alive soldiers view" on
	 * If no, returns false and sets "no soldiers view" on
	 * 
	 */
	public void CheckAliveMessage()
	{
		Debug.Log ("Current: " + Current);
		this.Start();
		ShowSoldier ();
	}

	public bool  CheckAliveStatus(){
		if (this.SOLDIERS.Count < 1) {
			NoAlive.SetActive (true);
			SomeAlive.SetActive (false);
			return false;

		} 

		if (Current<1)
			Current = 1;

		NoAlive.SetActive (false);
		SomeAlive.SetActive (true);
		return true;

	}



	public void ShowSoldier (){
		Debug.Log ("Current: " + Current + ", size " + SOLDIERS.Count);
		if (this.CheckAliveStatus())
			{
			
			Target = SOLDIERS [Current - 1];
			IMAGE.Set(Target);

			if (Target.soldierMName == "")
				this.View_Name.text = Target.soldierFName + " " + Target.soldierLName;
			else {
				this.View_Name.text = Target.soldierFName + " " + Target.soldierMName + " " + Target.soldierLName;
			}

			this.View_Details.text = Target.callsign + "\n" + Target.GetRank();
			
			this.View_Traits.text = Target.GetAttributes();
			
			
			// ei anneta suoraa numeraalisia arvoja pelaajille nähtäviksi
			string ReturnMorale;
			if (Target.morale >= 120)
			{
				ReturnMorale = "Heroic";
			}
			else if (Target.morale > 105)
			{
				ReturnMorale = "Great";
			}
			else if (Target.morale > 50)
			{
				ReturnMorale = "OK";
			}
			else if (Target.morale > 25)
			{
				ReturnMorale = "Poor";
			}
			else
			{
				ReturnMorale = "None";
			}
			
			
			string ReturnHeath;
			if (Target.health >= 120)
			{
				ReturnHeath = "Super";
			}
			else if (Target.health > 105)
			{
				ReturnHeath = "Great";
			}
			else if (Target.health > 50)
			{
				ReturnHeath = "OK";
			}
			else if (Target.health > 25)
			{
				ReturnHeath = "Poor";
			}
			else
			{
				ReturnHeath = "Dangerous";
			}

			string ReturnSkill ="";
			if (Target.skill >= 105)
			{
				ReturnSkill = "Great";
			}
			else if (Target.skill > 102)
			{
				ReturnSkill = "Good";
			}
			else if (Target.skill > 98)
			{
				ReturnSkill = "Average";
			}
			else if (Target.skill > 95)
			{
				ReturnSkill = "Poor";
			}
			else  if (Target.skill > 90)
			{
				ReturnSkill = "Bad";
			}
			else 
			{
				ReturnSkill = "Horrible";
			}

			
			string awards = Target.GetAwardsShort();
			
			this.View_Numbers.text = Target.soldierID + "\n" + Target.missions + "\n" + Target.kills + "\n" + ReturnMorale + "\n" + ReturnHeath +"\n" + ReturnSkill +"\n" + awards;
			
			
			
			if (Target.alive == true)
			{
				this.View_Alive.text = "ALIVE";
			}
			else
			{
				this.View_Alive.text = Target.HowDied;
 			}

			this.History.text = Target.GetEvents();
			this.History.rectTransform.sizeDelta = new Vector2( 500, Target.events.Count*30);

		}

	}

	public void NewCallsignForSoldier(string CallsignInsert)
	{

		//string NewCallsign = CallsignInsert;
		string NewCallsign = CallsignChanger.text;

		if (Target.callsign == NewCallsign)	// If trying the same nothing happens. This includes ""!
		{
		}
		else if (Target.callsign == "")
		{
			Target.AddEvent("Command assigned " + Target.soldierLName + " the callsign '" + NewCallsign + "'!\n");
		}
		else 
		{
			Target.AddEvent("Command assigned " + Target.soldierLName + " the new callsign '" + NewCallsign + "', replacing '"+Target.callsign +"'!\n");
		}
		Target.callsign = NewCallsign;

		CallsignChanger.text = "";

		this.ShowSoldier();
	}

	public void NextSoldier(){
		if (SOLDIERS.Count != 0) 
		{
			Current++;
			if (Current > SOLDIERS.Count)
			{Current = 1;
				Debug.Log ("Currently at "+Current);
				Debug.Log ("Soldiers total "+SOLDIERS.Count);
			}
			
			Debug.Log ("Currently at "+Current);
			Debug.Log ("Soldiers total "+SOLDIERS.Count);

			
		}
		
		ShowSoldier ();
		
	}

	public void PrevSoldier(){
		if (SOLDIERS.Count != 0) {
			Current --;
			if (Current <1)
			{
				Current =  SOLDIERS.Count;
			}
			
			Debug.Log ("Currently at "+Current);
			Debug.Log ("Soldiers total "+SOLDIERS.Count);

		}
			ShowSoldier ();
		
		
	}


}
