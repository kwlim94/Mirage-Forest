using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowScript : MonoBehaviour
{
	public GameObject target;
	public float damping = 1;
	Vector3 offset;

	void Start()
	{
		
		offset = target.transform.position - transform.position;
	}

//	void LateUpdate() {
//		float currentAngle = transform.eulerAngles.y;
//		float desiredAngle = target.transform.eulerAngles.y;
//		float angle = Mathf.LerpAngle(currentAngle, desiredAngle, Time.deltaTime * damping);
//
//		Quaternion rotation = Quaternion.Euler(0, angle, 0);
//		transform.position = target.transform.position - (rotation * offset);
//		//transform.eulerAngles = new Vector3(15.0f, rotation.y, 0.0f);
//
//		transform.LookAt(target.transform, new Vector3(0.5f, 0.0f, 0.0f));
//	}

	void LateUpdate()
	{
		transform.position = target.transform.position - offset;
	}
}
