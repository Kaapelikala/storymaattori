﻿using UnityEngine;
using System.Collections;

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
	public ArrayList events = new ArrayList ();


	public SoldierController (){


		// THESE NEED THE RANDOMISER!!


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

		int FNameRandomiser = Random.Range(0, 3);
		
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
			default:
				this. soldierFName = "Lily";
				break;
			}
		}

		this.pictureID = Random.Range(0, 5);

		int LNameRandomiser = Random.Range(0, 6);

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
		default:
			this. soldierFName = "Smith";
			break;
		}

		int CallsignNameRandomiser = Random.Range(0, 11);
		
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
		default:
			this. soldierFName = "Dwarf";
			break;
		}


		Debug.Log("Luotu Solttu '" + this.soldierFName + " '" + this.callsign + "' " + this.soldierLName);


	}



	public SoldierController (string LNameImput){

		soldierLName = LNameImput;
		Debug.Log("Luotu Solttu '" + this.soldierLName + "'!");

	}

	//ATTRIBUTES
	public void AddAttribute (string attribute)
	{
		attributes.Add (attribute);
	}
	public bool HasAttribute (string question)
	{
		return attributes.Contains(question);
	}
	
	public string GetAttributes ()
	{
		return attributes.ToString(); //does not work!
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

	//As character dying should quite big thing, this function SHOULD FIRE OFF MORE THIGNS - Depression in comrades, move from active soldiers to burial ground etc!
	public void die(){
		this.alive = false;

	


	}

}
