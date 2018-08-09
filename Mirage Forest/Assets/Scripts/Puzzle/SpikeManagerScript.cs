using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeManagerScript : RespawnManagerScript
{
    List<SpikeScript> spikeList;

    void Start()
    {
        spikeList = new List<SpikeScript>();
        for(int i = 0; i < transform.childCount; i++)
        {
            spikeList.Add(transform.GetChild(i).GetComponent<SpikeScript>());
            spikeList[i].spikeManagerScript = this;
        }

    }

}
