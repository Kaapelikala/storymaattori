using UnityEngine;
using System.Collections;

public class Event_Vacation : MonoBehaviour {

	// Wild party at motherbase. No worries of enemies but excess can be harmfull too!

	private string MissionName = "";
	private SoldierController target;

	public int Difficulty;
	public int GreatestRank;

	string [] DecisionVerbs = new string[] {
		"decided",
		"was ordered",
		"wanted",
		"started"
	};

	public Event_Vacation (string MissionNameImput)
	{
		this.MissionName = MissionNameImput;
	}

	public void Handle (SoldierController NEWTARGET, int DifficultyInject, int GreatestRankInject){
	
		this.target = NEWTARGET;
		this.Difficulty = DifficultyInject;
		this.GreatestRank = GreatestRankInject;

		this.target.ChangeHealth(10);

		this.target.skill -= 1;		//skill decrease, nasty!

		/*
		this.target.AddEvent("WILD PARTYYYY\n");
		this.target.ChangeMorale(20);
		*/



		// WHAT SOLDIER DOES!

		if (CheckTrait("wounded") && Random.Range(0,10) > target.health)
		{
			target.AddEvent(target.soldierFName + "was too wounded to actually enjoy the vacation!\n");
			target.ChangeMorale(-40);
		}
		else if (CheckTrait("depressed") && Random.Range(0,10) > target.health)
		{
			target.AddEvent("Terrors of the battlefield were too strong: " + target.soldierFName + " spent most of the vacation withdrawn.\n");
			target.ChangeMorale(-40);
		}
		else 
		{
			string choiceReturn = "";
					
			string ActionVerb = DecisionVerbs[(Mathf.RoundToInt(Random.value*(DecisionVerbs.GetLength(0)-1)))];

			choiceReturn = target.soldierFName + " " + ActionVerb + " to ";

			int Roll = (Mathf.RoundToInt(Random.Range(0, 100)));


			if (Roll < 30)
			{
				target.AddEvent(choiceReturn + "party!\n");
				target.ChangeMorale(20);
				this.Party();
			}
			else if (Roll < 45)
			{
				target.AddEvent(choiceReturn + "train.\n");
				this.Train();
			}
			else if (Roll < 70)
			{
				target.AddEvent(choiceReturn + "try to relax.\n");

				this.Relax();

			}
			else if (Roll < 90)
			{
				target.AddEvent(choiceReturn + "seek counseling.\n");
				this.Counsel();
			}
			else
			{
				target.AddEvent(choiceReturn + "do weird shit!\n");

				this.Weird();



			}
		}


		// Decide to TRAIN
		// Decide to PARTY	-Shots - Gamble - Hookers & Blow
		// Decide to Take it easy
		// Decide to try to care a problem
		// Weird shit

	}

	/*


					gain	remove		bonus/negative
		Heroic		-		?			
		accurate	!		Training	Moralebonus
		inaccurate	!		Training	Morale loss
		idiot		yes		no			Does something stupid, Chance of Death	
		loner		no		yes?		Morale- does not enjoy 					
		cook		Yes		-			Morale bonus to all!
		tough		-		-			Slight healing?
		young		-		yes?		Unaccustomed?
		drunkard	YES		counseling	Random, chance of death.
		coward		-		Yes?		Random
		techie		Yes		Yes?		
		lucky		-		-			GOOD!
		wounded		Yes		Yes			Chance to have to sit out part of the fun?
		depressed	yes		yes			Bad overall but can be lost?


		other: PTSD, ROAD TRIP, DRUGS

		-> CONNECTIONS DAMMIT!


	*/

	private void Weird()
	{
		int BeginHealth = target.health;
		int BeginMorale = target.morale;


		target.ChangeHealth(Random.Range(-40,30));
		target.ChangeMorale(Random.Range(-40,30));

		if (target.health < 0)
		{
			string Sexdiff = "";
			
			if (target.sex == 'm')
			{
				Sexdiff = "He";
			}
			else 
			{
				Sexdiff = "She";
			}

			target.AddEvent("Rave was too strong: " + Sexdiff +" never returned to base!\n");
			target.die("Unknown");
		}
		else if (target.health > BeginHealth && target.morale > BeginMorale)
		{
			target.AddEvent("JUST SO RIGHT!\n");
			target.ChangeMorale(40);
		}
		else
		{
			target.AddEvent("EUGH!\n");

			bool SomethingHappened = false;

			while (!SomethingHappened)
			{
			int BadLuck = Random.Range(0, 7);

			switch (BadLuck)
			{
				case 0:
					if (Random.Range(0,10) > 7)
					{
						SomethingHappened = true;
						target.AddAttribute("loner");
						target.AddEvent("The shadows begin to whisper...\n");
					}
					break;
				case 1:
					if (Random.Range(0,10) > 7)
					{
						SomethingHappened = true;
						target.AddAttribute("techie");
						target.AddEvent("Machines are so nice!\n");
					}
					break;
				case 2:
					if (Random.Range(0,10) > 7)
					{
						SomethingHappened = true;
						target.AddAttribute("cook");
						target.AddEvent("No clue what it was, but tasted amazing!\n");
					}
					break;
				case 3:
					if (Random.Range(0,10) > 7)
					{
						SomethingHappened = true;
						target.AddEvent("No memory, but... A Bravery Medal?\n");
						target.AddAward("Bravery Medal");
					}
					break;
				case 4:
					if (Random.Range(0,10) > 7)
					{
						SomethingHappened = true;
						target.AddAttribute("wounded");
						target.AddAttribute("tough");
						target.AddEvent("How did " + target.soldierFName + "survive all that?\n");
					}
					break;
				case 5:
					if (Random.Range(0,10) > 7)
					{
						SomethingHappened = true;
						target.AddAttribute("wounded");
						target.AddEvent("Shot your own leg? Why you were waiving your gun around?\n");
					}
					break;
				default:
					if (Random.Range(0,10) > 8)
					{
						SomethingHappened = true;
						target.AddAttribute("idiot");
						target.AddEvent("Brain felt fizzyyy....\n");
						if (Random.Range (0,120) > target.health)
						{
							target.die("Brain melted");
							target.AddEvent("... too fizzy.\n");
						}

					}
					break;
				}
			}


		}
	}

	private void Counsel()
	{

		string Sexdiff = "";
		
		if (target.sex == 'm')
		{
			Sexdiff = "His";
		}
		else 
		{
			Sexdiff = "Her";
		}
		
		target.AddEvent(Sexdiff + " request was denied!\n");
		
		if (Random.Range(1,10) > 6)
		{
			target.AddEvent("Oh well then. Another try!\n");
			this.Handle(target, Difficulty, GreatestRank);
		}
		else
		{
			target.AddEvent(target.soldierFName + " had no time to do anything else. It sucked.\n");
			target.ChangeMorale(-20);
		}

	}

	private void Relax()
	{
		int Roll = Random.Range (0, 100)-CheckTrait("coward", -10);
		
		if (Roll < 25)
		{
			target.AddEvent("It REALLY hit the spot!\n");
			target.ChangeHealth(15);
			target.ChangeMorale(40);

			if (CheckTrait("depressed"))
			{
				if (Random.Range (0, 10) > 6 )
				{
					target.AddEvent("Some of the night terrors even went away.\n");
					target.RemoveAttribute("depressed");
				}

			}
		}
		else if (Roll < 50)
		{
			target.AddEvent("It worked.\n");
			target.ChangeHealth(10);
			target.ChangeMorale(20);
		}
		else if (Roll < 75)
		{
			target.AddEvent("It could have been better.\n");
			target.ChangeHealth(5);
			target.ChangeMorale(10);
		}
		else 
		{
			target.AddEvent("The itch for combat did not go away.\n");
			target.ChangeHealth(5);
			target.ChangeMorale(-5);
		}

	}

	private void Train()
	{
		int Roll = Random.Range(0, 100);
		
		Roll += CheckTrait ("young", 5); 
		Roll += CheckTrait ("coward", -5); 
		Roll += CheckTrait ("idiot", -5); 
		Roll += CheckTrait ("loner", -5); 

		if (100-Roll < target.skill-100)
		{
			if (CheckTrait ("inaccurate"))
			{
				string Sexdiff = "";
				
				if (target.sex == 'm')
				{
					Sexdiff = "his";
				}
				else 
				{
					Sexdiff = "her";
				}
				target.AddEvent(target.soldierFName + " has managed to normalise " + Sexdiff + " aim!\n");
				target.RemoveAttribute("inaccurate");
				target.ChangeMorale(20);
			}
			else if (!CheckTrait ("inaccurate") && !CheckTrait ("accurate"))
			{
				target.AddEvent(target.soldierFName + " now often hits the bullseye!\n");
				this.AddAward("Markmanship Metal");
				target.AddAttribute("accurate");
				target.ChangeMorale(40);
			}
			else if (CheckTrait ("accurate"))
			{
				target.AddEvent(target.soldierFName + " won the shooting competition!\n");
				this.AddAward("Markmanship Metal");
				target.ChangeMorale(30);
			}

		}
		else if (Roll > 50)
		{
			target.AddEvent("Training was arduous but effective.\n");
			target.ChangeMorale(10);
			target.skill++;
			target.skill++;
		}
		else if (Roll < 10)
		{
			target.AddEvent("Training was hell. "+ target.soldierFName + " did not learn much.\n");
			target.skill--;
			target.ChangeHealth(-30);
			target.ChangeMorale(-40);

			if (target.health < 0)
			{
				target.AddEvent("It was too much. " + target.soldierFName + "perished!\n");
				target.die("Overexercise");
			}
		}
		else
		{
			target.AddEvent("Training was normal chore.\n");
			target.ChangeMorale(-10);
			target.skill++;
		}
			// HOW REACTS TO TRAINING?

	}


	private void Party()
	{

		//COOKING, GAMBLE, DINKING, 5D-MOVIE, Computer Games

		bool EventHappened = false;

		while (!EventHappened){

			int PartyEventRandomiser = Random.Range(0, 4);
			
			switch (PartyEventRandomiser)
			{
			case 0:
				if (CheckTrait("cook") && (Random.Range (0,10) > 3))
				{
					EventHappened = true;

					target.AddEvent(target.soldierFName + " held a delicious barbecue!\n");
					target.ChangeMorale(20);
					
				}
				else if (CheckTrait("idiot") && (Random.Range (0,10) > 7))
				{
					EventHappened = true;
					
					target.AddEvent(target.soldierFName + " burned a delicious barbecue to ash!!\n");
					target.ChangeMorale(-20);
						
				}
				break;
			case 1:
				EventHappened = true;

				target.AddEvent("Gambling time! \n");

				int GambleChange = Random.Range(0,10)+CheckTrait("lucky",2)+CheckTrait("idiot",-2);

				if (GambleChange > 9)
				{
					target.AddEvent(target.soldierFName + " won big time!! Yeah!\n");
					target.ChangeMorale(30);

					target.gear++;
				}
				else if (GambleChange > 5)
				{
					target.AddEvent(target.soldierFName + " won!\n");
					target.ChangeMorale(10);
				}
				else if (GambleChange > 3)
				{
					target.AddEvent(target.soldierFName + " lost!\n");
					target.ChangeMorale(-20);
				}
				else
				{
					target.AddEvent(target.soldierFName + " lost everything, even the favourite dice!\n");
					target.ChangeMorale(-40);

					target.gear--;
				}
				break;
				default:

				EventHappened = true;
				target.AddEvent("It was time for SHOT");
				this.Shots(target.health, 0);

				break;
			}
		}

		

	}

	private int Shots(int ConAmountLeft, int shotAmount)
	{
		int Roll = Random.Range(0, 100);

		Roll += CheckTrait ("drunkard", 20); 
		Roll += CheckTrait ("tough", 10); 
		Roll += CheckTrait ("idiot", 5); 


		if ( Roll > ConAmountLeft)
		{
			int LuckRoll = Random.Range(10, 100) + shotAmount;		//How Fun It WAS!


			LuckRoll = (CheckTrait("drunkard", -20));
			LuckRoll = (CheckTrait("newbie", -10));
			LuckRoll = (CheckTrait("lucky", 20));
			LuckRoll = (CheckTrait("young", 10));


			if (LuckRoll > 50)	//REAL FUN BEGINS!
			{
				target.AddEvent("and SHOT the party REALLY started!\n");

				int SurvivalRoll = Random.Range(0, 100);
				
				SurvivalRoll += CheckTrait("drunkard", -20);
				SurvivalRoll += CheckTrait("tough", 10);
				SurvivalRoll += CheckTrait("young", -5);
				SurvivalRoll += CheckTrait("robo-heart", 5);
				SurvivalRoll += CheckTrait("robo-organs", 20);
				
				if (SurvivalRoll < 33)	//ENJOYED THE NIGHT
				{
					target.AddEvent(target.soldierFName + " has no clue what happened but the bitemarks were awesome!\n");
					target.ChangeMorale(10 + shotAmount);
					target.ChangeHealth(-shotAmount);
					
					int DrunkBeginsRoll = Random.Range(0, 100);
					
					if (DrunkBeginsRoll > 50 && !CheckTrait("drunkard"))
					{
						target.AddEvent("Have to do this again!");
						target.AddAttribute("drunkard");
						target.ChangeMorale(10);
					} 
				}
				else if (SurvivalRoll < 66)
				{
					target.AddEvent("OH YEAH! Did anyone take pictures of those Bazookas?\n");
					target.ChangeMorale(30 + shotAmount);

				}
				else // NOT THAT GOOD NIGHT
				{
					target.AddEvent("Totally worth it!\n");
					target.ChangeHealth(-shotAmount);
					target.ChangeMorale(10 + shotAmount);
				}

				if (target.health < 0)
				{
					target.AddEvent("But " + target.soldierFName + "s heart melted!\n");
					target.die("Total organ failure");
				}
			}
			else
			{
				target.AddEvent("and SHOT it all went black\n");
				int SurvivalRoll = Random.Range(0, 100);

				SurvivalRoll += CheckTrait("drunkard", -20);
		
				if (SurvivalRoll < 50)	//ENJOYED THE NIGHT
				{
					target.AddEvent("It was heavenly\n");
					target.ChangeHealth(-shotAmount);
					target.ChangeMorale(shotAmount);

					int DrunkBeginsRoll = Random.Range(0, 100);
					
					if (DrunkBeginsRoll > 50 && !CheckTrait("drunkard"))
					{
						target.AddEvent("Have to do this again!\n");
						target.AddAttribute("drunkard");
					} 
				}
				else // NOT THAT GOOD NIGHT
				{
					target.AddEvent("Maybe it was not that smart\n");
					target.ChangeHealth(-10-shotAmount);
					target.ChangeMorale(-shotAmount);
				}

				if (target.health < 0)
				{
					target.AddEvent("But " + target.soldierFName + "s liver exploded!\n");
					target.die("Liver failure");
				}

			}
			return shotAmount;
		}

		target.AddEvent(", SHOT");
		return Shots(ConAmountLeft -10, shotAmount++);

	}
	public void AddAward(string AwardName)
	{
		if (!target.HasAward(AwardName))
		{
			string Sexdiff = "";
			
			if (target.sex == 'm')
			{
				Sexdiff = "He";
			}
			else 
			{
				Sexdiff = "She";
			}
			target.AddEvent(Sexdiff + " was awarded the " + AwardName + "\n");
			target.AddAward(AwardName);
			
			if (!AwardName.Equals("Campaing Medal") || !AwardName.Equals("Wound Badge"))
			{
				int Roll = Random.Range(0, 10);
				
				if (Roll < 33)
				{
					target.AddEvent("It felt great!");
					target.ChangeMorale(10);
				}
				else if (Roll < 66)
				{
					target.AddEvent(target.soldierFName + " did not care!");
				}
				else
				{
					target.AddEvent("It was not worth all the losses");
					target.ChangeMorale(-20);
				}
			}
			
		}
		
	}


	bool CheckTrait (string TraitName)
	{
		if (target.HasAttribute(TraitName))
		{
			return true;
		}
		return false;
		
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
