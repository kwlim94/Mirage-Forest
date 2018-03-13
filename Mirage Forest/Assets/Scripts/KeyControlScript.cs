using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InputKey
{
	public KeyCode keyboardKey;
	public string buttonKey;

}

public class KeyControlScript : MonoBehaviour
{
	public static KeyControlScript Instance {get; set;}

	void Awake()
	{
		if(Instance!= null && Instance != this)
			Destroy(gameObject);
		else
			Instance = this;
		DontDestroyOnLoad(gameObject);
	}

	public InputKey interactKey;
}
