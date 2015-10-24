using UnityEngine;
using System.Collections.Generic;

public class Event_Retreat : MonoBehaviour {

	List<SoldierController> squad;
	Campaing campaing;

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
		"hurt",
		"shot"
	};

	public Event_Retreat(Campaing injectionCampaing)
	{
		this.campaing = injectionCampaing;
	}

	public bool RetreatCheck( List<SoldierController> INJECT , int difficulty)
	{
		bool Retreat = false;

		this.squad = INJECT;

	    int MoraleCheck = 0;
	    int AliveSoldiers = 0;
	    foreach (SoldierController solttu in squad)
	    	{
			if (solttu.alive == true)
			{
				AliveSoldiers++;
				MoraleCheck += solttu.morale;
				MoraleCheck += solttu.rank*10;

				if (solttu.HasAttribute("newbie"))
					MoraleCheck += -10;
				if (solttu.HasAttribute("wounded"))
					MoraleCheck += -20;
				if (solttu.HasAttribute("coward"))
					MoraleCheck += -20;
				if (solttu.HasAttribute("heroic"))
					MoraleCheck += 40;
				if (solttu.HasAttribute("robo-heart") | solttu.HasAttribute("robo-organs"))
					MoraleCheck += 20;
				if (solttu.HasAttribute("veteran"))
					MoraleCheck += 40;
				if (solttu.HasAttribute("loner"))
					MoraleCheck += 10;
			}
			else
			{
				MoraleCheck += -20; // more deaths = more want to run!
			}
		
		
		}

		MoraleCheck = MoraleCheck / AliveSoldiers;	//Average!
		
		string [] cursings = new string[] {
			"Fuck",
			"Shit",
			"Darn",
			"Nope",
			"Ahh"
		};
		
		string cursingsInsert = cursings[(Mathf.RoundToInt(Random.value*(cursings.GetLength(0)-1)))];
		
		if (Random.Range(0, campaing.Campaing_Difficulty) > MoraleCheck) // if random roll is OVER the avg morale its time to FLEEE!
		{
			
			
			foreach (SoldierController solttu in squad) 
			{
				if (solttu.alive == true)
				{
					string [] runnings = new string[] {
						"flee",
						"fly",
						"run",
						"retreat",
						"get back to base"
					};
					
					
					string RunningsInsert = runnings[(Mathf.RoundToInt(Random.value*(runnings.GetLength(0)-1)))];
					
					solttu.AddHistory("-RUN-:" + campaing.TimeStamp);
					solttu.ChangeMorale(-20);
					
					solttu.AddEvent(cursingsInsert + ", time to " + RunningsInsert + "!!\n");

					if (Random.Range(1, difficulty) > 50)
					{
						this.RetreatRun(solttu, difficulty);
					}
				}
			}
			
			Retreat = true; 
			
		}
		else {
			foreach (SoldierController solttu in squad)
			{
				if (solttu.alive == true)
				{
					string [] Combatings = new string[] {
						"shit",
						"battle",
						"combat",
						"mission",
						"skirmish",
						"shootout",
						"",
						""
					};

					string [] hardnouns = new string[] {
						"HARD",
						"TOUGH",
						"CRAZY",
						"MAD",
						"WHACO",
						"DARK",
						"NOISY",
						"CROWDED",
						"MESSY",
						"DENSE",
						"BLOODY"
					};

					string CombatInsert = Combatings[(Mathf.RoundToInt(Random.value*(Combatings.GetLength(0)-1)))];

					string ToughtInsert = hardnouns[(Mathf.RoundToInt(Random.value*(hardnouns.GetLength(0)-1)))];

					solttu.AddEvent(cursingsInsert + " this " + CombatInsert+ " is getting "+ToughtInsert+"!\n");
					if (solttu.HasAttribute("wounded"))
						solttu.ChangeMorale(-20);
					if (solttu.HasAttribute("coward"))
						solttu.ChangeMorale(-20);
					if (solttu.HasAttribute("heroic"))
						solttu.ChangeMorale(2);	//Heroics like bullets going near them!
					else 
						solttu.ChangeMorale(-5);
				}
		}
			
		}

		return Retreat;
	}

	private void RetreatRun(SoldierController solttu, int difficulty)
	{
		int Runroll= Random.Range(1, solttu.skill);

		Runroll += CheckTrait("newbie", -10, solttu);
		Runroll += CheckTrait("wounded", -20, solttu);
		Runroll += CheckTrait("veteran", 20, solttu);
		Runroll += CheckTrait("young", 5, solttu);
		Runroll += CheckTrait("drunkard", -5, solttu);
		Runroll += CheckTrait("coward", 10, solttu);	//few places this is useful!!
		Runroll += CheckTrait("heroic", -5, solttu);
		Runroll += CheckTrait("robo-leg", 10, solttu);
		Runroll += CheckTrait("robo-movement", 20, solttu);

		if (solttu.health < 25)
			Runroll += -40;
		else if (solttu.health < 50)
			Runroll += -30;
		else if (solttu.health < 75)
			Runroll += -20;

		int EnemyGunRoll = Random.Range(1, difficulty);

		if (Runroll < EnemyGunRoll)	// HIT! quite same to the normal shot!
		{
			string returned = "";

			string monstername = monsternames[(Mathf.RoundToInt(Random.value*(monsternames.GetLength(0)-1)))];
			string verb = verbs[(Mathf.RoundToInt(Random.value*(verbs.GetLength(0)-1)))];

			solttu.ChangeHealth(-20);
			solttu.ChangeMorale(-30);
			solttu.AddAttribute("wounded");
			
			if (solttu.health < 0) 
			{
				returned ="During retreat, was " + verb +" by "+monstername + " in the back!!\n";
				solttu.die("Killed during retreat");
			}
			else
			{
				string hurtInsert = hits[(Mathf.RoundToInt(Random.value*(hits.GetLength(0)-1)))];
				returned ="During retreat, was " + hurtInsert +" by "+monstername + " in the back!!\n";

			}
			solttu.AddEvent(returned);

		}
		else
		{
			solttu.AddEvent("Fled without accidents.\n");
		}

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
