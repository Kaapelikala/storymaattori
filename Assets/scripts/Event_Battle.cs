using UnityEngine;
using System.Collections;


public class Event_Battle {

	public SoldierController target;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

		public void FightRound (SoldierController NEWTARGET, int Enemy_difficulty){

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

		if (Roll < HitChance)
		{
			target.AddKills(1);
			target.AddExperience(Enemy_difficulty/10);
		}

		else if (Roll < BothMissChance)
		{
			
		}
		else if (Roll < SoldierHitChance)
		{
			target.ChangeHealth(-10);
			target.ChangeMorale(-20);
			target.AddAttribute("wounded");

			if (target.morale < 0)
				target.alive=false;
		}
		else if (Roll < SoldierDeadChance)
		{
			target.alive = false;
		}



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


