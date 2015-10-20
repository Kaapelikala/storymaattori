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
				idler.AddEvent(" Recovered from wounds.\n");
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
					idler.AddEvent(" Wounds still hurt.\n");
			}
			

			
		}
		

		int MajorStuffHappening = Random.Range(0, 100);

		//modifiers?

		if (MajorStuffHappening > 50)
		//if (true)
		{
			// Big parties, drunkness, shooting competition, stupid shit!



			
			//Example: PARTY!!

			if (true)	// true for now!
			{
				int HowItWent = 0; // How cool the party was actually?

				foreach (SoldierController idler in idlers)
				{
					if (idler.alive == true)
					{
						idler.AddEvent("Partytime!\n");
						idler.AddHistory("-PARTY-:" + campaing.TimeStamp);
					}
				}
				/*
				foreach (SoldierController idler in idlers)
				{
					int HowMuchIsDoing = 0;
					// FOOD
					while (HowMuchIsDoing < ((Random.Range(0,2))+CheckTrait("heroic",1,idler)));
					{

						int DoingsRoll = (Mathf.RoundToInt(Random.Range(0, 3)));
						HowMuchIsDoing++;	

						switch (DoingsRoll)
						{
						case 0:
							if ((Random.Range(0,10) + CheckTrait("cook",3,idler)) > 5)
							{
								HowMuchIsDoing++;	
								HowItWent += this.cook(idler);
							}
							break;
						case 1:
							if ((Random.Range(0,10) + CheckTrait("drunkard",2,idler)) > 4)
							{
								HowMuchIsDoing++;	
								HowItWent += this.drinks(idler);
							}
							break;
						default:
							HowMuchIsDoing++;	
							HowItWent += this.arrange(idler);
							break;
						}
					}
				}
				*/

				int HowArrangementsWent = HowItWent;

				foreach (SoldierController idler in idlers)
				{if (idler.alive == true)
					{
						HowItWent += ActualParty(idler, HowArrangementsWent);
					}
				}

				//something more still? Meh for now!
			}

		}
		else
			MinorActionCheckForIdlers();

	}








	private int ActualParty(SoldierController partier, int Modifier)
	{
		int HowFun = 0;

		if (Random.Range(0,10) + CheckTrait("drunkard",2,partier) > 6)
		{
			wasted(partier);
			HowFun += Random.Range(-4,16);
		}
		if (Random.Range(0,10) + CheckTrait("drunkard",-2,partier) + CheckTrait("idiot",-10,partier) + CheckHistory(("-DRUNK-:"+ campaing.TimeStamp), -4, partier) > 7)
		{
			talkWithOtherSoldier(partier);
			HowFun += Random.Range(2,12);
		}

		foreach (SoldierController OtherPartier in idlers)
		{
			if (OtherPartier != partier)
				HowFun += partier.CompareHistory(OtherPartier);	// Who else is there?
		}

		int PartyRandomiser = Random.Range(0, 3);

		int Partylike = Random.Range(-5,15) + Random.Range(-5,15) + Modifier + HowFun;

		if (Partylike < -10)		//Particular distaste
		{
			switch (PartyRandomiser)
			{
			case 0:
				partier.AddEvent(" Party was shitty as hell\n");	
				partier.ChangeMorale(-20);
				break;
			case 1:
				partier.AddEvent(" Party was a bust.\n");	
				partier.ChangeMorale(-20);
				break;
			case 2:
				partier.AddEvent(" Party was sad effort.\n");	
				partier.ChangeMorale(-20);
				break;
			default:
				partier.AddEvent(" Party could have been much better.\n");	
				partier.ChangeMorale(-20);
				break;
			}
		}
		else if (Partylike < 10)		//Quite unknown
		{
			switch (PartyRandomiser)
			{
			case 0:
				partier.AddEvent(" Party was below average.\n");	
				partier.ChangeMorale(5);
				break;
			case 1:
				partier.AddEvent(" Party was okayish.\n");	
				partier.ChangeMorale(5);
				break;
			case 2:
				partier.AddEvent(" Party was boring.\n");	
				partier.ChangeMorale(5);
				break;
			default:
				partier.AddEvent(" Party could have been better.\n");	
				partier.ChangeMorale(5);
				break;
			}
		}
		else if (Partylike < 30)		//Comrade
		{
			
			switch (PartyRandomiser)
			{
			case 0:
				partier.AddEvent(" Party was coool!\n");	
				partier.ChangeMorale(15);
				partier.AddHistory("-PARTY-:" + campaing.TimeStamp);
				break;
			case 1:
				partier.AddEvent(" Party was fun!\n");	
				partier.ChangeMorale(15);
				partier.AddHistory("-PARTY-:" + campaing.TimeStamp);
				break;
			case 2:
				partier.AddEvent(" Party was easy!\n");	
				partier.ChangeMorale(15);
				partier.AddHistory("-PARTY-:" + campaing.TimeStamp);
				break;
			default:
				partier.AddEvent(" Party went for eternity!\n");	
				partier.ChangeMorale(15);
				partier.AddHistory("-PARTY-:" + campaing.TimeStamp);
				break;
			}
			
			
		}
		else if (Partylike < 50)		//friend
		{
			
			switch (PartyRandomiser)
			{
			case 0:
				partier.AddEvent(" Party was the best!\n");	
				partier.ChangeMorale(20);
				partier.AddHistory("-PARTY-:" + campaing.TimeStamp);
				break;
			case 1:
				partier.AddEvent(" Party was one to remember!");	
				partier.ChangeMorale(20);
				partier.AddHistory("-PARTY-:" + campaing.TimeStamp);
				break;
			case 2:
				partier.AddEvent(" Party was hellisly good!");	
				partier.ChangeMorale(20);
				partier.AddHistory("-PARTY-:" + campaing.TimeStamp);
				break;
			default:
				partier.AddEvent(" This party will not be forgotten!");	
				partier.ChangeMorale(20);
				partier.AddHistory("-PARTY-:" + campaing.TimeStamp);
				break;
			}
			
			
			
		}
		
		HowFun += Partylike;
		
		
		return HowFun;
	}
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	
	/// <summary>
	/// Each idler does something personal
	/// </summary>
	private void MinorActionCheckForIdlers()
	{//Trait based activites, MURDER 
		
		foreach (SoldierController idler in idlers)
		{

			if (idler.alive == false)		//nothing happens if you are dead!
			{}
			else
			{

				//idler.AddEvent("\nTS:" + campaing.TimeStamp + ":\n");

				int DoingsRoll = (Mathf.RoundToInt(Random.Range(0, 10)));

				bool DidSomething = false;

				switch (DoingsRoll)
				{
				case 0:
					string[] deco= new string[] 
					{
						"killmarks",
						"pinups",
						"checker pattern",
						"flames",
						"skulls",
						"delicate paintings",
						"flowers",
						"guns",
						"names of the fallen",
						"propaganda"
					};
					
					string decoInsert = deco[(Mathf.RoundToInt(Random.value*(deco.GetLength(0)-1)))];

					if (idler.HasHistory("-ARMO_DECO-"))
					    idler.AddEvent(" Continued with decorating armor!");
					else
					    idler.AddEvent(" Decorated armor with "+decoInsert+" !\n");

					idler.AddHistory("-ARMO_DECO-");
					
					idler.ChangeMorale(Random.Range(2,8));
					DidSomething = true;
					break;
				case 1:

					string[] foods= new string[] 
					{
						"berliner",
						"beef",
						"cesar salad",
						"meal",
						"breakfast",
						"eleventies",
						"bag of star-popcorn",
						"cookie",
						"warm soup"
					};

					string foodInsert = foods[(Mathf.RoundToInt(Random.value*(foods.GetLength(0)-1)))];

					idler.AddEvent(" Ate a truly delicious "+ foodInsert +"!\n");
					idler.ChangeMorale(10);
					DidSomething = true;
					break;
				case 2:
					this.talkWithOtherSoldier(idler);
					DidSomething = true;
					break;
				case 3:

					string[] relax= new string[] 
					{
						"relaxed",
						"took it easy",
						"had sauna",
						"slept a lot",
						"overslept",
						"took a hike",
						"bingewatched soap operas",
						"did not care",
						"ignored everything"
					};

					string relaxInsert = relax[(Mathf.RoundToInt(Random.value*(relax.GetLength(0)-1)))];

					idler.AddEvent("Just "+relaxInsert+"!\n");
					idler.ChangeMorale(15);
					idler.ChangeHealth(5);
					DidSomething = true;
					break;
				case 4:

					string[] reading= new string[] 
					{
						"newspaper",
						"news",
						"book",
						"weapons manual",
						"letter from home",
						"letter from friend",
						"instruction manual",
						"comic",
						"graffitti"
					};
					
					string readingInsert = reading[(Mathf.RoundToInt(Random.value*(reading.GetLength(0)-1)))];

					idler.AddEvent(" Read a "+readingInsert+".\n");
					idler.ChangeMorale(5);
					DidSomething = true;
					break;
				case 5:
					idler.AddEvent(" Listened to a propaganda broadcast,\n");
					if (Random.Range(0, 10) + CheckTrait("heroic",2, idler) > 5 )
					{
						idler.AddEvent("  It was inspiring!\n");
						idler.ChangeMorale(15);
					}
					else
					{
						idler.AddEvent("  It was boring..\n");
						idler.ChangeMorale(-10);
					}
					DidSomething = true;
					break;
				case 6:
					this.VisitBurialGround(idler);
					

					DidSomething = true;
					break;
				case 7:
					this.wasted(idler);
					DidSomething = true;
					break;
				case 8:
					idler.AddEvent(" Trained!\n");
					if(Random.Range(0,100) > idler.skill)
						idler.skill++;
					DidSomething = true;
					break;
				case 9:
					string[] rememberings= new string[] 
					{
						"past",
						"future",
						"home",
						"fallen fiends",
						"the war",
						"battles",
						"training",
						"food",
						"vodka",
						"creepy robots",
						"guns",
						"flowers",
						"butterflies"
					};
					
					string rememberingsInsert = rememberings[(Mathf.RoundToInt(Random.value*(rememberings.GetLength(0)-1)))];
					idler.AddEvent(" Thought about "+rememberingsInsert+".\n");
					idler.ChangeMorale(Random.Range(-4,12));
					DidSomething = true;
					break;
				default:


					break;
				}
				if (DidSomething == false)
					idler.AddEvent(" Nothing special happened.\n");
				

			}
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
								if (eater != idler && eater.alive == true)
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
								if (eater != idler&& eater.alive == true && eater.alive == true)
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
									if (eater != idler && eater.alive == true)
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

		string[] alcohols= new string[] 
		{
			"star-beer",
			"brandy",
			"red wine",
			"white wine",
			"blue wine",
			"vodka",
			"sider",
			"trad-beer",
			"whisky",
			"booze"
		};

		string alcoholType = alcohols[(Mathf.RoundToInt(Random.value*(alcohols.GetLength(0)-1)))];


		idler.AddEvent(" Brought " + alcoholType+"!\n");
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
			if (drinker != idler && drinker.alive == true)
			{
				if (ThereIsSomethingLeft == false)
				{
					drinker.AddEvent(" " + idler.getCallsignOrFirstname() + " began drinking beforehand!\n");
					HowItWent += -15;
				}
				else if (Random.Range(0,10)+CheckTrait("drunkard",3,drinker) > 5)
				{
					drinker.AddEvent(" " + idler.getCallsignOrFirstname() + " brought wonderful amount of "+alcoholType+"!\n");
					drinker.AddHistory("-DRINK-:"+ campaing.TimeStamp);
					HowItWent += 10;
				}
				else if (Random.Range(0,10) > 5)
					drinker.AddEvent(" " + idler.getCallsignOrFirstname()+ " brought "+alcoholType+".\n");
			}
		}

		return HowItWent;

	}

	private int arrange(SoldierController idler)
	{
		int HowItWent = 0;
		
		string[] arrangements= new string[] 
		{
			"decorations",
			"drinking songs",
			"songlist",
			"arrangements",
			"invitations",
			"pantomime",
			"comedy",
			"parody",
			"guitar-solo"
		};
		
		string WhatDid = arrangements[(Mathf.RoundToInt(Random.value*(arrangements.GetLength(0)-1)))];
		
		
		idler.AddEvent(" Did " + WhatDid+"!\n");
		idler.AddHistory("-ARRANG-:"+ campaing.TimeStamp);
		
		bool failure = false;
		
		if (idler.HasAttribute("idiot") && (Random.Range(0,10) > 5))
		{
			idler.AddEvent(" It went little overboard...\n");
			failure = true;
		}
		
		foreach (SoldierController appreciator in idlers)
		{
			if (appreciator != idler && appreciator.alive == true)
			{
				if (failure == true)
				{
					appreciator.AddEvent(" " + idler.getCallsignOrFirstname() + "s " +WhatDid+ " was horrible!\n");
					HowItWent += -10;
				}
				else if (Random.Range(0,10)+CheckTrait("young",2,appreciator)+CheckTrait("heroic",1,idler)+CheckTrait("veteran",1,idler) > 5)
				{
					appreciator.AddEvent(" " + idler.getCallsignOrFirstname() + " did really radical "+WhatDid+"!\n");
					appreciator.AddHistory("-ARRANG-:"+ campaing.TimeStamp);
					HowItWent += 5;
				}
				else if (Random.Range(0,10) > 5)
					appreciator.AddEvent(" " + idler.getCallsignOrFirstname()+ " did "+WhatDid+".\n");
			}
		}
		
		return HowItWent;
		
	}

	private void talkWithOtherSoldier(SoldierController idler)
	{		// If other is idiot no much converstation?
		string[] talksubject= new string[] 
		{
			"star-beer",
			"weather",
			"home",
			"the goverment",
			"space",
			"vodka",
			"enemies",
			"homebase",
			"other soldiers",
			"food",
			"boys",
			"girls",
			"parties",
			"families",
			"dead friends",
			"bad rations",
			"the " + campaing.EnemyName,
			"the " + campaing.FriendName
		};

		string talksubjectInject = talksubject[(Mathf.RoundToInt(Random.value*(talksubject.GetLength(0)-1)))];

		SoldierController otherToTalk = null;
			
		int attempts = Mathf.FloorToInt((idlers.Count-1)/2);

		while (otherToTalk == null && attempts > 0)
		{
			attempts --;

			int SoldierRandomiser = Mathf.FloorToInt(Random.Range(0,(idlers.Count-1)));

			if (idlers[SoldierRandomiser] != idler)
			{
				otherToTalk = idlers[SoldierRandomiser];

			}
		}

		if (attempts <= 0 && otherToTalk == null)
		{
			idler.ChangeMorale((Random.Range(-20,10)+CheckTrait("loner", 5, idler)));           
		}
		else
			                   {
			int HowWent = idler.CompareHistory(otherToTalk) + otherToTalk.CompareHistory(idler) + Random.Range(-5,+10);

			idler.AddEvent(" Talked with " + otherToTalk.getCallsignOrFirstname() +" about " +talksubjectInject+ ".\n");
			otherToTalk.AddEvent(" Talked with " + idler.getCallsignOrFirstname() +" about " +talksubjectInject+ ".\n");

			if (HowWent < -10)
			{
				idler.AddEvent("  We did not agree.\n");
				otherToTalk.AddEvent("  We did not agree.\n");
				idler.ChangeMorale(-20);
				otherToTalk.ChangeMorale(-20);
			}
			else if (HowWent < 0)
			{
				idler.AddEvent("  Our views were not the same.\n");
				otherToTalk.AddEvent("  Our views were not the same.\n");
			}
			else if (HowWent < 10)
			{
				idler.AddEvent("  The discussion was okay.\n");
				otherToTalk.AddEvent("  The discussion was okay.\n");
				idler.ChangeMorale(10);
				otherToTalk.ChangeMorale(10);
			}
			else if (HowWent <= 20)
			{
				idler.AddEvent("  It was fun to chat with "+ otherToTalk.getCallsignOrFirstname() +"!\n");
				otherToTalk.AddEvent("  It was fun to chat with "+ idler.getCallsignOrFirstname() +"!\n");
				idler.AddHistory("-TALK-:"+idler.soldierID+otherToTalk.soldierID);
				otherToTalk.AddHistory("-TALK-:"+idler.soldierID+otherToTalk.soldierID);
				idler.ChangeMorale(20);
				otherToTalk.ChangeMorale(20);
			}
			else if (HowWent <= 30)
			{
				idler.AddEvent("  Time flew by!\n");
				otherToTalk.AddEvent("  Time flew by!\n");
				idler.AddHistory("-TALK-:"+idler.soldierID+otherToTalk.soldierID);
				otherToTalk.AddHistory("-TALK-:"+idler.soldierID+otherToTalk.soldierID);
				idler.ChangeMorale(30);
				otherToTalk.ChangeMorale(30);

			}
			}


	}
	/// <summary>
	/// Visits burialground.
	/// </summary>
	/// <param name="visitor">Soldier who goes there. NEEDS to have been at someones funeral!!.</param>
	public void VisitBurialGround(SoldierController visitor)
	{
		bool VisitedSomeone = false;

		if (campaing.Soldiers.dead.Count > 0)	// so no nullpoint errors 
		{
			foreach (SoldierController deadComrade in campaing.Soldiers.dead)
			{
				if (visitor.HasHistory("-CARE-:"+deadComrade.soldierID))
				{
					if (visitor.CompareHistory(deadComrade)>Random.Range(-50,20))
					{
						if (deadComrade.HasHistory("-RECOVERED-"))
						{ 
							VisitedSomeone = true;
							visitor.AddEvent(" Visited the grave of "+ deadComrade.FullName() +".\n");
							this.regrieve(visitor, deadComrade);
						}
						else
						{ 
							VisitedSomeone = true;
							visitor.AddEvent(" Went to the memorial of "+ deadComrade.FullName() +".\n");
							this.regrieve(visitor, deadComrade);
						}
					}
				}
			}
			
			
			


		}
		if (VisitedSomeone == false)
		{
			visitor.AddEvent(" Wandered at the burial ground.\n");
			visitor.ChangeMorale(Random.Range(-4,12));
		}
		
	}
	
	
	public void regrieve(SoldierController visitor,SoldierController fallenfriend)
	{
		int Liking = visitor.CompareHistory(fallenfriend);

		string Sexdiff = "";
		
		if (fallenfriend.sex == 'm')
		{
			Sexdiff = "He";
		}
		else
			Sexdiff = "She";

		if (Liking < -30)
		{
			visitor.AddEvent(" " + Sexdiff + " was horrible person.\n");
			visitor.ChangeMorale(30);
		}
		else if(Liking < -10)
		{
			visitor.AddEvent(" " + Sexdiff + " had been constant thorn.\n");
			visitor.ChangeMorale(20);
		}
		else if(Liking < 10)
		{
			visitor.AddEvent(" " + Sexdiff + " were a comrade.\n");
			visitor.ChangeMorale(10);
		}
		else if(Liking < 20)
		{
			visitor.AddEvent(" " + Sexdiff + " had been a friend.\n");
			visitor.ChangeMorale(10);
		}
		else if(Liking < 20)
		{
			visitor.AddEvent(" " + Sexdiff + " had been true companion!\n");
			visitor.ChangeMorale(20);
		}
		
		
	}

	/// <summary>
	/// DRINKING between two!
	/// </summary>
	/// <param name="idler">Idler.</param>
	private void wasted(SoldierController idler)
	{		
		SoldierController otherDrinker = null;

		int attempts = Mathf.FloorToInt((idlers.Count-1)/2);

		while (otherDrinker == null && attempts > 0)
		{
			attempts --;
			int SoldierRandomiser = Mathf.FloorToInt(Random.Range(0,(idlers.Count-1)));
						
			if ((idlers[SoldierRandomiser] != idler))
			{
				otherDrinker = idlers[SoldierRandomiser];
				
			}
		}

		string[] drunklevel= new string[] 
		{
			"totally ",
			"madly ",
			"crazily ",
			"over the top ",
			"completely ",
			"annihilatingly ",
			""
		};

		string drunklevelInject = drunklevel[(Mathf.RoundToInt(Random.value*(drunklevel.GetLength(0)-1)))];

		if (attempts <= 0 && otherDrinker == null)
		{
			idler.AddEvent(" Got "+drunklevelInject+"wasted alone!\n");
			idler.ChangeMorale((Random.Range(-20,10)+CheckTrait("drunkard", 5, idler)));

		}
		else
		{

			


			int HowWent = 
				idler.CompareHistory(otherDrinker) 
					+ otherDrinker.CompareHistory(idler) 
					+ Random.Range(-5,+10)
					+ CheckTrait("drunkard", 5, idler)
					+ CheckTrait("drunkard", 5, otherDrinker)
				;
			
			idler.AddEvent(" Got "+drunklevelInject+"wasted with " + otherDrinker.getCallsignOrFirstname()+"!\n");
			otherDrinker.AddEvent(" Got "+drunklevelInject+"wasted with " + idler.getCallsignOrFirstname()+"!\n");
			
			if (HowWent < -10)
			{
				idler.AddEvent("  It went a bit over the hill...\n");
				otherDrinker.AddEvent("  It went a bit over the hill...\n");
				idler.ChangeMorale(-10);
				otherDrinker.ChangeMorale(-10);

				idler.ChangeHealth(-30);
				otherDrinker.ChangeHealth(-30);

				if (idler.health < 0)
				{
					idler.die("Liver destruction");
					idler.AddEvent("  Liver gave up!\n");
					otherDrinker.AddEvent("  " + idler.getCallsignOrFirstname() + "s liver was destroyed!\n");
					otherDrinker.ChangeMorale(-30);
				}
				if (otherDrinker.health < 0)
				{
					otherDrinker.die("Liver destruction");
					otherDrinker.AddEvent("  Liver gave up!\n");
					idler.AddEvent("  " + otherDrinker.getCallsignOrFirstname() + "s liver was destroyed!\n");
					idler.ChangeMorale(-30);
				}
			}
			else if (HowWent < 0)
			{
				idler.AddEvent("  It wasn't that fun.\n");
				otherDrinker.AddEvent("  It wasn't that fun.\n");
			}
			else if (HowWent < 10)
			{
				idler.AddEvent("  Jolly time!\n");
				otherDrinker.AddEvent("  Jolly time!\n");
				idler.ChangeMorale(10);
				otherDrinker.ChangeMorale(10);
			}
			else if (HowWent <= 20)
			{
				idler.AddEvent("  Hangover was worth it!\n");
				otherDrinker.AddEvent("  Hangover was worth it!\n");
				idler.AddHistory("-WASTED-:"+idler.soldierID+otherDrinker.soldierID);
				otherDrinker.AddHistory("-WASTED-:"+idler.soldierID+otherDrinker.soldierID);
				idler.ChangeMorale(20);
				otherDrinker.ChangeMorale(20);
			}
			else if (HowWent <= 30)
			{
				idler.AddEvent("  That was SO great!!\n");
				otherDrinker.AddEvent("  That was SO great!!\n");
				idler.AddHistory("-WASTED-:"+idler.soldierID+otherDrinker.soldierID);
				otherDrinker.AddHistory("-WASTED-:"+idler.soldierID+otherDrinker.soldierID);
				idler.ChangeMorale(30);
				otherDrinker.ChangeMorale(30);
				
			}
		
		}
		
	}
	
	private int CheckTrait(string TraitName, int modifier, SoldierController target)
	{
		if (target.HasAttribute(TraitName))
		{
			return modifier;
		}
		return 0;
				
	}
	private int CheckHistory(string TraitName, int modifier, SoldierController target)
	{
		if (target.HasHistory(TraitName))
		{
			return modifier;
		}
		return 0;
		
	}
}
