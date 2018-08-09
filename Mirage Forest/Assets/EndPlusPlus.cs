using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPlusPlus : MonoBehaviour
{
    bool isInteracted = false;

    void Start()
    {
        GetComponent<MeshRenderer>().enabled = false;
    }

	void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player" && !isInteracted)
        {
            EndConCheck.Instance.PlusPlus();
            isInteracted = true;
        }
    }
}
