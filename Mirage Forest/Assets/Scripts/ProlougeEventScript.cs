using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EventSequence
{
	LOOKING_AROUND = 0,
	MOVE_AROUND,
}

public class ProlougeEventScript : MonoBehaviour {

    //EventSequence eventSequence;
    bool isCompleted;

	void Start ()
	{
		//eventSequence = EventSequence.LOOKING_AROUND;
		InventoryScript.Instance.InventoryUpdate ();
		NarrativeControlScript.Instance.LoadConversation (100011, ref isCompleted);
	}

	void Update ()
	{
		
	}
}
