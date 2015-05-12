using UnityEngine;
using UnityEngine.UI;
using System.Collections;


//Shows details of Soldier at SQUADVIEW. 

public class SoldierView : MonoBehaviour {

	public SoldierController Target;
	public Text View_Name; 			//just the name
	public Text View_Details; 		//rank, name etc
	public Text View_Traits; 		//traits
	public Text View_Numbers; 		//Kills, missions, etc

	public Text View_Alive;			//is Soldier alive or dead?

	// Update is called once per frame
	void Update () {
	
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


		this.View_Numbers.text = Target.missions + "\n" + Target.kills + "\n" + ReturnMorale + "\n" + ReturnHeath;

		if (Target.alive == true)
		{
			this.View_Alive.text = "ALIVE";
		}
		else
		{
			this.View_Alive.text = "DEAD!";
		}
	}
}
