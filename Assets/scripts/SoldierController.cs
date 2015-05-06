using UnityEngine;
using System.Collections;

public class SoldierController : MonoBehaviour {

	public string name;
	public string callsign;
	public string [] attributes;
	public int experience=0;
	public int morale=100;
	public int health = 100;
	//Skill represents the soldier's capabilities in killing aliens. Gear can 
	//improve the soldier's chances. In some cases only skill is used.
	public int skill = 100;
	public int gear = 0;
	public string[] gearList;
	public bool alive=true;
	public string rank="Conscript";
	public ArrayList events = new ArrayList ();
	

	public void AddExperience(int experience){
		this.experience += experience;
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

}
