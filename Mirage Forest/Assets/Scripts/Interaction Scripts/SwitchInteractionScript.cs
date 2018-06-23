using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum OnOffState
{
	OFF = 0,
	ON = 1,
}

[System.Serializable]
public class ItemOnOffState
{
	public int idNumber;
	public OnOffState onOffState;
	public GameObject thingsToActivate;
	public GameObject thingsToDeactivate;
}

public class SwitchInteractionScript : InteractionScript
{
	OnOffState globalOnOffState;
	public List<GameObject> globalThingsToActivate;
	public List<GameObject> globalThingsToDeactivate;
	public List<ItemOnOffState> itemOnOffStateList;

	bool firstTime;
	public int storyIdBeforeActivate;
	public int storyIdConditionNotMet;
	public int storyIdAfterActivate;

	public override void OtherStart()
	{
		firstTime = true;
	}

	public override void Interact ()
	{
		base.Interact ();

		//! Turning on the switch (There is two ways to turn on the switch)
		if(globalOnOffState == OnOffState.OFF)
		{
			//! When there is NO CONDITIONS needed to be met to turn on the switch
			if(itemOnOffStateList.Count == 0)
			{
				globalOnOffState = OnOffState.ON;

				if(globalThingsToDeactivate.Count > 0)
				{
					for (int i = 0; i < globalThingsToDeactivate.Count; i++)
					{
						globalThingsToDeactivate[i].SetActive(false);
					}
				}
					
				if(globalThingsToActivate != null)
				{
					for (int i = 0; i < globalThingsToActivate.Count; i++)
					{
						globalThingsToActivate[i].SetActive(true	);
					}
				}
			}

			//! When there is conditions needed to be met to turn on the switch
			else
			{
				//! Turning on a particular switch
				for(int i = 0; i < itemOnOffStateList.Count; i++)
				{
					if(InventoryScript.Instance.inventoryList[InventoryScript.currentIndex] == itemOnOffStateList[i].idNumber)
					{
						itemOnOffStateList[i].onOffState = OnOffState.ON;

						if(itemOnOffStateList[i].thingsToDeactivate != null)
							itemOnOffStateList[i].thingsToDeactivate.SetActive(false);

						if(itemOnOffStateList[i].thingsToActivate != null)
							itemOnOffStateList[i].thingsToActivate.SetActive(true);

						InventoryScript.Instance.RemoveItem(InventoryScript.currentIndex);
						break;
					}
				}

				globalOnOffState = OnOffState.ON;

				//! Checking if all the elements in ON state
				for(int i = 0; i < itemOnOffStateList.Count; i++)
				{
					if(itemOnOffStateList[i].onOffState == OnOffState.OFF)
					{
						globalOnOffState = OnOffState.OFF;
					}
				}

				//! if all the conditions is met, then switch will turn on
				if(globalOnOffState == OnOffState.ON)
				{
					if(globalThingsToDeactivate.Count > 0)
					{
						for (int i = 0; i < globalThingsToDeactivate.Count; i++)
						{
							globalThingsToDeactivate[i].SetActive(false);
						}
					}

					if(globalThingsToActivate != null)
					{
						for (int i = 0; i < globalThingsToActivate.Count; i++)
						{
							globalThingsToActivate[i].SetActive(true);
						}
					}
				}
			}
		}

		//! Turning ofF the switch (Only available if there is no conditions to be met
		else if(globalOnOffState == OnOffState.ON)
		{
			if(itemOnOffStateList.Count == 0)
			{
				globalOnOffState = OnOffState.OFF;

				if(globalThingsToDeactivate.Count > 0)
				{
					for (int i = 0; i < globalThingsToDeactivate.Count; i++)
					{
						globalThingsToDeactivate[i].SetActive(true);
					}
				}

				if(globalThingsToActivate != null)
				{
					for (int i = 0; i < globalThingsToActivate.Count; i++)
					{
						globalThingsToActivate[i].SetActive(false);
					}
				}
			}
		}
			
		LoadStory();
	}

	void LoadStory()
	{
		if(globalOnOffState == OnOffState.OFF)
		{
			if(firstTime)
			{
				if(storyIdBeforeActivate != 0)
					NarrativeControlScript.Instance.LoadConversation(storyIdBeforeActivate);
				firstTime = false;
			}
			else
			{
				if(storyIdConditionNotMet != 0)
					NarrativeControlScript.Instance.LoadConversation(storyIdConditionNotMet);
			}
		}
		else if(globalOnOffState == OnOffState.ON)
		{
			if(storyIdAfterActivate != 0)
				NarrativeControlScript.Instance.LoadConversation(storyIdAfterActivate);
			
		}
	}

}
