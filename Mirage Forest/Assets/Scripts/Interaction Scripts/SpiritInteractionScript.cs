using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritInteractionScript : InteractionScript
{
	public override void Interact ()
	{
		base.Interact ();
		NarrativeControlScript.Instance.LoadConversation(idNumber, gameObject);
	}
}
