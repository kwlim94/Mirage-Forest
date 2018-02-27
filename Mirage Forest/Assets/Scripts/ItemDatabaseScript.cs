using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class ItemDatabaseScript : MonoBehaviour {

	[System.Serializable]
	public class Item
	{
		public string name;
		public Sprite itemImage;
		public int itemID;
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static ItemDatabaseScript Instance {get; set;}

	void Awake()
	{
		if(Instance!= null && Instance != this)
			Destroy(gameObject);
		else
			Instance = this;
	}

	public List<Item> ItemList;
}
