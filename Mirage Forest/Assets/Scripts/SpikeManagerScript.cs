using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeManagerScript : RespawnManagerScript
{
    public static SpikeManagerScript Instance { get; set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }
}
