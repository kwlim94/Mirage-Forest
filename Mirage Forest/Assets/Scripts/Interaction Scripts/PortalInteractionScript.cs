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
			int currentIndex = InventoryScript.currentIndex;
			if(currentIndex < InventoryScript.Instance.inventoryList.Count)
			{
				if(InventoryScript.Instance.inventoryList[currentIndex] == 1)
				{
					NarrativeControlScript.Instance.LoadConversation(100063, ref isCompleted);
					transform.GetChild(1).gameObject.SetActive(true);
					portalState = PortalState.ON;
					InventoryScript.Instance.RemoveItem(currentIndex);
				}
				else
				{
					NarrativeControlScript.Instance.LoadConversation(100062, ref isCompleted);
				}
			}
			else
			{
				NarrativeControlScript.Instance.LoadConversation(100062, ref isCompleted);
			}
		}
	}
}
