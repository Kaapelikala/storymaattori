using UnityEngine;
using System.Collections.Generic;


public class Event_PatrolNoEnemies : MonoBehaviour {

	public List<SoldierController> squad;


	string [] Locations = new string[] {
		"Location",
		"Place",
		"Target area",
		"Waypoint",
		"Point",
		"Site",
		"Area",
		"Zone",
		"Position"
	};

	string [] huhs = new string[] {
		"Woah!",
		"Huh!",
		"Hah!",
		"Lucky!",
		"Great!",
		"Yes!",
		"Wuhuu!",
		"",
		"",
		"",
		"",
		""
	};


	/// <summary>
	/// Single EMPTY Waypoint: what there is and what people do. NONLETHAL for now.
	/// </summary>
	/// <param name="squad_inject">To what squad do fun stuff!.</param>
	public void Handle(List<SoldierController> squad_inject)
	{
		this.squad = squad_inject;

		int locationRandomiser = Mathf.RoundToInt(Random.Range (0, 7));

		switch(locationRandomiser)
		{

		case 0:		//potential danger
			this.happening_suspicion();
			break;
	/*
		case 1:		//fun!
			this.happening_treasure();
			break;

	*/
		case 2:		//uh oh!
			this.happening_bomb();
			break; 

		default:	//HUH!
			this.happening_clear();
			break;
		}


	}

	private void happening_clear()
	{
		string [] clears = new string[] {
			"clear",
			"empty",
			"empty of hostiles",
			"deserted",
			"without enemies",
			"without trouble",
			"not a problem at all",
			"easy",
			"empty but eerie",
			"clear yet quiet"
		};


		string LocationsInsert = Locations[(Mathf.RoundToInt(Random.value*(Locations.GetLength(0)-1)))];


		foreach (SoldierController solttu in squad)
		{			
			string clearInsert = clears[(Mathf.RoundToInt(Random.value*(clears.GetLength(0)-1)))];
			string huhInsert = huhs[(Mathf.RoundToInt(Random.value*(huhs.GetLength(0)-1)))];
			
			string ClearReturn = "  " +  LocationsInsert + " was " + clearInsert + ". " + huhInsert + "\n";

			solttu.AddEvent(ClearReturn);
			solttu.ChangeMorale(Random.Range(3,10));

			if (this.CheckTrait("coward",solttu))	//cowards like non-action even more!
				solttu.ChangeMorale(Random.Range(1,10));
			
		}

	}

	private void happening_suspicion()
	{

		int BombChance = Random.Range (1, 100);		// chance that there is something FUN


		string [] suspicions = new string[] {
			"suspicious.",
			"a bit wrong.",
			"all too quiet.",
			"disturbed.",
			"fishy.",
			"worriesome",
			"eerie",
			"alarming",
			"annoyingly silent"
		};

		string LocationsInsert = Locations[(Mathf.RoundToInt(Random.value*(Locations.GetLength(0)-1)))];	// location name is same for all!

		foreach (SoldierController solttu in squad)
		{		
			string suspicionsInsert = suspicions[(Mathf.RoundToInt(Random.value*(suspicions.GetLength(0)-1)))];

			string PlaceIsSuspiciousReturn = "  " + LocationsInsert + " felt " + suspicionsInsert + "\n";
			
			solttu.AddEvent(PlaceIsSuspiciousReturn);
			
			solttu.ChangeMorale(Random.Range(-5,0));

		}

		if (Random.Range(0,100) < BombChance ) //DO We actually have something FUN?
		//if (1==1) //DO We actually have something FUN?
		{
			bool BombFound = false;

			foreach (SoldierController solttu in squad)
			{		
				if (solttu.alive == true && (Random.Range(1,solttu.skill) > BombChance ) && BombFound == false ) // DETECT ROLL
				{
					//BOMB FOUND!

					string [] notices = new string[] {
						"Found",
						"Detected",
						"Stumbled upon",
						"Saw",
						"Smelled",
						"Discovered",
						"Recognised",
						"Sensed",
						"Spotted"
					};

					string noticessInsert = notices[(Mathf.RoundToInt(Random.value*(notices.GetLength(0)-1)))];

					solttu.AddEvent("  " +noticessInsert+ " a hidden bomb!\n");
					BombFound = true;
					foreach (SoldierController othersolttu in squad)
					{
						if (othersolttu != solttu && othersolttu.alive == true)
							othersolttu.AddEvent("  " + solttu.getCallsignOrFirstname() + " found a hidden bomb!\n");
					}


					//DEFUSEEE

					int DefuseChance = solttu.skill;

					DefuseChance += this.CheckTrait("idiot",solttu,-40);
					DefuseChance += this.CheckTrait("drunkard",solttu,-10);
					DefuseChance += this.CheckTrait("coward",solttu,-15);
					DefuseChance += this.CheckTrait("newbie",solttu,-20);	
					DefuseChance += this.CheckTrait("wounded",solttu,-20);
					DefuseChance += this.CheckTrait("depressed",solttu,-20);	

					DefuseChance += this.CheckTrait("techie",solttu,50);		//a huge bonus!
					DefuseChance += this.CheckTrait("lucky",solttu,10);	
					DefuseChance += this.CheckTrait("heroic",solttu,5);	
					DefuseChance += this.CheckTrait("veteran",solttu,5);
					DefuseChance += this.CheckTrait("loner",solttu,5);

					DefuseChance += this.CheckTrait("robo-eye",solttu,5);	
					DefuseChance += this.CheckTrait("robo-vision",solttu,10);
					DefuseChance += this.CheckTrait("robo-manipulators",solttu,5);



					int DefuseRoll = Random.Range(1,DefuseChance);

					Debug.Log("Bomb Defuse Check: "+ solttu.FullName() +" BombChance:" + BombChance + "DefuceChance:" + DefuseChance + "Defuseroll:" + DefuseRoll);

					if (DefuseRoll > BombChance)	//Success!
					{

						string huhInsert = huhs[(Mathf.RoundToInt(Random.value*(huhs.GetLength(0)-1)))];

						solttu.AddEvent("  Bomb defused! "+huhInsert+"\n");
						solttu.ChangeMorale(20);
						solttu.AddHistory("-BOMBDEFUSER-");
						//solttu.AddAward("Bomb Defuse Medallion");	// moevd to debrief

						foreach (SoldierController othersolttu in squad)
						{
							if (othersolttu != solttu && othersolttu.alive == true)
							{
								string OtherhuhInsert = huhs[(Mathf.RoundToInt(Random.value*(huhs.GetLength(0)-1)))];

								if(solttu.HasAttribute("techie"))
								{
									othersolttu.AddEvent("  " + solttu.getCallsignOrFirstname() + " defused the bomb expertly!\n");
									othersolttu.ChangeMorale(5);
								}
								else
								   othersolttu.AddEvent("  " + solttu.getCallsignOrFirstname() + " defused the bomb! "+OtherhuhInsert+"\n");
								
								solttu.AddHistory("-BOMBDEFUSE-:"+solttu.soldierID + "-" + othersolttu.soldierID);
								othersolttu.AddHistory("-BOMBDEFUSE-:"+solttu.soldierID + "-" + othersolttu.soldierID);

								othersolttu.ChangeMorale(10);
							}
						}

					}
					else 	//BOOM!
					{
						foreach (SoldierController allsolttu in squad)
						{
							if (allsolttu.alive == true)
							{
								allsolttu.AddEvent("  The bomb exploded!\n");
								allsolttu.ChangeMorale(-10);
							}
						}
						this.boom(solttu);
				
					}
				}


			}
			if (BombFound == false)
			{
				this.happening_bomb();	// if no bomb found there is a chance that someone steps on it!
			}

		}
		else
		{
			
			this.happening_clear();
		}
		
	}

	private void happening_bomb()
	{
		int WhoTheBombHits = Mathf.RoundToInt(Random.Range(0,6));	//chance for it not to hit!


		if (WhoTheBombHits > 3)
		{}
		else if (squad [WhoTheBombHits] == null) 
		{
			this.happening_clear ();
		} 
		else 
		{ // if no-one steps on a bomb the place is clear!!
			SoldierController target = squad [WhoTheBombHits];
			
			if (target.alive == true) {
				
				target.AddEvent ("  Stepped on a bomb!\n");
				foreach (SoldierController solttu in squad) {		
					if (solttu != target)
						solttu.AddEvent ("  " + target.getCallsignOrFirstname () + " stepped on a bomb!\n");
					solttu.ChangeMorale (-20);	//the only difference: peopple are more shocked!
					
				}
				this.boom (target);	// it should be custom but now uses the same!
			}
		}

	}

	private void happening_treasure()
	{
		foreach (SoldierController solttu in squad)
		{
			solttu.AddEvent("LOOT LOOT!\n");	//needs more details!!!
			solttu.ChangeMorale(Random.Range(10,20));
		}
	}

	/// <summary>
	/// Difficult to pass terrrain. chance of wounding
	/// </summary>
	private void happening_dangerousTerrain()
	{
		foreach (SoldierController solttu in squad)
		{			
			solttu.AddEvent("DANGEER!!\n");	//needs more details!!!
			solttu.ChangeMorale(Random.Range(-20,20));
		}
	}

	/// <summary>
	/// Localized detonation on a soldier
	/// </summary>
	private void boom(SoldierController target)
	{	
		target.ChangeHealth(Random.Range(-50, -5) + Random.Range(-50, -5));	//way more than a grenade!
		target.ChangeMorale(-30);
		target.AddAttribute("wounded");
		
		
		if (target.health < 0){
			
			this.TargetKIA_Bomb(target);
		}
		else 
			target.AddEvent("  Was wounded by the detonation!!\n");

		//OTHER CASUALTIES

		foreach (SoldierController solttu in squad)
		{
			if (solttu.alive == true && solttu != target)
			{
				if (Random.Range (1,solttu.skill) < (Random.Range(0,100)))	// sraplen hits!
				{

					solttu.ChangeHealth(Random.Range(-10, -1) + Random.Range(-10, -1));	//less than a grenade!
					solttu.ChangeMorale(-30);
					solttu.AddAttribute("wounded");
					
					
					if (solttu.health < 0){
						
						this.TargetKIA_BombWave(solttu);
					}
					else 
						solttu.AddEvent("  Was hit by the blast!\n");
				}
				else
				{
					int DodgeRandomiser = (Mathf.RoundToInt(Random.Range(0, 6)));
					
					string Dodgetype = "";
					
					switch (DodgeRandomiser)
					{
					case 0:
						Dodgetype = "Dodged";
						break;
					case 1:
						Dodgetype = "Evaded";
						break;
					case 2:
						Dodgetype = "Ignored";
						break;
					case 3:
						Dodgetype = "Ducked away from";
						break;
					case 4:
						Dodgetype = "Took cover from";
						break;
					case 5:
						Dodgetype = "Jumped away from";
						break;
					default:
						Dodgetype = "Avoided";
						break;
					}
					
					solttu.AddEvent ("  " +Dodgetype + " the blast!\n");

				}

			}

		}

	}



	private void TargetKIA_Bomb(SoldierController target)
	{
		foreach (SoldierController solttu in squad)
		{
			if (solttu.alive == true && solttu != target)
			{
				solttu.AddEvent("  " + target.GetRank() + " " +target.getCallsignOrFirstname() + " was blown up by a bomb!!!\n");
				
				if (solttu.HasAttribute("coward"))
					solttu.ChangeMorale(-20);
				else if (solttu.HasAttribute("loner"))
				{}	//nada!
				else
					solttu.ChangeMorale(-10);
			}
			
		}

		target.die("Blown up by a bomb");
		target.AddEvent ("Was blown up by enemy bomb!\n");
	}

	private void TargetKIA_BombWave(SoldierController target)
	{
		foreach (SoldierController solttu in squad)
		{
			if (solttu.alive == true && solttu != target)
			{
				solttu.AddEvent("  " + target.GetRank() + " " +target.getCallsignOrFirstname() + " was killed by the blastwave!!!\n");
				
				if (solttu.HasAttribute("coward"))
					solttu.ChangeMorale(-20);
				else if (solttu.HasAttribute("loner"))
				{}	//nada!
				else
					solttu.ChangeMorale(-10);
			}
			
		}
				
		target.die("Blown up by a bomb");
		target.AddEvent ("Was blown up by enemy bomb!\n");
	}


	bool CheckTrait (string TraitName, SoldierController target)
	{
		if (target.HasAttribute(TraitName))
		{
			return true;
		}
		return false;
		
	}

		int CheckTrait (string TraitName, SoldierController target, int modifier)
		{
			if (target.HasAttribute(TraitName))
			{
				return modifier;
			}
			return 0;
			
		}

}
