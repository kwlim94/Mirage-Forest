using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionScript : MonoBehaviour
{
	GameObject player;
	public bool isInteractable;

	void Start ()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		isInteractable = false;
	}

	void Update ()
	{
		CheckInRange ();
	}

	void CheckInRange ()
	{
		if(Vector3.Distance(player.transform.position, this.transform.position) <= 1.0f)
		{
			isInteractable = true;
		}
		else
		{
			isInteractable = false;
		}
	}

	public virtual void Interact ()
	{
		
	}

}
