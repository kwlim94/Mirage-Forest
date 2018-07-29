using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresurePlateScript : MonoBehaviour
{
    Vector3 initialLocation;
    Vector3 pressedLocation;
    bool isEntered;
    float time;

    void Start()
    {
        InitialSetup();
    }

    void Update()
    {
        time += Time.deltaTime;
    }

    void InitialSetup()
    {
        initialLocation = transform.position;
        pressedLocation = new Vector3(initialLocation.x, initialLocation.y - 0.09f, initialLocation.z);
        isEntered = false;
    }

    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player" && !isEntered)
        {
            PressurePlateManagerScript.Instance.SequenceCheck(transform.name);
            transform.position = pressedLocation;
            isEntered = true;
            time = 0.0f;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player" && time > 0.1f)
        {
            transform.position = initialLocation;
            isEntered = false;
        }
    }
}
