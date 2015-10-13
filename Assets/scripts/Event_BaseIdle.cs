using UnityEngine;
using System.Collections.Generic;

public class Event_BaseIdle : MonoBehaviour {

	public List<SoldierController> idlers;
	public Campaing campaing;

	public int Difficulty;
	public int GreatestRank;

	public Event_BaseIdle(Campaing CampaingInsert)
	{
		this.campaing = CampaingInsert;
	}

	public void Handle(List<SoldierController> IdlerINSERT)
	{
		this.idlers = IdlerINSERT;

		int MajorStuffHappening = Random.Range(0, 100);

		//modifiers?

		//if (MajorStuffHappening > 50)
		if (true)
		{
			// Big parties, drunkness, shooting competition, stupid shit!


			foreach (SoldierController idler in idlers)
			{
				idler.ChangeHealth(5);
				idler.ChangeMorale(5);
				
				idler.AddEvent("\nTS:" + campaing.TimeStamp + ":\n");

				if ((idler.HasAttribute("wounded")) && (Random.Range(0,100) < (idler.health-20+CheckTrait("techie", 20, idler))))
				{
					Event_Debrief RoboHospital = new Event_Debrief();

					RoboHospital.target = idler;
					RoboHospital.CheckForBionics();
					idler.RemoveAttribute("wounded");

				}
				else if ((idler.HasAttribute("wounded")) && (Random.Range(0,100) < (idler.health+20)))		//Healing!!
				{
					idler.AddEvent("Recovered from wounds.\n");
					idler.RemoveAttribute("wounded");
				}
				else if (idler.HasAttribute("wounded"))
				{
					idler.ChangeHealth(-80);	// total of -5 then!
					if (idler.health <= 0)
					{
						idler.AddEvent("Died of infected wound!");
						idler.die("Infected wound");
					}
					else
						idler.AddEvent("Wounds still hurt.\n");
				}



			}
			
			//Example: PARTY!!

			if (true)	// true for now!
			{
				int HowItWent = 0; // How cool the party was actually?

				foreach (SoldierController idler in idlers)
				{
					idler.AddEvent("Partytime!\n");
					idler.AddHistory("-PARTY-:" + campaing.TimeStamp);
				}

				foreach (SoldierController idler in idlers)
				{
					int HowMuchIsDoing = 0;
					// FOOD
					//if (HowMuchIsDoing < ((Random.Range(0,3))+CheckTrait("heroic",1,idler)));

						int DoingsRoll = (Mathf.RoundToInt(Random.Range(0, 4)));
						HowMuchIsDoing++;

						switch (DoingsRoll)
						{
						case 0:
							if ((Random.Range(0,10) + CheckTrait("cook",3,idler)) > 5)
								HowItWent += this.cook(idler);
							break;
						case 1:
							this.drinks(idler);
							break;
						default:
							//this.arrange(idler);
							break;
						}
				}
			}

		}
		else
			MinorActionCheckForIdlers();

	}

	/// <summary>
	/// Each idler does something personal
	/// </summary>
	private void MinorActionCheckForIdlers()
	{//Trait based activites, MURDER 

		foreach (SoldierController idler in idlers)
		{
		
			idler.AddEvent("\nTS:" + campaing.TimeStamp + ":\n");

			idler.AddEvent("Nothing special happened.\n");

		}
						
	}


	private int cook(SoldierController idler)
	{



							int whatCooksRoll = Random.Range(0,100);
							
							int HowItWent = 0;
							
							if (whatCooksRoll < 33) // grill!
							{
								idler.AddEvent(" Grilled a huge steak!\n");
								idler.AddHistory("-FOOD-:"+ campaing.TimeStamp);
								
								foreach (SoldierController eater in idlers)
								{
									if (eater != idler)
									{
										if (Random.Range(0,10)+CheckTrait("cook",3,idler) > 5)
										{
											eater.AddEvent(" " + idler.getCallsignOrFirstname() + " grilled a delicious steak!\n");
											eater.AddHistory("-FOOD-:"+ campaing.TimeStamp);
											HowItWent += 10;
										}
										else if (Random.Range(0,10) > 5)
											eater.AddEvent(" " + idler.getCallsignOrFirstname()+ " grilled an ok steak!\n");
									}
								}
								
							}
							else if (whatCooksRoll < 66)	// cake!
							{
								idler.AddEvent(" Baked a fluffy cake!\n");
								idler.AddHistory("-FOOD-:"+ campaing.TimeStamp);
								
								foreach (SoldierController eater in idlers)
								{
									if (eater != idler)
									{
										if (Random.Range(0,10)+CheckTrait("cook",3,idler) > 5)
										{
											eater.AddEvent(" "+idler.getCallsignOrFirstname()+" made a sugary cake!\n");
											eater.AddHistory("-FOOD-:"+ campaing.TimeStamp);
											HowItWent += 10;
										}
										else if (Random.Range(0,10) > 5)
											eater.AddEvent(" "+idler.getCallsignOrFirstname()+" baked okay cake!\n");
									}
								}
								
							}
							else // ice-cream
							{
								idler.AddEvent(" Made vatfuls of ice-cream!\n");
								idler.AddHistory("-FOOD-:"+ campaing.TimeStamp);
								
								foreach (SoldierController eater in idlers)
								{
									if (eater != idler)
									{
										if (Random.Range(0,10)+CheckTrait("cook",3,idler) > 5)
										{
											eater.AddEvent(" " + idler.getCallsignOrFirstname() + " made lots of juicy ice-cream!\n");
											eater.AddHistory("-FOOD-:"+ campaing.TimeStamp);										
											HowItWent += 10;
										}
										else if (Random.Range(0,10) > 5)
											eater.AddEvent(" " + idler.getCallsignOrFirstname() + " crafted lots of eatable ice-cream!\n");
										
									}
								}
								
							}
							

		return HowItWent;

	}

	private int drinks(SoldierController idler)
	{
		int HowItWent = 0;

		idler.AddEvent(" Brought refreshments!\n");
		idler.AddHistory("-DRINK-:"+ campaing.TimeStamp);

		bool ThereIsSomethingLeft = true;

		if (idler.HasAttribute("drunkard") && (Random.Range(0,10) > 5))
		{
			idler.AddHistory("-DRUNK-:"+ campaing.TimeStamp);
			idler.AddEvent(" Might have tasted a bit beforehand...\n");
			ThereIsSomethingLeft = false;
		}

		foreach (SoldierController drinker in idlers)
		{
			if (drinker != idler)
			{
				if (ThereIsSomethingLeft == false)
				{
					drinker.AddEvent(" " + idler.getCallsignOrFirstname() + " began drinking beforehand!\n");
					HowItWent += -15;
				}
				else if (Random.Range(0,10)+CheckTrait("drunkard",3,drinker) > 5)
				{
					drinker.AddEvent(" " + idler.getCallsignOrFirstname() + " brought wonderful amount of booze!\n");
					drinker.AddHistory("-DRINK-:"+ campaing.TimeStamp);
					HowItWent += 10;
				}
				else if (Random.Range(0,10) > 5)
					drinker.AddEvent(" " + idler.getCallsignOrFirstname()+ " brought drinks.\n");
			}
		}

		return HowItWent;

	}

	private int CheckTrait(string TraitName, int modifier, SoldierController target)
	{
		if (target.HasAttribute(TraitName))
		{
			return modifier;
		}
		return 0;
				
	}
}
