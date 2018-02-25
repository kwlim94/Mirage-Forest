using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NarrativeControlScript : MonoBehaviour
{
	List <GameObject> characterList;
	int currentId;
	int pageNumber;
	public Image speechBubble;

	public static NarrativeControlScript Instance {get; set;}

	void Awake()
	{
		if(Instance!= null && Instance != this)
			Destroy(gameObject);
		else
			Instance = this;
	}

	void Update ()
	{
		if(speechBubble.gameObject.activeSelf)
		{
			if(Input.GetMouseButtonDown(0))
			{
				NextPage ();
			}
		}
	}

	void FindCharacters ()
	{
		characterList = new List<GameObject>();
		characterList.AddRange(GameObject.FindGameObjectsWithTag("Player"));
		characterList.AddRange(GameObject.FindGameObjectsWithTag("Characters"));
	}
		

	public void LoadConversation (int IdNumber)
	{
		speechBubble.gameObject.SetActive(true);

		List<NarrativeDatabase> tempList = NarrativeDatabaseScript.Instance.NarrativeDatabaseList;
		
		FindCharacters ();
		characterList[0].GetComponent<CharacterControlScript>().enabled = false;
		characterList[0].transform.GetChild(1).GetComponent<Animator>().SetBool("Walk", false);

		for (int i = 0; i < tempList.Count; i++)
		{
			if(tempList[i].IdNumber == IdNumber)
			{
				currentId = i;
				break;
			}
		}

		pageNumber = -1;
		NextPage ();
	}

	public void NextPage ()
	{
		if (pageNumber < NarrativeDatabaseScript.Instance.NarrativeDatabaseList[currentId].DialogueList.Count - 1)
		{
			pageNumber ++;
			speechBubble.transform.GetChild(0).GetComponent<Text>().text
			= NarrativeDatabaseScript.Instance.NarrativeDatabaseList[currentId].DialogueList[pageNumber].speech;
			speechBubble.transform.position = Camera.main.WorldToScreenPoint(characterList[0].transform.GetChild(0).position);
		}
		else
		{
			characterList[0].GetComponent<CharacterControlScript>().enabled = true;
			speechBubble.gameObject.SetActive(false);
		}
	}
}
