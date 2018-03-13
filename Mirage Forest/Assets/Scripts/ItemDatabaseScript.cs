using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType
{
	Usable= 0,
	Collectable,
}

public class ItemDatabaseScript : MonoBehaviour
{

	[System.Serializable]
	public class Item
	{
		public string name;
		public Sprite itemImage;
		public int itemID;
		public ItemType itemType;
	}

	public static ItemDatabaseScript Instance {get; set;}
	public List<Image> inventoryImageList;

	void Awake()
	{
		if(Instance!= null && Instance != this)
			Destroy(gameObject);
		else
			Instance = this;
	}

	public List<Item> ItemList;
}
