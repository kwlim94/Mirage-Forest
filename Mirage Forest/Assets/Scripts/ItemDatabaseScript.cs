using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDatabaseScript : MonoBehaviour {

	[System.Serializable]
	public class ItemsData
	{
		public string name;
		public Image itemImage;
		public int itemID;
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public List<ItemsData> ItemList;
}
