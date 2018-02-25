using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventTriggerScript : MonoBehaviour
{
	public int idNumber;

	void Start ()
	{
		GetComponent<MeshRenderer>().enabled = false;
	}
		
	void OnTriggerEnter (Collider col)
	{
		if(col.tag == "Player")
		{
			NarrativeControlScript.Instance.LoadConversation (idNumber);
			gameObject.SetActive(false);
		}
	}
}
