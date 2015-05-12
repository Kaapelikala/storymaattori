
using UnityEngine;
using System.Collections;
using Storymaattori;

namespace Storymaattori
{
	public class Soldier : MonoBehaviour 
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

		private int experience; // used for level
			//lvl 2: 4 exp aka kills?
			//lvl 3: 8 exp?


		private ArrayList traits = new ArrayList ();


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

		public string GetFormalName ()
		{
			string returned = this.GetRank() + " " + firstName + " " + lastName;
			return returned;
		}

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
			string[] temp = {"","",""};
			int[] temp2 = {3,0};
			TextGenerator generator = new TextGenerator ();
			this.callsign=generator.Generate (temp, temp2);
		}

		public void SetCallsign(string callsign)
		{
			this.callsign = callsign;
		}


		public int AddExperience (int experience)
		{
			this.experience = this.experience + experience;
			this.rank = this.experience;
			return this.experience;
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
}