using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeRandomizerScript : MonoBehaviour
{
	public List<GameObject> treeList;
	List<GameObject> spawedTreeList;
	public int amountOfTrees;
	Vector3 randomPosition;
	int size = 15;

	void Start ()
	{
		spawedTreeList = new List<GameObject>();
		for(int i = 0; i < amountOfTrees; i++)
		{
			RandomizePosition();
			GameObject randomTree = treeList[Random.Range(0, treeList.Count)];
			spawedTreeList.Add(Instantiate(randomTree, randomPosition, Quaternion.identity));
		}
	}

	//! Randomizes a random vector3 for the tree to spawn
	void RandomizePosition()
	{
		randomPosition = new Vector3(this.transform.position.x + Random.Range(-size/2, size/2), 5.0f, this.transform.position.z + Random.Range(-size/2, size/2));

//		for(int j = 0; j < spawedTreeList.Count; j++)
//		{
//			if(Vector3.Distance(spawedTreeList[j].transform.position, randomPosition) < 1.0f)
//			{
//				RandomizePosition();
//				break;
//			}
//		}
	}
}
