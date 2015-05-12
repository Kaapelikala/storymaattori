using UnityEngine;
using UnityEngine.UI;
using System.Collections;


//Shows details of Soldier at SQUADVIEW. 

public class SoldierView : MonoBehaviour {

	public SoldierManager ALIVE_SOLDIERS;
	public int Current = 0;		// keeps track of which soldier we are looking at.
	public SoldierController Target;

	public Text View_Name; 			//just the name
	public Text View_Details; 		//rank, name etc
	public Text View_Traits; 		//traits
	public Text View_Numbers; 		//Kills, missions, etc

	public Text View_Alive;			//is Soldier alive or dead?

	public SoldierHeadImage IMAGE;

	void Start (){
	
		this.NextSoldier();
	}

	// Update is called once per frame
	void Update () {
	
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

		this.View_Numbers.text = Target.missions + "\n" + Target.kills + "\n" + ReturnMorale + "\n" + ReturnHeath + "\n\n" + awards;



		if (Target.alive == true)
		{
			this.View_Alive.text = "ALIVE";
		}
		else
		{
			this.View_Alive.text = "DEAD!";
		}
	}

	public void NextSoldier(){

		Current++;

		if (Current > ALIVE_SOLDIERS.NumberAlive)
			Current = 1;

		Target = ALIVE_SOLDIERS.soldiers[Current-1];


	}

	public void PrevSoldier(){

		Current--;

		if (Current < 1)
			Current = ALIVE_SOLDIERS.NumberAlive;

		
		Target = ALIVE_SOLDIERS.soldiers[Current-1];

		
	}
}
