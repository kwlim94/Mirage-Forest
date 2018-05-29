using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectEventTriggerScript: InteractionScript
{
	public GameObject ThingsToActivate;

	public override void OtherStart()
	{
		ThingsToActivate.SetActive(false);
	}

	public override void Interact ()
	{
		base.Interact ();
		ThingsToActivate.SetActive(true);
		NarrativeControlScript.Instance.LoadConversation(idNumber, ThingsToActivate);
	}
}
