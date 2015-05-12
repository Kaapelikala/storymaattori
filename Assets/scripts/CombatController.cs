using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CombatController : MonoBehaviour {

	public SoldierController target;
	//Tähän myöhemmin LISTA TAISTELIJOISTA.

	public Text Battlelog;



	private Event_Battle tappelu = new Event_Battle();

		// Use this for initialization
		void Start () {
			//2D taulukko - SOLDIER - INITIATIVE millo toimii seuraavan kerran
		}
		
		// Update is called once per frame
		void Update () {
			//JOs jonku sotilaan NEXTPULSE ni tekee eventin!
		}

	/*NY VAA TESTIMEININGILLÄ!
	void OnGUI () {
		if (GUI.Button (new Rect (10,10,200,20), "FIGHT")) {
			if (target.alive == true)
			{
			tappelu.FightRound(target, 100);
			}
		}

	}*/
	public void fight(){

		//test continues!
		if (target.alive == true)
		{
			Battlelog.text = tappelu.FightRound(target, 100);
		}
	}
}
	


