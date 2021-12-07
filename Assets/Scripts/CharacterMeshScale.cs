using UnityEngine;
using System.Collections;

public class CharacterMeshScale : MonoBehaviour 
{
	public float minHeight = 1.4f;
	public float maxHeight = 2.4f;

	public float minWidth = 1.4f;
	public float maxWidth = 2.4f;

	private float _width = 1.8f;
	private float _height = 1.8f;

	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		PlayerModelCustomization._scale = new Vector2(_width, _height);
	}

	void OnGUI()
	{
		_width = GUI.HorizontalSlider(new Rect(310, Screen.height - 50, 300, 20), _width, minWidth, maxWidth);
		_height = GUI.VerticalSlider(new Rect(Screen.width - 50, 50, 20, 300), _height, maxHeight, minHeight);

	}
}
