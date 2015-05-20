using UnityEngine;
using System.Collections;

//Battle - Classical shootoff!
public class Event_Battle {

	public SoldierController target;
	public string MissionName = "";

	string[] monsternames= new string[] {"Mauler","Snotling","Waster","Kitten","Puppy","Green-Haired Screamer","Triclops","Duclops","Soldier"};
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
		"nomnomed"
	};

	public Event_Battle()
	{
	}

	public Event_Battle(string NameInsert)
	{
		this.MissionName = NameInsert;
	}

		public void addCombatEvent(bool negative)
	{

		string returned="";
		//int temp;
		string monstername = monsternames[(Mathf.RoundToInt(Random.value*(monsternames.GetLength(0)-1)))];
		string verb = verbs[(Mathf.RoundToInt(Random.value*(verbs.GetLength(0)-1)))];


		if (negative) {
			returned = verb +" by "+monstername + "\n";
		}
		else
		{
			returned = verb + " a " + monstername + "!\n";
		}



		target.AddEvent(returned);
	}

		public string FightRound (SoldierController NEWTARGET, int Enemy_difficulty){

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
		modifier +=  CheckTrait("idiot", -10);
		modifier +=  CheckTrait("newbie", -5);
		modifier +=  CheckTrait("wounded", -10);

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
			target.AddKills(1);
			addCombatEvent (false);
			target.AddExperience(Enemy_difficulty/10);
			return (target.callsign + " killed enemy!");
		}

		else if (Roll < BothMissChance)
		{
			return (target.callsign + " missed!");
		}
		else if (Roll < SoldierHitChance)
		{

			target.ChangeHealth(-10);
			target.ChangeMorale(-20);
			target.AddAttribute("wounded");

			if (target.health < 0){
				addCombatEvent (true);
				return (target.callsign + " Died!");
			}

			string monstername = monsternames[(Mathf.RoundToInt(Random.value*(monsternames.GetLength(0)-1)))];

			Debug.Log(target.callsign + " was wounded by a " + monstername + "!\n");
			target.AddEvent("Was wounded by a " + monstername + "!\n");

			return (target.callsign + " was hit by enemy!");

		}

		
		target.die ("Died in battle");
		addCombatEvent (true);
		return (target.callsign + " Died!");
		



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


