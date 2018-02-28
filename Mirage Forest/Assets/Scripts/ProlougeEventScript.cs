using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EventSequence
{
	LOOKING_AROUND = 0,
	MOVE_AROUND,
}

public class ProlougeEventScript : MonoBehaviour {

	EventSequence eventSequence;

	void Start ()
	{
		eventSequence = EventSequence.LOOKING_AROUND;
		NarrativeControlScript.Instance.LoadConversation (100011);
	}

	void Update ()
	{
		
	}
}
