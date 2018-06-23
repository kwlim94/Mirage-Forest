using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryScript : MonoBehaviour
{
	public List<int> inventoryList;
	public Sprite background;
	int pastIndex;
	public static int currentIndex;
	const int maxSize = 5;

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
		currentIndex = 0;
		if(ItemDatabaseScript.Instance != null)
			ItemDatabaseScript.Instance.inventoryImageList[currentIndex].color = Color.red;
	}

	void Update ()
	{
		if(ItemDatabaseScript.Instance != null)
			Select();
	}

	void Select ()
	{
		if(Input.GetKeyDown(KeyControlScript.Instance.navigationLeft.keyboardKey))
		{
			pastIndex = currentIndex;
			if(currentIndex > 0)
				currentIndex--;
			else
				currentIndex = maxSize - 1;
			ItemDatabaseScript.Instance.inventoryImageList[currentIndex].color = Color.red;
			ItemDatabaseScript.Instance.inventoryImageList[pastIndex].color = Color.black;
		}
		else if(Input.GetKeyDown(KeyControlScript.Instance.navigationRight.keyboardKey))
		{
			pastIndex = currentIndex;
			if(currentIndex < maxSize - 1)
				currentIndex++;
			else
				currentIndex = 0;
			ItemDatabaseScript.Instance.inventoryImageList[currentIndex].color = Color.red;
			ItemDatabaseScript.Instance.inventoryImageList[pastIndex].color = Color.black;
		}
	}

	public void AddItem (int idNum)
	{
		inventoryList.Add(idNum);
		InventoryUpdate ();
	}

	public void RemoveItem (int element)
	{
		inventoryList.RemoveAt(element);
		InventoryUpdate();
	}

	public void InventoryUpdate ()
	{
		for(int i = 0; i < inventoryList.Count; i++)
		{
			for(int j = 0; j < ItemDatabaseScript.Instance.ItemList.Count; j++)
			{
				if(inventoryList[i] == ItemDatabaseScript.Instance.ItemList[j].itemID)
				{
					ItemDatabaseScript.Instance.inventoryImageList[i].transform.GetChild(0).GetComponent<Image>().sprite = ItemDatabaseScript.Instance.ItemList[j].itemImage;
					break;
				}
			}
		}

		for(int i = 0; i < 5 - inventoryList.Count; i++)
		{
			ItemDatabaseScript.Instance.inventoryImageList[inventoryList.Count + i].transform.GetChild(0).GetComponent<Image>().sprite = background;
		}
		ItemDatabaseScript.Instance.inventoryImageList[currentIndex].color = Color.red;
	}
}


