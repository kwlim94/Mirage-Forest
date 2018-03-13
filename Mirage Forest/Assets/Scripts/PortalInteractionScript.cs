using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalInteractionScript : InteractionScript
{
	public override void Interact ()
	{
		base.Interact ();
		SceneManagerScript.Instance.LoadScene(idNumber);
	}
}
