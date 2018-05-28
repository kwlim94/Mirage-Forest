using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalSoundManagerScript : MonoBehaviour
{
	//! variables
	bool isWalkingOnWood;
	bool isWalkingOnSoil;
	AudioSource walkingSound;

	void Start()
	{
		walkingSound = gameObject.AddComponent<AudioSource>();
		GlobalSoundManagerScript.Instance.SFXLoopingSoundsList.Add(walkingSound);
	}

	void Update ()
	{
		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
		{
			if(isWalkingOnSoil)
			{
				walkingSound.clip = GlobalSoundManagerScript.Instance.FindAudioClip(AudioClipID.SFX_WalkOnSoil);
				if(!walkingSound.isPlaying)
				{
					walkingSound.loop = true;
					walkingSound.Play();
				}
			}
			else if(isWalkingOnWood)
			{
				walkingSound.clip = GlobalSoundManagerScript.Instance.FindAudioClip(AudioClipID.SFX_WalkOnSoil);
				if(!walkingSound.isPlaying)
				{
					walkingSound.loop = true;
					walkingSound.Play();
				}
			}
			else
			{
				walkingSound.Stop();
			}

			if(Input.GetKey(KeyCode.LeftShift))
			{
				walkingSound.pitch = 0.75f;
			}
			else
			{
				walkingSound.pitch = 1.6f;
			}
		}
		else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
		{
			walkingSound.Stop();
		}
	}

	/*
	==================================================
				   COLLISION DETECTION
	==================================================
	*/

	void OnCollisionStay(Collision col)
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
		}
		else if(col.transform.tag == "Ground_Wood")
		{
			isWalkingOnWood = false;
		}
	}

}
