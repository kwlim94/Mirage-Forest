﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritInteractionScript : InteractionScript
{

	public override void Interact ()
	{
		base.Interact ();
		Debug.Log ("Interacting with spirit");
	}


}
