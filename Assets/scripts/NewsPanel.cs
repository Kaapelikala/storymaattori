using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class NewsPanel : MonoBehaviour {

	public Text NewsText;
	public GameObject ThisActual;

	public NewsPanel(string TextInject)
	{
		NewsText.text = TextInject;

	}
	public void UpdatePanel(string TextInject)
	{
		NewsText.text = TextInject;
	}

	public void OkayNowGo()
	{
		GameObject.Destroy(ThisActual);

	}
}
