using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInteractionScript : InteractionScript
{

	public int idNumber;

	public override void Interact ()
	{
		base.Interact ();

		player.GetComponent<InventoryScript>().itemInHand[idNumber - 1] = true;

		Image mySprite = GetComponent<Image>();

		mySprite = player.GetComponent<InventoryScript>().ItemSprite[idNumber - 1].GetComponent<Image>();

		mySprite.sprite = player.GetComponent<InventoryScript>().ItemList[idNumber - 1].itemImage;
	}

}
