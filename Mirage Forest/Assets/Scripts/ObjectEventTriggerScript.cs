using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectEventTriggerScript: InteractionScript
{
	public GameObject ThingsToActivate;

	public override void Interact ()
	{
		base.Interact ();
		ThingsToActivate.SetActive(true);
	}
}
