using System;

public class Soldier 
	{
		private int id;
		private string callsign;
		private string firstName;
		private string lastName;

		private int rank = 0;
			//Level != Military rank?
			//0 Recruit
			//1 Trooper
			//2 Corporal
			//3 Sergeant
			//4 Liutenant

		private	int level= 1;
		private int experience; // used for level
			//lvl 2: 4 exp aka kills?
			//lvl 3: 8 exp?


		private int traits;


		//STATS

		private int constitution;	//ruumiinkunto
		private int morale;			//mielenkunto
		private int combatSkill;	//tappeluissa käytetään usesti, simppeliöinnin takia luku?




		public Soldier (int recruitmentSelection)
		{
			//More recruitmentSelection = more good traits + Constitution

		}

		public Soldier ()
		{
			//Generate new random Soldier!
		}

		public string GetCallsign ()
		{
			return callsign;
		}

		public string GetFullName ()
		{
			return firstName + " '" + callsign + "' " + lastName;
		}
		/*
		public string GetFormalName ()
		{
			return this.GetRank + " " + firstName + " " + lastName;
		}
		*/

		public string GetRank ()
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
		public void SetCallsign()
		{
			//TextGenerator generates new
		}

		public void SetCallsign(string callsign)
		{
			this.callsign = callsign;
		}

		public int GetLevel ()
		{
			return (int)(experience % 1000);  //Level on lineaarinen? ääh
		}

		public int AddExperience (int experience)
		{
			this.experience = this.experience + experience;
			return this.GetLevel ();
		}

		/* jokin väärin tässä, ny en jaksa korjat
		public bool HasTrait (int trait)
		{

			if (traits & trait == trait)
				return true;
			else
				return false;

		}*/

	}
