using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class NewsPanel : MonoBehaviour {

	public Text MainText;
	public Text HeaderText;
	public GameObject ThisActual;

	public NewsPanel(string TextInject)
	{
		MainText.text = TextInject;

	}
	public void UpdatePanel(string TextInject)
	{
		MainText.text = TextInject;
	}
	public void UpdatePanel(string TextInject, String HeaderInject)
	{
		MainText.text = TextInject;
		HeaderText.text = HeaderInject;
	}

	public void OkayNowGo()
	{
		GameObject.Destroy(ThisActual);

	}
}
