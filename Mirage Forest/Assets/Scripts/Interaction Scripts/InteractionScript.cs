using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionScript : MonoBehaviour
{
	public GameObject player;
	public bool isInteractable;
	public int idNumber;
    public bool isCompleted;
    public bool isInteracted;

    void Start ()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		isInteractable = false;
        isCompleted = false;
        isInteracted = false;
        OtherStart();
	}

	void Update ()
	{
		if(isInteractable && Input.GetKeyDown(KeyControlScript.Instance.interactKey.keyboardKey) && !isInteracted)
		{
            isInteracted = true;
            CharacterControlScript.Instance.interactImage.SetActive(false);
			Interact ();
		}

        OtherUpdate();
	}

	public virtual void OtherStart(){}
    public virtual void OtherUpdate(){}

	public virtual void Interact ()
	{
		Debug.Log ("Interact on the base class");
		player.GetComponent<CharacterControlScript>().interactImage.SetActive(false);
	}

}
