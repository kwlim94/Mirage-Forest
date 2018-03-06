using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionScript : MonoBehaviour
{
	public GameObject player;
	public GameObject Obj;
	public bool isInteractable;
	public bool InteractableObj;
	public int idNumber;

	void Start ()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		Obj = GameObject.FindGameObjectWithTag("InterObj");
		InteractableObj = false;
		isInteractable = false;
	}

	void Update ()
	{
		if(isInteractable && InteractableObj && Input.GetKeyDown(KeyCode.E))
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
