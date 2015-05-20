using UnityEngine;
using System.Collections;

public class Event_Debrief : MonoBehaviour {


	public SoldierController target;
	public string MissionName = "";
		
			

	public void Handle (SoldierController NEWTARGET){
			
		this.target = NEWTARGET;

		target.AddEvent("Debriefing: \n");

		if (CheckTrait("wounded")){

			if (CheckTrait("tough"))		//being tough helps a lot!
			    target.ChangeHealth(10);

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
						result += " but " + target.callsign + " began to drink too much.\n";
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
					target.AddEvent(target.callsign + "Would have hoped better\n");
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
					target.AddEvent(target.callsign + "It sucked royally.\n");
					target.ChangeMorale(-10);
				}
			}


		}

		if (target.kills > 4 && target.GetRank() == "Recruit")
		{}

		
			
//
//		    if (rank == 0)
//		    return "Recruit";
//		    
//		    if (rank == 1)
//		    return "Trooper";
//		    
//		    if (rank == 2)
//		    return "Corporal";
//		    
//		    if (rank == 3)
//		    return "Sergeant";
//		    
//		    if (rank == 4)
//		    return "Liutenant";

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
