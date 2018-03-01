using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
	public List<int> inventoryList;
	public List<Image> inventoryImageList;
	public Sprite background;

	public static InventoryScript Instance {get; set;}

	void Awake()
	{
		if(Instance!= null && Instance != this)
			Destroy(gameObject);
		else
			Instance = this;
	}

	void Start ()
	{
		inventoryList = new List<int> ();
	}

	public void AddItem (int idNum)
	{
		inventoryList.Add(idNum);
		InventoryUpdate ();
	}

	public void RemoveItem (int element)
	{
		inventoryList.Remove(element);
		InventoryUpdate();
	}

	void InventoryUpdate ()
	{
		for(int i = 0; i < inventoryList.Count; i++)
		{
			for(int j = 0; j < ItemDatabaseScript.Instance.ItemList.Count; j++)
			{
				if(inventoryList[i] == ItemDatabaseScript.Instance.ItemList[j].itemID)
				{
					inventoryImageList[i].transform.GetChild(0).GetComponent<Image>().sprite = ItemDatabaseScript.Instance.ItemList[j].itemImage;
					break;
				}
			}
		}

		for(int i = 0; i < 5 - inventoryList.Count; i++)
		{
			inventoryImageList[inventoryList.Count + i].transform.GetChild(0).GetComponent<Image>().sprite = background;
		}
	}
}
