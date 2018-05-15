using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInteractionScript : InteractionScript
{

	public override void Interact ()
	{
		base.Interact ();
		InventoryScript.Instance.AddItem(idNumber);
		Destroy(gameObject);
	}

}
