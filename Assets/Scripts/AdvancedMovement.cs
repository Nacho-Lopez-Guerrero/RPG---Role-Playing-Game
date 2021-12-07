using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController))]				//Requires a ChracterController to the GameObject this script is attached to
public class AdvancedMovement : MonoBehaviour 
{
	public enum Turn
	{
		left = -1,
		none = 0,
		right = 1
	}


	public enum Forward
	{
		back = -1,
		none = 0,
		forward = 1
	}


	public enum State
	{
		Idle,
		Init,
		Setup,
		Run
	}


	public float rotateSpeed = 250;
	public float walkSpeed = .1f;
	public float strafeSpeed = .01f;
	public float runMultiplier = 4;
	public float gravity = 5;
	public float airtime = 0;									//How long we have been in the air
	public float fallTime = 0.5f;								//Time we have to be falling before the system knows it's a fall
	public float jumpHeight = 1.75f;
	public float jumpTime = 2;

	public CollisionFlags _collisionFlags;

	public AnimationClip meleeAttack;
	public AnimationClip combatIdle;

	private Transform _myTransform;
	private CharacterController _controller;
	private Vector3 _moveDirection;

	private Turn _turn;
	private Turn _strafe;
	private Forward _forward;
	private bool _run;
	private bool _jump;
	private bool _isSwimming;

	private State _state;
	private BaseCharacter _bc;


	void Awake()
	{
		_myTransform = transform;
		_bc = gameObject.GetComponent<BaseCharacter>();
		_controller = GetComponent<CharacterController>();
		_state = State.Init;
	}


	// Use this for initialization
	IEnumerator Start () 
	{
		while(true)
		{
			switch(_state)
			{
				case State.Init:
					Init();
					break;
				case State.Setup:
					Setup();
					break;
				case State.Run:
					ActionPicker();
					break;
			}
			yield return null;
		}
	}


	void Update()
	{

	}


	private void Init()
	{
		if(!GetComponent<CharacterController>()) return;
		if(!GetComponent<Animation>()) return;

		_state = AdvancedMovement.State.Setup;
	}


	private void Setup()
	{
		GetComponent<Animation>().Stop();
		
		GetComponent<Animation>().wrapMode = WrapMode.Loop;
		GetComponent<Animation>()["Jump"].layer = 1;							//Layer = Crossfades priority
		GetComponent<Animation>()["Jump"].wrapMode = WrapMode.Once;
		
		//animation.Play("idle");

		_moveDirection = Vector3.zero;
		_turn = Turn.none;
		_forward = Forward.none;
		_run = false;
		_strafe = Turn.none;
		_jump = false;
		_isSwimming = false;
		_state = AdvancedMovement.State.Run;
	}


	private void ActionPicker()
	{
		_myTransform.Rotate(0, (int)_turn * Time.deltaTime * rotateSpeed, 0);

		
		if(_controller.isGrounded || _isSwimming)
		{
//			Debug.Log("**** On the ground ****");
			
			airtime = 0;
			
			_moveDirection = new Vector3((int)_strafe, 0, (int)_forward);
			_moveDirection = _myTransform.TransformDirection(_moveDirection).normalized;
			_moveDirection *= walkSpeed;
			
			//Walk & Run
			if(_forward != Forward.none)
			{
				if(_isSwimming)
				{
					Swim();
				}
				else if(_run)
				{
					_moveDirection *= runMultiplier;
					Run();
				}
				else
					Walk();
			}
			else if(_strafe != Turn.none)
			{
				Strafe();
			}
			else
			{
				Idle();
			}
			//Jump
			if(_jump)
			{
				if(airtime < jumpTime)
				{
					_moveDirection.y += jumpHeight;
					Jump();
					_jump = false;
				}
			}
		}
		else
		{
//			Debug.Log("**** Not on the ground ****");
			
			if((_collisionFlags & CollisionFlags.CollidedBelow) == 0)
			{
				airtime += Time.deltaTime;
				
				if(airtime > fallTime)
				{
					Fall();
				}
			}
		}

		if(!_isSwimming)
			_moveDirection.y -= gravity * Time.deltaTime;

		_collisionFlags = _controller.Move(_moveDirection);
	}


	public void MoveMeForward(Forward z)
	{
		_forward = z;
	}


	public void ToggleRun()
	{
		_run = !_run;
	}


	public void ActivateRun()
	{
		_run = true;
	}


	public void DeActivateRun()
	{
		_run = false;
	}


	public void RotateMe(Turn y)
	{
		_turn = y;
	}


	public void StrafeMe(Turn x)
	{
		_strafe = x;
	}


	public void JumpMe()
	{
		_jump = true;
	}


	public void SwimMe(bool swim)
	{
		_isSwimming = swim;
	}





	/// <summary>
	///	Below there are functions that play Animations 
	/// </summary>
	public void Fall()
	{
//		animation.CrossFade("Fall");
	}


	public void Idle()
	{
		if(!_bc.InCombat)
			GetComponent<Animation>().CrossFade("idle");
		else
		{
			if(!GetComponent<Animation>().isPlaying)
				GetComponent<Animation>().Play(combatIdle.name);

		}
			//animation.Play(combatIdle.name);


	}

	public void Walk()
	{
		GetComponent<Animation>().CrossFade("Walk");
	}


	public void Strafe()
	{
		GetComponent<Animation>().CrossFade("Strafe");
	}


	public void Jump()
	{
		GetComponent<Animation>()["Jump"].speed = 3f;
		GetComponent<Animation>().CrossFade("Jump");
	}


	public void Attack()
	{

		GetComponent<Animation>().CrossFade("Attack");
	}


	public void PlayMeleeAttack()
	{
		GetComponent<Animation>()[meleeAttack.name].wrapMode = WrapMode.Once;

		if(meleeAttack == null)
		{
			Debug.LogWarning("We need a meleeAttack Animation for this mob!");
			return;
		}


		//animation[meleeAttack.name].speed = animation[meleeAttack.name].length / 20f;

		Debug.Log("Length: " + meleeAttack.length + " | Speed: " + GetComponent<Animation>()[meleeAttack.name].speed );
		GetComponent<Animation>().Play(meleeAttack.name);

	}

	public void Run()
	{
		GetComponent<Animation>().CrossFade("Run");
	}


	public void Swim()
	{	Debug.Log("Swimming");
		GetComponent<Animation>().CrossFade("swim");
	}
}
