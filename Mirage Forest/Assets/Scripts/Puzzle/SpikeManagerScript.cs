using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeManagerScript : RespawnManagerScript
{
    public List<SpikeScript> spikeList;

    void Start()
    {
        spikeList = new List<SpikeScript>();
        for(int i = 0; i < transform.childCount; i++)
        {
            spikeList.Add(transform.GetChild(i).GetComponent<SpikeScript>());
        }

        spikeList.Remove(spikeList[transform.childCount - 1]);

        for (int j = 0; j < transform.childCount; j++)
        {
            spikeList[j].spikeManagerScript = gameObject.GetComponent<SpikeManagerScript>();
        }
    }

}
