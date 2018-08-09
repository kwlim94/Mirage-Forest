using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeScript : MonoBehaviour
{
    public float interval;
    public float startTime;
    float intervalCount;
    float risingInterval;
    Vector3 initialLocation;
    public SpikeManagerScript spikeManagerScript;

    void Start()
    {
        intervalCount = startTime;
        initialLocation = transform.position;
        risingInterval = 1.0f / 0.5f;
    }

    void Update()
    {
        if(intervalCount > interval && intervalCount < interval + 0.5f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + risingInterval * Time.deltaTime, transform.position.z);
        }
        else if(intervalCount > interval + 0.5f && intervalCount < interval + 1.0f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - risingInterval * Time.deltaTime, transform.position.z);
        }
        else if (intervalCount > interval + 1.0f)
        {
            transform.position = initialLocation;
            intervalCount = 0.0f;
        }
        intervalCount += Time.deltaTime;
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Player")
        {
            print("spike");
            spikeManagerScript.Respawn(1.0f);
        }
    }
}
