using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// A grenade threathens the existance of ALL squad members!
/// </summary>
public class Event_Grenade : MonoBehaviour {
	
	public List<SoldierController> squad;public SoldierController target;

	public Event_Grenade()
	{
	}
	
	public Event_Grenade(List<SoldierController> INJECT)
	{
		this.squad = INJECT;
	}
	
	/// <summary>
	/// Checks if the Grenade does antyhingh! 
	/// <param name="NEWTARGET">NEWTARGE.</param>
	/// <param name="Enemy_difficulty">Enemy_difficulty.</param>
	public void CheckGrenade (List<SoldierController> INJECT, int Enemy_difficulty){
		
		//Enemy Difficulty should not be about 100!


		
		this.squad = INJECT;

		SoldierController Hero = null;		// if someone jumps on the grenade!



		foreach (SoldierController solttu in squad)		// Chance for each soldier to be a HERO
		{
			int HowManyAlive = 0;
			foreach (SoldierController tester in squad)		// Check if there is only one soldier alive - no need for heroism then!!!
			{
				if (tester.alive == true)
					HowManyAlive++;
			}

			if (HowManyAlive > 1)
			{
				if (solttu.alive == true && !solttu.HasAttribute("coward"))
				{
					int Modifier = 0;

					Modifier += CheckTrait("heroic", 10, solttu);
					Modifier += CheckTrait("tough", 5, solttu);
					Modifier += CheckTrait("idiot", 10, solttu);
					Modifier += CheckTrait("veteran", -10, solttu);
					Modifier += CheckTrait("lucky", -10, solttu);

					int SacrificeRoll = Random.Range(0, 100);

					if ((SacrificeRoll + Modifier > 90) && (Random.Range(0, 10) > 5) && Hero == null)	// rare but happens. does not care about Morale but soldiers spirit?
					{
						Hero = solttu;
					}	// Actual happening gets calculated later!
				}
			}
		}
			



		if (Hero == null)		//No one is covering their friends, everyone gets hit!
		{
			foreach (SoldierController target in squad)		// Hurts!
			{
				if (target.alive == true)
				{
					int SoldierSkill = target.skill;

					int modifier = SoldierSkill - Enemy_difficulty;

					// SIMPLE GRENADE : SOLDIER SKILL -20 = OK!
					
					
					int Roll = Random.Range(0, 100);
					
					Roll = Roll - modifier;
					
					//Positive modifier = Good = rolls lower.
					//Negative modifier = Bad = rolls higher.
					
					target.RemoveAttribute("newbie");


					Roll += CheckTrait("lucky", -10, target);	//lucky helps to dodge!
					Roll += CheckTrait("robo-leg", -5, target);	//robo-leg too!
					Roll += CheckTrait("robo-leg", -5, target);	//if both legs robotic its the best!
					Roll += CheckTrait("coward", -5, target);	//cowardism has some beneficts..
					Roll += CheckTrait("veteran", -10, target);	//vets just know how to react
					Roll += CheckTrait("idiot", 20, target);	//but idiots just die

					
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
							Dodgetype = "Survived";
							break;
						default:
							Dodgetype = "Avoided";
							break;
						}

						target.AddEvent (Dodgetype + " enemy grenade!\n");
					}
					else
					{
						target.ChangeHealth(Random.Range(-20, -5) + Random.Range(-20, -5));	//nasty!
						target.ChangeMorale(-30);
						target.AddAttribute("wounded");


						if (target.health < 0){

							this.TargetKIA(target);
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

						target.AddEvent ("Was "+ damagetype + " by enemy grenade!\n");
					}
				}
			}
		}
		else {
			// the hero gets hurt!

			Hero.AddEvent ("Jumped on a grenade to save rest of the squad!");

			Hero.ChangeHealth(Random.Range(-50, -15) + Random.Range(-50, -15));	//100 to 30 damage. Chance to survive!
			Hero.ChangeMorale(+20);

			bool HeroAlive = true;

			if (Hero.health < 0)
			{
				HeroAlive = false;
				Hero.die("Heroic sacrifice");
				Hero.AddEvent (" It was completely fatal!\n");
				Hero.AddHistory("-SACR-DIE-THIS");

			}
			else {
				Hero.AddEvent(Hero.getCallsignOrFirstname() + "survived!!\n");
				Hero.ChangeMorale(20);	//WHOAH
			}
			foreach (SoldierController reacter in squad)
			{
				if (reacter.alive == true && reacter != Hero)
				{
					if (HeroAlive == true)
					{
						reacter.AddEvent(Hero.getCallsignOrFirstname()  + " jumped on a grenade and did not die!!!\n");
						reacter.AddHistory("-SACR-SURV-:" + Hero.soldierID);
					}
					else
					{
						reacter.AddEvent(Hero.getCallsignOrFirstname()  + " jumped on a grenade and died!\n");
						reacter.AddHistory("-SACR-DIE-:" + Hero.soldierID);
					}
					
				}
				
			}
		}

	}

	private void TargetKIA(SoldierController target)
	{
		foreach (SoldierController solttu in squad)
		{
			if (solttu.alive == true && solttu != target)
			{
				solttu.AddEvent(target.GetRank() + " " +target.getCallsignOrFirstname() + " was blown up by a grenade!!!\n");
				
				if (solttu.HasAttribute("coward"))
					solttu.ChangeMorale(-20);	//should it compare histories once more?
				else if (solttu.HasAttribute("loner"))
				{}	//nada!
				else
					solttu.ChangeMorale(-10);
			}
			
		}


		target.die("Blown up by grenade");
		target.AddEvent ("Was blown up by enemy grenade!\n");
	}
	
	int CheckTrait (string TraitName, int modifier, SoldierController target)
	{
		if (target.HasAttribute(TraitName))
		{
			return modifier;
		}
		return 0;
		
	}

}
