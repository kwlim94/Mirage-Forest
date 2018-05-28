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

	//! Lists
	public List<AudioClipInfo> AudoClipInfoList;
	public List<AudioSource> BGMLoopingSoundsList = new List<AudioSource>();
	public List<AudioSource> SFXLoopingSoundsList = new List<AudioSource>();

	//! Variables
	public AudioSource BGMAudioSource;
	public AudioSource SFXAudioSource;
	public float bgmVolume;
	public float sfxVolume;

	void Start()
	{
		bgmVolume = 0.5f;
		sfxVolume = 0.5f;
		AddLoopingSound(0.0f, AudioClipID.BGM_Forest, 1);
	}

	public AudioClip FindAudioClip(AudioClipID audioClipID)
	{
		for (int i = 0; i < AudoClipInfoList.Count; i++)
		{
			if (audioClipID == AudoClipInfoList[i].audioClipID)
			{
				return AudoClipInfoList[i].audioClip;
			}
		}

		Debug.LogError("Audio Clip ID not found (Global)");

		return null;
	}

	/*
	==================================================
					AUDIO PLAYBACK
	==================================================
	*/


	//! audio type: 1 for BGM 2 for SFX
	public void PlaySound(AudioClipID audioClipID, int audioType)
	{
		if(audioType == 1)
		{
			BGMAudioSource.PlayOneShot(FindAudioClip(audioClipID),bgmVolume);
			BGMAudioSource.loop = false;
		}
		else
		{
			SFXAudioSource.PlayOneShot(FindAudioClip(audioClipID),sfxVolume);
			SFXAudioSource.loop = false;
		}
	}

	//! audio type: 1 for BGM 2 for SFX
	public void AddLoopingSound(float fadeInDuration, AudioClipID audioClipID, int audioType)
	{
		AudioClip clipToPlay = FindAudioClip(audioClipID);

		if(audioType == 1)
		{
			for(int i = 0; i < BGMLoopingSoundsList.Count; i++)
			{
				if(BGMLoopingSoundsList[i].clip == clipToPlay)
				{
					if(!BGMLoopingSoundsList[i].isPlaying)
					{
						BGMLoopingSoundsList[i].Play();
					}
					return;
				}
			}

			AudioSource newAudio = BGMAudioSource.gameObject.AddComponent<AudioSource>();
			newAudio.clip = clipToPlay;
			newAudio.loop = true;
			newAudio.Play();
			newAudio.volume = bgmVolume;
			BGMLoopingSoundsList.Add(newAudio);
		}
		else
		{
			for(int i = 0; i < SFXLoopingSoundsList.Count; i++)
			{
				if(SFXLoopingSoundsList[i].clip == clipToPlay)
				{
					if(!SFXLoopingSoundsList[i].isPlaying)
					{
						SFXLoopingSoundsList[i].Play();
					}
					return;
				}
			}

			AudioSource newAudio = SFXAudioSource.gameObject.AddComponent<AudioSource>();
			newAudio.clip = clipToPlay;
			newAudio.loop = true;
			newAudio.Play();
			newAudio.volume = sfxVolume;
			SFXLoopingSoundsList.Add(newAudio);
		}
	}

	//! audio type: 1 for BGM 2 for SFX
	public void StopLoopingSound(float fadeOutDuration, AudioClipID audioClipID, int audioType)
	{
		AudioClip clipToStop = FindAudioClip(audioClipID);

		if(clipToStop == null)
		{
			return;
		}

		if(audioType == 1)
		{
			for(int i = 0; i < BGMLoopingSoundsList.Count; i++)
			{
				if(BGMLoopingSoundsList[i].clip == clipToStop)
				{
					BGMLoopingSoundsList[i].Stop();
					break;
				}
			}
		}
		else
		{
			for(int i = 0; i < SFXLoopingSoundsList.Count; i++)
			{
				if(SFXLoopingSoundsList[i].clip == clipToStop)
				{
					SFXLoopingSoundsList[i].Stop();
					break;
				}
			}
		}
	}

	/*
	==================================================
					VOLUME CONTROL
	==================================================
	*/

	public void BGMVolumeControl(float value)
	{
		bgmVolume = value;
		BGMAudioSource.volume = bgmVolume;
		for(int i = 0; i < BGMLoopingSoundsList.Count; i++)
		{
			BGMLoopingSoundsList[i].volume = bgmVolume;
		}
	}

	public void SFXVolumeControl(float value)
	{
		sfxVolume = value;
		SFXAudioSource.volume = sfxVolume;
		for(int i = 0; i < SFXLoopingSoundsList.Count; i++)
		{
			SFXLoopingSoundsList[i].volume = sfxVolume;
		}
	}
}
