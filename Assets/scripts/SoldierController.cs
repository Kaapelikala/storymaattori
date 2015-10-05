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

		this.health = 90 + Random.Range(0, 10) + Random.Range(0, 10);
		this.morale = 90 + Random.Range(0, 10) + Random.Range(0, 10);
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

		
		int FNameRandomiser = Random.Range(0, 12);
		
		if (this.sex == 'm')
		{
			string [] MaleFirstNames = new string[] {
				"Smirnov",
				"Henrik",
				"James",
				"Bolton",
				"Arken",
				"Damien",
				"Piper",
				"Tybalt",
				"Torres",
				"Gavais",
				"Bort",
				"Jerry",
				"Bathul",
				"Ketil",
				"Erue",
				"Frogor",
				"Karhu",
				"Wolfie",
				"Reba",
				"Kala"
			};

			this.soldierFName = MaleFirstNames[(Mathf.RoundToInt(Random.value*(MaleFirstNames.GetLength(0)-1)))];
		}
		else
		{
			string [] FemaleFirstNames = new string[] {
				"Jane",
				"Mary",
				"Rose",
				"Emily",
				"Vyce",
				"Felixia",
				"Alexandria",
				"Gweythe",
				"Lily",
				"Catherine",
				"Oceania",
				"Laura",
				"Balia",
				"Nelma",
				"Ice",
				"Saurela",
				"Regina"
			};
			
			this.soldierFName = FemaleFirstNames[(Mathf.RoundToInt(Random.value*(FemaleFirstNames.GetLength(0)-1)))];

		}

		string [] LastNames = new string[] {
			"Barrow",
			"Care",
			"Lien",
			"Hamrond",
			"Berren",
			"Fury",
			"Mestos",
			"Cotton",
			"Bener",
			"Fulgrimo",
			"Zrobsson",
			"Spielman",
			"Vindictus",
			"Tanwaukar",
			"Swartz",
			"Delifus",
			"Grimm",
			"Swartz",
			"Fermen",
			"Perho",
			"Bortsson",
			"Langred",
			"Smith",
			"Legerd",
			"Fromeo",
			"King", 
			"Orrala",
			"Nukkula",
			"Muno",
			"Wazabi",
			"Kurosava",
			"Majora",
			"Capu",
			"Oligarki",
			"Van Saarek",
			"Piggi",
			"McKilligan",
			"Kerrigan",
			"Muro",
			"Keksi",
			"Dinner",
			"Sharpe",
			"Wulf",
			"Xero",
			"Ikina"
		};
	
		this.soldierLName = LastNames[(Mathf.RoundToInt(Random.value*(LastNames.GetLength(0)-1)))];
	
		int LNameRandomiser = Random.Range(0, 22);



		this.AddAttribute("newbie");	//every soldier has this - goes away after first kill or wound!


		/*	MOVED TO SOLDIERMANAGER CREATE NEW SOLDIER!
		int traitRandomiser = Random.Range(0, 12);

		switch(traitRandomiser)
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
		} */
		 
		Debug.Log("Luotu " +  soldierID + " '" + this.soldierFName + " '" + this.callsign + "' " + this.soldierLName);


	}



	public SoldierController (string LNameImput, int IDimput){

		soldierLName = LNameImput;
		this.soldierID = IDimput;
		Debug.Log("Luotu  " + soldierID + " '" + this.soldierFName + " '" + this.callsign + "' " + this.soldierLName);

	}

	public string getCallsignOrFirstname(){
		if (this.callsign == "")
			return this.soldierFName;
		return this.callsign;

	}

	public string FullName(){

		string returnoitava = "";
		
		if (this.callsign == "")
		{
			returnoitava = this.GetRank() + " " + this.soldierFName +" "+ this.soldierLName;
		}
		else
		{
			returnoitava = this.GetRank() + " '" + this.callsign +"' "+ this.soldierLName;
		}

		return returnoitava;
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

	public void AddAward (string awardToAdd)
	{
		if (this.HasAward(awardToAdd) == false)
		{
			awards.Add (awardToAdd);
		}
		
	}
	
	public void RemoveAward (string awardToRemove)
	{
		this.awards.Remove (awardToRemove);
	}
	
	public bool HasAward (string question)
	{
		return awards.Contains(question);
	}

	public string GetAwards ()	
	{
		string returnoitava = "";
		
		foreach (string item in awards)
		{
			returnoitava += item + ", ";
		}
		
		return returnoitava;
	}


	public void AddEvent(string combatEvent)
	{
		events.Add(combatEvent);
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

		Returnoitava += "Awards: " + this.GetAwards() + "\n";

		Returnoitava += "History: " + this.GetEvents() + "\n";



		return Returnoitava;


	}

}
