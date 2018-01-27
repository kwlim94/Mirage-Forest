using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControlScript : MonoBehaviour
{
	//HT Animator and RigidBody for the character model
	Animator anim;
	Rigidbody rb;

	public Transform model; //HT The ganmeObject that refers to the character model itself and not the Character(with camera) as a whole 

	public float walkingSpeed; //HT This influences the character as a whole
	public float cameraRotationSpeed; //HT for camera rotation involving mouse
	bool isGround; //HT to check if the character is grounded

	//HT This values are the check the values of the movement control keys to influencen the actual movement
	float veticalAxis;
	float horizontalAxis;

	float rotationDamping = 0.1f; //HT the value of gradual rotation when left and right movement is applied

	void Start ()
	{
		Cursor.visible = false;
		anim = model.GetComponent<Animator>();
		rb = GetComponent<Rigidbody>();
		isGround = true;
	}

	void Update ()
	{
		CameraRotation ();
		Movement ();
		if (Input.GetKeyDown(KeyCode.Q))
		{
			ResetCamera ();
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


			model.localEulerAngles = new Vector3(0.0f, rotationAngle * Mathf.Rad2Deg, 0.0f);
			transform.Translate(new Vector3(horizontalAxis, 0.0f, veticalAxis) * walkingSpeed * Time.deltaTime);

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
		transform.eulerAngles = new Vector3(transform.eulerAngles.x,
											transform.eulerAngles.y + model.transform.localEulerAngles.y,
											transform.eulerAngles.z);

		model.transform.localEulerAngles = Vector3.zero;
	}

	void OnCollisionEnter(Collision col)
	{
		if(col.transform.tag == "Ground")
		{
			isGround = true;
		}
	}
}
