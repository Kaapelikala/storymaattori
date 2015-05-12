using UnityEngine;
using UnityEngine.UI;
using System.Collections;

//VT. Handles head icons showing up.

public class SoldierHeadImage : MonoBehaviour {

	public char Sex = 'm';
	public int ImageNumber = 0;

	public Image Effect;

	public Sprite Default;

	public Sprite M1;
	public Sprite M2;
	public Sprite M3;
	public Sprite M4;
	public Sprite M5;

	public Sprite F1;
	public Sprite F2;
	public Sprite F3;
	public Sprite F4;
	public Sprite F5;

	public void Set(char sexInsert, int ImageSet)
	{
		this.Sex = sexInsert;
		this.ImageNumber = ImageSet;
		this.checkPicture();

	}


	// Use this for initialization
	void Start () {
		Effect.overrideSprite = Default;
		this.checkPicture();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void checkPicture()
	{
		if (ImageNumber == 0)
		{
			Effect.overrideSprite = Default;
		}
		else if (Sex == 'm')
		{
			if (ImageNumber == 1)
			{
				Effect.overrideSprite = M1;
			}
			if (ImageNumber == 2)
			{
				Effect.overrideSprite = M2;
			}
			if (ImageNumber == 3)
			{
				Effect.overrideSprite = M3;
			}
			if (ImageNumber == 4)
			{
				Effect.overrideSprite = M4;
			}
			if (ImageNumber == 5)
			{
				Effect.overrideSprite = M5;
			}
			
		}
		else if (Sex == 'f')
		{
			if (ImageNumber == 1)
			{
				Effect.overrideSprite = F1;
			}
			if (ImageNumber == 2)
			{
				Effect.overrideSprite = F2;
			}
			if (ImageNumber == 3)
			{
				Effect.overrideSprite = F3;
			}
			if (ImageNumber == 4)
			{
				Effect.overrideSprite = F4;
			}
			if (ImageNumber == 5)
			{
				Effect.overrideSprite = F5;
			}
			
		}
	}
}
