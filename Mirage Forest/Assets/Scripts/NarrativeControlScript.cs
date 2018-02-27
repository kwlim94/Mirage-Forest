using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NarrativeControlScript : MonoBehaviour
{
	List <GameObject> characterList;
	//int currentId;
	int pageNumber;
	int currentCharacterIndex;
	public Image speechBubble;
	List<Dialogue> tempDialogueList;
	float timeElasped;

	public static NarrativeControlScript Instance {get; set;}

	void Awake()
	{
		if(Instance!= null && Instance != this)
			Destroy(gameObject);
		else
			Instance = this;
	}

	void Start ()
	{
		tempDialogueList = new List<Dialogue>();
	}

	void Update ()
	{
		if(speechBubble.gameObject.activeSelf)
		{
//			if(Input.GetMouseButtonDown(0))
//			{
//				NextPage ();
//			}
				
			if(timeElasped > tempDialogueList[pageNumber].duration)
			{
				timeElasped = 0;
				NextPage ();
			}
			timeElasped += Time.deltaTime;
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
				tempDialogueList = tempList[i].DialogueList;
				break;
			}
		}

		pageNumber = -1;
		timeElasped = 0;
		NextPage ();
	}

	public void NextPage ()
	{
		if (pageNumber < tempDialogueList.Count - 1)
		{
			pageNumber ++;
			speechBubble.transform.GetChild(0).GetComponent<Text>().text
			= tempDialogueList[pageNumber].speech;
			speechBubble.transform.position = Camera.main.WorldToScreenPoint(characterList[0].transform.GetChild(0).position);

			for(int i = 0; i < characterList.Count; i++)
			{
				if(characterList[i].GetComponent<CharacterIDTagScript>().ID == tempDialogueList[pageNumber].chracterID)
				{
					currentCharacterIndex = i;
					break;
				}
			}

			characterList[currentCharacterIndex].GetComponent<CharacterControlScript> ().RotateCamera(tempDialogueList[pageNumber].wantedAngle);
			characterList[currentCharacterIndex].
			GetComponent<CharacterAnimationScript> ().ChangeAnimation(tempDialogueList[pageNumber].characterAnimation);
		}
		else
		{
			characterList[0].GetComponent<CharacterControlScript>().enabled = true;
			speechBubble.gameObject.SetActive(false);
		}
	}
}
