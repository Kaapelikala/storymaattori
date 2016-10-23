using UnityEngine;
using System.Collections;
using System.Collections.Generic;


//The Actual Soldier - Has stats and creation!
public class SoldierController : ScriptableObject {

	public string soldierFName="";	//Fistname
	public string soldierMName="";	//Middle Name, not used often!
	public string soldierLName="Batman";	//Lastname
	public string callsign="";
	public char sex = 'm';
	public int pictureID;	// PictureDNA at some time? Or "Face" Scribtableobject?
	public int hairID;
	public int soldierID;
	public ArrayList attributes=new ArrayList();
	public ArrayList awards=new ArrayList();
	public ArrayList history=new ArrayList();	//Non-visible to players, used to see what soldier has done!
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
	public List<string> events = new List<string> ();		//Events written down for player to see!

	public string HowDied = "";

	void Start () {

	}

	public SoldierController (int IDimput){

		// THESE NEED THE RANDOMISER!!

		this.soldierID = IDimput;

		this.health = 80 + Random.Range(0, 20) + Random.Range(0, 20);
		this.morale = 80 + Random.Range(0, 10) + Random.Range(0, 20);
		this.skill = 80 + Random.Range(0, 20) + Random.Range(0, 20); //More diversity!

//		this.health = 90 + Random.Range(0, 10) + Random.Range(0, 10);
//		this.morale = 90 + Random.Range(0, 10) + Random.Range(0, 10);
//		this.skill = 90 + Random.Range(0, 10) + Random.Range(0, 10); //Average of 100

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

		this.pictureID = (Mathf.RoundToInt(Random.Range(0, 5.4f)));
		this.hairID = (Mathf.RoundToInt(Random.Range(1, 5.4f)));
		
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
				"Kala",
				"Joh",
				"Mika",
				"Ernicos",
				"Kullervo",
				"Tumpelo",
				"Pax",
				"Miekka"

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
				"Regina",
				"Nia",
				"Bella",
				"Vindi",
				"Peace",
				"Nancy",
				"Eliza",
				"Sarah",
				"Maura",
				"Ilona"
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
			"Ikina",
			"Muro",
			"Konari",
			"Worry",
			"Clock",
			"Battery",
			"Agisson",
			"Borzsson",
			"Pommi",
			"Kranu",
			"Pisla",
			"Voi",
			"Lake",
			"Soldat",
			"Marsh",
			"Dinner",
			"Dinker",
			"Blovinsky"
		};
	
		this.soldierLName = LastNames[(Mathf.RoundToInt(Random.value*(LastNames.GetLength(0)-1)))];
		//this.soldierLName = GenerateSurname();

		int LNameRandomiser = Random.Range(0, 22);



		this.AddAttribute("newbie");	
		//every soldier has this - goes away after first kill or wound!


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
		
		this.name = IDimput + " " + this.soldierLName + " " + this.soldierFName;	// for UNITY debugging is easier!


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

	/// <summary>
	/// Rank, First + Middle OR Callsign, Lastname
	/// </summary>
	/// <returns>The name.</returns>
	public string FullName(){

		string returnoitava = "";


		returnoitava = this.GetRank()+ " ";
		
		if (this.callsign == "")
		{
			returnoitava += this.soldierFName;
			
			if (this.soldierMName !="")
				returnoitava += " " + this.soldierMName;
			
			returnoitava +=" ";
		}
		else
		{
			returnoitava += " '" + this.callsign +"' ";
		}
		
		returnoitava +=  this.soldierLName;
		return returnoitava;
	}

	/// <summary>
	/// Rank, First, Middle, Callsign, Last Name
	/// </summary>
	/// <returns>The names.</returns>
	public string AllNames(){
		
		string returnoitava = "";

		
		returnoitava = this.GetRank()+ " ";

		
		returnoitava += this.soldierFName;
		
		if (this.soldierMName !="")
			returnoitava += " " + this.soldierMName;
		
		returnoitava +=" ";

		if (this.callsign != "")
		{
			returnoitava += "'" + this.callsign +"' ";
		}
	
		returnoitava +=  this.soldierLName;
		
		return returnoitava;
	}

	/// <summary>
	/// Firstname, Middlename, Callsign, Lastname
	/// </summary>
	/// <returns>The names no RAN.</returns>
	public string AllNamesNoRANK(){
		
		string returnoitava = "";

		
		returnoitava += this.soldierFName;
		
		if (this.soldierMName !="")
			returnoitava += " " + this.soldierMName;
		
		returnoitava +=" ";
		
		if (this.callsign != "")
		{
			returnoitava += "'" + this.callsign +"' ";
		}
		
		returnoitava +=  this.soldierLName;
		
		return returnoitava;
	}

	/// <summary>
	/// Rank, Lastname ONLY
	/// </summary>
	/// <returns>The name</returns>
	public string GetFormalName(){
		
		string returnoitava = "";
			
		returnoitava = this.GetRank()+ " ";

		returnoitava +=  this.soldierLName;
		
		return returnoitava;
	}

	/// <summary>
	/// ShortRank, Lastname
	/// </summary>
	/// <returns>The formal name_ short rank.</returns>
	public string GetFormalName_ShortRank(){
		
		string returnoitava = "";
		
		returnoitava = this.GetFormalName_ShortRank()+ " ";
		
		returnoitava +=  this.soldierLName;
		
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
	public bool HasAttribute (string question)	//throws nullpint if nothing in there!
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

		bool firstAdd = true;

		foreach (string item in attributes)
		{
			if (item != "newbie")
			{

				if (firstAdd)
				{
					returnoitava += item;
					firstAdd = false;
				}
				else {
					returnoitava +=  ", " +item;
				}
			}
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

		bool firstAdd = true;

		foreach (string item in awards)
		{
			if (firstAdd)
			{
				returnoitava += item;
				firstAdd = false;
			}
			else {
				returnoitava +=  ", " +item;
			}returnoitava += item + ", ";
		}
		
		return returnoitava;
	}
	/// <summary>
	/// Returns list of short award names
	/// </summary>
	/// <returns>The awards short.</returns>
	public string GetAwardsShort ()	
	{
		string returnoitava = "";
		
		bool firstAdd = true;
		
		foreach (string item in awards)
		{
			string toAdd = "";
			if (item == "Wound Badge")
				toAdd = "WB";
			else if (item == "Campaing Medal")
				toAdd = "CM";
			else if (item == "Bravery Medal")
				toAdd = "BM";
			else if (item == "Kill Award")
				toAdd = "K1";
			else if (item == "Kill Sword")
				toAdd = "K2";
			else if (item == "Kill Bomb")
				toAdd = "K3";
			else if (item == "Kill Nuke")
				toAdd = "K4";
			else if (item == "Kill Armangeddon")
				toAdd = "K5";
			else if (item == "Markmanship Metal")
				toAdd = "MarM";
			else if (item == "Bomb Defuse Medallion")
				toAdd = "BDM";

			if (firstAdd)
			{
				returnoitava += toAdd;
				firstAdd = false;
			}
			else {
				returnoitava +=  " " + toAdd;
			}
		}
		
		return returnoitava;
	}
	/// <summary>
	/// Adds piece of history
	/// CODE IS IN STRING - "-X-:Y" 
	/// X IS what happened
	/// Y is DETAIL: 
	///   either CAMPAING MISSION NUMBER or CAMPAING TIMESTAMP 
	/// </summary>
	/// <example>
	/// "-DEAD-:57"
	/// </example>
	/// <param name="lisattava">what to add</param>
	public void AddHistory (string lisattava)		
	{
		// 
		history.Add(lisattava);
		
	}
	public void RemoveHistory (string toDelete)
	{
		history.Remove (toDelete);
	}
	public bool HasHistory (string question)
	{
		return history.Contains(question);
	}

	/// <summary>
	/// Compares Two Soldiers, their Attributes and mutual history. Does soldiers like eachother?.
	/// </summary>
	/// <returns>Integer of how much this soldier likes THE OTHER.</returns>
	/// <param name="COMPARED">COMPARE.</param>
	public int CompareHistory(SoldierController target)
	{
		int HowMuchLikes = 0;

		if (target.HasAttribute("newbie"))
		{
			if (this.HasAttribute("newbie"))
				HowMuchLikes += 20;		//fellow noobs like eachother!
			else
				HowMuchLikes += -20;	//But no-one else does!

		}

		if (this.history[0] == target.history[0])		// First entry is when joined. If both joined at the same time, like eachother!
		{
			HowMuchLikes += 20;

		}

		if (target.HasAttribute("coward"))
		{
			HowMuchLikes += -10;	//No-body likes a coward!
			
		}

		if (target.HasAttribute("idiot") && !this.HasAttribute("idiot"))
		{
			HowMuchLikes += -20;	//Only idiots like other idiots
			
		}

		if (target.HasAttribute("techie") && target.HasHistory("-ROBO-"))
			HowMuchLikes += 10;

		foreach (string TraitLog in this.attributes)		//Traits are good!
		{
			if (target.HasAttribute(TraitLog))
				HowMuchLikes += 10;
		}

			//OPPOSITES
			if (target.sex == 'f' && this.sex =='m')	// so heterosexual :P
				HowMuchLikes += 3;
			if (target.sex == 'm' && this.sex =='f')
				HowMuchLikes += 3;
			if (this.HasAttribute("heroic") && target.HasAttribute("coward"))
			    HowMuchLikes += -20;
			if (this.HasAttribute("coward") && target.HasAttribute("heroic"))
			    HowMuchLikes += -20;
			if (this.HasAttribute("accurate") && target.HasAttribute("inaccurate"))
				HowMuchLikes += -20;
			if (this.HasAttribute("inaccurate") && target.HasAttribute("accurate"))
				HowMuchLikes += -20;
		

		foreach (string historyLog in (this.history))		//More history together = better friends!
		{
			if (target.HasHistory(historyLog))
				HowMuchLikes += 1;
		}

		if (this.HasHistory("-SACR-SURV-:" + target.soldierID))	// If This soldier witnessed heroic jumping of grenade by Target
			HowMuchLikes += 10;

		if (this.HasHistory("-SACR-DIE-:" + target.soldierID))	// If This soldier witnessed heroic death by Target
			HowMuchLikes += 15;


		return HowMuchLikes;

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

		if (this.HasAttribute("newbie"))
			return "Newbie";

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
		if (this.HasAttribute("newbie"))
			return "NEW";
		
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

		// Need some kind of approval for the soldier - gets cooler nickname if other soldiers like him more?
		// Also: Callsings based on traits?

		if (this.callsign != "")
			return this.callsign;


		string [] Callsigns = new string[] {
			"Skull",
			"Fughenson",
			"Mad",
			"Red",
			"Tobacco",
			"Noob",
			"Charlie",
			"Blue",
			"Kova",
			"Easy",
			"TV",
			"Feral",
			"Dwarf",
			"Elfie",
			"Yellow",
			"Marshmallow",
			"Bone",
			"Huge",
			"Black",
			"Gunnie",
			"Fun",
			"Bad",
			"Ugly",
			"Good",
			"Tunnel",
			"Quirky",
			"Courageous",
			"Laughing",
			"Lady",
			"Hope",
			"Morning",
			"Booze",
			"Kling",
			"Night",
			"Bat",
			"Dog",
			"Birdie",
			"Eagle",
			"Glass",
			"Mountain",
			"Sky",
			"Warrior",
			"Snake",
			"Lion",
			"Tiger",
			"Eagle",
			"Bear",
			"Zeke"
		};

		this. callsign = Callsigns[(Mathf.RoundToInt(Random.value*(Callsigns.GetLength(0)-1)))];

		return this.callsign;
	}
	public void AssignNewCallsign(string NewCallsign)
	{
		if (NewCallsign == "" && this.callsign != "")
		{
			this.AddEvent("Command removed " + this.soldierLName + "s callsign '" + callsign + "'!\n");
		}
		else if (this.callsign == NewCallsign) // if same nothing happens!
		{
		}
		else if (this.callsign == "")
		{
			this.AddEvent("Command assigned " + this.soldierLName + " the callsign '" + NewCallsign + "'!\n");
		}
		else 
		{
			this.AddEvent("Command assigned " + this.soldierLName + " the new callsign '" + NewCallsign + "', replacing '"+this.callsign +"'!\n");
		}
		this.callsign = NewCallsign;

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
		Debug.Log("DEAD SOLDIER: "+ this.FullName() + " HOWDIED:" + how);

	}

	public void dieHome(string how){
				
		this.AddHistory("-DIED@BASE-");
		this.alive = false;
		this.HowDied = how;
		Debug.Log("DEAD SOLDIER: "+ this.FullName() + " HOWDIED:" + how);
 	
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
	 
	private string GenerateSurname()	// experiment
	{

		string [] SurNameParts = new string[] {
			"ska",
			"na",
			"ma",
			"ta",
			"ga",
			"ce",
			"ne",
			"ko",
			"il",
			"ha",
			"ho",
			"ty",
			"ge",
			"ke",
			"we",
			"mo",
			"mi",
			"lo",
			"pu",
			"pe",
			"ju",
			"we",
			"ca",
			"hu",
			"me",
			"smi",
			"jho",
			"tar,",
			"lym",
			"bora",
			"fe",
			"co",
			"ve",
			"nen",
			"is",
			"ner",
			"o",
			"a",
			"qa",
			"xa",
			"qe",
			"ze",
			"bul",
			"lu"
		};

		string Returnoitava = "";

		Returnoitava = SurNameParts[(Mathf.RoundToInt(Random.value*(SurNameParts.GetLength(0)-1)))];
		int Counter = Mathf.RoundToInt(Random.Range(1,3));

		while (Counter>0)
		{
			Counter = Counter -1;

			Returnoitava += SurNameParts[(Mathf.RoundToInt(Random.value*(SurNameParts.GetLength(0)-1)))];

		}


		return Returnoitava;
	}

}

/* WEAPON PLANNS

		Ranges:
	CLOSE
	NEAR
	FAR

	WORSE	--		-40
	BAD		-		-20
	OK		K		0
	GOOD	+		+20
	GREAT	++		+40

				C	N	F
Basics
	Gun			K	K	K
	Shotgun		+	K	-
	Rifle		-	K	+

Specials
	Flamer		++	-	--
	Sniper		--	-	++
	Machinegun	-	++	-

Meleee
	Pistol		+	-	--
	Sword		++	--	--

Heavy
	Minigun		K	++	--
	Rocket		--	+	++
	Missile		--	-	++
	Autocannon	-	+	K

 */ 

