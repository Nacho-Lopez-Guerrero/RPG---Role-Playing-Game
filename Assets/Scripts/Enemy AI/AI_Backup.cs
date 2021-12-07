using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AdvancedMovement))]
[RequireComponent (typeof(SphereCollider))]
public class AI_Backup : MonoBehaviour 
{
	private enum State
	{
		Idle,
		Init,
		Setup,
		Search,
		Attack,
		Retreat,
		Flee
	}

	public float baseMeleeRange = 8;
	public Transform target;
	public float runDistance = 20;
	public float perceptiumRadius = 25;

	private Transform _myTransform;
	private Transform _home;

	private const float ROTATION_DAMP = 0.03f;
	private const float FORWARD_DAMP = 0.9f;

	private State _state;
	private bool _alive = true;
	public bool _colliderFlag = false;

	private SphereCollider _sphereCollider;

	void Awake()
	{

	}

	void Start()
	{
		_state = AI_Backup.State.Init;
		StartCoroutine("FSM");

		
		//		yield return null;
	}


	IEnumerator FSM()																				//FSM = Finite State Machine
	{
		while(_alive)
		{
//			Debug.Log("FSM Enter");

			switch(_state)
			{
			case State.Init:
				Init();
				break;
			case State.Setup:
				Setup();
				break;
			case State.Search:
				Search();
				break;

			case State.Attack:
				Attack();
				break;
			case State.Flee:
				Flee();
				break;
			case State.Retreat:
				Retreat();
				break;
			}
		}
//		Debug.Log("FSM Exit");

		yield return null;
	}

	void LateUpdate()
	{

		if(_colliderFlag & !_alive)
		{
//			Debug.Log("REACTIVAMOS FSM");

//			_state = AI.State.Search;
			_alive = true;
			StopAllCoroutines();
			StartCoroutine("FSM");					//aniadido por mi!!!!!!!!!
		}
	}

	private void Init()
	{
//		Debug.Log("Init");

		_home = transform.parent.transform;
		_myTransform = transform;

		_sphereCollider = GetComponent<SphereCollider>();
		if(_sphereCollider == null)
		{
			Debug.LogError("SphereCollider not present");
			return;
		}

		_state = AI_Backup.State.Setup;
	}

	private void Setup()
	{
//		Debug.Log("****Setup****");

		_sphereCollider.center = GetComponent<CharacterController>().center;
		_sphereCollider.radius = perceptiumRadius;
		_sphereCollider.isTrigger = true;
		_state = AI_Backup.State.Search;

		_alive = false;

	}

	private void Search()
	{
//		Debug.Log("****Search****");
		Move();	
		_state = AI_Backup.State.Attack;
	}

	private void Attack()
	{
//		Debug.Log("****Attack****");

		Move();
		_state = AI_Backup.State.Retreat;
	}

	private void Flee()
	{
//		Debug.Log("****Flee****");

		Move();
//		_alive = false;

		_state = AI_Backup.State.Search;
	}

	private void Retreat()
	{
//		Debug.Log("****Retreat****");

		_myTransform.LookAt(target);
		Move();
		_alive = false;																		//puesto por mi a False porque sino congela Unity el bucle while(_alive)
		_state = AI_Backup.State.Search;
	}

	private void Move()
	{
		if(target)
		{
			Vector3 dir = (target.position - _myTransform.position).normalized;
			float direction = Vector3.Dot(dir, _myTransform.forward);										//Deveuelve un valor entre 1 y -1 dependeindo de la posicion realtiva de los dos vectores
			
			
			float dist = Vector3.Distance(target.position, _myTransform.position);
			
			
			if(direction > FORWARD_DAMP && dist > baseMeleeRange)
			{
				if(dist > runDistance)
					SendMessage("ActivateRun", AdvancedMovement.Forward.none);											

				else
					SendMessage("DeActivateRun", AdvancedMovement.Forward.none);											
				
				
				
				SendMessage("MoveMeForward", AdvancedMovement.Forward.forward);								//Sends a message to all monobehaviour scripts that are ATTACHED to this gameObject
			}
			else
				SendMessage("MoveMeForward", AdvancedMovement.Forward.none);
			
			
			
			
			dir = (target.position - _myTransform.position).normalized;
			direction = Vector3.Dot(dir, _myTransform.right);	
			
			if(direction > ROTATION_DAMP)
				SendMessage("RotateMe", AdvancedMovement.Turn.right);
			else if (direction < -ROTATION_DAMP)
				SendMessage("RotateMe", AdvancedMovement.Turn.left);
			else
				SendMessage("RotateMe", AdvancedMovement.Turn.none);
		}
		else
		{
			SendMessage("MoveMeForward", AdvancedMovement.Forward.none);
			SendMessage("RotateMe", AdvancedMovement.Turn.none);
		}
	}

	public void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Player"))
		{
			target = other.transform;

			_alive = true;
			_colliderFlag = true;
//			Debug.Log("Trigger Enter");
//			Debug.Log("State: " +_state);


			StartCoroutine("FSM");
		}
	}

	public void OnTriggerExit(Collider other)
	{
//		Debug.Log("Trigger Exit");
		if(other.CompareTag("Player"))
		{
//			_alive = true;							//aniadido por mi!!!!!!!!!
			target = _home;


//			_alive = false;
		}
	}
}
