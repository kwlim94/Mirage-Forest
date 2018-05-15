using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSoundManagerScript : MonoBehaviour
{
	public static SceneManagerScript Instance {get; set;}

	void Awake()
	{
		if(Instance!= null && Instance != this)
			Destroy(gameObject);
		else
			Instance = this;
		DontDestroyOnLoad(gameObject);
	}

	void Start ()
	{

	}

	void Update ()
	{

	}

	public void PlaySound();
	public void AddLoopingSound(float fadeInDuration);
	public void StopLoopingSound(float fadeOutDuration);
	public void DeleteAllSounds(float fadeOutDuration);
}
