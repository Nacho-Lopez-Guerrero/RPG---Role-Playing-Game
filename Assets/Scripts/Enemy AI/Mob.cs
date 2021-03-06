#define DEBUGGER

using UnityEngine;
using System.Collections;
using System;							//To acces enum class


[AddComponentMenu("Hack And Slash/Enemy/AI/All Enemy Scripts")]
[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(AI))]
[RequireComponent(typeof(AdvancedMovement))]

public class Mob : BaseCharacter 
{
#if DEBUGGER
	public bool debugger = true;
#endif
	private	Transform _displayName;

	static public GameObject camera;


	// Use this for initialization
	void Start () 
	{
		_displayName = transform.FindChild("Name");
		camera = GameObject.Find("Main Camera");

//		GetPrimaryAttribute((int)AttributeName.Constitution).BaseValue = 100;
//		GetVital((int)VitalName.Health).Update();

		if(_displayName == null)
		{
			Debug.LogWarning("Pllease add name script to this enemy");
			return;
		}

		_displayName.GetComponent<TextMesh>().text = gameObject.name;
	}


	new void Awake()
	{
		base.Awake();

		Spawn();
	}


	void Update()
	{
		if(_displayName == null)
		{
			Debug.LogWarning("Pllease add name script to this enemy");
			return;
		}

		_displayName.LookAt(camera.transform);
		_displayName.Rotate(new Vector3(0, 180, 0));
	}


	public void DisplayHealth()
	{
//		Messenger<int, int>.Broadcast("mob health update", curHealth, maxHealth);		//Es un Trigger. curHealth y maxHealth son los argumentos con que se lama al metodo 'OnChangeHealthBarSize'	 
	}


	private void Spawn()
	{
		//Setup attributes and skills
		SetupStats();

		//Setup gear
		SetupGear();
	}


	private void SetupStats()
	{			
		for(int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++)
			GetPrimaryAttribute(cnt).BaseValue = UnityEngine.Random.Range(50, 101);

		StatUpdate();

		for(int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++)
			GetVital(cnt).CurValue = UnityEngine.Random.Range(50, 101);
	}

	#if DEBUGGER
	//Display mob's stats if we have a debugger defined
	void OnGUI()
	{	
		if(debugger)
		{
			int lh = 25;
			//Attributes
			for(int cnt = 0; cnt < Enum.GetValues(typeof(AttributeName)).Length; cnt++)
				GUI.Label(new Rect(10, 10 + cnt * lh, 140, 25), ((AttributeName)cnt).ToString() + " : " + GetPrimaryAttribute(cnt).BaseValue);
			//Vitals
			for(int cnt = 0; cnt < Enum.GetValues(typeof(VitalName)).Length; cnt++)
				GUI.Label(new Rect(10, 10 + cnt * lh + Enum.GetValues(typeof(AttributeName)).Length * lh, 140, 25), ((VitalName)cnt).ToString() + " : " + GetVital(cnt).CurValue + " / " + GetVital(cnt).AdjustedBaseValue);
			//Skills
			for(int cnt = 0; cnt < Enum.GetValues(typeof(SkillName)).Length; cnt++)
				GUI.Label(new Rect(310, 10 + cnt * lh, 140, 25), ((SkillName)cnt).ToString() + " : " + GetSkill(cnt).AdjustedBaseValue);
		}
	}
	#endif

	private void SetupGear()
	{
		EquipedWeapon = ItemGenerator.CreateItem(ItemType.MeleeWeapon);
	}
}	
	
