using UnityEngine;
using System.Collections;

public class Event_Debrief : MonoBehaviour {


	public SoldierController target;
	public string MissionName = "";
		
	public string CookName = "";
			

	public void Handle (SoldierController NEWTARGET){
			
		this.target = NEWTARGET;

		target.AddEvent("Debriefing: \n");

		if (CheckTrait("tough"))		//being tough helps a lot!
			target.ChangeHealth(10);
		if (CheckTrait("heroic"))		//being tough helps a lot!
			target.ChangeMorale(10);
		if (CheckTrait("coward"))		//being tough helps a lot!
			target.ChangeMorale(-10);


		//HANDLING OF WOUNDEDS!
		if (CheckTrait("wounded")){

			int Roll = Random.Range(0, 100);

			if (Roll < 50)			//NICE EXTENDED REST, did Character enjoy it?
			{
				target.AddEvent("Was sent to an extended rest.\n");
				target.RemoveAttribute("wounded");		//Wound goes away!
				target.ChangeHealth(20);

				int SecRoll = Random.Range(0, 100);

				if (CheckTrait("drunkard"))
				{
					target.AddEvent("Heavy drinking commerced.\n");
					target.ChangeMorale(40);
					target.ChangeHealth(-10);
				}
				else if (SecRoll < 50)
				{

					string result ="";
					result += "It was enjoyable";
					target.ChangeMorale(30);

					int ThirdRoll = Random.Range(0, 5);		//Chance for alcoholism
					if (ThirdRoll >4)
					{
						result += " but " + target.callsign + " began to drink too much!\n";
						target.AddAttribute("drunkard");
					}
					else
						result += ".\n";

					target.AddEvent(result);

				}
				else
				{
					target.AddEvent("It was boring\n");
					target.ChangeMorale(10);
				}



			}
			else if (Roll < 75)			// NORMAL REST
			{
				target.AddEvent("Was given basic medical treatment.\n");
				target.RemoveAttribute("wounded");		//Wound goes away!
				target.ChangeHealth(20);

				int SecRoll = Random.Range(0, 100);

				if (SecRoll < 50)
				{
					target.AddEvent("It was OK.\n");
					target.ChangeMorale(20);
										
				}
				else
				{
					target.AddEvent(target.callsign + " would have hoped better\n");
					target.ChangeMorale(5);
				}
			}
			else if (Roll < 90)			// NO REST
			{
				target.AddEvent("There was no time for medical help.\n");
				target.ChangeHealth(10);

				int SecRoll = Random.Range(0, 100);
				
				if (SecRoll < 25)
				{
					target.AddEvent(target.callsign + " 'found' a medkit anyway\n");
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
					target.ChangeMorale(-10);
				}
			}


		}		
		else
		{
			target.AddEvent("Did not need medical help.\n");
		}


		//PROMOTIONS
		if (this.target.kills > 4 && target.rank == 0)			//TROOPER
		{
			if (Random.Range(0, 100) > 70)
			{
				Promote(this.target);
				target.ChangeMorale(10);
			}
			else
			{
				target.AddEvent("Did not get deserved promotion!\n");
				target.ChangeMorale(-20);
			}
		}
		else if (this.target.kills > 10 && target.rank == 1)		//CORP
		{
			if (Random.Range(0, 100) > 70)
			{
				Promote(this.target);
				target.ChangeMorale(20);
			}
			else
			{
				target.AddEvent("Did not get deserved promotion!\n");
				target.ChangeMorale(-40);
			}
		}
		else if (this.target.kills > 20 && target.rank == 2)		//CAPT
		{
			if (Random.Range(0, 100) > 70)
			{
				Promote(this.target);
				target.ChangeMorale(30);
			}
			else
			{
				target.AddEvent("Did not get deserved promotion!\n");
				target.ChangeMorale(-60);
			}
		}
		else if (this.target.kills > 40 && target.rank == 3)		//LIUTENANT
		{
			if (Random.Range(0, 100) > 70)
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

		//CHANCE TO BANG HIMSELF due to POOR MORALE
		if (this.target.morale < 0){		

			if (Random.Range(0, 100) > 70)
			{
				string Sexdiff = "";
				if (this.target.sex == 'm')
				{
					Sexdiff = "him";
				}
				else 
				{
					Sexdiff = "her";
				}
					if (CheckTrait("inaccurate"))
					    {
						    this.target.AddEvent("Due to poor morale, " + target.callsign + "tried to shot " + Sexdiff + "self but missed!\n");
					    }
				else
				{
					target.AddEvent("Due to poor morale, " + target.callsign + "shot " + Sexdiff + "self!\n");
					target.die("Killed " + Sexdiff + "self.");
				}
			}
			else
				target.AddEvent("Due to poor morale, " + target.callsign + "thought about self-termination.\n");
		}


		// CHANCE FOR NEW TRAITS
		if (Random.Range(0, 100) > 70){


			int traitRandomiser = Random.Range(0, 2);
		
			switch (traitRandomiser)
			{
			case 0:
			{
				if (CheckTrait("coward")){
					break;
				}
				target.AddEvent(target.callsign + " found new resolve!\n" );

				this.target.AddAttribute("heroic");
				break;
				}
			case 1:
				{

				target.AddEvent(target.callsign + " does not care about scratches anymore!\n" );
				this.target.AddAttribute("tough");
				break;
				}
			case 2:
				{
				if (CheckTrait("heroic")) {
					break;
				}
				target.AddEvent(target.callsign + " began fear the dark!\n" );
				this.target.AddAttribute("coward");
				break;
				}
			}

		}

		if (CookName != ""){
			this.target.ChangeMorale(10);
			this.target.AddEvent("Cooking of " + CookName + "was splendid!\n"); 
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
		


}
