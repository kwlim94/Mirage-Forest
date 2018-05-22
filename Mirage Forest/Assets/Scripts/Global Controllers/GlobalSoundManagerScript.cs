using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AudioClipID
{
	BGM_Forest = 101,

	SFX_WalkOnSoil = 201,
	SFX_WalkOnWood,
}

[System.Serializable]
public class AudioClipInfo
{
	public AudioClipID audioClipID;
	public AudioClip audioClip;
}

public class GlobalSoundManagerScript : MonoBehaviour
{
	public static GlobalSoundManagerScript Instance {get; set;}

	void Awake()
	{
		if(Instance!= null && Instance != this)
			Destroy(gameObject);
		else
			Instance = this;
		DontDestroyOnLoad(gameObject);
	}

	public List<AudioClipInfo> AudoClipInfoList;
	public AudioSource BGMAudioSource;
	public List<AudioSource> LoopingSoundsList = new List<AudioSource>();

	void Start()
	{
		AddLoopingSound(0.0f, AudioClipID.BGM_Forest, 1);
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

	public void PlaySound(AudioClipID audioClipID)
	{
		BGMAudioSource.PlayOneShot(FindAudioClip(audioClipID));
		BGMAudioSource.loop = false;
		BGMAudioSource.Play();
	}

	//! audio type: 1 for BGM 2 for SFX
	public void AddLoopingSound(float fadeInDuration, AudioClipID audioClipID, int audioType)
	{
		AudioClip clipToPlay = FindAudioClip(audioClipID);

		if(audioType == 1)
		{
			BGMAudioSource.clip = clipToPlay;
			BGMAudioSource.loop = true;
			BGMAudioSource.Play();
		}
		else
		{
			for(int i = 0; i < LoopingSoundsList.Count; i++)
			{
				if(LoopingSoundsList[i].clip == clipToPlay)
				{
					if(!LoopingSoundsList[i].isPlaying)
					{
						LoopingSoundsList[i].Play();
					}
					return;
				}
			}

			AudioSource newAudio = BGMAudioSource.gameObject.AddComponent<AudioSource>();
			newAudio.clip = clipToPlay;
			newAudio.loop = true;
			newAudio.Play();
			LoopingSoundsList.Add(newAudio);
		}
	}

	public void StopLoopingSound(float fadeOutDuration, AudioClipID audioClipID)
	{
		AudioClip clipToStop = FindAudioClip(audioClipID);

		if(clipToStop == null)
		{
			return;
		}

		for(int i = 0; i < LoopingSoundsList.Count; i++)
		{
			if(LoopingSoundsList[i].clip == clipToStop)
			{
				LoopingSoundsList[i].Stop();
			}
		}
	}
}
