using UnityEngine;
using System.Collections;

	public class GamePauseMenu : MonoBehaviour {

		public GameObject menu;

		public void Resume()
		{
			Time.timeScale = 1.0f;
			menu.SetActive (false);
		}

		public void Save()
		{

			//TODO: Saving
		}

		public void Exit()
		{
			Application.Quit ();
		}

		public void ShowMenu()
		{
			menu.SetActive (true);
		}

	}
