using UnityEngine;
using System.Collections;

public class Event_Grenade : MonoBehaviour {
	
	public SoldierController target;
	public string MissionName = "";

	public Event_Grenade()
	{
	}
	
	public Event_Grenade(string NameInsert)
	{
		this.MissionName = NameInsert;
	}
	
	/// <summary>
	/// Checks if the Grenade does antyhingh! 
	/// <param name="NEWTARGET">NEWTARGE.</param>
	/// <param name="Enemy_difficulty">Enemy_difficulty.</param>
	public string CheckGrenade (SoldierController NEWTARGET, int Enemy_difficulty){

		//Enemy Difficulty should not be about 100!

		this.target = NEWTARGET;

		int SoldierSkill = this.target.skill;

		int modifier = SoldierSkill - Enemy_difficulty;

		// SIMPLE GRENADE : SOLDIER SKILL -20 = OK!
		
		
		int Roll = Random.Range(0, 100);
		
		Roll = Roll - modifier;
		
		//Positive modifier = Good = rolls lower.
		//Negative modifier = Bad = rolls higher.
		
		target.RemoveAttribute("newbie");

		if (CheckTrait("lucky", 1) == 1)	//lucky helps to dodge!
		{
			Roll += -10;
		}
		if (Roll < SoldierSkill)
		{
			target.AddExperience(Enemy_difficulty/10);
			target.AddEvent ("Dodged enemy grenade!\n");
		}



		target.ChangeHealth(Random.Range(-5, -20) + Random.Range(-5, -20));	//nasty!
		target.ChangeMorale(-20);
		target.AddAttribute("wounded");


		if (target.health < 0){
			target.die("Blown up by grenade");
			target.AddEvent ("Was blown up by enemy grenade!\n");
			return (target.callsign + " was blown up by enemy grenade!");
		}

		target.AddEvent ("Was wounded by enemy grenade scrapnel!\n");
		return (target.callsign + "Was hit by scrapnel!");

	}
	
	int CheckTrait (string TraitName, int modifier)
	{
		if (target.HasAttribute(TraitName))
		{
			return modifier;
		}
		return 0;
		
	}

}
