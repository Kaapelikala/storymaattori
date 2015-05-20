using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIMainMenu : MonoBehaviour {

    public Text campaignNameLabel;

	public Image DESC_FADE;

	public AudioSource BeginClick;



    public void ClickPlayButton()
    {
		//DESC_FADE.IsActive(true);
		//BeginClick.Play();

		//new WaitForSeconds(10);

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