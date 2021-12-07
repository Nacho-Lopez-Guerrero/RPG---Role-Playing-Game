using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour 
{
	private Transform _target;
	public float walkDistance;
	public float rundistance;
	public float height;
	public float xSpeed = 250.0f;
	public float ySpeed = 120.0f;
	public float heightDamping = 2.0f;					//adds a delay when rotating cam
	public float rotationDamping = 3.0f;				//adds a delay when rotating cam
	public string playerTagName = "Player";

	private Transform _myTransform;

	private float _x;
	private float _y;


	private bool _aux = false;

	private bool _camButtonDown = false;
	private bool _rotateCameraKey = false;
	public string CameraTagName;
	// Use this for initialization
	void Start () 
	{
		if(_target == null)
		{
			Debug.Log("We do not have target");

			//Added By Me
			GameObject go = GameObject.FindGameObjectWithTag(CameraTagName);
			
			if(go == null)
				return;
			else
				_target = go.transform;

			CameraSetup();

			// /added

		}
		else
		{
			CameraSetup();
		}
		//CameraSetup();
	}

	void Awake()
	{
		_myTransform = transform;
	}
	
	// Update is called once per frame
	void Update () 
	{

		if(Input.GetButtonDown("Rotate Camera Button"))
		{
			_camButtonDown = true;
		}
		if(Input.GetButtonUp("Rotate Camera Button"))
		{
			_x = 0;
			_y = 0;

			_camButtonDown = false;
		}

		if(Input.GetButtonDown("Rotate Camera Horizontal Button") || Input.GetButtonDown("Rotate Camera Vertical Button"))
		{
			_rotateCameraKey = true;
		}
		if(Input.GetButtonUp("Rotate Camera Horizontal Button") || Input.GetButtonUp("Rotate Camera Vertical Button"))
		{
			_x = 0;
			_y = 0;

			_rotateCameraKey = false;
		}
	}

	//You must adjust the camera on last moment of the frame
	void LateUpdate()
	{
		if(_target != null)
		{
			if(_rotateCameraKey)
			{
				_x += Input.GetAxis("Rotate Camera Horizontal Button") * xSpeed * 0.02f;
				_y += Input.GetAxis("Rotate Camera Vertical Button") * ySpeed * 0.02f;

				RotateCamera();
			}
			if(_camButtonDown)
			{
				_x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
				_y += Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

				RotateCamera();
			}
			else
			{
//				_myTransform.position = new Vector3(target.position.x, target.position.y + height, target.position.z - walkDistance);
//				_myTransform.LookAt(target);


				// Calculate the current rotation angles
				float wantedRotationAngle = _target.eulerAngles.y;
				float wantedHeight = _target.position.y + height;
				
				float currentRotationAngle = _myTransform.eulerAngles.y;
				float currentHeight = _myTransform.position.y;
				
				// Damp the rotation around the y-axis
				currentRotationAngle = Mathf.LerpAngle (currentRotationAngle, wantedRotationAngle, rotationDamping * Time.deltaTime);
				
				// Damp the height
				currentHeight = Mathf.Lerp (currentHeight, wantedHeight, heightDamping * Time.deltaTime);
				
				// Convert the angle into a rotation
				Quaternion currentRotation = Quaternion.Euler (0, currentRotationAngle, 0);
				
				// Set the position of the camera on the x-z plane to:
				// distance meters behind the target
				_myTransform.position = _target.position;
				_myTransform.position -= currentRotation * Vector3.forward * walkDistance;
				
				// Set the height of the camera
				_myTransform.position = new Vector3(_myTransform.position.x, currentHeight, _myTransform.position.z);
				
				// Always look at the target
				_myTransform.LookAt (_target);

				float tempRot = _myTransform.rotation.x;
//				_myTransform.rotation = Quaternion.Euler(_myTransform.rotation.x + 5, _myTransform.rotation.y, _myTransform.rotation.z);
			}
		}
		else
		{
			GameObject go = GameObject.FindGameObjectWithTag(playerTagName);

			if(go == null)
				return;
			else
				_target = go.transform;
		}


	}

	public void CameraSetup()
	{
		_myTransform.position = new Vector3(_target.position.x, _target.position.y + height, _target.position.z - walkDistance);
		_myTransform.LookAt(_target);
	}

	private void RotateCamera()
	{
		Quaternion rotation = Quaternion.Euler(_y, _x, 0);
		Vector3 position = rotation * new Vector3(0.0f, 0.0f, -walkDistance) + _target.position;
		
		_myTransform.rotation = rotation;
		_myTransform.position = position;
	}
}
