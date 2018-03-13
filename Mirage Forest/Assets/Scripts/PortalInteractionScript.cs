using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum PortalState
{
	ON = 0,
	OFF,
}

public class PortalInteractionScript : InteractionScript
{
	PortalState portalState = PortalState.OFF;

	public override void Interact ()
	{
		base.Interact ();
		if(portalState == PortalState.ON)
			SceneManagerScript.Instance.LoadScene(idNumber);
		else
		{
			if(InventoryScript.Instance.inventoryList.Count > 0 && InventoryScript.Instance.inventoryList[InventoryScript.currentIndex] == 1)
			{
				NarrativeControlScript.Instance.LoadConversation(100063);
				transform.GetChild(1).gameObject.SetActive(true);
				portalState = PortalState.ON;
			}
			else
			{
				NarrativeControlScript.Instance.LoadConversation(100062);
			}
		}
	}
}
