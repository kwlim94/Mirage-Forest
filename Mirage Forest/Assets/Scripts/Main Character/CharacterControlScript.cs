using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//! This script is used for Movement and Camera controls for the player
public class CharacterControlScript : MonoBehaviour
{
	//HT Animator and RigidBody for the character model
	Animator anim;
	Rigidbody rb;

	//! Character Related variables
	public Transform model; //HT The ganmeObject that refers to the character model itself and not the Character(with camera) as a whole 
	public float walkingSpeed; //HT This influences the character as a whole
	bool isGround; //HT to check if the character is grounded

	//HT This values are the check the values of the movement control keys to influence the actual movement
	float veticalAxis;
	float horizontalAxis;

	//! Camera Related Variables
	public float cameraRotationSpeed; //HT for camera rotation involving mouse
	float rotationDamping = 0.2f; //HT the value of gradual rotation when left and right movement is applied
	float lerpDamping = 500.0f; //HT This is the rate of the camera rotation for camera reset
	float desiredRotationAngle; //HT Not Used at the moment
	public bool isLerping; //HT This used to check if the camera is in the middle of reseting

	void Start ()
	{
		Cursor.visible = false; //HT Temporary put it here first
		anim = model.GetComponent<Animator>();
		rb = GetComponent<Rigidbody>();
		isGround = true;
		isLerping = false;
	}

	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Q))
			ResetCamera ();
		
		if (isLerping)
		{
			LerpAngle ();
		}
		else
		{
			CameraRotation ();
			Movement ();
		}

	}

	void Movement ()
	{
		horizontalAxis = Input.GetAxis("Horizontal");
		veticalAxis = Input.GetAxis("Vertical");

		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
		{
			anim.SetBool("Walk", true);
			float rotationAngle = Mathf.Atan2(horizontalAxis ,veticalAxis) - Mathf.Atan2(0.0f, 1.0f);


			model.localEulerAngles = new Vector3(0.0f, rotationAngle * Mathf.Rad2Deg, 0.0f); //HT This is for player rotation
			transform.Translate(new Vector3(horizontalAxis, 0.0f, veticalAxis) * walkingSpeed * Time.deltaTime); //HT This is for player translation

			//HT Gradual Rotation of the camera when left and right is pressed
			if(Input.GetKey(KeyCode.D))
			{
				transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + rotationDamping, transform.eulerAngles.z);
				model.transform.eulerAngles = new Vector3(model.transform.eulerAngles.x, model.transform.eulerAngles.y - rotationDamping, model.transform.eulerAngles.z);
			}
			else if(Input.GetKey(KeyCode.A))
			{
				transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y - rotationDamping, transform.eulerAngles.z);
				model.transform.eulerAngles = new Vector3(model.transform.eulerAngles.x, model.transform.eulerAngles.y + rotationDamping, model.transform.eulerAngles.z);
			}
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
		//if(Input.GetMouseButton(0))
		{
			transform.eulerAngles = new Vector3(transform.eulerAngles.x,
												transform.eulerAngles.y + Input.GetAxis("Mouse X") * cameraRotationSpeed * Time.deltaTime,
												transform.eulerAngles.z);
			
			model.transform.localEulerAngles = new Vector3(model.transform.localEulerAngles.x,
															model.transform.localEulerAngles.y - Input.GetAxis("Mouse X") * cameraRotationSpeed * Time.deltaTime,
															model.transform.localEulerAngles.z);
		}
	}

	void ResetCamera ()
	{
		//desiredRotationAngle = transform.eulerAngles.y + model.transform.localEulerAngles.y; //! This code is now not in use at the moment
		isLerping = true;

		//HT This code may be used again
//		transform.eulerAngles = new Vector3(transform.eulerAngles.x,
//											transform.eulerAngles.y + model.transform.localEulerAngles.y,
//											transform.eulerAngles.z);
//
//		model.transform.localEulerAngles = Vector3.zero;
	}

	//HT This function slowly lerps the camera until it is behind the player
	void LerpAngle ()
	{
		transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y + (lerpDamping * Time.deltaTime), transform.eulerAngles.z);
		model.transform.localEulerAngles = new Vector3(model.transform.localEulerAngles.x, model.transform.localEulerAngles.y - (lerpDamping * Time.deltaTime), model.transform.localEulerAngles.z);

		if(model.transform.localRotation.y <= 0.0f)
			isLerping = false;
	}

	void OnCollisionEnter(Collision col)
	{
		if(col.transform.tag == "Ground")
		{
			isGround = true;
		}
	}
}
