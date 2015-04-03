using System;

namespace Storymaattori
{
	public class Soldier
	{
		private int id;
		private string callsign;
		private int experience;
		private int traits;

		public Soldier (int recruitmentSelection)
		{
		}

		public Soldier ()
		{
		}

		public string GetCallsign ()
		{
			return callsign;
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
			return (int)(experience % 1000);
		}

		public int AddExperience (int experience)
		{
			this.experience = this.experience + experience;
			return this.GetLevel ();
		}

		public bool HasTrait (int trait)
		{
			
				return false;
		}

	}
}