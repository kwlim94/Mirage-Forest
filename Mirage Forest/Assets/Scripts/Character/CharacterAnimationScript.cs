using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterAnimation
{
	IDLE = 0,
	WALK,
	JUMP,
	GET_UP,
	SIT_ON_CHAIR,
	SIT_ON_GROUND,
}

public class CharacterAnimationScript : MonoBehaviour
{
	Animator anim;
	public Transform model; //HT The ganmeObject that refers to the character model itself and not the Character(with camera) as a whole 

	CharacterAnimation characterAnimation;

	void Start()
	{
		anim = model.GetComponent<Animator>();
		characterAnimation = CharacterAnimation.IDLE;
	}

	public void ChangeAnimation (CharacterAnimation charaAnim)
	{
		if(charaAnim != characterAnimation && !anim.IsInTransition(0))
		{
			characterAnimation = charaAnim;
			PlayAnimation ();
		}
	}

	void PlayAnimation ()
	{
		switch (characterAnimation)
		{
		case CharacterAnimation.IDLE:
			anim.Play("Idle");
			break;
		case CharacterAnimation.WALK:
			anim.SetBool("Walk", true);
			break;
		case CharacterAnimation.JUMP:
			anim.Play("Jump");
			break;
		case CharacterAnimation.GET_UP:
			anim.Play("Stand Up");
			break;
		case CharacterAnimation.SIT_ON_CHAIR:
			anim.Play("Sit");
			break;
		}
	}
}
