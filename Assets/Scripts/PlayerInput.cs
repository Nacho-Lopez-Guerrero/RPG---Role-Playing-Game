using UnityEngine;
using System.Collections;

[AddComponentMenu("Hack And Slash/Player/All Players Scripts")]
[RequireComponent (typeof(AdvancedMovement))]
[RequireComponent (typeof(PC))]
public class PlayerInput : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetButtonDown("Toggle Inventory"))
			Messenger.Broadcast("ToggleInventory");															//Broadcast: Sends a message to the Defined 'listeners' of all scene's GameObjects

		if(Input.GetButtonDown("Toggle Character Window"))
			Messenger.Broadcast("ToggleCharacterWindow");		

		if(Input.GetButton("Move Forward"))
		{
			if(Input.GetAxis("Move Forward") > 0)
				SendMessage("MoveMeForward", AdvancedMovement.Forward.forward);								//SendMessage: Sends a message to all monobehaviour scripts that are attahced to this gameObject
			else
				SendMessage("MoveMeForward", AdvancedMovement.Forward.back);											
		}
		if(Input.GetButtonUp("Move Forward"))
			SendMessage("MoveMeForward", AdvancedMovement.Forward.none);											




		if(Input.GetButton("Rotate Player"))
		{
			if(Input.GetAxis("Rotate Player") > 0)
				SendMessage("RotateMe", AdvancedMovement.Turn.right);										
			else
				SendMessage("RotateMe", AdvancedMovement.Turn.left);		
		}
		if(Input.GetButtonUp("Rotate Player"))
			SendMessage("RotateMe", AdvancedMovement.Turn.none);


		if(Input.GetButton("Strafe"))
		{
			if(Input.GetAxis("Strafe") > 0)
				SendMessage("StrafeMe", AdvancedMovement.Turn.right);										
			else
				SendMessage("StrafeMe", AdvancedMovement.Turn.left);		
		}
		if(Input.GetButtonUp("Strafe"))
			SendMessage("StrafeMe", AdvancedMovement.Turn.none);

		
		if(Input.GetButtonDown("Run"))
			SendMessage("ToggleRun", AdvancedMovement.Forward.none);											


		if(Input.GetButtonDown("Jump"))
			SendMessage("JumpMe", AdvancedMovement.Forward.none);											



	}

	public void OnTriggerEnter(Collider other)
	{
		if(other.CompareTag("Water"))
			SendMessage("SwimMe", true);											
	}
	
	public void OnTriggerExit(Collider other)
	{	
		if(other.CompareTag("Water"))
			SendMessage("SwimMe", false);											
	}
	

}
