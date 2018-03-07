using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchInteractionScript : InteractionScript
{
	
	public enum SwitchOnOff
	{
		ON = 0,
		OFF,
	}

	public SwitchOnOff switchonoff;

	public override void Interact ()
	{
		

		base.Interact ();

		if(switchonoff == SwitchOnOff.OFF)
		{
			switchonoff = SwitchOnOff.ON;
		}
		else if(switchonoff == SwitchOnOff.ON)
		{
			switchonoff = SwitchOnOff.OFF;
		}


	}


}
