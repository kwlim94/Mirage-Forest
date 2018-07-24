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
        initialLocation = transform.position;
        pressedLocation = new Vector3(initialLocation.x, initialLocation.y - 0.09f, initialLocation.z);
        isEntered = false;
    }

    void Update()
    {
        time += Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" && !isEntered)
        {
            PressurePlateManagerScript.Instance.SequenceCheck(transform.name);
            transform.position = pressedLocation;
            isEntered = true;
            time = 0.0f;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && time > 0.1f)
        {
            transform.position = initialLocation;
            isEntered = false;
        }
    }
}
