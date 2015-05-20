using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;


//Shows details of Soldier at SQUADVIEW. 

public class DeadSoldierView : MonoBehaviour {
	
	public SoldierManager ALIVE_SOLDIERS;
	public int Current = 1;		// keeps track of which soldier we are looking at.
	public SoldierController Target;
	
	public Text View_Name; 			//just the name
	public Text View_Details; 		//rank, name etc
	public Text View_Traits; 		//traits
	public Text View_Numbers; 		//Kills, missions, etc
	
	public Text View_Alive;			//is Soldier alive or dead?
	
	public SoldierHeadImage IMAGE;
	
	public GameObject NoAlive;
	public GameObject SomeAlive;
	/*
	 * CheckAliveStatus checks if there are alive dead
	 * If yes, returns true and sets "alive dead view" on
	 * If no, returns false and sets "no dead view" on
	 * 
	 */
	
	public void CheckAliveMessage()
	{
		Debug.Log ("Current: " + Current);
		ShowSoldier ();
	}
	
	public bool  CheckAliveStatus(){
		if (ALIVE_SOLDIERS.dead.Count <1) {
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
		Debug.Log ("Current: " + Current + ", size " + ALIVE_SOLDIERS.dead.Count);
		if (CheckAliveStatus())
		{
			
			Target = ALIVE_SOLDIERS.dead [Current - 1];
			IMAGE.Set(Target.sex , Target.pictureID);
			
			this.View_Name.text = Target.soldierFName + " " + Target.soldierLName;
			this.View_Details.text = Target.callsign + "\n" + Target.GetRank();
			
			this.View_Traits.text = Target.GetAttributes();
			
			
			// ei anneta suoraa numeraalisia arvoja pelaajille nähtäviksi
			string ReturnMorale;
			if (Target.morale >= 100)
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
			if (Target.health >= 100)
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
			
			
			int awards = Target.awards.Count;   
			
			this.View_Numbers.text = Target.soldierID + "\n" + Target.missions + "\n" + Target.kills + "\n" + ReturnMorale + "\n" + ReturnHeath + "\n\n" + awards;
			
			this.View_Alive.text = Target.events[0];

			
		}
		
	}
	
	public void NextSoldier(){
		if (ALIVE_SOLDIERS.dead.Count != 0) 
		{
			Current++;
			if (Current >= ALIVE_SOLDIERS.dead.Count)
			{Current = 0;
				Debug.Log ("Currently at "+Current);
				Debug.Log ("Soldiers total "+ALIVE_SOLDIERS.dead.Count);
			}
			
			Debug.Log ("Currently at "+Current);
			Debug.Log ("Soldiers total "+ALIVE_SOLDIERS.dead.Count);
			
			
		}
		
		ShowSoldier ();
		
	}
	
	public void PrevSoldier(){
		if (ALIVE_SOLDIERS.dead.Count != 0) {
			Current --;
			if (Current <1)
			{
				Current =  ALIVE_SOLDIERS.dead.Count;
			}
			
			Debug.Log ("Currently at "+Current);
			Debug.Log ("Soldiers total "+ALIVE_SOLDIERS.dead.Count);
			
		}
		ShowSoldier ();
		
		
	}
}
