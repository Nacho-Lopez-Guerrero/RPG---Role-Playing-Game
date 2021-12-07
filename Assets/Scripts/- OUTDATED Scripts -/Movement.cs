using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController))]
public class Movement : MonoBehaviour 
{
	public float rotateSpeed = 250;
	public float moveSpeed = 12;
	public float strafeSpeed = 5;
	public float runMultiplier = 4;

	private Transform _myTransform;
	private CharacterController _controller;

	void Awake()
	{
		_myTransform = transform;
		_controller = GetComponent<CharacterController>();
	}

	// Use this for initialization
	void Start () 
	{
		GetComponent<Animation>().wrapMode = WrapMode.Loop;				//Set all the animation to loop when played
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(!_controller.isGrounded)
		{
		//	_controller.Move(Vector3.down * Time.deltaTime * 25);
		}

		Turn();
		Walk();
		Strafe();
	}

	private void Turn()
	{
		if(Mathf.Abs(Input.GetAxis("Rotate Player")) > 0)
		{
			_myTransform.Rotate(0, Input.GetAxis("Rotate Player") * Time.deltaTime * rotateSpeed, 0);
		}
	}

	private void Walk()
	{
		if(Mathf.Abs(Input.GetAxis("Move Forward")) > 0)
		{
			if(Input.GetButton("Run"))
			{
				GetComponent<Animation>().CrossFade("Run");
				_controller.SimpleMove(_myTransform.TransformDirection(Vector3.forward) * Input.GetAxis("Move Forward") * moveSpeed	* runMultiplier);
			}
			else
			{
				GetComponent<Animation>().CrossFade("Walk");
				_controller.SimpleMove(_myTransform.TransformDirection(Vector3.forward) * Input.GetAxis("Move Forward")	* moveSpeed);
			}
		}
		else
		{
			GetComponent<Animation>().CrossFade("idle");
		}
	}

	private void Strafe()
	{
		if(Mathf.Abs(Input.GetAxis("Strafe")) > 0)
		{
			GetComponent<Animation>().CrossFade("Strafe");
			_controller.SimpleMove(_myTransform.TransformDirection(Vector3.right) * Input.GetAxis("Strafe")	* strafeSpeed);		
		}
	}

}
