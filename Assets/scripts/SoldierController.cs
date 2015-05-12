using UnityEngine;
using System.Collections;

public class SoldierController : MonoBehaviour {

	public string soldierFName="Flash";	//Fistname
	public string soldierLName="Batman";	//Lastname
	public string callsign="Superman";
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

}
