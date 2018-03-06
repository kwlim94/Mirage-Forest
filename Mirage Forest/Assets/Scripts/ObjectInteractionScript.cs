using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInteractionScript : InteractionScript
{

	void Update()
	{
		base.Interact ();
		//InteractObject ();
	}
		

	void InteractObject (Collider act)
		{
		print("Interactable!");
		if(act.transform.tag == "InterObj")
			{
				player.GetComponent<CharacterControlScript>().interactImage.SetActive(true);
				act.GetComponent<InteractionScript>().InteractableObj = true;
				this.gameObject.transform.Rotate(0.0f, 90.0f, 0.0f);
			}
			else
			{
				player.GetComponent<CharacterControlScript>().interactImage.SetActive(false);
			}
			
		}
}
