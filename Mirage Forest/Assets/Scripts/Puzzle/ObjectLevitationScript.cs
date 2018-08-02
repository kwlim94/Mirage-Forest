using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLevitationScript : MonoBehaviour
{

	public float x_Amplitude, y_Amplitude, z_Amplitide;
	public float x_Speed, y_Speed, z_Speed;

	Vector3 startingPosition;
	float x_Angle, y_Angle, z_Angle;
	GameObject player;

	void Start ()
	{
		startingPosition = transform.position;
		player = GameObject.FindGameObjectWithTag("Player");
	}

	void Update ()
	{
		transform.position = startingPosition + new Vector3 (Mathf.Sin(x_Angle) * x_Amplitude, Mathf.Sin(y_Angle) * y_Amplitude, Mathf.Sin(z_Angle) * z_Amplitide);

		x_Angle += x_Speed * Time.deltaTime;
		y_Angle += y_Speed * Time.deltaTime;
		z_Angle += z_Speed * Time.deltaTime;

		AngleReset(ref x_Angle);
		AngleReset(ref y_Angle);
		AngleReset(ref z_Angle);
	}

	void AngleReset(ref float angle)
	{
		if(angle >= Mathf.PI * 2)
		{
			angle = 0.0f;
		}
	}

	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.tag == "Player")
		{
			player.transform.parent = this.transform;
		}
	}

	void OnCollisionExit(Collision col)
	{
		if(col.gameObject.tag == "Player")
		{
			this.transform.DetachChildren();
		}
	}
}
