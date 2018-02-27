using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProlougeEventScript : MonoBehaviour {

	void Start ()
	{
		NarrativeControlScript.Instance.LoadConversation (100011);
	}

	void Update ()
	{
		
	}
}
