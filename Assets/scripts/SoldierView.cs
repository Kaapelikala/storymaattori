using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


//Shows details of Soldier at SQUADVIEW. 

public class SoldierView : MonoBehaviour {

	public SoldierManager ALIVE_SOLDIERS;
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


	public GUIStyle Style;

	public Vector2 scrollPosition;

	//public rect

		
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
		ShowSoldier ();
	}

	public bool  CheckAliveStatus(){
		if (ALIVE_SOLDIERS.soldiers.Count <1) {
			NoAlive.SetActive (true);
			SomeAlive.SetActive (false);
			return false;

		} else {
			if (Current<1)
				Current = 1;
			
			NoAlive.SetActive (false);
			SomeAlive.SetActive (true);
			return true;
		}
	}



	public void ShowSoldier (){
		Debug.Log ("Current: " + Current + ", size " + ALIVE_SOLDIERS.soldiers.Count);
		if (CheckAliveStatus())
			{
			
			Target = ALIVE_SOLDIERS.soldiers [Current - 1];
			IMAGE.Set(Target.sex , Target.pictureID);
			
			this.View_Name.text = Target.soldierFName + " " + Target.soldierLName;
			this.View_Details.text = Target.callsign + "\n" + Target.GetRank();
			
			this.View_Traits.text = Target.GetAttributes();
			
			
			// ei anneta suoraa numeraalisia arvoja pelaajille nähtäviksi
			string ReturnMorale;
			if (Target.morale >= 120)
			{
				ReturnMorale = "Heroic";
			}
			else if (Target.morale >= 100)
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
			else if (Target.health >= 100)
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
			else
			{
				ReturnSkill = "Bad";
			}

			
			int awards = Target.awards.Count;   
			
			this.View_Numbers.text = Target.soldierID + "\n" + Target.missions + "\n" + Target.kills + "\n" + ReturnMorale + "\n" + ReturnHeath +"\n" + ReturnSkill +"\n" + awards;
			
			
			
			if (Target.alive == true)
			{
				this.View_Alive.text = "ALIVE";
			}
			else
			{
				this.View_Alive.text = "DEAD!";
 			}

			this.History.text = Target.GetEvents();
			this.History.rectTransform.sizeDelta = new Vector2( 500, Target.events.Count*30);

		}

	}

	public void NextSoldier(){
		if (ALIVE_SOLDIERS.soldiers.Count != 0) 
		{
			Current++;
			if (Current > ALIVE_SOLDIERS.soldiers.Count)
			{Current = 1;
				Debug.Log ("Currently at "+Current);
				Debug.Log ("Soldiers total "+ALIVE_SOLDIERS.soldiers.Count);
			}
			
			Debug.Log ("Currently at "+Current);
			Debug.Log ("Soldiers total "+ALIVE_SOLDIERS.soldiers.Count);

			
		}
		
		ShowSoldier ();
		
	}

	public void PrevSoldier(){
		if (ALIVE_SOLDIERS.soldiers.Count != 0) {
			Current --;
			if (Current <1)
			{
				Current =  ALIVE_SOLDIERS.soldiers.Count;
			}
			
			Debug.Log ("Currently at "+Current);
			Debug.Log ("Soldiers total "+ALIVE_SOLDIERS.soldiers.Count);

		}
			ShowSoldier ();
		
		
	}


}
