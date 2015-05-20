using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SoldierController : ScriptableObject {

	public string soldierFName="Flash";	//Fistname
	public string soldierLName="Batman";	//Lastname
	public string callsign="Superman";
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
	private int rank = 0;
	public List<string> events = new List<string> ();

	public string HowDied = "";


	public SoldierController (int IDimput){


		// THESE NEED THE RANDOMISER!!

		this.soldierID = IDimput;

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

		int FNameRandomiser = Random.Range(0, 8);
		
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
				this. soldierLName = "Arken";
				break;
			case 5:
				this. soldierLName = "Damien";
				break;
			case 6:
				this. soldierLName = "Piper";
				break;
			case 7:
				this. soldierLName = "Tybalt";
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
				this. soldierLName = "Vyce";
				break;
			case 5:
				this. soldierLName = "Felixia";
				break;
			default:
				this. soldierFName = "Lily";
				break;
			}
		}

		this.pictureID = Random.Range(0, 5);

		int LNameRandomiser = Random.Range(0, 7);

		switch (LNameRandomiser)
		{
		case 0:
			this. soldierLName = "Barrow";
			break;
		case 1:
			this. soldierLName = "Care";
			break;
		case 2:
			this. soldierLName = "Who";
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
			break;
		default:
			this. soldierFName = "Smith";
			break;
		}

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

		this.AddAttribute("newbie");	//every soldier has this - goes away after first kill or wound!

		int traitRandomiser = Random.Range(0, 11);

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
	/// Prints list of all attributes of the Soldier
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
	}

	public void ChangeHealth(int healed)
	{
		this.health += healed;
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
			returnoitava += item + ", ";
			
		}
		
		return returnoitava;
	}


	//As character dying should quite big thing, this function SHOULD FIRE OFF MORE THIGNS - Depression in comrades, move from active soldiers to burial ground etc!
	public void die(string how){
		this.alive = false;
		this.HowDied = how;

	


	}

}
