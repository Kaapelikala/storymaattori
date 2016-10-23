using UnityEngine;
using System.Collections;


//After Battle : Rest!
public class Event_Debrief : MonoBehaviour {


	public SoldierController target;
	public string MissionName = "";
		
	public string CookName = "";
			

	public void Handle (SoldierController NEWTARGET, Mission MissionImput, bool AwardBraveryMedal){
			
		this.target = NEWTARGET;

		target.RemoveAttribute("newbie");

		target.AddEvent("--Debriefing: \n");


		if (MissionImput.retreat == true)
		{
			target.AddEvent("Mission ended in a retreat.");
			target.ChangeMorale(-20);

			this.CheckMoodAfterDefeat();

		}
		else if (MissionImput.victory == true)
		{
			string[] winnouns= new string[] 
			{
				"win",
				"success",
				"victory",
				"triumph"
			};
			
			string winInject = winnouns[(Mathf.RoundToInt(Random.value*(winnouns.GetLength(0)-1)))];
			
			target.AddEvent("Mission was a "+winInject+"!\n");
			target.AddHistory("-Victory-:"+MissionImput.MissionName); // so other soldiers of same battle like this soldier more!
			target.ChangeMorale(30);
		}
			else
		{
			string[] failurenouns= new string[] 
			{
				"defeat",
				"failure",
				"loss"
			};

			string failureInject = failurenouns[(Mathf.RoundToInt(Random.value*(failurenouns.GetLength(0)-1)))];

			target.AddEvent("Mission was a "+failureInject+".");
			target.ChangeMorale(-10);

			this.CheckMoodAfterDefeat();

		}


		AddAward("Campaing Medal");

		if (CheckTrait("tough"))		//being tough helps a lot!
			target.ChangeHealth(10);
		if (CheckTrait("heroic"))		//being heroic helps a lot!
			target.ChangeMorale(10);
		if (CheckTrait("coward"))		//being coward doesn't help a lot...
			target.ChangeMorale(-10);
		if (CheckTrait("depressed"))		//being depressed sucks.
			target.ChangeMorale(-10);
		
		if (target.callsign == "" && target.kills > 4 && (Random.Range(0, 10) > 2))		//Small chance it does not happen for now
		{
			string NewCallsign = target.GenerateCallSign();

			string sexdiff = "";

			if (this.target.sex == 'm')
			{
				sexdiff = "He";
			}
			else
				sexdiff = "She";

			target.AddEvent(sexdiff + " was given callsign '" + target.callsign + "!\n");

			target.skill ++;
			target.ChangeMorale(10);

		}

		if (target.missions > 8 && target.kills > 10 && (Random.Range(0, 10) > 2) && !CheckTrait("veteran"))		//Veteranship!!
		{

			target.AddEvent("Soldiers call " + this.target.getCallsignOrFirstname() + " a veteran!\n");
			target.AddAttribute("veteran");
			target.AddHistory("-VET-");	//Target is now a veteran!
			target.skill ++;
			target.ChangeMorale(20);
			
		}
		
		//HANDLING OF WOUNDEDS!
		this.CheckHealing();
		
		//PROMOTIONS
		this.CheckPromotions();

		if (AwardBraveryMedal == true)
		{
			target.AddEvent("Having participated in mission crucial to the War Effort, " +target.soldierLName+ " was awarded the Bravery Medal!\n");
			target.AddAward("Bravery Medal");
		}

		if ((target.HasHistory("-BOMBDEFUSER-") == true) && (target.HasAward("Bomb Defuse Medallion") == false))
		{
			target.AddEvent("Having successfully defused an enemy explosive, " +target.soldierLName+ " was awarded the Bomb Defusing Medallion!\n");
			target.AddAward("Bomb Defuse Medallion");	// worthy of an award!
		}

		this.CheckKillAwards();

		// CHANCE FOR NEW TRAITS
		if (Random.Range(0, 100) > 70){


			int traitRandomiser = Mathf.RoundToInt(Random.Range(0, 2));
		
			switch (traitRandomiser)
			{
			case 0:
			{
				if (CheckTrait("coward") | CheckTrait("heroic")){
					break;
				}
				target.AddEvent(target.getCallsignOrFirstname() + " found new resolve!\n" );
				this.target.AddAttribute("heroic");
				break;
				}
			case 1:
				{
				if (CheckTrait("tough")) {
					break;
				}
				target.AddEvent(target.getCallsignOrFirstname() + " does not care about scratches anymore!\n" );
				this.target.AddAttribute("tough");
				break;
				}
			case 2:
				{
				if (CheckTrait("heroic") | CheckTrait("coward")) {
					break;
				}
				target.AddEvent(target.getCallsignOrFirstname() + " began fear the dark!\n" );
				this.target.AddAttribute("coward");
				break;
				}
			}

			
			//Chance to kill oneself!!
			this.CheckSuecide();

			target.RemoveHistory("-ONMISSION-");	//The are again back at base!

		}

	}


	void Promote (SoldierController Ylennettava)
	{
		string Sexdiff = "";
		if (Ylennettava.sex == 'm')
		{
			Sexdiff = "his";
		}
		else 
		{
			Sexdiff = "her";
		}
		string FormerRank = Ylennettava.GetRank();
		
		Ylennettava.rank++;
		
		Ylennettava.AddEvent("Due to " + Sexdiff +" actions, " + FormerRank + " "+ Ylennettava.soldierLName +" was promoted to " + Ylennettava.GetRank() + "!\n");

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

	public void CheckPromotions()
	{
		if (this.target.kills > 1 && target.rank == 0)			//TROOPER
		{
			if ((Random.Range(0, 100)+ CheckTrait("heroic",10)+ CheckTrait("drunkard",-20)) > 40)
			{
				Promote(this.target);
				target.ChangeMorale(10);
				target.skill++;
			}
			else
			{
				target.AddEvent("Did not get deserved promotion!\n");
				target.ChangeMorale(-20);
			}
		}
		else if (this.target.kills > 8 && target.rank == 1)		//CORP
		{
			if ((Random.Range(0, 100)+CheckTrait("young",-10)+CheckTrait("heroic",10)+CheckTrait("drunkard",-20)) > 50)
			{
				Promote(this.target);
				target.ChangeMorale(20);
				target.skill++;
			}
			else
			{
				target.AddEvent("Did not get deserved promotion!\n");
				target.ChangeMorale(-30);
			}
		}
		else if (this.target.kills > 20 && target.rank == 2)		//CAPT
		{
			if ((Random.Range(0, 100)+CheckTrait("young",-10)+CheckTrait("heroic",10)+CheckTrait("drunkard",-20)) > 60)
			{
				Promote(this.target);
				target.ChangeMorale(30);
				target.skill++;
			}
			else
			{
				target.AddEvent("Did not get deserved promotion!\n");
				target.ChangeMorale(-50);
			}
		}
		else if (this.target.kills > 40 && target.rank == 3)		//LIUTENANT
		{
			if ((Random.Range(0, 100)+CheckTrait("young",-10)+CheckTrait("heroic",10)+CheckTrait("drunkard",-20)) > 70)
			{
				Promote(this.target);
				target.ChangeMorale(40);
			}
			else
			{
				target.AddEvent("Did not get deserved promotion!\n");
				target.ChangeMorale(-60);
			}
		}

	}

	public void CheckKillAwards()
	{
		
		if (target.kills > 10 && !target.HasAward("Kill Award"))
		{
			AddAward("Kill Award");
		}
		if (target.kills > 20 && target.HasAward("Kill Sword"))
		{
			AddAward("Kill Sword");
		}
		if (target.kills > 40 && !target.HasAward("Kill Bomb"))
		{
			AddAward("Kill Bomb");
		}
		if (target.kills > 80 && !target.HasAward("Kill Nuke"))
		{
			AddAward("Kill Nuke");
		}
		if (target.kills > 160 && !target.HasAward("Kill Armangeddon"))
		{
			AddAward("Kill Armangeddon");
		}

	}

	public void CheckForBionics()
	{
		if (CheckTrait("wounded")){

			bool AddedNewThing = false;

			string Sexdiff = "";
			if (target.sex == 'm')
			{
				Sexdiff = "His";
			}
			else 
			{
				Sexdiff = "Her";
			}

			int Roll = Random.Range(0, 100);


			if (Roll < 70)
			{
			}
			else if (Roll < 80)
			{				
				if (target.HasAttribute("robo-movement"))
				{
				}
				else if (target.HasAttribute("robo-leg"))
				{
					AddedNewThing = true;
					target.AddEvent(Sexdiff + " remaining leg was replaced with robotic one too.\n");
					target.AddAttribute("robo-movement");
					target.RemoveAttribute("robo-leg");
					target.skill++;		//as currently legs do not do much this helps at least!
				}
				else
				{
					AddedNewThing = true;
					target.AddEvent(Sexdiff + " wounded leg was replaced with robotic one.\n");
					target.AddAttribute("robo-leg");
					target.skill++;		//as currently legs do not do much this helps at least!
				}
			}
			else if (Roll < 90)
			{
				if (target.HasAttribute("robo-manipulators"))
				{
				}
				else if (target.HasAttribute("robo-arm"))
				{
					AddedNewThing = true;
					target.AddEvent(Sexdiff + " remaining arm was replaced with robotic one too.\n");
					target.AddAttribute("robo-manipulators");
					target.RemoveAttribute("robo-arm");
					target.skill++;
				}
				else
				{
					AddedNewThing = true;
					target.AddEvent(Sexdiff + " shredded arm was replaced with robotic one.\n");
					target.AddAttribute("robo-arm");
					target.skill++;
				}
			}
			else if (Roll < 95)
			{	
				if (target.HasAttribute("robo-organs"))
				{
				}
				else if (target.HasAttribute("robo-heart"))
				{
					AddedNewThing = true;
					target.AddEvent(Sexdiff + " leftover organs were replaced too.\n");
					target.AddAttribute("robo-organs");
					target.RemoveAttribute("robo-heart");
					target.ChangeHealth(20);
				}
				else
				{
					AddedNewThing = true;
					target.AddEvent(Sexdiff + " damaged heart was replaced with robotic one.\n");
					target.AddAttribute("robo-heart");
					target.ChangeHealth(10);
				}

			}
			else 
			{
				if (target.HasAttribute("robo-vision"))
				{
				}
				else if (target.HasAttribute("robo-eye"))
				{
					AddedNewThing = true;
					target.AddEvent(Sexdiff + " remaining eye was replaced with robotic one also.\n");
					target.AddAttribute("robo-vision");
					target.RemoveAttribute("robo-eye");
				}
				else
				{
					AddedNewThing = true;
					target.AddEvent(Sexdiff + " blasted eye was replaced with robotic one.\n");
					target.AddAttribute("robo-eye");
				}
			}




			if (AddedNewThing == true)
			{
				//Does Soldier like the new metal

				target.AddHistory("-ROBO-");	//Target is now at least somewhat Robotic
				
				Roll = Random.Range(0, 100);
				int SecondRoll = Random.Range(0, 5);

				if (target.sex == 'm')
				{
					Sexdiff = "He";
				}
				else 
				{
					Sexdiff = "She";
				}


				if (CheckTrait ("techie"))
				{
					target.AddEvent(Sexdiff + " was overjoyed by the new metal!\n");
					target.ChangeMorale(20);
				}
				else if (Roll < 33)
				{
					target.AddEvent(Sexdiff + " hated the new addition!!\n");
					target.ChangeMorale(-10);
				}
				else if (Roll < 66) 
				{
					target.AddEvent("Replacement felt better than the old one.\n");
					target.ChangeMorale(+10);

					if (SecondRoll > 3)
					{
						target.AddEvent(Sexdiff + " began to appreciate technology more\n");
						target.AddAttribute("techie");
					}
				}
				else
				{
					target.AddEvent(Sexdiff + " did not care\n");
				}





				
			}


		}



	}

	public void CheckHealing()
	{
		if (CheckTrait("wounded")){
			
			int Roll = Random.Range(0, 100);
			
			if (Roll < 50)			//NICE EXTENDED REST, did Character enjoy it?
			{
				CheckForBionics();
				target.AddEvent("Was sent to an extended rest.\n");
				target.RemoveAttribute("wounded");		//Wound goes away!
				target.ChangeHealth(20);
				
				int SecRoll = Random.Range(0, 100);
				
				if (CheckTrait("drunkard"))
				{
					target.AddEvent("Heavy drinking commerced.\n");
					target.ChangeMorale(40);
					target.ChangeHealth(-30);
					
					string sexdiff = "";
					
					if (this.target.sex == 'm')
					{
						sexdiff = "He";
					}
					else
						sexdiff = "She";
					
					if (this.target.health < 0)
					{
						target.AddEvent(sexdiff + " drank way too much and suffocated!!\n");
						target.dieHome("Partied to death!");
					}
				}
				else if (SecRoll < 50)
				{
					
					string result ="";
					result += "It was fun";
					target.ChangeMorale(30);
					
					int ThirdRoll = Random.Range(0, 5);		//Chance for alcoholism
					if (ThirdRoll >4)
					{
						result += " but " + target.getCallsignOrFirstname() + " began to drink too much!\n";
						target.AddAttribute("drunkard");
					}
					else
						result += ".\n";
					
					target.AddEvent(result);
					
				}
				else
				{
					target.AddEvent("It was relaxing.\n");
					target.ChangeMorale(10);
				}
				
				
				
			}
			else if (Roll < 75)			// NORMAL REST
			{
				CheckForBionics();
				target.AddEvent("Was given basic medical treatment.\n");
				target.RemoveAttribute("wounded");		//Wound goes away!
				target.ChangeHealth(10);
				
				int SecRoll = Random.Range(0, 100);
				
				if (SecRoll < 50)
				{
					target.AddEvent("It was OK.\n");
					target.ChangeMorale(20);
					
				}
				else
				{
					target.AddEvent(target.getCallsignOrFirstname() + " would have hoped better\n");
					target.ChangeMorale(5);
				}
			}
			else if (Roll < 90)			// NO REST
			{
				target.AddEvent("There was no time for medical help.\n");
				target.ChangeHealth(5);
				
				int SecRoll = Random.Range(0, 100);
				
				if (SecRoll < 25 + CheckTrait("lucky",10))		//being lucky helps!
				{
					target.AddEvent(target.getCallsignOrFirstname() + " 'found' a medkit!\n");
					target.ChangeHealth(5);
					target.ChangeMorale(10);
					
				} 
				else if (SecRoll < 50)
				{
					target.AddEvent("It was understandable.\n");
					target.ChangeMorale(5);
					
				}
				else
				{
					target.AddEvent("It sucked royally.\n");
					target.ChangeMorale(-20);
				}
			}
			
			AddAward("Wound Badge");		//Aways if wounded!
			
		}		
		else
		{
			target.AddEvent("Did not need medical help.\n");
			
			int SecRoll = Random.Range(0, 100);
			
			if (SecRoll < 25)
			{
				target.AddEvent("It was time to rest anyway!\n");
				target.ChangeHealth(5);
				target.ChangeMorale(20);
				
			} 
			else if (SecRoll < 50)
			{
				target.ChangeMorale(10);
				
			}
			else
			{
				target.ChangeMorale(-5);
			}
		}

	}

	/// <summary>
	/// CHANCE TO BANG HIMSELF due to POOR MORALE
	/// </summary>
	public void CheckSuecide()
	{
		if (this.target.morale < 0){		
			
			if (Random.Range(0, 100) > (70 +CheckTrait("heroic",10)+CheckTrait("coward",-10)+CheckTrait("depressed",-30)))
			{
				string Sexdiff = "";
				
				if (this.target.sex == 'm')
				{
					Sexdiff = "him";
				}
				else
					Sexdiff = "her";
				
				if ((CheckTrait("inaccurate") | CheckTrait("idiot")) && ((Random.Range(0, target.skill)) > 60))
				{
					this.target.AddEvent("Due to poor morale, " + target.getCallsignOrFirstname() + " tried to shot " + Sexdiff + "self but missed!\n");
				}
				else
				{
					target.AddEvent("Due to poor morale, " + target.getCallsignOrFirstname() + " shot " + Sexdiff + "self!\n");
					target.dieHome("Killed " + Sexdiff + "self.");

				}
			}
			else
				target.AddEvent("Due to poor morale, " + target.getCallsignOrFirstname() + " thought about self-termination.\n");
		}
	}

	private void CheckMoodAfterDefeat()
	{
		if (CheckTrait("coward") && ((Random.Range(0, 100)) < 70))
		{
			target.AddEvent(" It was horrific.\n");
			target.ChangeMorale(-30);
		}
		else if (CheckTrait("depressed")&& ((Random.Range(0, 100)) < 60))		//being depressed sucks royally
		{
			target.AddEvent(" It was crumbling!\n");
			target.ChangeMorale(-40);
		}
		else if ((Random.Range(0, 100)) < 50)
		{
			target.AddEvent(" It felt shitty.\n");
			target.ChangeMorale(-20);
		}
		else
		{
			target.AddEvent("\n");
		}

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

			if (AwardName != "Campaing Medal")
			{}
			else if  (AwardName !="Wound Badge")
			{}
			else 
			{
				int Roll = Random.Range(0, 100) + CheckTrait("depressed",20);

				if (Roll < 33)
				{
					target.AddEvent("It felt great!\n");
					target.ChangeMorale(10);
				}
				else if (Roll < 66)
				{
					target.AddEvent(target.soldierFName + " did not care!\n");
				}
				else
				{
					target.AddEvent("It was not worth all the losses.\n");
					target.ChangeMorale(-20);
				}
			}

		}

	}
		


}
