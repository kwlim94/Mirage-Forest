using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemInteractionScript : InteractionScript
{
	[System.Serializable]
	public class ItemsData
	{
		public string name;
		public Image itemImage;
		public int itemID;
	}

	void Start()
	{
		ItemList = new List<ItemsData>();
	}

	public override void Interact ()
	{
		base.Interact ();
	}

	public List<ItemsData> ItemList;
}
