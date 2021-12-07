/// <summary>
/// Vital bar.cs
/// 03/12/2014
/// Nacho Lopez
/// 
/// This class is the responsible for displaying a Vital for the player character or a mob
/// </summary>
using UnityEngine;
using System.Collections;

public class VitalBar : MonoBehaviour {

	public bool _isPlayerHealthBar;		//This values tells if it is a player's health bar or a mob's health bar

	private int _maxBarLength;				//This is how large the vital bar can be if the target is at 100% health
	private int _currentBarLength;			//This is the current length of the vital bar

	private GUITexture _display;


	void Awake()
	{
		_display = gameObject.GetComponent<GUITexture>();		//Obtienes el GUITexture de 'this' gameObject
	}

	// Use this for initialization
	void Start () 
	{

		_maxBarLength = (int)_display.pixelInset.width;

		OnEnable();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	/// <summary>
	/// This method is called when the gameObject is enabled
	/// </summary>
	public void OnEnable()
	{
		if(_isPlayerHealthBar)
			Messenger<int, int>.AddListener("player health update", OnChangeHealthBarSize);		//Cuando se produzca el evento 'player health update' se ejecutara la funcion 'OnChangeHealthBarSize'
		else
		{
			ToggleDisplay(false);

			Messenger<int, int>.AddListener("mob health update", OnChangeHealthBarSize);
			Messenger<bool>.AddListener("show mob vitalBars", ToggleDisplay);
		}
	}

	/// <summary>
	/// This method is called when the gameObject is disabled
	/// </summary>
	public void OnDisable()
	{
		if(_isPlayerHealthBar)
			Messenger<int, int>.RemoveListener("player health update", OnChangeHealthBarSize);		//Cuando se produzca el evento 'player health update' se ejecutara la funcion 'OnChangeHealthBarSize'
		else
		{
			Messenger<int, int>.RemoveListener("mob health update", OnChangeHealthBarSize);			//int, int porque le metodo que llama recibe dos argumentos
			Messenger<bool>.RemoveListener("show mob vitalBars", ToggleDisplay);
		}
	}

	/// <summary>
	/// This method will calculate the total size of the healthBar in relation to the % of health that target has left
	/// </summary>
	/// <param name="">.</param>
	public void OnChangeHealthBarSize(int curHealth, int maxHealth)
	{
//		Debug.Log("We heard and event. CurHealth = " + curHealth + " - maxHealth = " + maxHealth);
		_currentBarLength = (int)((curHealth / (float)maxHealth) * _maxBarLength); 
//		_display.pixelInset = new Rect(_display.pixelInset.x, _display.pixelInset.y, _currentBarLength, _display.pixelInset.height);
		_display.pixelInset = CalculatePosition();

	}

	/// <summary>
	/// Sets the player healthBar to the player or mob
	/// </summary>
	/// <param name="b">
	/// 1 = playerHealthBar; 0 = mobHealthBar
	/// </param>
	public void SetPlayerHealth(bool b)
	{
		_isPlayerHealthBar = b;
	}

	private Rect CalculatePosition()
	{
		float yPos = _display.pixelInset.y / 2 - 15;

		if(!_isPlayerHealthBar)	
		{
			float xPos = (_maxBarLength - _currentBarLength) - (_maxBarLength / 4 + 10);
			return new Rect(xPos, yPos, _currentBarLength, _display.pixelInset.height);
		}

		return new Rect(_display.pixelInset.x, yPos, _currentBarLength, _display.pixelInset.height);

	}

	private void ToggleDisplay(bool show)
	{
		_display.enabled = show;
	}
}
