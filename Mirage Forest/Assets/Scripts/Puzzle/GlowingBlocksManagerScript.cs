using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowingBlocksManagerScript : RespawnManagerScript
{
    public static GlowingBlocksManagerScript Instance { get; set; }

    void Awake ()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }

    
}
