using UnityEngine;
using System.Collections;

public class RotatePlayerArrow : MonoBehaviour 
{
	public bool rotateClockwise = true;

	private bool _arrowPressed = false;

	public void OnMouseEnter()
	{
		HighLight(true);
	}
	
	public void OnMouseExit()
	{
		HighLight(false);
		
	}
	
	public void OnMouseDown()
	{
		_arrowPressed = true;

//		Messenger.Broadcast("ToggleGender");
	}

	public void OnMouseUp()
	{
		_arrowPressed = false;
	}


	public void HighLight(bool glow)
	{
		Color color = Color.white;
		
		if(glow)
			color = Color.red;
		
		GetComponent<Renderer>().material.color = color;
	}

	void Update()
	{
		if(_arrowPressed)
			Messenger<bool>.Broadcast("RotatePlayerClockWise", rotateClockwise);
	}
}
