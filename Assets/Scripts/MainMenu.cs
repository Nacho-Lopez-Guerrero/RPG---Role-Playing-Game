//check if we have some saved data on the playerprefs
//check the version of the saved data
//if the saved version of the data is not the current version
//do something
//else if the saved data version is the current version
//check to see if we have a character saved -> check for a character name
//if we do not have a character saved, load the Character Generator Scene
//else if we have a character saved
//if user want to load character, load character and level 1
//if user want to delete character, delete character and go to Player Generator Scene


using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public bool clearPrefs = false;

	private string _levelToLoad = "";

	private string _characterGenerator = GameSettings2.levelNames[1];
	private string _level1 = GameSettings2.levelNames[3];

	private bool _hasCharacter = false;
	private float _percentLoaded = 0;
	private bool _displayOptions = true;

	// Use this for initialization
	void Start () 
	{
		if(clearPrefs)
			PlayerPrefs.DeleteAll();

		if(PlayerPrefs.HasKey(GameSettings2.VERSION_KEY_NAME))
		{
			Debug.Log("There is a version key!");
			if(GameSettings2.LoadGameVersion() != GameSettings2.VERSION_NUMBER)
			{
				Debug.Log("Saved version MISSMATCH!");
				/*Upgrade playerPrefs here*/

				_levelToLoad = _characterGenerator;
			}
			else
			{
				Debug.Log("Saved version MATCHES current version");
				if(PlayerPrefs.HasKey("Name"))
				{
					Debug.Log("There is a Player Name Tag!");
					if(PlayerPrefs.GetString("Name") == "")
					{
						Debug.Log("The player name key doesnt have a value");
						PlayerPrefs.DeleteAll();
						GameSettings2.SaveGameVersion();
						_levelToLoad = _characterGenerator;
					}
					else
					{
						Debug.Log("The player name key has a value");
						_hasCharacter = true;
						_displayOptions = true;
						// _levelToLoad = _level1;
					}
				}
				else
				{
					Debug.Log("There is no player name key!");
					PlayerPrefs.DeleteAll();
					GameSettings2.SaveGameVersion();
					GameSettings2.SaveGameVersion();

					_levelToLoad = _characterGenerator;
				}
			}
		}
		else
		{
			Debug.Log("There is no ver key!");
			PlayerPrefs.DeleteAll();
			GameSettings2.SaveGameVersion();
			_levelToLoad = _characterGenerator;
		}
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(_levelToLoad == "")
			return;

		if(Application.GetStreamProgressForLevel(_levelToLoad) == 1)					//Check if all level is loaded to cliente (streamed)
		{
			Debug.Log("Level Ready!");
			_percentLoaded = 1;

			if(Application.CanStreamedLevelBeLoaded(_levelToLoad))						//Check if loaded level can be loaded
			{
				Application.LoadLevel(_levelToLoad);
			}

		}
		else
		{
			_percentLoaded = Application.GetStreamProgressForLevel(_levelToLoad);
		}
	}

	void OnGUI()
	{
		if(_displayOptions = true)
		{
			if(_hasCharacter)
			{
				if(GUI.Button(new Rect(10, 10, 110, 25), "Load Character"))
				{
					_levelToLoad = _level1;
					_displayOptions = false;
				}
				
				if(GUI.Button(new Rect(10, 40, 110, 25), "Delete Character"))
				{
					PlayerPrefs.DeleteAll();
					GameSettings2.SaveGameVersion();
					_levelToLoad = _characterGenerator;
					_displayOptions = false;
				}
			}
		}

		if(_levelToLoad == "")
			return;

		GUI.Label(new Rect(Screen.width / 2 - 50, Screen.height - 45, 100, 25), (_percentLoaded * 100 + "%"));
		GUI.Box(new Rect(0, Screen.height - 20, Screen.width * _percentLoaded, 15), "");
	}
}
