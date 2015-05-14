using UnityEngine;
using System.Collections;

namespace Storymaattori{

public class TextGenerator : MonoBehaviour {

	private string []nouns;
	private string []verbs;
	private string []adverbs;
	private string []adjectives;



	/*words:
	*
	*0=nouns
	*1=verbs
	*2=adverbs
	*3=adjectives
	*/

	// Use this for initialization
	void Start () {


		//LOAD WORD LISTS
	
	}

	public string Generate(string[] parts, int[] generatedWordTypes)
	{
		int current = 0;
		string returned = "";
		foreach (string part in parts) {
			returned = returned+part;
			if (current < parts.Length)
			{int temp;
				//add a randomly selected word from correct wordlist.

				if (generatedWordTypes[current] ==0)
				{	temp=(Mathf.RoundToInt(Random.value*nouns.GetLength(0)));
					returned = returned+nouns[temp];
				}
					else if (generatedWordTypes[current]==1)
				{	temp=(Mathf.RoundToInt(Random.value*verbs.GetLength(0)));
					returned = returned+verbs[temp];
				}
					else if (generatedWordTypes[current] ==2)
				{	temp=(Mathf.RoundToInt(Random.value*adverbs.GetLength(0)));
					returned = returned+adverbs[temp];
				}
				else
				{	temp=(Mathf.RoundToInt(Random.value*adjectives.GetLength(0)));
					returned = returned+adjectives[temp];
				}

			}
			current++;
		}
		return returned;
	}


	

}
}