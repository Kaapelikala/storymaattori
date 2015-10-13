using UnityEngine;
using System.Collections;

public class Event_Burial : MonoBehaviour {

	public SoldierController corpse;
	public SoldierManager soldiers;

	string [] Shames = new string[] {
		"Shame.",
		"Darn.",
		"Damm.",
		"So often.",
		//"Always happens to the best.",
		"Bugger.",
		"Hell.",
		""	// Also possible that no shame!
	};

	string [] Founds = new string[] {
		"found",
		"retreived",
		"recovered",
		"buried properly",
		"dragged back to base",
		"brought back"
	};

	string [] Corpses = new string[] {
		"body",
		"corpse",
		"shell",
		"cadaver",
		"skeleton"
	};

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool Bury(SoldierController newBody, SoldierManager soldiers, bool AwardBraveryMedal){
		//Currently mission victory is only calculated later, but it SHOULD influrce the recovery of the corpse!

		this.corpse = newBody;
		this.soldiers = soldiers;

		corpse.AddHistory("-DEAD-:"+soldiers.campaing.TimeStamp);

		corpse.AddEvent("--Burial: \n");

		corpse.AddAward("Campaing Medal");
		corpse.AddAward("Wound Badge");

		if (AwardBraveryMedal == true)
		{
			corpse.AddEvent("Having participated in mission crucial to the War Effort, " +corpse.soldierLName+ " was awarded posthumorous Bravery Medal.\n");
			corpse.AddAward("Bravery Medal");
		}

		int ChanceMod = 0;	//Chance for person to mourn the dead!

		int RandomRoll = Random.Range(0, 100);
		
		if (RandomRoll < (60 + (100-soldiers.campaing.Campaing_Difficulty)))
		{
			if (((Random.Range(1, 10)) < 5) && (corpse.rank >= 1)){
				corpse.AddEvent("The body of " + corpse.AllNames() + " was buried with full honours at the motherbase.\n");
				ChanceMod += 10;
			}
			if ((Random.Range(1, 10)) < 5){
				corpse.AddEvent("The body of " + corpse.AllNames() + " was laid to grave at the motherbase.\n");
				ChanceMod += 5;
			}
			else {
				corpse.AddEvent("The body of " + corpse.AllNames() + " was buried hastily at the motherbase.\n");
			}

			SoldierController speaker = null;

			int HowManyAttendees = 0;

			foreach (SoldierController solttu in (soldiers.GetSquad()))
			{

				if (solttu.HasHistory("-ONMISSION-")) // cant attend if somewhere else! (basically if dead happens at motherbase while they away!
				{
					solttu.AddHistory("-NOATTEND_BURI-:" + corpse.soldierID);
					corpse.AddEvent(" " + solttu.FullName() +" was away.\n"); 

					int SadnessRandomRoll = Random.Range(0, 100);

					SadnessRandomRoll += solttu.CompareHistory(corpse); // CHANCE TO ATTEND INCREASES WITH FRIENDSHIP! 
					
					SadnessRandomRoll += this.CheckTrait("depressed", -20, solttu);
					SadnessRandomRoll += this.CheckTrait("coward", -10, solttu);
					SadnessRandomRoll += this.CheckTrait("idiot", -40, solttu);
					SadnessRandomRoll += this.CheckTrait("heroic", 20, solttu);

					if (SadnessRandomRoll < 40)
					{
						solttu.AddHistory("-NOCARE-:" + corpse.soldierID);	// dont care
					}
					else
					{
						solttu.AddEvent("\nWas too busy to go the funeral of " + corpse.FullName() + "\n");	//cares but cannot participate
					}

				}
				else if (solttu.alive == true && !solttu.HasAttribute("newbie") && solttu != corpse)	//someone should carry the coffin?
				{

					if ((solttu.CompareHistory(corpse) < (Random.Range(1, 6))) && (solttu.CompareHistory(corpse) > (Random.Range(-5,-15))) ) //does not attend burials of nobodies!
					{
						solttu.AddHistory("-NOATTEND_BURI-:" + corpse.soldierID);
						solttu.AddHistory("-NOCARE-:" + corpse.soldierID);
						corpse.AddEvent(" " + solttu.FullName() +" did not attend.\n"); 
					}
					else
					{

						int SadnessRandomRoll = Random.Range(0, 100);	//More interactions! Crying, stoik looking, sadness!



						SadnessRandomRoll += solttu.CompareHistory(corpse); // CHANCE TO ATTEND INCREASES WITH FRIENDSHIP! 

						SadnessRandomRoll += this.CheckTrait("depressed", -20, solttu);
						SadnessRandomRoll += this.CheckTrait("coward", -10, solttu);
						SadnessRandomRoll += this.CheckTrait("idiot", -40, solttu);
						SadnessRandomRoll += this.CheckTrait("heroic", 20, solttu);

						if (solttu.HasAttribute("wounded") && (Random.Range(0, 100) > solttu.health+solttu.CompareHistory(corpse))) {
							solttu.AddEvent("\nCould not go to the funeral of " + corpse.FullName() + "\n");
							corpse.AddEvent(" " + solttu.FullName() +" was too wounded to attend.\n"); 
							solttu.AddHistory("-NOATTEND_BURI-:" + corpse.soldierID);
							solttu.ChangeMorale(-40);  //this sucks royally
						}
						else if (solttu.HasAttribute("loner") && (Random.Range(0, 100) > 60)) {
							solttu.AddEvent("\nDid not go to the funeral of " + corpse.FullName() + "\n");
							corpse.AddEvent(" " + solttu.FullName() +" did not attend.\n"); 
							solttu.AddHistory("-NOATTEND_BURI-:"+ corpse.soldierID);
							solttu.AddHistory("-NOCARE-:" + corpse.soldierID);
						}
						else if (((Random.Range(0, 100)+this.CheckTrait("heroic",20,solttu)-this.CheckTrait("coward",20,solttu)) > 60) && speaker == null) {
							speaker = solttu;		//Some speaks at the funeral. Others like?
							solttu.AddHistory("-ATTEND_BURI-:"+ corpse.soldierID);
							HowManyAttendees++;
						}
						else if (SadnessRandomRoll < 20) { 
							solttu.AddEvent("\nMissed the the funeral of " + corpse.FullName() + "\n");
							solttu.ChangeMorale(-10);
							corpse.AddEvent(" " + solttu.FullName() +" did not attend.\n"); 
							ChanceMod =-10;
							solttu.AddHistory("-NOATTEND_BURI-:"+ corpse.soldierID);
						}
						else if (SadnessRandomRoll < 35) { 
							solttu.AddEvent("\nWas at the funeral of " + corpse.FullName() + "\n");
							corpse.AddEvent(" " + solttu.FullName() +" was there.\n"); 
							solttu.AddHistory("-ATTEND_BURI-:"+ corpse.soldierID);
							HowManyAttendees++;
						}
						else if (SadnessRandomRoll < 50) { 
							solttu.AddEvent("\nAttended the funeral of " + corpse.FullName() + "\n");
							corpse.AddEvent(" " + solttu.FullName() +" was there.\n"); 
							solttu.AddHistory("-ATTEND_BURI-:"+ corpse.soldierID);
							HowManyAttendees++;
						}
						else if (SadnessRandomRoll < 80) { 
							solttu.AddEvent("\nBrought flowers to the the funeral of " + corpse.FullName() + "\n");
							corpse.AddEvent(" " + solttu.FullName() +" brought flowers.\n"); 
							ChanceMod =+10;
							solttu.AddHistory("-ATTEND_BURI-:"+ corpse.soldierID);
							HowManyAttendees++;
						}
						else{ 
							solttu.AddEvent("\nCried at the funeral of " + corpse.FullName() + "\n");
							corpse.AddEvent(" " + solttu.FullName() +" cried a lot.\n"); 
							solttu.ChangeMorale(-10);
							ChanceMod =+15;
							solttu.AddHistory("-ATTEND_BURI-:"+ corpse.soldierID);
							HowManyAttendees++;
						}

					}
				}

			}

			if (speaker != null)			// SPEAKINGS
			{
				int SpeakAppreciation = 0;

				foreach (SoldierController solttu in (soldiers.GetSquad()))
				{
					if (solttu.alive == true && !solttu.HasAttribute("newbie") && solttu != corpse && solttu != speaker && solttu.HasHistory("-ATTEND_BURI-:"+ corpse.soldierID) && !solttu.HasHistory("-NOCARE-:"+ corpse.soldierID))	//NOT new, not the dead or not the SPEAKER HIMSELF and IS HERE and DOES CARE
					{
						int SpeakingRandomRoll = Random.Range(0, 100);

						SpeakingRandomRoll += this.CheckTrait("heroic", 20, speaker);
						SpeakingRandomRoll += this.CheckTrait("veteran", 20, speaker);
						SpeakingRandomRoll += this.CheckTrait("young", -5, speaker);
						SpeakingRandomRoll += this.CheckTrait("drunkard", -10, speaker);
						SpeakingRandomRoll += this.CheckTrait("depressed", -10, speaker);
						SpeakingRandomRoll += this.CheckTrait("coward", -10, speaker);
						SpeakingRandomRoll += this.CheckTrait("wounded", -10, speaker);

						SpeakingRandomRoll += this.CheckTrait("loner", -20, solttu);


						if (SpeakingRandomRoll < 25) { 	//BAD
							solttu.AddEvent(" Although nice, words of " + speaker.getCallsignOrFirstname() + " " + speaker.soldierLName + " did not help.\n");
							solttu.ChangeMorale(-20);
							ChanceMod =+5;
							SpeakAppreciation += -1;
						}
						else if (SpeakingRandomRoll < 50) { // OKAY
							solttu.AddEvent(" " + speaker.getCallsignOrFirstname() + " " + speaker.soldierLName + " spoke gently about " + corpse.getCallsignOrFirstname() + ".\n");
							solttu.ChangeMorale(10);
							ChanceMod =+10;

						}
						else if (SpeakingRandomRoll < 75) {  // GOOD
							solttu.AddEvent(" " + speaker.getCallsignOrFirstname() + " " + speaker.soldierLName + " summoned up warm memories of " + corpse.getCallsignOrFirstname() + ".\n");
							
							ChanceMod =+10;
							SpeakAppreciation += 1;
						}
						else{  // INSPIRED
							solttu.AddEvent(" Was inspired by the tender speech by " + speaker.getCallsignOrFirstname() + " " + speaker.soldierLName +"!\n");
							solttu.ChangeMorale(+10);
							
							ChanceMod =+20;
							SpeakAppreciation += 2;
						}
					}
					
				}

				if  (HowManyAttendees == 1){

					if (Random.Range(0, 10) > 5){	//GOOD
						speaker.AddEvent("\nSpoke to empty hall at the funeral of " + corpse.FullName() + "\n");
						corpse.AddEvent(" " + speaker.FullName() +" held monology to the coffin, although there was no-one else to hear.\n"); 
						speaker.ChangeMorale(20);
					}
					else{	//BAD
						speaker.AddEvent("\nWanted to speak at the funeral of " + corpse.FullName() + ", but nobody else attended.\n");
						corpse.AddEvent(" " + speaker.FullName() +" had prepared a speech but did not bother to speak to empty hall.\n"); 
						speaker.ChangeMorale(-20);
						ChanceMod += 20;

					}


				}
				else if (SpeakAppreciation < 0){

					speaker.AddEvent("\nSpoke at the funeral of " + corpse.FullName() + "\n");
					corpse.AddEvent(" " + speaker.FullName() +" held speech. Nobody clapped.\n"); 
					speaker.AddEvent(" Others did not like it.\n");
					speaker.ChangeMorale(-30);
				}
				else if (SpeakAppreciation > 3){	//GOOD
					
					speaker.AddEvent("\nSpoke at the funeral of " + corpse.FullName() + "\n");
					corpse.AddEvent(" " + speaker.FullName() +" held a fiery speech!\n"); 
					speaker.AddEvent(" Everyone had tears in their eyes.\n");
					speaker.ChangeMorale(20);
					ChanceMod += 20;
				}
				else {
					
					speaker.AddEvent("\nSpoke at the funeral of " + corpse.FullName() + "\n");
					corpse.AddEvent(" " + speaker.FullName() +" held speech. It was average.\n"); 
					speaker.AddEvent(" It was decreed to be okay.\n");
					speaker.ChangeMorale(10);
				}
			}


		
			return Grieving(true, ChanceMod);
			
		} 

			// CORPSE NOT FOUND EVER


		string CorpseInsert = Corpses[(Mathf.RoundToInt(Random.value*(Corpses.GetLength(0)-1)))];
		
		string FoundsInsert = Founds[(Mathf.RoundToInt(Random.value*(Founds.GetLength(0)-1)))];

		corpse.AddEvent("The " + CorpseInsert + " of " + corpse.FullName() + " was never "+ FoundsInsert +".\n");

		string CorpseInsert2 = Corpses[(Mathf.RoundToInt(Random.value*(Corpses.GetLength(0)-1)))];

		string FoundsInsert2 = Founds[(Mathf.RoundToInt(Random.value*(Founds.GetLength(0)-1)))];
		
		string ShameInsert = Shames[(Mathf.RoundToInt(Random.value*(Shames.GetLength(0)-1)))];


		foreach (SoldierController solttu in (soldiers.GetSquad()))
		{

			if (solttu.alive == true && !solttu.HasAttribute("newbie") && solttu != corpse)	//NOT new, not the dead or not the DEAD HIMSELF
			{

				if (solttu.CompareHistory(corpse) < (Random.Range(1, 6) )) //does not attend burials of nobodies!
				{
					solttu.AddHistory("-NOCARE-:" + corpse.soldierID);
				}
				else
				{
					solttu.AddEvent("\nThe " + CorpseInsert2 + " of " + corpse.FullName() + " was never " + FoundsInsert2 +". " + ShameInsert+ "\n");
					solttu.ChangeMorale(-10);
				}
			}
		}



		ChanceMod =+20;



		return Grieving(false, ChanceMod);
	}

	private bool Grieving(bool corpseburied, int ChanceMod)
	{


		string Sexdiff = "";
		if (corpse.sex == 'm')
		{
			Sexdiff = "his";
		}
		else 
		{
			Sexdiff = "her";
		}

		int RandomRoll = Random.Range(0, 100);
	
		RandomRoll += ChanceMod;
		RandomRoll += corpse.kills;
		RandomRoll += corpse.missions;

		RandomRoll += CheckTrait("heroic", 10);
		RandomRoll += CheckTrait("lucky", 10);
		RandomRoll += CheckTrait("young", -10);
		RandomRoll += CheckTrait("coward", -20);
		RandomRoll += CheckTrait("loner", -20);
	
		foreach (SoldierController solttu in (soldiers.GetSquad()))
		{
			
			//DEBUG FOR LIKINGS
			Debug.Log((solttu.soldierID + " liked " + corpse.soldierID + " for " + solttu.CompareHistory(corpse)));

			if (solttu.HasHistory("-NOCARE-:" + corpse.soldierID) | solttu == corpse)
			{

			}
			else if (RandomRoll + (solttu.CompareHistory(corpse)) > 50 && solttu.alive == true && !solttu.HasAttribute("newbie"))	
				//Not everyone is grieved but this is PERSONAL!
			{
				// THESE NEED MORE TRAIT-BASED PERSONALISATION!

				int howMuchLiked = (solttu.CompareHistory(corpse));

				int GrievingRandomiser = Random.Range(0, 5);

				if (howMuchLiked < -10)		//Particular distaste
				{
					switch (GrievingRandomiser)
					{
					case 0:
						solttu.AddEvent(" It was good to hear that "+ corpse.getCallsignOrFirstname() + " was dead!\n");	
						solttu.ChangeMorale(20);
						break;
					case 1:
						solttu.AddEvent(" News of "+ Sexdiff + " demise felt good!\n");	
						solttu.ChangeMorale(20);
						break;
					case 2:
						solttu.AddEvent(" Never " + solttu.getCallsignOrFirstname() +" had liked "+ corpse.getCallsignOrFirstname() + ".\n");	
						solttu.ChangeMorale(20);
						break;
					case 3:
						solttu.AddEvent(" " + corpse.getCallsignOrFirstname() + " is dead. Great!\n");	
						solttu.ChangeMorale(20);
						break;
					default:
						solttu.AddEvent(" Never " + solttu.getCallsignOrFirstname() +" had cared about "+ corpse.getCallsignOrFirstname() + ".\n");	
						solttu.ChangeMorale(20);
						break;
					}
				}
				else if (howMuchLiked < 10)		//Quite unknown
				{
					switch (GrievingRandomiser)
					{
					case 0:
						solttu.AddEvent(" " + solttu.getCallsignOrFirstname() + " wished to have had more time to get to known " +corpse.getCallsignOrFirstname()+ ".\n");	
						break;
					case 1:
						string ShameInsert = Shames[(Mathf.RoundToInt(Random.value*(Shames.GetLength(0)-1)))];
						solttu.AddEvent(" " + ShameInsert + "\n");	
						break;
					case 2:
						solttu.AddEvent(" And so new blood flows..\n");	
						break;
					case 3:
						string ShameInsert2 = Shames[(Mathf.RoundToInt(Random.value*(Shames.GetLength(0)-1)))];
						solttu.AddEvent(" Another one bites the dust. "+ShameInsert2+"\n");	
						break;
					case 4:
						solttu.AddEvent(" One more to go.\n");	
						break;
					default:
						solttu.AddEvent(" Could not sleep next night, trying to remember " + Sexdiff + " face.\n");
						solttu.ChangeMorale(-10);
						break;
					}
				}
				else if (howMuchLiked < 30)		//Comrade
				{

					switch (GrievingRandomiser)
					{
					case 0:
						solttu.AddEvent(" Live is different without " + corpse.getCallsignOrFirstname()+ "\n");	
						solttu.ChangeMorale(-10);
						break;
					case 1:
						solttu.AddEvent(" Another hero has fallen.\n");	
						solttu.ChangeMorale(-10);
						break;
					case 2:
						if (solttu.HasAttribute("drunkard"))
						{
							solttu.AddEvent(" Back at the base, drank to A LOT " + corpse.getCallsignOrFirstname() + "s memory!\n");
						}
						else{
							solttu.AddEvent(" Back at the base, drank to " + corpse.getCallsignOrFirstname() + "s memory!\n");
							solttu.ChangeMorale(-10);
						}
						break;
					case 3:
						solttu.AddEvent(" Memories of " + corpse.getCallsignOrFirstname() + " always brought smile to " +solttu.getCallsignOrFirstname()+ "s face!\n");	
						solttu.ChangeMorale(-10);
						break;
					case 4:
						solttu.AddEvent(" " + corpse.getCallsignOrFirstname() + " had been a good comrade.\n");	
						solttu.ChangeMorale(-10);
						break;
					default:
						solttu.AddEvent(" Afterwards mourned " + Sexdiff + " death. He had been true comrade!\n");
						solttu.ChangeMorale(-10);
						break;
					}


				}
				else if (howMuchLiked < 50)		//friend
				{

					switch (GrievingRandomiser)
					{
					case 0:
						solttu.AddEvent(" Life feels dull without " +corpse.getCallsignOrFirstname()+ "..\n");
						solttu.ChangeMorale(-20);
						break;
					case 1:
						solttu.AddEvent(" How can life go on without " + corpse.getCallsignOrFirstname() + "?\n");
						solttu.ChangeMorale(-20);
						break;
					case 2:
						solttu.AddEvent(" Vowed to avenge " + Sexdiff + " death!\n");
						solttu.ChangeMorale(10);
						break;
					case 3:
						solttu.AddEvent(" Felt " + Sexdiff + " precence, perhaps " + corpse.getCallsignOrFirstname()+" now watches over me.\n");
						solttu.ChangeMorale(10);
						solttu.skill++;
						break;
					case 4:
						solttu.AddEvent(" " + corpse.getCallsignOrFirstname() + " had been a good friend!\n");	
						solttu.ChangeMorale(10);
						break;
					default:
						solttu.AddEvent(" A true hero has fallen.\n");
						solttu.ChangeMorale(-20);
						break;
					}



				}


			}
			return corpseburied;
		}
		return corpseburied;
	}

	int CheckTrait (string TraitName, int modifier)
	{
		if (corpse.HasAttribute(TraitName))
		{
			return modifier;
		}
		return 0;
		
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
