using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionScript : MonoBehaviour
{
	public GameObject player;
	public bool isInteractable;
	public int idNumber;

	void Start ()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		isInteractable = false;
	}

	void Update ()
	{
		if(isInteractable && Input.GetKeyDown(KeyCode.E))
		{
			Interact ();
		}
	}

	public virtual void Interact ()
	{
		Debug.Log ("Interact on the base class");
		player.GetComponent<CharacterControlScript>().interactImage.SetActive(false);
		Destroy(gameObject);
	}

}
