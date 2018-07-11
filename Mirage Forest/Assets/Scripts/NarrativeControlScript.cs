using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NarrativeControlScript : MonoBehaviour
{
	List <GameObject> characterList;
	int pageNumber;
	int currentCharacterIndex;
	public Image speechBubble;
    public Image picture;
	List<Dialogue> tempDialogueList;
    public List<Sprite> pictureList;
	float timeElasped;
	GameObject toDeactivate;
    public bool isCompleted_L;

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
			if(Input.GetMouseButtonDown(0))
			{
                timeElasped = 0;
				NextPage ();
			}
				
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
		

	public void LoadConversation (int IdNumber, ref bool isCompleted)
	{
		toDeactivate = null;
        speechBubble.gameObject.SetActive(true);
        picture.gameObject.SetActive(true);
        //isCompleted_L = false;

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

	public void LoadConversation (int IdNumber, GameObject toDeactivate, ref bool isCompleted)
	{
		LoadConversation (IdNumber, ref isCompleted);
		this.toDeactivate = toDeactivate;
	}

	public void NextPage ()
	{
		if (pageNumber < tempDialogueList.Count - 1)
		{
			pageNumber ++;
			speechBubble.transform.GetChild(0).GetComponent<Text>().text
			= tempDialogueList[pageNumber].speech;

            speechBubble.transform.GetChild(1).GetChild(0).GetComponent<Text>().text
            = tempDialogueList[pageNumber].name;

            for (int i = 0; i < characterList.Count; i++)
			{
				if(characterList[i].GetComponent<CharacterIDTagScript>().ID == tempDialogueList[pageNumber].chracterID)
				{
					currentCharacterIndex = i;
					break;
				}
			}

            picture.sprite = pictureList[tempDialogueList[pageNumber].chracterID - 1];

			//speechBubble.transform.position = Camera.main.WorldToScreenPoint(characterList[currentCharacterIndex].transform.GetChild(0).position);
			characterList[0].GetComponent<CharacterControlScript> ().RotateCamera(tempDialogueList[pageNumber].wantedAngle);
			characterList[currentCharacterIndex].
			GetComponent<CharacterAnimationScript> ().ChangeAnimation(tempDialogueList[pageNumber].characterAnimation);
		}
		else
		{
			characterList[0].GetComponent<CharacterControlScript>().enabled = true;
            speechBubble.gameObject.SetActive(false);
            picture.gameObject.SetActive(false);
            isCompleted_L = true;
			if(toDeactivate != null)
			{
				toDeactivate.SetActive(false);
			}
		}
	}
}
