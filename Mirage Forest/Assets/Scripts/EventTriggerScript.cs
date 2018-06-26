using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TriggerType
{
	STORY = 0,

	TUTORIAL = 99,
}

public class EventTriggerScript : MonoBehaviour
{
	public int idNumber;
	public TriggerType triggerType;
	public List<GameObject> thingsToActivate;
	public List<GameObject> thingsToDeactivate;
	public List<int> nextConversations; //! if there is a conversation right after this
	public int nextScene; //! if there is a scene change right after this
	public bool isActivatedThingsToBeDeactivated;
	public List<bool> nextIsActivatedThingsToBeDeactivated;
    public bool isCompleted;
	bool isInteract;

	void Start ()
	{
		GetComponent<MeshRenderer>().enabled = false;
        isCompleted = false;
		isInteract = false;
	}

	void Update ()
	{
		if (NarrativeControlScript.Instance.isCompleted_L && isInteract)
		{
			Debug.Log("New Story");
			if (thingsToActivate.Count > 0)
				thingsToActivate.RemoveAt(0);

			if (nextConversations.Count > 0)
			{
				GameObject newGameObject = Instantiate(new GameObject(), this.transform.position, Quaternion.identity);
				newGameObject.AddComponent<EventTriggerScript>();
				EventTriggerScript newScript = gameObject.GetComponent<EventTriggerScript>();
				newScript.idNumber = nextConversations[0];
				nextConversations.RemoveAt(0);
				newScript.nextConversations = nextConversations;
				if (nextIsActivatedThingsToBeDeactivated.Count > 0)
				{
					newScript.isActivatedThingsToBeDeactivated = nextIsActivatedThingsToBeDeactivated[0];
					nextIsActivatedThingsToBeDeactivated.RemoveAt(0);
				}
				newScript.nextIsActivatedThingsToBeDeactivated = nextIsActivatedThingsToBeDeactivated;
				newScript.thingsToDeactivate = thingsToDeactivate;
				newScript.thingsToActivate = thingsToActivate;
				NarrativeControlScript.Instance.isCompleted_L = false;
				newScript.TriggerCollisionAction();
			}
		}
	}

	void OnTriggerEnter (Collider col)
	{
		if(col.tag == "Player" && !isInteract)
		{
			TriggerCollisionAction();
		}
	}

	public void TriggerCollisionAction()
	{
		isInteract = true;
		if(triggerType == TriggerType.STORY)
		{
			if(thingsToDeactivate.Count > 0)
			{
				thingsToDeactivate[0].SetActive(false);
				thingsToDeactivate.RemoveAt(0);
			}

			if(thingsToActivate.Count > 0)
			{
				thingsToActivate[0].SetActive(true);
				thingsToActivate.RemoveAt(0);
				if(isActivatedThingsToBeDeactivated)
					NarrativeControlScript.Instance.LoadConversation (idNumber, thingsToActivate[0], ref isCompleted);
				else
					NarrativeControlScript.Instance.LoadConversation (idNumber, ref isCompleted);
			}
			else
			{
				NarrativeControlScript.Instance.LoadConversation (idNumber, ref isCompleted);
			}

		

//			if(nextConversations.Count > 0)
//			{
//				GameObject newGameObject = Instantiate(new GameObject(), this.transform.position, Quaternion.identity);
//				newGameObject.AddComponent<EventTriggerScript>();
//				EventTriggerScript newScript = gameObject.GetComponent<EventTriggerScript>();
//				newScript.idNumber = nextConversations[0];
//				nextConversations.RemoveAt(0);
//				newScript.nextConversations = nextConversations;
//				newScript.thingsToDeactivate = thingsToDeactivate;
//				newScript.thingsToActivate = thingsToActivate;
//				newScript.triggerType = TriggerType.STORY;
//				newScript.TriggerCollisionAction();
//			}

			//gameObject.SetActive(false);
		}
	}
}
