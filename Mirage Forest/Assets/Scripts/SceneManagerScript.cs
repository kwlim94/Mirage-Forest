using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
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

	public void LoadScene (int index)
	{
		switch(index)
		{
		case 0:
			SceneManager.LoadScene("Prologue");
			break;
		case 999:
			SceneManager.LoadScene("TestScene");
			break;
		}
	}
}
