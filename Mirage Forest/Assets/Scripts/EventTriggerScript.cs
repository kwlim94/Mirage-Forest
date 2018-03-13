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

	void Start ()
	{
		//GetComponent<MeshRenderer>().enabled = false;
	}
		
	void OnTriggerEnter (Collider col)
	{
		if(col.tag == "Player")
		{
			if(triggerType == TriggerType.STORY)
			{
				NarrativeControlScript.Instance.LoadConversation (idNumber);
				gameObject.SetActive(false);
			}
		}
	}
}
