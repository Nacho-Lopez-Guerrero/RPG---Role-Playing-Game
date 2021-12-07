using UnityEngine;
using System.Collections;
using System;			//Used for access Enum Class

public class CharacterGenerator : MonoBehaviour 
{

	//private PlayerCharacter _toon;
	private const int STARTING_POINTS = 350;		//Constantes nombre en mayuscula
	private const int MIN_STARTING_ATTRIUTE_VALUE = 10;
	private const int STARTING_VALUE = 50;
	private int pointsLeft;

	private const int OFFSET = 5;
	private const int LINE_HEIGHT = 20;

	private const int STAT_LABEL_WIDTH = 140;
	private const int BASEVALUE_LABEL_WIDTH = 30;
	private const int BUTTON_WIDTH = 20;
	private const int BUTTON_HEIGHT = 20;

	private int statStartingPos = 40;
	
	public GUISkin mySkin;

//	public GameObject playerPrefab;

	public float delayTimer = .15f;
	private float _lastClick = 0;

	void Awake()
	{
	//	PC.Instance.Initialize();
	}

	// Use this for initialization
	void Start () 
	{
		pointsLeft = STARTING_POINTS;

		for(int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++)
		{
			PC.Instance.GetPrimaryAttribute(cnt).BaseValue = STARTING_VALUE;
			pointsLeft -= (STARTING_VALUE - MIN_STARTING_ATTRIUTE_VALUE);
		}
		PC.Instance.StatUpdate();
	}
	
	// Update is called once per frame
	void Update () 
	{
	}

	void OnGUI()
	{
//		GUI.skin = mySkin;

		DisplayName();
		DisplayPointsLeft();
		DisplayAttributes();

//		GUI.skin = null;

		DisplayVitals();

//		GUI.skin = mySkin;

		DisplaySkills();

		if(PC.Instance.Name != "" && pointsLeft < 1)
			DisplayCreateButton();

//		if(_toon.Name == "" || pointsLeft > 0)
//			DisplayCreateLabel();
//		else 
//			DisplayCreateButton();

	}

	private void DisplayName()
	{
		GUI.Label(new Rect(10, 10, 50, 25), "Name:");
		PC.Instance.Name = GUI.TextField(new Rect(65, 10, 100, 25), PC.Instance.Name);		//TextField solo deja introducir una linea
	}

	private void DisplayAttributes()
	{
		for(int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++)
		{
			GUI.Label(new Rect(OFFSET, statStartingPos + (cnt * LINE_HEIGHT), STAT_LABEL_WIDTH, LINE_HEIGHT), ((AttributeName)cnt).ToString());
			GUI.Label(new Rect(STAT_LABEL_WIDTH + OFFSET, statStartingPos + (cnt * LINE_HEIGHT), BASEVALUE_LABEL_WIDTH, LINE_HEIGHT), PC.Instance.GetPrimaryAttribute(cnt).AdjustedBaseValue.ToString());

			if(GUI.RepeatButton(new Rect(OFFSET + STAT_LABEL_WIDTH + BASEVALUE_LABEL_WIDTH, statStartingPos + (cnt * BUTTON_HEIGHT), BUTTON_WIDTH, BUTTON_HEIGHT), "-"))		//Crea y asigna funcion al boton
			{
				if(Time.time - _lastClick > delayTimer)
				{
					if(PC.Instance.GetPrimaryAttribute(cnt).BaseValue > MIN_STARTING_ATTRIUTE_VALUE)
					{
						PC.Instance.GetPrimaryAttribute(cnt).BaseValue--;
						pointsLeft++;
						PC.Instance.StatUpdate();
					}
					_lastClick = Time.time;
				}
			}

				if(GUI.RepeatButton(new Rect(OFFSET + STAT_LABEL_WIDTH + BASEVALUE_LABEL_WIDTH  + BUTTON_WIDTH, statStartingPos + (cnt * BUTTON_HEIGHT), BUTTON_WIDTH, BUTTON_HEIGHT), "+"))
				{
					if(Time.time - _lastClick > delayTimer)
					{
						if(pointsLeft > 0)
						{
							PC.Instance.GetPrimaryAttribute(cnt).BaseValue++;
							pointsLeft--;
							PC.Instance.StatUpdate();
						}
						_lastClick = Time.time;
					}
				}

		}
	}

	private void DisplayVitals()
	{
		//GUI.BeginGroup(new Rect(40, 90 + LINE_HEIGHT * (Enum.GetValues(typeof(AttributeName)).Length), (Screen.width - 80) / 2, LINE_HEIGHT * (Enum.GetValues(typeof(VitalName)).Length)));

		for(int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++)
		{
			GUI.Label(new Rect(OFFSET, statStartingPos + ((cnt + 7) * LINE_HEIGHT), STAT_LABEL_WIDTH, LINE_HEIGHT), ((VitalName)cnt).ToString());
			GUI.Label(new Rect(OFFSET + STAT_LABEL_WIDTH, statStartingPos + ((cnt + 7) * LINE_HEIGHT), BASEVALUE_LABEL_WIDTH, LINE_HEIGHT), PC.Instance.GetVital(cnt).AdjustedBaseValue.ToString());
		}

		//GUI.EndGroup();
	}

	private void DisplaySkills()
	{
		for(int cnt = 0; cnt < Enum.GetValues(typeof(SkillName)).Length; cnt++)
		{
			GUI.Label(new Rect(OFFSET + STAT_LABEL_WIDTH + BASEVALUE_LABEL_WIDTH + BUTTON_WIDTH * 2 + OFFSET * 8, statStartingPos + (cnt * LINE_HEIGHT), STAT_LABEL_WIDTH, LINE_HEIGHT), ((SkillName)cnt).ToString());
			GUI.Label(new Rect(OFFSET + STAT_LABEL_WIDTH + BASEVALUE_LABEL_WIDTH + BUTTON_WIDTH * 2 + OFFSET * 8 + STAT_LABEL_WIDTH, statStartingPos + (cnt * LINE_HEIGHT), BASEVALUE_LABEL_WIDTH, LINE_HEIGHT), PC.Instance.GetSkill(cnt).AdjustedBaseValue.ToString());
		}
	}

	private void DisplayPointsLeft()
	{
		GUI.Label(new Rect(250, 10, 100, 25), "Points Left: " + pointsLeft);
	}

	private void DisplayCreateButton()
	{
		if(GUI.Button(new Rect(Screen.width / 2 - 50,				//x
		                    statStartingPos + 10 * LINE_HEIGHT,		//y
		                    100, 									//width
		                    LINE_HEIGHT),							//height			
		           			"Next"))								//Label del Boton
		{
			GameObject gs = GameObject.Find("__GameSettings");

			//Changed on tutorial #195
			//GameSettings gsCript = gs.GetComponent<GameSettings>();

			//change the curValue of the vital to the max modified value of that vital
			UpdateCurVitalValues();

			//Changed on tutorial #195
			//Save the character data
			//gsCript.SaveCharacterData();

			//Changed in tutorial #195
			//Application.LoadLevel(GameSettings.levelNames[2]);

//			GameSettings2.pc = PC.Instance;

			GameSettings2.SaveName(PC.Instance.Name);

			GameSettings2.SaveAttributes(PC.Instance.primaryAttributes);
			GameSettings2.SaveVitals(PC.Instance.vitals);
			GameSettings2.SaveSkills(PC.Instance.skills);

			Application.LoadLevel(GameSettings2.levelNames[2]);

		}
	}

	private void DisplayCreateLabel()
	{
		GUI.Label(new Rect(Screen.width / 2 - 50,						//x
		                       statStartingPos + 10 * LINE_HEIGHT,		//y
		                       100, 									//width
		                       LINE_HEIGHT),							//height			
		             		   "Creating..." , 								//Label del Boton
		          			   "Button");								//Apariencia (GUIStyle)

	}

	private void UpdateCurVitalValues()
	{
		for(int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++)
		{
			PC.Instance.GetVital(cnt).CurValue = PC.Instance.GetVital(cnt).AdjustedBaseValue;
		}
	}
}
