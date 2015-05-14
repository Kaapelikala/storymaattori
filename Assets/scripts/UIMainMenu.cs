using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIMainMenu : MonoBehaviour {

    public Text campaignNameLabel;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ClickPlayButton()
    {
        Application.LoadLevel("GameScene");
    }

    public void setCampaignName(string s) { 
        this.campaignNameLabel.text = s;
    }

	public void ClickLoadButton ()
	{
		//show load screen
		//GameObject.Find ("LoadMenu").SetActive ();
	}
	public void ClickExitButton()
	{
		Application.Quit();
	}

}