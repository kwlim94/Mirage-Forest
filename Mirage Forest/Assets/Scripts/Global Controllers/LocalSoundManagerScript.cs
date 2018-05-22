using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalSoundManagerScript : MonoBehaviour
{
	bool isWalkingOnWood;
	bool isWalkingOnSoil;

	void Update ()
	{
		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
		{
			if(isWalkingOnSoil)
			{
				GlobalSoundManagerScript.Instance.AddLoopingSound(0.0f, AudioClipID.SFX_WalkOnWood, 2); //HT Temp
			}
			else if(isWalkingOnWood)
			{
				GlobalSoundManagerScript.Instance.AddLoopingSound(0.0f, AudioClipID.SFX_WalkOnWood, 2);
			}
		}
		else
		{
			if(isWalkingOnSoil)
			{
				GlobalSoundManagerScript.Instance.StopLoopingSound(0.0f,  AudioClipID.SFX_WalkOnWood); //HT Temp
			}
			else if(isWalkingOnWood)
			{
				GlobalSoundManagerScript.Instance.StopLoopingSound(0.0f,  AudioClipID.SFX_WalkOnWood);
			}
		}
	}

	void OnCollisionEnter(Collision col)
	{
		if(col.transform.tag == "Ground_Soil")
		{
			isWalkingOnWood = false;
			isWalkingOnSoil = true;
		}
		else if(col.transform.tag == "Ground_Wood")
		{
			isWalkingOnWood = true;
			isWalkingOnSoil = false;
		}
	}

	void OnCollisionExit(Collision col)
	{
		if(col.transform.tag == "Ground_Soil")
		{
			isWalkingOnSoil = false;
			if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
				GlobalSoundManagerScript.Instance.StopLoopingSound(0.0f,  AudioClipID.SFX_WalkOnWood); //HT Temp
		}
		else if(col.transform.tag == "Ground_Wood")
		{
			isWalkingOnWood = false;
			if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
				GlobalSoundManagerScript.Instance.StopLoopingSound(0.0f,  AudioClipID.SFX_WalkOnWood);
		}
	}

}
