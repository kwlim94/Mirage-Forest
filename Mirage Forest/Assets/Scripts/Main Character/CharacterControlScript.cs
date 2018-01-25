using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControlScript : MonoBehaviour
{

	Animator anim;
	Rigidbody rb;
	public Transform character;
	public Transform model;
	public float walkingSpeed;
	public float cameraRotationSpeed;
	//HT This is to offset the character rotation with the camera
	float characterRotationOffset;
	bool isGround;

	float veticalAxis;
	float horizontalAxis;

	void Start ()
	{
		anim = character.GetComponent<Animator>();
		rb = GetComponent<Rigidbody>();
		isGround = true;

		characterRotationOffset = 0.0f;
	}

	void Update ()
	{
		CameraRotation ();
		Movement ();
	}

	void Movement ()
	{
		horizontalAxis = Input.GetAxis("Horizontal");
		veticalAxis = Input.GetAxis("Vertical");

		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
		{
			anim.SetBool("Walk", true);
			float rotationAngle = Mathf.Atan2(horizontalAxis ,veticalAxis) - Mathf.Atan2(0.0f, 1.0f);

			character.eulerAngles = new Vector3(0.0f, rotationAngle * Mathf.Rad2Deg, 0.0f);
			transform.Translate(new Vector3(horizontalAxis, 0.0f, veticalAxis) * walkingSpeed * Time.deltaTime);
		}
		else
		{
			anim.SetBool("Walk", false);
		}



		if(Input.GetKeyDown(KeyCode.Space) && isGround)
		{
			//HT This is hard overide
			anim.Play("Jump", 0, 0);
       		//anim.SetBool("Jump", true);
			isGround = false;
   			rb.AddForce(new Vector3(0.0f, 10000.0f, 0.0f));
		}
		else
		{
			anim.SetBool("Jump", false);
		}
	}

	void CameraRotation ()
	{
		if(Input.GetMouseButton(0))
		{
			Camera.main.transform.eulerAngles = new Vector3(Camera.main.transform.eulerAngles.x,
															Camera.main.transform.eulerAngles.y + Input.GetAxis("Mouse X") * cameraRotationSpeed * Time.deltaTime,
															Camera.main.transform.eulerAngles.z);
			
			characterRotationOffset = Camera.main.transform.eulerAngles.y;
		}
	}

	void OnCollisionEnter(Collision col)
	{
		if(col.transform.tag == "Ground")
		{
			isGround = true;
		}
		Debug.Log(isGround);
	}
}
