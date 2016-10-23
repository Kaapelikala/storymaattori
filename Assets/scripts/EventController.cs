using UnityEngine;
using System.Collections.Generic;
//using Events;


//HANDLES ACTUAL EVENTS - FIGHTING GOES TROUGHT THIS!
public class EventController : MonoBehaviour {

	public SoldierManager manager;
	public Campaing campaing;

	public Mission targetlocation;


	
	private Event_Battle Battle;
	private Event_Grenade Grenade;
	private Event_EnemyEmplacement EnemyBunker;
	private Event_Retreat MoraleLost;
	private Event_Debrief MotherBase;
	private Event_BaseIdle MotherPeace;
	private Event_Burial Grave;

	/// <summary>
	/// Actual Fight between Our Squad and Enemy Squad. 
	///Enemies have NUMBER, successfull kills reduce this. Combat lasts until other side flees / is wiped out
	/// </summary>
	public void FireFight(List<SoldierController> squad, Mission missionImput, int Difficulty, bool MissionTargetDone, bool Retreat, bool EnemyRetreat)
	{
		// Event Initialisations 
		
		this.Battle = new Event_Battle (squad);
		this.Grenade = new Event_Grenade (squad);
		this.EnemyBunker = new Event_EnemyEmplacement();
		this.MoraleLost = new Event_Retreat(campaing);
		
		
		// pre-battle calculations!
		
		
		int GreatestRank = 0;	// Highest rank soldier! Affects number of fight rounds but lessens difficulty!
		
		foreach (SoldierController solttu in squad) {
			
			
			solttu.AddEvent("--ENEMIES ENCOUNTERED!\n");
			
			solttu.AddEvent(this.PrintBattleRange());
			
			if (solttu.rank > GreatestRank)
				GreatestRank = solttu.rank;
			if (GreatestRank == 1)		//troopers do not affect gameplay yet!
				GreatestRank = 0;
		}
		
		
		//while (missionImput.StillSomethingToKill() && (squad.Count > 0) && !Retreat)
		while (missionImput.StillSomethingToKill() == true && (squad.Count > 0) && (Retreat == false) && (EnemyRetreat == false)) { // THE BATTLE LOOP
			
			//randomising the soldier!
			
			int SoldierRandomiser = Mathf.FloorToInt (Random.Range (0, (squad.Count - 1)));
			
			int i = SoldierRandomiser;
			
			Debug.Log ("Currently FIGHTING: " + squad [i].soldierID + "KILLS: " + missionImput.kills + "HOSTILES: " + missionImput.Hostiles + "\nCurrently SOMETHING TO KILL: " + missionImput.StillSomethingToKill () + "COUNT: " + (squad.Count > 0) + "RETREAT: " + Retreat);
			
			if (squad [i].alive == true) {		//dead do not fight!
				int BattleEventRandomiser = Mathf.FloorToInt (Random.Range (0, 100));
				
				//Other ideas : Enemy heavy  gun fire, OUR heavy gun fire?	CHANCE TO RETREAT!
				
				if (BattleEventRandomiser < 70) {		// THE BASIC ATTACK, 70 chance
					squad [i].AddKills (missionImput.AddKills (Battle.FightRound (squad [i], Difficulty + (Mathf.FloorToInt (Random.Range (-10, 10))) - GreatestRank)));
				}					
				//					else if (targetlocation.type == "Assault" && BattleEventRandomiser > 90 && !MissionTargetDone)
				//					{
				//						//The HQ attack special here?
				//						
				//						// Only one chance to do it properly?
				//						
				//					}
				else {
					if (Random.Range (0, 100) < 50) { // Enemy bunker fires at one!
						squad [i].AddKills (missionImput.AddKills (EnemyBunker.Encounter (squad [i], Difficulty + (Mathf.FloorToInt (Random.Range (-10, 10))) - GreatestRank)));
					} else {		// Grenade threathens the whole squad!
						Grenade.CheckGrenade (squad, Difficulty + (Mathf.FloorToInt (Random.Range (-10, 10))) - GreatestRank);
					}
				}
			} else {	// Checks against the Average Morale of the remaining soldiers!
				Retreat = MoraleLost.RetreatCheck (squad, campaing.Campaing_Difficulty);
				if (Retreat == false)
				{
					missionImput.retreat = false;	//THIS was if false missionimput retreat = true WHYYYY?
				}
			}	
			
			//Retreat Check for ENEMIES! Exactly same as for our soldiers but simplified
			
			if ((Random.Range (1, missionImput.Hostiles) <= missionImput.kills ) && Retreat == false) {	// one of them must fall before retreatchecking starts!
				int EnemyMoraleCalculation = (((missionImput.Hostiles - missionImput.kills) * 100) / missionImput.Hostiles);
				//Check is averaged, every dead hostile chips enemy morale
				if (Random.Range (0, 100) > EnemyMoraleCalculation) {
					EnemyRetreat = true;
					missionImput.Enemyretreat = true;
					
					foreach (SoldierController soldier in squad) {
						soldier.AddEvent ("Enemies fled!\n");
						
					}
				}
			}
			
			
			//Everyone has enough processing power anyways.
			manager.MoveDeadsAway ();
			
		}
		
	}


	//---------MISSION TYPES------------------------------------------------------------------------------------------------------------------------------------------------------------------
	
	
	
	//
	//
	//	public void Fight (int Difficulty, Mission missionImput)
	//	{
	//		int[] indexes = {-1,-1,-1,-1};
	//		Fight (indexes, Difficulty, missionImput);
	//	}
	//

	public void Fight(int [] indexes, int Difficulty, Mission missionImput)
	{
		targetlocation = missionImput;
		string MissionName = campaing.GetNextMission(); // this gets our mission NAME
		List<SoldierController> squad;


		this.MotherBase = new Event_Debrief();
		this.Grave = new Event_Burial();



		//Get new squad. 
		if (indexes [0]==-1)
			squad = manager.GetSquad ();
		else
		{
			squad = manager.GetSquad(indexes);
		}

		int soldierAmount = 0;


		// Go trought all and give them mission numbers etc
		soldierAmount = this.LeavingForMissionCheckings (squad, targetlocation, MissionName);



		//Enemies have NUMBER, successfull kills reduce this. Combat lasts until other side flees / is wiped out?

		bool MissionTargetDone = false;
		bool Retreat = false;
		bool EnemyRetreat = false;


		/* 
				ACTUAL BATTLE HERE:
		 */

		this.FireFight (squad, targetlocation, Difficulty, MissionTargetDone, Retreat, EnemyRetreat);



		//Extra angst if only survivor

		this.SurvivorAngst (squad, soldierAmount);


		// MISSION CALCULATES RESULTS - Was it victorius?


		bool Victory = targetlocation.IsVictory();
	
		int corpseRecoveryMod = 0;
		if (Retreat == true)
		{
			corpseRecoveryMod = Random.Range(-60,-20);
		}


		this.BaseIdle();	// OOTHERS PARTYYY



		//DEBRIEFING FOR EACH!

		bool AwardBraveryMedal = false;
		
		if (targetlocation.type == "Assault" && Victory == true)
		{
			AwardBraveryMedal = true;	
		}

		foreach (SoldierController solttu in squad)		//ACTUAL DEBRIEFING: "-ONMISSION-" gets deleted there!
		{
			if (solttu.alive == true)	
			{
				MotherBase.Handle(solttu, missionImput, AwardBraveryMedal);
			}
		}

		foreach (SoldierController solttu in squad)
		{

			if (solttu.alive == false)	
			{
				if (Retreat == true && solttu.alive == false)
					solttu.AddHistory("-RETREATDEATH-");
				Grave.Bury(solttu,manager, AwardBraveryMedal, corpseRecoveryMod);		//Burials
				
				manager.DeadSoldierNOTE(solttu);
			}
		}

		manager.MoveDeadsAway ();
		
		//manager.campaing.ButtonCont.CreateNewsPopup(missionImput.ToString());
	}













//
//
//	public void Vacate (int Difficulty)
//	{
//		int[] indexes = {-1,-1,-1,-1};
//		Vacate (indexes, Difficulty);
//	}
//
	/// <summary>
	/// Squad parties at motherbase instead of real battle
	/// </summary>
	public void Vacate(int [] indexes, int Difficulty, Mission missionImput)			//VACATION!
	{
		string MissionName = campaing.GetNextMission();
		List<SoldierController> squad;
		
		//Get new squad. 
		if (indexes [0]==-1)
			squad = manager.GetSquad ();
		else
		{
			squad = manager.GetSquad(indexes);
		}
				
		int soldierAmount = 0;
		int GreatestRank = 0;

		Event_Vacation Party = new Event_Vacation (MissionName);
		Event_Burial Grave = new Event_Burial();

		// Go trought all and give them mission numbers etc
		soldierAmount = this.LeavingForMissionCheckings (squad, missionImput, MissionName);


		foreach (SoldierController solttu in squad)
		{
			Party.Handle(solttu, Difficulty, GreatestRank);
		}

	

		this.SurvivorAngst (squad, soldierAmount);


		this.BaseIdle();	// others at the base idle 


		foreach (SoldierController solttu in squad)
		{
			solttu.RemoveHistory("-ONMISSION-");	//The are again back at base!
		}
		
		
		foreach (SoldierController solttu in squad)	//burials. Simplefied than in battles
		{
			
			if (solttu.alive == false)	
			{
				Grave.Bury(solttu,manager, false, 100);
				
				manager.DeadSoldierNOTE(solttu);
			}
		}
		manager.MoveDeadsAway ();
		
	}





		
	public void Patrol(int [] indexes, int Difficulty, Mission missionImput)
	{
		targetlocation = missionImput;
		string MissionName = campaing.GetNextMission(); // this gets our mission NAME
		List<SoldierController> squad;
		
		
		this.MotherBase = new Event_Debrief();
		this.Grave = new Event_Burial();
		
		Event_PatrolNoEnemies WaypointCheck = new Event_PatrolNoEnemies ();
		
		
		//Get new squad. 
		if (indexes [0]==-1)
			squad = manager.GetSquad ();
		else
		{
			squad = manager.GetSquad(indexes);
		}
		
		int soldierAmount = 0;
		
		
		// Go trought all and give them mission numbers etc
		soldierAmount = this.LeavingForMissionCheckings (squad, targetlocation, MissionName);

		bool MissionTargetDone = false;
		bool Retreat = false;
		bool EnemyRetreat = false;

		// PATROL SPECIFIC MATERRIAL DOWN
		// Patrol Checks Few "Locations" for enemy - if encountered battle WILL come

		int HowManyLocationsToCheck = 3;

		int whenEncounter = Random.Range (1, HowManyLocationsToCheck);

		bool ThereIsEnemies = false;

		if (targetlocation.Hostiles > 0) { 		// If there is enemies is randomised already in the mission.
			ThereIsEnemies = true;
		}


		for (int i = 1; i <= HowManyLocationsToCheck; i++)
		{
			int WaypointNumber = (i-1)*10 + Random.Range (1,10);
			foreach (SoldierController solttu in squad)
			{
				solttu.AddEvent("-Checking waypoint " + WaypointNumber + "..\n");
			}
		
			if (( i == whenEncounter ) && ThereIsEnemies == true) //			ACTUAL BATTLE HERE:
			{
				this.FireFight (squad, targetlocation, Difficulty, MissionTargetDone, Retreat, EnemyRetreat);

				HowManyLocationsToCheck = 0;	//after fight return to base!

			} else 
			{
				WaypointCheck.Handle(squad);

			}
		
		}


		// SPECIFIC MATERIALL ENDS --------------


		
		//Extra angst if only survivor
		
		this.SurvivorAngst (squad, soldierAmount);
		
		
		// MISSION CALCULATES RESULTS - Was it victorius?
		
		
		bool Victory = targetlocation.IsVictory();
		
		int corpseRecoveryMod = 0;
		if (Retreat == true)
		{
			corpseRecoveryMod = Random.Range(-60,-20);
		}
		
		
		this.BaseIdle();	// OOTHERS PARTYYY
		
		
		
		//DEBRIEFING FOR EACH!
//		
//		bool AwardBraveryMedal = false;
//		
//		if (targetlocation.type == "Assault" && Victory == true)
//		{
//			AwardBraveryMedal = true;	
//		}
		
		foreach (SoldierController solttu in squad)		//ACTUAL DEBRIEFING: "-ONMISSION-" gets deleted there!
		{
			if (solttu.alive == true)	
			{
				MotherBase.Handle(solttu, missionImput, false);
			}
		}
		
		foreach (SoldierController solttu in squad)
		{
			
			if (solttu.alive == false)	
			{
				if (Retreat == true && solttu.alive == false)
					solttu.AddHistory("-RETREATDEATH-");
				Grave.Bury(solttu,manager, false, corpseRecoveryMod);		//Burials
				
				manager.DeadSoldierNOTE(solttu);
			}
		}
		
		manager.MoveDeadsAway ();
	}

	
	/// <summary>
	/// What happens in the base while squad is away?
	/// </summary>	
	public void BaseIdle ()
	{
		int[] indexes = {-1,-1,-1,-1};
		BaseIdle (indexes);
	}
	
	public void BaseIdle(int [] indexes)
	{
		List<SoldierController> idlers = new List<SoldierController> (0);		//those who are at the Base!
		
		this.MotherPeace = new Event_BaseIdle(campaing);
		this.Grave = new Event_Burial();
		
		foreach (SoldierController solttu in manager.GetSquad())
		{
			if (solttu.HasHistory("-ONMISSION-") == false)		// those who are NOT at MISSION!
			{
				idlers.Add(solttu);
				solttu.AddHistory("-IDLE-:" + campaing.TimeStamp);	//to persons to remember this :D
			}
		}
		
		MotherPeace.Handle(idlers);	// The actual happenings
		
		
		
		foreach (SoldierController solttu in idlers)
		{
			if (solttu.alive == false)	
			{
				Grave.Bury(solttu, manager, false, 50);
				
				manager.DeadSoldierNOTE(solttu);
			}
		}
		
		manager.MoveDeadsAway ();
		
		
	}



	//--------------OTHER FUNCTIONS----------------------------------------------------------------------------------------------------------
	

	
	
	
	
	
	
	
	










	private void CheckSoldierMorale(SoldierController solttu)	//How this soldier likes this mission?

	{

		
		int MoraleRoll = Random.Range(0, 100);
	
		if (solttu.morale > MoraleRoll)
		{
			if (solttu.HasAttribute("veteran") && solttu.HasAttribute("heroic"))
		{
			solttu.AddEvent("Come on you apes, who wants to live forever?\n");
			solttu.ChangeMorale(20);
		}
		else if ((solttu.HasAttribute("newbie") || solttu.HasAttribute("coward")) && !solttu.HasAttribute("heroic"))
		{
			if (Random.Range (0,10) > 5)
			{
				solttu.AddEvent(solttu.soldierLName + " was afraid to leave the base\n");
				solttu.ChangeMorale(-10);
			}
			else if (Random.Range (0,10) > 3)
			{
				solttu.AddEvent("This is not Kansas anymore..\n");
				solttu.ChangeMorale(-20);
			}
			else
			{
				solttu.AddEvent(solttu.GetRank() + " missed home!\n");
				solttu.ChangeMorale(-20);
			}
		}
		else if (solttu.HasAttribute("heroic") && (Random.Range (0,10) > 3))
		{
			solttu.AddEvent("Onward to glory!\n");
			solttu.ChangeMorale(10);
		}
		else if (Random.Range (0,10) > 7)
		{
			solttu.AddEvent(solttu.soldierLName + " looked forward to the mission!\n");
		}
		else if (Random.Range (0,10) > 7)
		{
			solttu.AddEvent("This one will be OK\n");
		}
		else
		{
			solttu.AddEvent("Lets do this!\n");
		}
		}
		else if (solttu.morale-20 < MoraleRoll) // minor fail
		{
			if (solttu.HasAttribute("veteran") && (Random.Range (0,10) > 3) )
			{
				solttu.AddEvent("One more.\n");
			}
			else if (solttu.missions > 6 && solttu.HasAttribute("veteran") && (Random.Range (0,10) > 6))
			{
				solttu.AddEvent("Still more missions.\n");
				solttu.ChangeMorale(-10);
			}
			else if (solttu.missions > 3 && !solttu.HasAttribute("veteran") && (Random.Range (0,10) > 6))
			{
				solttu.AddEvent("Urgh, another mission.\n");
				solttu.ChangeMorale(-10);
			}
			else if ((solttu.HasAttribute("newbie") || solttu.HasAttribute("coward")) && !solttu.HasAttribute("heroic"))
			{
				solttu.AddEvent("It was hard to leave the base.\n");
				solttu.ChangeMorale(-25);
			}
			else
			{
				solttu.AddEvent("It was not fun to go to mission.\n");
				solttu.ChangeMorale(-20);
			}
		}
		else if (solttu.morale < MoraleRoll) // minor fail
		{
			if (solttu.HasAttribute("veteran"))
			{
				solttu.AddEvent("One more. Who dies this time?\n");
				solttu.ChangeMorale(-10);
			}
			else if ((solttu.HasAttribute("coward") || solttu.HasAttribute("newbie")) && !solttu.HasAttribute("heroic"))
			{
				solttu.AddEvent("No! I am gona die out there! \n");
				solttu.ChangeMorale(-40);
			}
			else {
				solttu.AddEvent("Leaving for the mission was hell.\n");
				solttu.ChangeMorale(-30);
			}
		}			
	}


	/// <summary>
	/// Checks EVERY single squad that goes for a mission. MUST BE RUN!
	/// </summary>
	/// <returns>how many squad members</returns>
	private int LeavingForMissionCheckings (List<SoldierController> squad, Mission missionImput, string MissionName){
		
		int soldierAmount = 0;
		
		foreach (SoldierController solttu in squad)
		{
			solttu.AddHistory("-ONMISSION-");	//denotes that is NOT AT THE MOTHERBASE!

			solttu.AddHistory("-MISSION-:"+campaing.missionNumber+ ": " + missionImput.type);
			solttu.AddEvent("\nTS:" + campaing.TimeStamp + ":" + missionImput.MissionName +": "+ missionImput.type+ "\n");
		
			solttu.missions++;
			soldierAmount++;
			
			this.CheckSoldierMorale(solttu);
			
			
		}

		return soldierAmount;
	}


	private void SurvivorAngst(List<SoldierController> squad, int soldierAmount){

		//after all missions: 

		int deadAmount = 0;

		foreach (SoldierController solttu in squad) {
		
			if (solttu.alive == false) {
				deadAmount++;
			}
		}
		if ((soldierAmount - deadAmount) == 1) {
			HandleOnlySurvivor (squad);
		}

	}

	private void HandleOnlySurvivor(List<SoldierController> squad){

		foreach (SoldierController solttu in squad)
		{
			if (solttu.alive == true)	//find the one alive
			{
				string Sexdiff = "";
				if (solttu.sex == 'm')
				{
					Sexdiff = "He";
				}
				else 
				{
					Sexdiff = "She";
				}
				
				if (solttu.HasAttribute("loner") && solttu.HasAttribute("coward"))
				{
					solttu.AddEvent(Sexdiff + " was the only survivor. It was not good at all.\n");
					solttu.ChangeMorale(-10);
				}
				else if (solttu.HasAttribute("loner") == true)
				{
					solttu.AddEvent(Sexdiff + " was the only survivor. It did not matter at all.\n");
				}
				else if (solttu.HasAttribute("coward") == true)
				{
					solttu.AddEvent(Sexdiff + " was the only survivor. It was completely horrible!\n");
					solttu.ChangeMorale(-50);
				}
				else if ((Random.Range(0, 100) > 80))
				{
					solttu.AddEvent(solttu.getCallsignOrFirstname() + " was the only survivor. " + Sexdiff +" did not care any more.\n");
					solttu.AddAttribute("loner");
					solttu.skill++;
				}
				else if ((Random.Range(0, 100) > 80) && !solttu.HasAttribute("heroic") && !solttu.HasAttribute("coward"))
				{
					solttu.AddEvent(solttu.getCallsignOrFirstname() + " was the only survivor. Mental scars will not heal.\n");
					solttu.AddAttribute("coward");
					solttu.ChangeMorale(-30);
				}
				else
				{
					solttu.AddEvent(Sexdiff + " was the only survivor. It was depressing.\n");

					solttu.ChangeMorale(-20);
					if (Random.Range (0,0) > 8)
					{
						solttu.AddAttribute("depressed");
						solttu.AddEvent("Really depressing!\n");
					}
				}
				
			}
		}

	}

	private string PrintBattleRange()
	{
		string encounterrange = this.targetlocation.getEncounterRange();

		string[] battles= new string[] 
		{
			"battle",
			"shootout",
			"skirmish",
			"mission",
			"combat",
			"fight"
		};

		string battleInsert = battles[(Mathf.RoundToInt(Random.value*(battles.GetLength(0)-1)))];

		string[] nears= new string[] 
		{
			"near",
			"average",
			"okay",
			"50m"
		};

		string[] closes= new string[] 
		{
			"close",
			"cqc",
			"melee",
			"short"
		};

		string[] fars= new string[] 
		{
			"far",
			"distant",
			"long"
		};

		string RangeInsert = "";

		if (encounterrange == "Far")
		{
			RangeInsert = fars[(Mathf.RoundToInt(Random.value*(fars.GetLength(0)-1)))];
		}
		else if (encounterrange == "Close")
		{
			RangeInsert = closes[(Mathf.RoundToInt(Random.value*(closes.GetLength(0)-1)))];
		}
		else
		{
			RangeInsert = nears[(Mathf.RoundToInt(Random.value*(nears.GetLength(0)-1)))];
		}

		return ("The " + battleInsert + " took place in "+ RangeInsert +" distance!\n");

	}



	/*void OnGUI () {
		if (GUI.Button (new Rect (30,250,250,20), "!!!TAPPELU!!!")) {
			this.Fight();
			
		}*/
		/*if (GUI.Button (new Rect (50,250,200,40), "CHECK DEAD")) {
			this.MoveDeadsAway();
			
		}*/
	//}


}

