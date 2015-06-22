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


		Roll += CheckTrait("lucky", -10);	//lucky helps to dodge!
		Roll += CheckTrait("robo-leg", -5);	//robo-leg too!
		Roll += CheckTrait("robo-leg", -5);	//if both legs robotic its the best!
		Roll += CheckTrait("coward", -5);	//cowardism has some beneficts..
		Roll += CheckTrait("idiot", 10);	//but idiots just die

		
		if (Roll < SoldierSkill -20)
		{
			target.AddExperience(Enemy_difficulty/10);

			int DodgeRandomiser = (Mathf.RoundToInt(Random.Range(0, 6)));
			
			string Dodgetype = "";
			
			switch (DodgeRandomiser)
			{
			case 0:
				Dodgetype = "Dodged";
				break;
			case 1:
				Dodgetype = "Evaded";
				break;
			case 2:
				Dodgetype = "Ignored";
				break;
			case 3:
				Dodgetype = "Ducked away from";
				break;
			case 4:
				Dodgetype = "Sidestepped";
				break;
			case 5:
				Dodgetype = "Parried";
				break;
			default:
				Dodgetype = "Avoided";
				break;
			}

			target.AddEvent (Dodgetype + " enemy grenade!\n");
			return (target.callsign + "Dodged enemy grenade!");
		}

		target.ChangeHealth(Random.Range(-20, -5) + Random.Range(-20, -5));	//nasty!
		target.ChangeMorale(-30);
		target.AddAttribute("wounded");


		if (target.health < 0){
			target.die("Blown up by grenade");
			target.AddEvent ("Was blown up by enemy grenade!\n");
			return (target.callsign + " was blown up by enemy grenade!");
		}

		int TextRandomiser = (Mathf.RoundToInt(Random.Range(0, 6)));

		string damagetype = "";

		switch (TextRandomiser)
		{
		case 0:
			damagetype = "wounded";
			break;
		case 1:
			damagetype = "rended";
			break;
		case 2:
			damagetype = "thrown";
			break;
		case 3:
			damagetype = "hit";
			break;
		case 4:
			damagetype = "plasmad";
			break;
		case 5:
			damagetype = "grazed";
			break;
		default:
			damagetype = "burned";
			break;
		}

		target.AddEvent ("Was "+ damagetype + " by enemy grenade scrapnel!\n");
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
