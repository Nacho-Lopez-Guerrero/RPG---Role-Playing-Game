using UnityEngine;
using System.Collections;

public class HeadChanger : MonoBehaviour 
{
	private int _headIndex = 0;

	private int _maxTexturesIndex = 2;

	public void OnMouseDown()
	{
		_headIndex++;
		if(_headIndex > _maxTexturesIndex)
			_headIndex = 0;

		ChangeCharacterHead(_headIndex);
	}

	public static void ChangeCharacterHead(int index)
	{
		PlayerModelCustomization.ChangeHeadIndex(index);
	}

	public void OnMouseEnter()
	{
		HighLight(true);
	}
	
	public void OnMouseExit()
	{
		HighLight(false);
		
	}
	
	public void HighLight(bool glow)
	{
		Color color = Color.white;
		
		if(glow)
			color = Color.red;
		
		GetComponent<Renderer>().material.color = color;
	}
}
