using UnityEngine;
using System.Collections.Generic;

//Battle - Classical shootoff!
public class Event_Battle {

	public List<SoldierController> squad;
	public SoldierController target;

	string[] monsternames= new string[] 
	{
		"Mauler",
		"Snotling",
		"Waster",
		"Kitten",
		"Puppy",
		"Green-Haired Screamer",
		"Triclops",
		"Duclops",
		"Soldier"
	};

	string[] monsterAdjectives = new string[] 
	{
		"Red",
		"Hungry",
		"Nasty",
		"Evil",
		"Cold",
		"Vicious",
		"Stealthy",
		"Frenzized",
		"Fanatic",
		"Mad",
		"Lucky",
		"Black",
		"Screaming",
		"Acrobatic",
		"Quick"
	};

	string [] verbs = new string[] {
		"mauled",
		"wasted",
		"squashed",
		"flattened",
		"dematerialized",
		"cooked",
		"chewed",
		"samurai sworded",
		"packaged",
		"smoshed",
		"drowned",
		"zapped",
		"flamed",
		"grilled",
		"asskicked",
		"freezed",
		"nomnomed",
		"shot",
		"blazed",
		"blamed"
	};

	string [] hits = new string[] {
		"hit",
		"wounded",
		"grazed",
		"hurt"
	};
	
	public Event_Battle(List<SoldierController> INJECT)
	{
		this.squad = INJECT;

	}

	public void addCombatEvent(bool negative)
	{

		string returned="";
		//int temp;
		string monstername = monsternames[(Mathf.RoundToInt(Random.value*(monsternames.GetLength(0)-1)))];
		string monsterADJ = monsterAdjectives[(Mathf.RoundToInt(Random.value*(monsterAdjectives.GetLength(0)-1)))];
		string verb = verbs[(Mathf.RoundToInt(Random.value*(verbs.GetLength(0)-1)))];

		// Negative = Soldier got killed!
		if (negative) {
			returned ="Was " + verb +" by "+  monsterADJ + " "+monstername + "!!\n";		//needs more drama!

			foreach (SoldierController solttu in squad)
			{
				if (solttu.alive == true && solttu != target)
				{
					solttu.AddEvent(target.GetRank() + " " +target.getCallsignOrFirstname() + " got " + verb +"!!\n");

					if (solttu.HasAttribute("coward"))
						solttu.ChangeMorale(-30);	//should it compare histories once more?
					else if (solttu.HasAttribute("loner"))
					{}	//nada!
					else
						solttu.ChangeMorale(-10);
				}

			}
		}
		else
		{
			returned = verb + " a " +  monsterADJ + " "+ monstername + "!\n";
		}



		target.AddEvent(returned);
	}

	public int FightRound (SoldierController NEWTARGET, int Enemy_difficulty)
	{

		this.target = NEWTARGET;

		//Enemy_difficulty pitäs olla about 100 normaalisti.

		int HitChance = 50;
		int BothMissChance = 10;
		int SoldierHitChance = 30;
		int SoldierDeadChance = 10;

		//calculating modifiers based on Enemy Difficulty and Traits

		int modifier = 0;

		modifier +=  target.SkillTotal() - Enemy_difficulty;

		modifier += CheckTrait("heroic", 5);
		modifier += CheckTrait("veteran", 5);
		modifier +=  CheckTrait("idiot", -10);
		modifier +=  CheckTrait("newbie", -5);
		modifier +=  CheckTrait("wounded", -20);
		modifier +=  CheckTrait("robo-eye", 5);
		modifier +=  CheckTrait("robo-vision", 10);
		modifier +=  CheckTrait("robo-manipulators", 5);
		modifier +=  CheckTrait("stressed", -5);

		HitChance +=  CheckTrait("accurare", 5);

		HitChance +=  CheckTrait("inaccurate", -10);
		BothMissChance +=  CheckTrait("inaccurate", +10);

		//combining numbers for Single Roll

		BothMissChance +=  HitChance;
		SoldierHitChance +=  BothMissChance;
		SoldierDeadChance  +=  SoldierHitChance;

		// actual battle!


		int Roll = Random.Range(0, 100);


		Roll = Roll - modifier;

		 //Positive modifier = Good = rolls lower.
		 //Negative modifier = Bad = rolls higher.

		target.RemoveAttribute("newbie");

		if (Roll < HitChance)
		{
			//target.AddKills(1); // this is done in EVENTCONTROLLER THESE DAYS!
			addCombatEvent (false);
			target.AddExperience(Enemy_difficulty/10);		//EXP NOT USED FOR NOW BUT IT IS THERE
			//return (target.callsign + " killed enemy!");
			return 1;
		}

		else if (Roll < BothMissChance)		// THIS SHOULD HAVE SOMETHING ELSE AS WELL!!! EVENT OR SOMETHING?
		{
			string monstername = monsternames[(Mathf.RoundToInt(Random.value*(monsternames.GetLength(0)-1)))];
			string monsterADJ = monsterAdjectives[(Mathf.RoundToInt(Random.value*(monsterAdjectives.GetLength(0)-1)))];
			target.AddEvent("Missed a " +  monsterADJ + " " + monstername + "!\n");
			target.ChangeMorale(5);
			return 0;
			//return (target.callsign + " missed!");
		}
		else if (Roll < SoldierHitChance)
		{

			target.ChangeHealth(-20);
			target.ChangeMorale(-20);
			target.AddAttribute("wounded");

			if (target.health < 0){
				addCombatEvent (true);
				return 0;
				//return (target.callsign + " Died!");
			}

			string monstername = monsternames[(Mathf.RoundToInt(Random.value*(monsternames.GetLength(0)-1)))];
			string monsterADJ = monsterAdjectives[(Mathf.RoundToInt(Random.value*(monsterAdjectives.GetLength(0)-1)))];

			string hurtInsert = hits[(Mathf.RoundToInt(Random.value*(hits.GetLength(0)-1)))];

			Debug.Log(target.callsign + " was " + hurtInsert +" by a " +  monsterADJ + " " + monstername + "!\n");
			target.AddEvent("Was " + hurtInsert +" by a "+  monsterADJ + " " + monstername + "!\n");

			//return (target.callsign + " was hit by enemy!");
			return 0;

		}

		
		target.die ("Died in battle");
		addCombatEvent (true);
		//return (target.callsign + " Died!");
		return 0;



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


