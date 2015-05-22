using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//The Actual Soldier - Has stats and creation!
public class SoldierController : ScriptableObject {

	public string soldierFName="";	//Fistname
	public string soldierLName="Batman";	//Lastname
	public string callsign="";
	public char sex = 'm';
	public int pictureID;
	public int soldierID;
	public ArrayList attributes=new ArrayList();
	public ArrayList awards=new ArrayList();
	public int experience=0;
	public int missions=0;
	public int kills=0;
	public int morale=100;
	public int health = 100;
	//Skill represents the soldier's capabilities in killing aliens. Gear can 
	//improve the soldier's chances. In some cases only skill is used.
	public int skill = 100;
	public int gear = 0;
	public string[] gearList;
	public bool alive=true;
	public int rank = 0;
	public List<string> events = new List<string> ();

	public string HowDied = "";


	public SoldierController (int IDimput){


		// THESE NEED THE RANDOMISER!!

		this.soldierID = IDimput;

		this.skill = 90 + Random.Range(0, 10) + Random.Range(0, 10); //Average of 100

		int SexRandomiser = Random.Range(0, 2);

		switch (SexRandomiser)
		{

			case 0:
				this.sex = 'f';
				break;
			case 1:
				this.sex = 'm';
				break;
			default:
				this.sex = 'm';
				break;
		}


		this.pictureID = 0;

		
		int FNameRandomiser = Random.Range(0, 11);
		
		if (this.sex == 'm')
		{
			switch (FNameRandomiser)
			{
			case 0:
				this. soldierFName = "Smirnov";
				break;
			case 1:
				this. soldierFName = "Henrik";
				break;
			case 2:
				this. soldierFName = "James";
				break;
			case 3:
				this. soldierFName = "Bolton";
				break;
			case 4:
				this. soldierFName = "Arken";
				break;
			case 5:
				this. soldierFName = "Damien";
				break;
			case 6:
				this. soldierFName = "Piper";
				break;
			case 7:
				this. soldierFName = "Tybalt";
				break;
			case 8:
				this. soldierFName = "Torres";
				break;
			case 9:
				this. soldierFName = "G.";
				break;
			case 10:
				this. soldierFName = "B.";
				break;
			default:
				this. soldierFName = "Jerry";
				break;
			}
		}
		if (this.sex == 'f')
		{
			switch (FNameRandomiser)
			{
			case 0:
				this. soldierFName = "Jane";
				break;
			case 1:
				this. soldierFName = "Mary";
				break;
			case 2:
				this. soldierFName = "Rose";
				break;
			case 3:
				this. soldierFName = "Emily";
				break;
			case 4:
				this. soldierFName = "Vyce";
				break;
			case 5:
				this. soldierFName = "Felixia";
				break;
			case 6:
				this. soldierFName = "Alexandria";
				break;
			case 7:
				this. soldierFName = "Gweythe";
				break;
			default:
				this. soldierFName = "Lily";
				break;
			}
		}

		int LNameRandomiser = Random.Range(0, 21);

		switch (LNameRandomiser)
		{
		case 0:
			this. soldierLName = "Barrow";
			break;
		case 1:
			this. soldierLName = "Care";
			break;
		case 2:
			this. soldierLName = "Lien";
			break;
		case 3:
			this. soldierLName = "Hamrond";
			break;
		case 4:
			this. soldierLName = "Berren";
			break;
		case 5:
			this. soldierLName = "Fury";
			break;
		case 6:
			this. soldierLName = "Mestos";
			this.skill++;		//easter egg!
			break;
		case 7:
			this. soldierFName = "Cotton";
			break;
		case 8:
			this. soldierLName = "Bener";
			break;
		case 9:
			this. soldierLName = "Fulgrimo";
			break;
		case 10:
			this. soldierLName = "Zrobsson";
			break;
		case 11:
			this. soldierLName = "Spielman";
			break;
		case 12:
			this. soldierLName = "Vindictus";
			break;
		case 13:
			this. soldierLName = "Tanwaukar";
			break;
		case 14:
			this. soldierLName = "Swartz";
			break;
		case 15:
			this. soldierLName = "Delifus";
			break;
		case 16:
			this. soldierLName = "Grimm";
			break;
		case 17:
			this. soldierLName = "Swartz";
			break;
		case 18:
			this. soldierLName = "Fermen";
			break;
		case 19:
			this. soldierLName = "Perho";
			break;
		default:
			this. soldierFName = "Smith";
			break;
		}



		this.AddAttribute("newbie");	//every soldier has this - goes away after first kill or wound!

		int traitRandomiser = Random.Range(0, 12);

		switch (traitRandomiser)
		{
		case 0:
			this.AddAttribute("heroic");
			break;
		case 1:
			this.AddAttribute("accurate");
			break;
		case 2:
			this.AddAttribute("inaccurate");
			break;
		case 3:
			this.AddAttribute("idiot");
			break;
		case 4:
			this.AddAttribute("loner");
			break;
		case 5:
			this.AddAttribute("cook");
			break;
		case 6:
			this.AddAttribute("tough");
			break;
		case 7:
			this.AddAttribute("young");
			break;
		case 8:
			this.AddAttribute ("drunkard");
			break;
		case 9:
			this.AddAttribute("coward");
			break;
		case 10:
			this.AddAttribute("techie");
			break;
		default:
			this.AddAttribute("lucky");
			break;
		}
		 
		Debug.Log("Luotu " +  soldierID + " '" + this.soldierFName + " '" + this.callsign + "' " + this.soldierLName);


	}



	public SoldierController (string LNameImput, int IDimput){

		soldierLName = LNameImput;
		this.soldierID = IDimput;
		Debug.Log("Luotu  " + soldierID + " '" + this.soldierFName + " '" + this.callsign + "' " + this.soldierLName);

	}

	//ATTRIBUTES
	public void AddAttribute (string lisattava)
	{
		if (this.HasAttribute(lisattava) == false)
		{
			attributes.Add (lisattava);
		}

	}

	public void RemoveAttribute (string attribute)
	{
		attributes.Remove (attribute);
	}
	public bool HasAttribute (string question)
	{
		return attributes.Contains(question);
	}

	/// <summary>
	/// Prints list of all attributes of the Soldier. THROWS NULLPOINT IF THERE IS NONE SO THERE HAS TO BE SOMETHING!
	/// </summary>
	/// <returns>list of attributes.</returns>
	public string GetAttributes ()	
	{
		string returnoitava = "";

		foreach (string item in attributes)
		{
			returnoitava += item + ", ";
		}

		return returnoitava;
	}


	public void AddExperience(int experience){
		this.experience += experience;
	}

	public void AddKills (int NewKills)
	{
		this.kills += NewKills;
	}

	public void ChangeMorale(int changedAmount){
		this.morale += changedAmount;
		if (this.morale > 150)
		{
			this.morale = 150;
		}
	}

	public void ChangeHealth(int healed)
	{
		this.health += healed;
		if (this.health > 150)
		{
			this.health = 150;
		}
	}
	public void ChangeSkill(int howMuch)
	{
		this.skill += howMuch;
		if (this.skill > 150)
		{
			this.skill = 150;
		}
	}

	public int SkillTotal()
	{
		return skill + gear;
	}
	

	public string GetRank()
	{
			
		if (rank == 0)
			return "Recruit";

		if (rank == 1)
			return "Trooper";

		if (rank == 2)
			return "Corporal";

		if (rank == 3)
			return "Sergeant";

		if (rank == 4)
			return "Liutenant";

		return "Recruit";

	}
	public string GetRankShort()
	{
		
		if (rank == 0)
			return "RCT";
		
		if (rank == 1)
			return "TRP";
		
		if (rank == 2)
			return "CRP";
		
		if (rank == 3)
			return "SGT";
		
		if (rank == 4)
			return "LTN";
		
		return "Recruit";
		
	}
	
	public string GenerateCallSign(){

		if (this.callsign != "")
			return this.callsign;


		int CallsignNameRandomiser = Random.Range(0, 12);
		
		switch (CallsignNameRandomiser)
		{
		case 0:
			this. callsign = "Skull";
			break;
		case 1:
			this. callsign = "Fughenson";
			break;
		case 2:
			this. callsign = "Mad";
			break;
		case 3:
			this. callsign = "Red";
			break;
		case 4:
			this. callsign = "Tobacco";
			break;
		case 5:
			this. callsign = "Noob";
			break;
		case 6:
			this. callsign = "Charlie";
			break;
		case 7:
			this. callsign = "Blue";
			break;
		case 8:
			this. callsign = "Kova";
			break;
		case 9:
			this. callsign = "Easy";
			break;
		case 10:
			this. callsign = "TV";
			break;
		case 11:
			this. callsign = "Feral";
			break;
		default:
			this. callsign = "Dwarf";
			break;
		}

	return this.callsign;
}
	public void AddEvent(string combatEvent)
	{
		events.Add(combatEvent);
	}

	/// <summary>
	/// Prints list of all events of the Soldier
	/// </summary>
	/// <returns>list of events.</returns>
	public string GetEvents ()	
	{
		string returnoitava = "";
		
		foreach (string item in events)
		{
			returnoitava += item;
			
		}
		
		return returnoitava;
	}


	//As character dying should quite big thing, this function SHOULD FIRE OFF MORE THIGNS - Depression in comrades, move from active soldiers to burial ground etc!
	public void die(string how){
		this.alive = false;
		this.HowDied = how;

	


	}

	public string toString(){

		string Returnoitava = "";

		Returnoitava += this.GetRank() + " " 
			+ this.soldierFName + " '" 
				+ this.callsign + "' " 
				+ this.soldierLName + "\n";

		Returnoitava += "Missions:" + this.missions + "\n"
			+ "Kills:" + this.kills + "\n";

		Returnoitava += "Attributes: " + this.GetAttributes() + "\n";

		Returnoitava += "History: " + this.GetEvents() + "\n";



		return Returnoitava;


	}

}
