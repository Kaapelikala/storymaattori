using UnityEngine;
using System.Collections;

namespace Storymaattori{

public class TextGenerator : MonoBehaviour {

	private string []nouns;
	private string []verbs;
	private string []adverbs;
	private string []adjectives;
		private string []monsternames;



	/*words:
	*
	*0=nouns
	*1=verbs
	*2=adverbs
	*3=adjectives
	*4=monstenames
	*/

	// Use this for initialization
	void Start () {
	
			monsternames= new string[] {"Mauler","Snotling","Waster","Kitten","Puppy","Green-Haired Screamer","Triclops","Duclops"};
			verbs = new string[] {"mauled", "wasted", "squashed", "flattened","dematerialized", "cooked", "chewed","samurai sworded","packaged","smoshed","drowned","zapped","flamed","grilled","asskicked","freezed","nomnomed"}
		//LOAD WORD LISTS
	
	}

	public string GetWord(int type)
		{
			string returned="";
			int temp;
			if (type ==0)
			{	temp=(Mathf.RoundToInt(Random.value*nouns.GetLength(0)));
				returned = returned+nouns[temp];
			}
			else if (type==1)
			{	temp=(Mathf.RoundToInt(Random.value*verbs.GetLength(0)));
				returned = returned+verbs[temp];
			}
			else if (type ==2)
			{	temp=(Mathf.RoundToInt(Random.value*adverbs.GetLength(0)));
				returned = returned+adverbs[temp];
			}
			
			else if (type ==3)
			{	temp=(Mathf.RoundToInt(Random.value*adjectives.GetLength(0)));
				returned = returned+adverbs[temp];
			}
			else
			{	temp=(Mathf.RoundToInt(Random.value*monsternames.GetLength(0)));
				returned = returned+adjectives[temp];
			}


			return (returned);
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