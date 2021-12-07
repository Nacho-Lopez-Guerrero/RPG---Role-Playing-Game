using UnityEngine;
using System.Collections;

public class SkinToneSelector : MonoBehaviour 
{
	public int colorCode = 1;

	public void OnMouseDown()
	{
		PlayerModelCustomization.ChangePlayerSkinColor(colorCode);
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
