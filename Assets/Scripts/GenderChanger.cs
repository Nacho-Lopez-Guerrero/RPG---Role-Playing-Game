using UnityEngine;
using System.Collections;

public class GenderChanger : MonoBehaviour 
{
	public Texture2D[] symbols;

	private int _index = 0;

	// Use this for initialization
	void Start () 
	{
		if(symbols.Length < 0)
			Debug.LogWarning("We do not have any gender symbos loaded");

		GetComponent<Renderer>().material.mainTexture = symbols[_index];
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

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
		_index++;
		if(_index > symbols.Length - 1)
			_index = 0;

		GetComponent<Renderer>().material.mainTexture = symbols[_index];

		Messenger.Broadcast("ToggleGender");
	}

	public void HighLight(bool glow)
	{
		Color color = Color.white;

		if(glow)
			color = Color.red;

		GetComponent<Renderer>().material.color = color;
	}
}
