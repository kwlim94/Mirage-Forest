using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//! This is just a stupid script of ridiculous hard code
public class EndConCheck : MonoBehaviour
{

    int num = 0;
    public GameObject endConImage;

    public static EndConCheck Instance { get; set; }

    void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if(num == 2)
        {
            endConImage.SetActive(true);
        }
    }

    public void PlusPlus()
    {
        num++;
    }
}
