using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControlScript : MonoBehaviour
{

	Animator anim;
	Rigidbody rb;
	Transform character;
	public float walkingSpeed;
	bool isGround;

	void Start ()
	{
		character = transform.GetChild(0);
		anim = character.GetComponent<Animator>();
		rb = GetComponent<Rigidbody>();
		isGround = true;
	}

	void Update ()
	{
		Movement ();
	}

	void Movement ()
	{
		if(Input.GetKey(KeyCode.W) && anim.GetBool("Walk"))
		{
			character.eulerAngles = new Vector3(0.0f, 0.0f, 0.0f);
			transform.Translate(Vector3.forward * walkingSpeed * Time.deltaTime);
		}
		else if(Input.GetKey(KeyCode.S) && anim.GetBool("Walk"))
		{
			character.eulerAngles = new Vector3(0.0f, 180.0f, 0.0f);
			transform.Translate(Vector3.back * walkingSpeed * Time.deltaTime);
		}
		if(Input.GetKey(KeyCode.A) && anim.GetBool("Walk"))
		{
			character.eulerAngles = new Vector3(0.0f, -90.0f, 0.0f);
			transform.Translate(Vector3.left * walkingSpeed * Time.deltaTime);
		}
		else if(Input.GetKey(KeyCode.D) && anim.GetBool("Walk"))
		{
			character.eulerAngles = new Vector3(0.0f, 90.0f, 0.0f);
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

	void OnCollisionEnter(Collision col)
	{
		if(col.transform.tag == "Ground")
		{
			isGround = true;
		}
		Debug.Log(isGround);
	}
}
