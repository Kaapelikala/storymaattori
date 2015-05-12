using UnityEngine;
using System.Collections;
using SimpleJSON;
using System.IO;
using System;

public class GameLoaderScript : MonoBehaviour {

    string filepathResources = "/Assets/Resources/";
    JSONNode campaigns;
    int selectedCampaignNumber;
    string selectedCampaignName;
    string selectedCampaignFolderName;



    void Awake() {
        this.loadCampaignsInfo();
        this.createNewCampaign("newCampaign", 1);
        // get latest campaign name
	
    }
	
	void Start () {
    
    }
	
	
	void Update () {
	
	}

    public void createNewCampaign(string name, int number) { 
        // create new campaign name after previous campaign
        this.selectedCampaignName = name;
        this.selectedCampaignNumber = number;
        this.selectedCampaignFolderName = this.selectedCampaignName + "_" + this.selectedCampaignNumber;

        // add campaign infor to Campaigns.json
        this.campaigns["campaignsArray"][-1]["name"] = name;
        this.campaigns["campaignsArray"][this.campaigns["campaignsArray"].Count - 1]["number"] = number.ToString();
        this.writeToJSON(this.campaigns, "", "Campaigns.json");

        // create new campaign folder
        Directory.CreateDirectory(Environment.CurrentDirectory + this.filepathResources + "/CampaignsFolder/" + @"\" + this.selectedCampaignFolderName);
        
        // create json files inside the new campaign folder
        File.Create(Environment.CurrentDirectory + this.filepathResources + "/CampaignsFolder/" + this.selectedCampaignFolderName + "/" + @"\" + "soldiers.json");
        // write to json files
         
    }

    public void saveCurrentCampaign() {
        
    }

    public void loadCampaignsInfo() {
        this.campaigns = this.loadJSONFile("", "Campaigns");
        this.selectedCampaignName = this.campaigns["campaignsArray"][this.campaigns["campaignsArray"].Count - 1]["name"].Value;
        this.selectedCampaignNumber = this.campaigns["campaignsArray"][this.campaigns["campaignsArray"].Count - 1]["number"].AsInt;
        this.selectedCampaignFolderName = this.selectedCampaignName + "_" + this.selectedCampaignNumber;
    }

    public void writeToJSON(JSONNode node, string filepath, string filename) {
        File.WriteAllText(Environment.CurrentDirectory + this.filepathResources + filepath + @"\" + filename, node.ToString());
    }



    public JSONNode loadJSONFile(string path, string fileName)
    {
        if (fileName == null)
        {
            Debug.Log("Couldn't find filename");
        }

        TextAsset ta = Resources.Load(path + fileName) as TextAsset;

        JSONNode N = JSONNode.Parse(ta.text);
        return N;
    }

}
