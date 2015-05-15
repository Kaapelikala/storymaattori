using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIMainMenu : MonoBehaviour {

    public Text campaignNameLabel;

    public void ClickPlayButton()
    {
        Application.LoadLevel("scene");
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