using System.Collections.Generic;
using UnityEngine;
using System.Threading;
using System.Threading.Tasks;

public class ObjectEventTriggerScript: InteractionScript
{
	public List<GameObject> thingsToActivate;
	public List<GameObject> thingsToDeactivate;
	public List<int> nextConversations; //! if there is a conversation right after this
	public List<bool> nextIsActivatedThingsToBeDeactivated;
	public int nextScene; //! if there is a scene change right after this
	public bool isActivatedThingsToBeDeactivated;
    bool isInteract;

	public override void OtherStart()
	{
        isInteract = false;
	}

    public override void Interact ()
	{

        NarrativeControlScript.Instance.isCompleted_L = false;

        if (thingsToDeactivate.Count > 0)
		{
			thingsToDeactivate[0].SetActive(false);
			thingsToDeactivate.RemoveAt(0);
		}


        if (thingsToActivate.Count > 0)
        {
            thingsToActivate[0].SetActive(true);
            if (isActivatedThingsToBeDeactivated)
            {
                NarrativeControlScript.Instance.LoadConversation(idNumber, thingsToActivate[0]);
            }
            else
            {
                NarrativeControlScript.Instance.LoadConversation(idNumber);
            }
        }
        else
        {
            NarrativeControlScript.Instance.LoadConversation(idNumber);
        }

        isInteract = true;

    }

    public override void OtherUpdate()
    {
        if (NarrativeControlScript.Instance.isCompleted_L && isInteract)
        {
            Debug.Log("New Story");
            if (thingsToActivate.Count > 0)
                thingsToActivate.RemoveAt(0);

            if (nextConversations.Count > 0)
            {
                GameObject newGameObject = Instantiate(new GameObject(), this.transform.position, Quaternion.identity);
                newGameObject.AddComponent<ObjectEventTriggerScript>();
                ObjectEventTriggerScript newScript = gameObject.GetComponent<ObjectEventTriggerScript>();
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
                newScript.Interact();
            }
        }
    }

}
