using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalSoundManagerScript : MonoBehaviour
{
	bool isWalkingOnWood;
	bool isWalkingOnSoil;
	AudioSource walkingSound;

	public List<AudioClipInfo> AudoClipInfoList;

	void Start()
	{
		walkingSound = gameObject.AddComponent<AudioSource>();
	}

	void Update ()
	{
		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
		{
			if(isWalkingOnSoil)
			{
				walkingSound.clip = FindAudioClip(AudioClipID.SFX_WalkOnSoil);
				if(!walkingSound.isPlaying)
				{
					walkingSound.loop = true;
					walkingSound.Play();
				}
			}
			else if(isWalkingOnWood)
			{
				walkingSound.clip = FindAudioClip(AudioClipID.SFX_WalkOnSoil);
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
		}
		else if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
		{
			walkingSound.Stop();
		}
	}

	AudioClip FindAudioClip(AudioClipID audioClipID)
	{
		for (int i = 0; i < AudoClipInfoList.Count; i++)
		{
			if (audioClipID == AudoClipInfoList[i].audioClipID)
			{
				return AudoClipInfoList[i].audioClip;
			}
		}

		Debug.Log("Audio Clip ID not found");

		return null;
	}

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
