using UnityEngine;
using System.Collections;

public class Event_Burial : MonoBehaviour {

	public SoldierController corpse;
	public SoldierManager soldiers;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool Bury(SoldierController newBody, SoldierManager soldiers){
		//Currently mission victory is only calculated later, but it SHOULD influrce the recovery of the corpse!

		this.corpse = newBody;
		this.soldiers = soldiers;

		corpse.AddEvent("--Burial: \n");

		corpse.AddAward("Campaing Medal");
		corpse.AddAward("Wound Badge");

		int ChanceMod = 0;

		int RandomRoll = Random.Range(0, 100);
		
		if (RandomRoll < 80)
		{



			corpse.AddEvent("The body of " + corpse.FullName() + " was buried with full honours at the motherbase.\n");

			foreach (SoldierController solttu in (soldiers.GetSquad()))
			{
				if (solttu.alive == true && !solttu.HasAttribute("newbie") && solttu != corpse)	//someone should carry the coffin?
				{
					int SadnessRandomRoll = Random.Range(0, 100);	//More interactions! Crying, stoik looking, sadness!

					if (solttu.HasAttribute("loner")) {
						solttu.AddEvent("\nDid not go to the funeral of " + corpse.FullName() + "\n");
						corpse.AddEvent(" " + solttu.FullName() +" did not attend.\n"); 
					}
					else if (solttu.HasAttribute("heroic")) {
						solttu.AddEvent("\nSpoke warmly at the funeral of " + corpse.FullName() + "\n");
						corpse.AddEvent(" " + solttu.FullName() +" held imperssive speech.\n"); 
					}
					else if (SadnessRandomRoll < 20) { 
						solttu.AddEvent("\nMissed the the funeral of " + corpse.FullName() + "\n");
						solttu.ChangeMorale(-10);
						corpse.AddEvent(" " + solttu.FullName() +" did not attend.\n"); 
						ChanceMod =-10;
					}
					else if (SadnessRandomRoll < 50) { 
						solttu.AddEvent("\nAttended the funeral of " + corpse.FullName() + "\n");
						corpse.AddEvent(" " + solttu.FullName() +" was there.\n"); 
					}
					else if (SadnessRandomRoll < 80) { 
						solttu.AddEvent("\nBrought flowers to the the funeral of " + corpse.FullName() + "\n");
						corpse.AddEvent(" " + solttu.FullName() +" brought flowers.\n"); 
						ChanceMod =+10;
					}
					else{ 
						solttu.AddEvent("\nCried at the funeral of " + corpse.FullName() + "\n");
						corpse.AddEvent(" " + solttu.FullName() +" cried a lot.\n"); 
						solttu.ChangeMorale(-10);
						ChanceMod =+15;
					}
				}

			}

			return Grieving(true, ChanceMod);
			
		} 
		

		corpse.AddEvent("The corpse of " + corpse.FullName() + " was never found.\n");
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

		RandomRoll += corpse.kills;
		RandomRoll += corpse.missions;

		RandomRoll += CheckTrait("heroic", 10);
		RandomRoll += CheckTrait("lucky", 10);
		RandomRoll += CheckTrait("young", -10);
		RandomRoll += CheckTrait("coward", -20);
		RandomRoll += CheckTrait("loner", -20);

		if (RandomRoll < 70)	//Not everyone is grieved
		{

	

			foreach (SoldierController solttu in (soldiers.GetSquad()))
			{
				if (solttu.alive == true && !solttu.HasAttribute("newbie") && solttu != corpse)
				{

					int GrievingRandomiser = Random.Range(0, 5);
					
					switch (GrievingRandomiser)
					{
					case 0:
						solttu.AddEvent("Afterwards mourned " + Sexdiff + " death!\n");	
						solttu.ChangeMorale(-10);
						break;
					case 1:
						solttu.AddEvent("Back at the base, drank to " + Sexdiff + " memory!\n");
						break;
					case 2:
						solttu.AddEvent("Vowed to avenge " + Sexdiff + " death!\n");
						solttu.ChangeMorale(10);
						break;
					case 3:
						solttu.AddEvent("Could not sleep next night, trying to remember " + Sexdiff + " face.\n");
						solttu.ChangeMorale(-20);
						break;
					default:
						solttu.AddEvent("Thought warmly about time with " + corpse.getCallsignOrFirstname() + ".\n");
						solttu.ChangeMorale(10);
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

}
