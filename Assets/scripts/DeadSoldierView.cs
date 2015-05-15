using UnityEngine;
using UnityEngine.UI;
using System.Collections;


//Shows details of Soldier at SQUADVIEW. 

public class DeadSoldierView : MonoBehaviour {

	public SoldierManager DEAD_SOLDIERS;
	public int Current = 0;		// keeps track of which soldier we are looking at.
	public SoldierController Target;		//whot are we looking curretly

	public Text View_Name; 			//just the name
	public Text View_Details; 		//rank, name etc
	public Text View_Traits; 		//traits
	public Text View_Numbers; 		//Kills, missions, etc

	public Text HowDied;			//Or something else??

	public SoldierHeadImage IMAGE;

	public GameObject NoDead;
	public GameObject SomeDead;


	void Start (){
	
		this.NextSoldier();
	}

	public void ShowSoldier(){
		if (DEAD_SOLDIERS.NumberDead == 0) {
			NoDead.SetActive (true);
			SomeDead.SetActive (false);
			
		} else {
			
			NoDead.SetActive (false);
			SomeDead.SetActive (true);
			
			IMAGE.Set (Target.sex, Target.pictureID);
			
			this.View_Name.text = Target.soldierFName + " " + Target.soldierLName;
			this.View_Details.text = Target.callsign + "\n" + Target.GetRank ();
			
			this.View_Traits.text = Target.GetAttributes ();
			
			
			// ei anneta suoraa numeraalisia arvoja pelaajille nähtäviksi
			string ReturnMorale;
			if (Target.morale >= 100) {
				ReturnMorale = "Great";
			} else if (Target.morale > 50) {
				ReturnMorale = "OK";
			} else if (Target.morale > 25) {
				ReturnMorale = "Poor";
			} else {
				ReturnMorale = "None";
			}
			
			
			string ReturnHeath;
			if (Target.health >= 100) {
				ReturnHeath = "Great";
			} else if (Target.health > 50) {
				ReturnHeath = "OK";
			} else if (Target.health > 25) {
				ReturnHeath = "Poor";
			} else {
				ReturnHeath = "Dangerous";
			}
			
			
			int awards = Target.awards.Count;   
			
			this.View_Numbers.text = Target.soldierID + "\n" + Target.missions + "\n" + Target.kills + "\n" + ReturnMorale + "\n" + ReturnHeath + "\n\n" + awards;
			
			
			this.HowDied.text = Target.HowDied;
			
		}
	}

	public void NextSoldier(){

		Current++;

		if (Current > DEAD_SOLDIERS.NumberDead)
			Current = 1;

		Target = DEAD_SOLDIERS.dead[Current-1];


	}

	public void PrevSoldier(){

		Current--;

		if (Current < 1)
			Current = DEAD_SOLDIERS.NumberDead;

		
		Target = DEAD_SOLDIERS.dead[Current-1];

		
	}
}
