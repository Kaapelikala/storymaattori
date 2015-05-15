using UnityEngine;
using System.Collections;

//using Events;

public class OtherEvent : MonoBehaviour
{
	private int eventType;


	public OtherEvent (ArrayList soldiers)
	{
		switch (Random.Range (0, 10)) {
		case (0):
			Party(soldiers);
			break;
		case (1):
			Clone(soldiers);
			break;
		case (2):
			break;
		case (3):

			break;
		default:

			break;
		}
	}

	public void Party(ArrayList soldiers)
	{

	}

	public void Clone(ArrayList soldiers)
	{
	}



}


