using UnityEngine;
using System.Collections;

public class Event_EnemyEmplacement : MonoBehaviour {

	public SoldierController target;
	
	string[] gunTypes= new string[] {
		"machinegun",
		"flamer",
		"blaster",
		"fletchetter",
		"artillery",
		"minigun",
		"plasmoid",
		"BFG",
		"Spitter",
		"rocket",
		"cannon"
	};

	string[] emplacementNames= new string[] {
		"nest",
		"emplacement",
		"bunker",
		"hideout",
		"trench",
		"trap"
	};

	string [] verbs = new string[] {
		"killed",
		"blasted",
		"holeyed",
		"cooked",
		"chewed",
		"carnaged",
		"flamed",
		"shot",
		"blamed",
		"annihilated"
	};

	/// <summary>
	/// SOLDIER ENCOUNTERS ENEMY BASE - WHICH SEES WHICH FIRST AND WHAT HAPPENS THEN? BANG BANG!
	/// </summary>
	/// <param name="NEWTARGET">NEWTARGE.</param>
	/// <param name="Enemy_difficulty">Enemy_difficulty.</param>
	public void Encounter (SoldierController NEWTARGET, int Enemy_difficulty){
		
		this.target = NEWTARGET;
		
		//Enemy_difficulty pitäs olla about 100 normaalisti.

		
		//calculating modifiers based on Enemy Difficulty and Traits
		
		int InitiativeModifier = 0;
		
		InitiativeModifier +=  target.SkillTotal() - Enemy_difficulty;
		
		InitiativeModifier +=  CheckTrait("heroic", 5);
		InitiativeModifier +=  CheckTrait("veteran", 5);
		InitiativeModifier +=  CheckTrait("lucky", 20);
		InitiativeModifier +=  CheckTrait("idiot", -20);
		InitiativeModifier +=  CheckTrait("newbie", -5);
		InitiativeModifier +=  CheckTrait("wounded", -20);
		InitiativeModifier +=  CheckTrait("robo-eye", 15);
		InitiativeModifier +=  CheckTrait("robo-vision", 30);


		// WHICH SEES WHICH FIRST??

		if ((Random.Range(0, 100) + InitiativeModifier) > 60) //Slightly more chance for ENEMY to go first - nasty!
		{
			this.FirstSoldier();	//Lucky soldier!

		}
		else
		{
			this.EnemyFirst();	//Uh oh

		}
	}

	private void FirstSoldier(){

		string gunInsert = gunTypes[(Mathf.RoundToInt(Random.value*(gunTypes.GetLength(0)-1)))];
		string emplacementInsert = emplacementNames[(Mathf.RoundToInt(Random.value*(emplacementNames.GetLength(0)-1)))];
		string verbInsert = verbs[(Mathf.RoundToInt(Random.value*(verbs.GetLength(0)-1)))];

		target.AddEvent("Stumbled upon an enemy "  + gunInsert + " " + emplacementInsert + " and " + verbInsert + " it!\n" );
		target.AddKills((Mathf.RoundToInt(Random.Range(1, 4))));

	}


	private void EnemyFirst(){

		string gunInsert = gunTypes[(Mathf.RoundToInt(Random.value*(gunTypes.GetLength(0)-1)))];
		string emplacementInsert = emplacementNames[(Mathf.RoundToInt(Random.value*(emplacementNames.GetLength(0)-1)))];
		string verbInsert = verbs[(Mathf.RoundToInt(Random.value*(verbs.GetLength(0)-1)))];

		target.AddEvent("Was " + verbInsert + " by enemy " + gunInsert + " " + emplacementInsert + "!\n" );

		
		target.ChangeHealth((Random.Range(-60, -30)));
		target.ChangeMorale((Random.Range(-60, -30)));
		target.AddAttribute("wounded");


		if (target.health < 0)
		{
			target.die("Killed by " + gunInsert);
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
