using UnityEngine;
using System.Collections;
using System;

public static class GameSettings2 
{
	public const string VERSION_KEY_NAME = "ver";
	public const float VERSION_NUMBER = 0.201f;

	//Differents attacks ranges
	public const float BASE_MELEE_ATTACK_TIMER = 2.0f;
	public const float BASE_MELEE_ATTACK_SPEED = 2.0f;

	public const float BASE_MELEE_RANGE = 10f;
	public const float BASE_RANGED_RANGE = 20f;
	public const float BASE_MAGIC_RANGE = 15f;

	//Player_Prefs Keys
	private const string HAIR_COLOR = "Hair Color";
	private const string HAIR_MESH = "Hair Mesh";
	private const string NAME = "Name";
	private const string BASE_VALUE = " - Base Value";
	private const string EXP_TO_LEVEL = " - Exp To Level";
	private const string CURRENT_VALUE = " - Current Value";
	private const string CHARACTER_WIDTH = " - Char Width";
	private const string CHARACTER_HEIGHT = " - Char Height";
	private const string CHARACTER_MODEL_INDEX = " - Model Index";
	private const string PLAYER_POSITION = " - Player Position";
	private const string PLAYER_HEAD_INDEX = "Head Index";
	private const string SKIN_COLOR = "Skin Color";

	//Resources paths
	#region
	public const string MALE_MODEL_PATH = "Character Models/Prefabs/Human/Male/";
	public const string FEMALE_MODEL_PATH = "Character Models/Prefabs/Human/Female/";
	public const string HEAD_TEXTURE_PATH = "Character Models/Materials/Faces/Human/Male/Textures/";

	public const string MELEE_WEAPON_PATH = "Icons/Weapons Icons/Melee/";
	public const string MELEE_WEAPON_MESH_PATH = "Weapons/Melee/Prefabs/";

	public const string SHIELDS_PATH = "Icons/Armor Icons/Shields/";
	public const string SHIELDS_MESH_PATH = "Armor/Shields/Prefabs/";

	public const string HELMETS_PATH = "Icons/Armor Icons/Helmets/";
	public const string HELMETS_MESH_PATH = "Armor/Helmets/Prefabs/";

	public const string HUMAN_MALE_HAIR_MESH_PATH = "Character Models/Hair Mounts/Prefabs/Human/Male/";
	public const string HUMAN_MALE_HAIR_COLOR_PATH = "Character Models/Hair Mounts/Textures/";
	#endregion

	public static Vector3 startingPos = new Vector3(500, 6, 120);

	public static string[] maleModels = {"Samurai", "Knight"};
	public static string[] femaleModels;

//	public static PC pc;

	public static string[] levelNames = {
		"Main Menu", 
		"Character Generator", 
		"Character Customization", 
		"Level 1"
	};


	static GameSettings2()
	{
	//	PC.Instance.Awake();
	}


	public static void SaveCharacterWidth(float width)
	{
		PlayerPrefs.SetFloat(CHARACTER_WIDTH, width);
	}


	public static void SaveCharacterHeight(float height)
	{
		PlayerPrefs.SetFloat(CHARACTER_HEIGHT, height);
	}


	public static void SaveCharacterScale(float width, float height)
	{
		SaveCharacterHeight(height);
		SaveCharacterWidth(width);
	}

	public static float LoadCharacterWidth()
	{
		return PlayerPrefs.GetFloat(CHARACTER_WIDTH, 1);
	}


	public static float LoadCharacterHeight()
	{
		return PlayerPrefs.GetFloat(CHARACTER_HEIGHT, 1);
	}


	public static Vector2 LoadCharacterScale()
	{
		return new Vector2(PlayerPrefs.GetFloat(CHARACTER_WIDTH, 1), PlayerPrefs.GetFloat(CHARACTER_HEIGHT, 1));
	}


	public static void SaveHairColor(int index)
	{
		PlayerPrefs.SetInt(HAIR_COLOR, index);
		Debug.Log(HAIR_COLOR + index);
	}

	public static int LoadHairColor()
	{
		return PlayerPrefs.GetInt(HAIR_COLOR, 0);
	}


	public static void SaveHairMesh(int index)
	{
		PlayerPrefs.SetInt(HAIR_MESH, index);
	}


	public static int LoadHairMesh()
	{
		return PlayerPrefs.GetInt(HAIR_MESH, 0);
	}


	public static void SaveHair(int mesh, int color)
	{
		SaveHairColor(color);
		SaveHairMesh(mesh);
	}


	public static void SaveName(string name)
	{
		PlayerPrefs.SetString(NAME, name);
	}


	public static string LoadName()
	{
		return PlayerPrefs.GetString(NAME, "No name");
	}


	public static void SaveAttribute(AttributeName name, Attribute att)							//The following methods could be done with BaseCharacter Class only, which all this classes derives from
	{
		PlayerPrefs.SetInt(((AttributeName)name).ToString() + BASE_VALUE, att.BaseValue);
		PlayerPrefs.SetInt(((AttributeName)name).ToString() + EXP_TO_LEVEL, att.ExpToLevel);
	}


	public static void LoadAttribute(AttributeName name)
	{
		PC.Instance.GetPrimaryAttribute((int)name).BaseValue =	PlayerPrefs.GetInt(((AttributeName)name).ToString() + BASE_VALUE, 0);
		PC.Instance.GetPrimaryAttribute((int)name).ExpToLevel = PlayerPrefs.GetInt(((AttributeName)name).ToString() + EXP_TO_LEVEL, 0);
	}

	public static void SaveAttributes(Attribute[] atts)
	{
		for(int cnt = 0; cnt < atts.Length; cnt++)
			SaveAttribute((AttributeName)cnt, atts[cnt]);
	}


	public static void LoadAttributes()
	{
		Attribute[] att = new Attribute[Enum.GetValues(typeof(AttributeName)).Length];

		for(int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt ++)
			LoadAttribute((AttributeName)cnt);

	}


	public static void SaveVital(VitalName name, Vital vital)
	{
		PlayerPrefs.SetInt(((VitalName)name).ToString() + BASE_VALUE, vital.BaseValue);
		PlayerPrefs.SetInt(((VitalName)name).ToString() + EXP_TO_LEVEL, vital.ExpToLevel);
		PlayerPrefs.SetInt(((VitalName)name).ToString() + CURRENT_VALUE, vital.CurValue);
	}
	

	public static void LoadVital(VitalName name)
	{
		Vital temp = new Vital();

		PC.Instance.GetVital((int)name).BaseValue = PlayerPrefs.GetInt(((VitalName)name).ToString() + BASE_VALUE, 0);
		PC.Instance.GetVital((int)name).ExpToLevel = PlayerPrefs.GetInt(((VitalName)name).ToString() + EXP_TO_LEVEL, 0);
		//			pcClass.GetVital(cnt).CurValue = PlayerPrefs.GetInt(((VitalName)cnt).ToString() + " - Current Value", 0);
		
		//Calculamos el 'AdjustedValue' para Obtener el 'CurValue' actualizado
		PC.Instance.GetVital((int)name).Update();
		
		//Obtien el valor almacenado de cada Vital
		PC.Instance.GetVital((int)name).CurValue = PlayerPrefs.GetInt(((VitalName)name).ToString() + CURRENT_VALUE, 1);


		/*
		Vital temp = new Vital();

		temp.BaseValue = PlayerPrefs.GetInt(((VitalName)name).ToString() + BASE_VALUE, 0);
		temp.ExpToLevel = PlayerPrefs.GetInt(((VitalName)name).ToString() + EXP_TO_LEVEL, 0);

		//Calculamos el 'AdjustedValue' para Obtener el 'CurValue' actualizado
		temp.Update();

		//Obtien el valor almacenado de cada Vital
		temp.CurValue = PlayerPrefs.GetInt(((VitalName)name).ToString() + CURRENT_VALUE, 1);

		return temp;
		*/
	}


	public static void LoadVitals()
	{
		for(int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt ++)
			LoadVital((VitalName)cnt);
	}


	public static void SaveVitals(Vital[] vitals)
	{
		for(int cnt = 0; cnt < vitals.Length; cnt++)
			SaveVital((VitalName)cnt, vitals[cnt]);
	}


	public static void SaveSkill(SkillName name, Skill skill)
	{
		PlayerPrefs.SetInt(((SkillName)name).ToString() + BASE_VALUE, skill.BaseValue);
		PlayerPrefs.SetInt(((SkillName)name).ToString() + EXP_TO_LEVEL, skill.ExpToLevel);
	}


	public static void SaveSkills(Skill[] skills)
	{
		for(int cnt = 0; cnt < skills.Length; cnt++)
			SaveSkill((SkillName)cnt, skills[cnt]);
	}


	public static void LoadSkill(SkillName name)
	{
		PC.Instance.GetSkill((int)name).BaseValue =	PlayerPrefs.GetInt(((SkillName)name).ToString() + BASE_VALUE, 0);
		PC.Instance.GetSkill((int)name).ExpToLevel = PlayerPrefs.GetInt(((SkillName)name).ToString() + EXP_TO_LEVEL, 0);
		PC.Instance.GetSkill((int)name).Update();
	}


	public static void LoadSkills()
	{
		for(int cnt = 0; cnt < Enum.GetValues(typeof(SkillName)).Length; cnt ++)
			LoadSkill((SkillName)cnt);
	}


	public static void SaveCharacterModelIndex(int index)
	{
		PlayerPrefs.SetInt(CHARACTER_MODEL_INDEX, index);
	}


	public static int LoadCharacterModelIndex()
	{
		return PlayerPrefs.GetInt(CHARACTER_MODEL_INDEX, 0);
	}


	public static void SavePlayerPosition(Vector3 pos)
	{
		PlayerPrefs.SetFloat(PLAYER_POSITION + "x", pos.x);
		PlayerPrefs.SetFloat(PLAYER_POSITION + "y", pos.y);
		PlayerPrefs.SetFloat(PLAYER_POSITION + "z", pos.z);
	}


	public static Vector3 LoadPlayerPosition()
	{
		Vector3 pos = new Vector3();

		pos.x = PlayerPrefs.GetFloat(PLAYER_POSITION + "x", startingPos.x);
		pos.y = PlayerPrefs.GetFloat(PLAYER_POSITION + "y", startingPos.y);
		pos.z = PlayerPrefs.GetFloat(PLAYER_POSITION + "z", startingPos.z);

		return pos;

	}


	public static void SaveHeadlIndex(int index)
	{
		PlayerPrefs.SetInt(PLAYER_HEAD_INDEX, index);
	}


	public static int LoadHeadIndex()
	{
		return PlayerPrefs.GetInt(PLAYER_HEAD_INDEX, 0);
	}


	public static void SaveSkinColor(int index)
	{
		PlayerPrefs.SetInt(SKIN_COLOR, index);
	}


	public static int LoadSkinColor()
	{
		return PlayerPrefs.GetInt(SKIN_COLOR, 0);
	}


	public static void SaveGameVersion()
	{
		PlayerPrefs.SetFloat(VERSION_KEY_NAME, VERSION_NUMBER);
	}


	public static float LoadGameVersion()
	{
		return PlayerPrefs.GetFloat(VERSION_KEY_NAME, 0);
	}

}


public enum CharacterMaterialIndex
{
	Feet = 0,
	Face = 1,
	Pants = 2,
	Torso = 3,
	Hands = 4
}
