using UnityEngine;
using System.Collections;
using System;

public class GameSettings : MonoBehaviour {

	public const string PLAYER_SPAWN_POINT = "Player Spawn Point";			//Name of the GameObject where the player will spawn at begin



	public static string[] levelNames = {
		"Main Menu", 
		"Character Generator", 
		"Character Customization", 
		"Level 1"
	};

	void Awake()
	{
		DontDestroyOnLoad(this);
	}

	public void SaveCharacterData()
	{
		GameObject player = GameObject.Find("pc");

		PlayerCharacter pcClass = player.GetComponent<PlayerCharacter>();

//		PlayerPrefs.DeleteAll();

		PlayerPrefs.SetString("PlayerName", pcClass.Name);

		//Guardamos valor de atributos del jugador en PlayerPrefs
		for(int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt ++)
		{
			PlayerPrefs.SetInt(((AttributeName)cnt).ToString() + " - Base Value", pcClass.GetPrimaryAttribute(cnt).BaseValue);
			PlayerPrefs.SetInt(((AttributeName)cnt).ToString() + " - Exp to Level", pcClass.GetPrimaryAttribute(cnt).ExpToLevel);

		}

		//Guardamos valor de Vitals del jugador en PlayerPrefs
		for(int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt ++)
		{
			PlayerPrefs.SetInt(((VitalName)cnt).ToString() + " - Base Value", pcClass.GetVital(cnt).BaseValue);
			PlayerPrefs.SetInt(((VitalName)cnt).ToString() + " - Exp to Level", pcClass.GetVital(cnt).ExpToLevel);
			PlayerPrefs.SetInt(((VitalName)cnt).ToString() + " - Current Value", pcClass.GetVital(cnt).CurValue);

//			PlayerPrefs.SetString(((VitalName)cnt).ToString() + " - Mods", pcClass.GetVital(cnt).GetModifiyingAttributesString());

//			pcClass.GetVital(cnt).GetModifiyingAttributesString();
		}



		//Guardamos valor de las Skills del jugador en PlayerPrefs
		for(int cnt = 0; cnt < Enum.GetValues(typeof(SkillName)).Length; cnt ++)
		{
			PlayerPrefs.SetInt(((SkillName)cnt).ToString() + " - Base Value", pcClass.GetSkill(cnt).BaseValue);
			PlayerPrefs.SetInt(((SkillName)cnt).ToString() + " - Exp to Level", pcClass.GetSkill(cnt).ExpToLevel);

//			PlayerPrefs.SetString(((SkillName)cnt).ToString() + " - Mods", pcClass.GetSkill(cnt).GetModifiyingAttributesString());

//			pcClass.GetSkill(cnt).GetModifiyingAttributesString();
		}

		Debug.Log(PlayerPrefs.GetString("Ranged_Offence - Mods"));
		Debug.Log(PlayerPrefs.GetString("Mana - Mods"));
		Debug.Log(PlayerPrefs.GetString("Energy - Mods"));
		Debug.Log(PlayerPrefs.GetString("Health - Mods"));

	}

	public void LoadCharacterData()
	{
		GameObject player = GameObject.Find("pc");

		PlayerCharacter pcClass = player.GetComponent<PlayerCharacter>();

		pcClass.Name = PlayerPrefs.GetString("PlayerName", "Name Me");
//		pcClass.Awake();
		//Leemos y asignamos valor de atributos del jugador en PlayerPrefs
		for(int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt ++)
		{

			pcClass.GetPrimaryAttribute(cnt).BaseValue = PlayerPrefs.GetInt(((AttributeName)cnt).ToString() + " - Base Value", 0);
			pcClass.GetPrimaryAttribute(cnt).ExpToLevel = PlayerPrefs.GetInt(((AttributeName)cnt).ToString() + " - Exp to Level", Attribute.STARTING_EXP_COST);		
		}


		
		//cargamos valor de Vitals del jugador en PlayerPrefs
		for(int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt ++)
		{
			if(PlayerPrefs.HasKey(((VitalName)cnt).ToString() + " - Current Value"))
				Debug.Log(((VitalName)cnt).ToString() + " " + PlayerPrefs.GetInt(((VitalName)cnt).ToString() + " - Current Value", Attribute.STARTING_EXP_COST));
	
			pcClass.GetVital(cnt).BaseValue = PlayerPrefs.GetInt(((VitalName)cnt).ToString() + " - Base Value", 0);
			pcClass.GetVital(cnt).ExpToLevel = PlayerPrefs.GetInt(((VitalName)cnt).ToString() + " - Exp to Level", 0);
//			pcClass.GetVital(cnt).CurValue = PlayerPrefs.GetInt(((VitalName)cnt).ToString() + " - Current Value", 0);

			//Calculamos el 'AdjustedValue' para Obtener el 'CurValue' actualizado
			pcClass.GetVital(cnt).Update();

			//Obtien el valor almacenado de cada Vital
			pcClass.GetVital(cnt).CurValue = PlayerPrefs.GetInt(((VitalName)cnt).ToString() + " - Current Value", 1);

		}

		//Cargamos el valor de las habilidades desde PlayerPrefs
		for(int cnt = 0; cnt < Enum.GetValues(typeof(SkillName)).Length; cnt ++)
		{
			pcClass.GetSkill(cnt).BaseValue = PlayerPrefs.GetInt(((SkillName)cnt).ToString() + " - Base Value", 0);
			pcClass.GetSkill(cnt).ExpToLevel = PlayerPrefs.GetInt(((SkillName)cnt).ToString() + " - Exp to Level", 0);

			//			pcClass.GetSkill(cnt).GetModifiyingAttributesString();
		}


				for(int i = 0; i < Enum.GetValues(typeof(SkillName)).Length; i++)
				{
			Debug.Log(((SkillName)i).ToString() + ":" + pcClass.GetSkill(i).BaseValue + "-" + pcClass.GetSkill(i).ExpToLevel);
				}

	}	

}


