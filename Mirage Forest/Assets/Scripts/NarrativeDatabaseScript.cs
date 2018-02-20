using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
	public string name;
	public int chracterID;
	public string speech;
}

[System.Serializable]
public class NarrativeDatabase
{
	public string name;
	public int IdNumber;
	public string Description; //HT For refrence usage only
	public List<Dialogue> DialogueList;
}

public class NarrativeDatabaseScript : MonoBehaviour
{
	public static NarrativeDatabaseScript Instance {get; set;}

	void Awake()
	{
		if(Instance!= null && Instance != this)
			Destroy(gameObject);
		else
			Instance = this;
	}

	public List<NarrativeDatabase> NarrativeDatabaseList;
}