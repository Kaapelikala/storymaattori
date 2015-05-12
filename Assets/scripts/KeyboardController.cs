using UnityEngine;
using System.Collections;

public class KeyboardController : MonoBehaviour {

	public GameObject menu; 
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("escape")) {

			menu.SetActive (!menu.activeSelf);
		}
	}
}
