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
		if(Input.GetKey(KeyCode.W) && anim.GetBool("Walk"))
		{
			character.eulerAngles = new Vector3(0.0f, characterRotationOffset + 0.0f, 0.0f);
			transform.Translate(Vector3.forward * walkingSpeed * Time.deltaTime);
		}
		else if(Input.GetKey(KeyCode.S) && anim.GetBool("Walk"))
		{
			character.eulerAngles = new Vector3(0.0f, characterRotationOffset + 180.0f, 0.0f);
			transform.Translate(Vector3.back * walkingSpeed * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.A) && anim.GetBool("Walk"))
		{
			character.eulerAngles = new Vector3(0.0f, characterRotationOffset - 90.0f, 0.0f);
			transform.Translate(Vector3.left * walkingSpeed * Time.deltaTime);
		}
		else if(Input.GetKey(KeyCode.D) && anim.GetBool("Walk"))
		{
			character.eulerAngles = new Vector3(0.0f, characterRotationOffset + 90.0f, 0.0f);
			transform.Translate(Vector3.right * walkingSpeed * Time.deltaTime);
		}

		if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
		{
			anim.SetBool("Walk", true);
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
