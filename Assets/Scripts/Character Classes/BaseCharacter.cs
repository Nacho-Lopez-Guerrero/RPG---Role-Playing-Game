using UnityEngine;
using System.Collections;
using System;			//added to acces the 'enum' class

public class BaseCharacter : MonoBehaviour 
{
	public GameObject weaponMount;
	public GameObject offHandMount;

	public GameObject characterMaterialMesh;
	public GameObject hairMount;
	public GameObject helmetMount;

	private string _name;
	private int _level;
	private uint _freeExp;

	public Attribute[] primaryAttributes;
	public Vital[] vitals;
	public Skill[] skills;

	private bool _inCombat;

	public float meleeAttackTimer = GameSettings2.BASE_MELEE_ATTACK_TIMER;
	public float meleeAttackSpeed = GameSettings2.BASE_MELEE_ATTACK_SPEED;
	public float meleeResetTimer = 0f;

	protected Item[] _equipment = new Item[(int)EquipmentSlot.COUNT];			//Protected para que peuden acceder al atributo las clases derivadas


	public virtual void Awake()
	{			
		_name = string.Empty;
		_level = 0;
		_freeExp = 0;
		_inCombat = false;
		primaryAttributes = new Attribute[Enum.GetValues(typeof(AttributeName)).Length];
		vitals = new Vital[Enum.GetValues(typeof(VitalName)).Length];
		skills = new Skill[Enum.GetValues(typeof(SkillName)).Length];

		SetupPrimaryAttributes();
		SetupVitals();
		SetupSkills();
	}


	//Setters and Getters
	public string Name
	{
		get{ return _name; }
		set{ _name = value; }
	}

	public bool InCombat
	{
		get{ return _inCombat; }
		set{ _inCombat = value; }
	}

	public int Level
	{
		get{ return _level; }
		set{ _level = value; }
	}


	public uint FreeExp
	{
		get{ return _freeExp; }
		set{ _freeExp = value; }
	}
	//end Getters Setters


	public Attribute GetPrimaryAttribute(int index)
	{
		return primaryAttributes[index];
	}


	public Vital GetVital(int index)
	{
		return vitals[index];
	}


	public Skill GetSkill(int index)
	{
		return skills[index];
	}


	public void ClearModifiers()
	{
		for(int cnt = 0; cnt < vitals.Length; cnt++)
			vitals[cnt].ClearModifiers();

		for(int cnt = 0; cnt < skills.Length; cnt++)
			skills	[cnt].ClearModifiers();

		SetupVitalModifiers();
		SetupSkillModifiers();
	}


	public void AddExp(uint exp)
	{
		_freeExp += exp;
		CalculateLevel();
	}


	public void CalculateLevel()
	{

	}


	private void SetupPrimaryAttributes()
	{
		for(int i = 0; i < primaryAttributes.Length; i++)
		{
			primaryAttributes[i] = new Attribute();
			primaryAttributes[i].Name = ((AttributeName)i).ToString();

		}
	}


	private void SetupVitals()
	{
		for(int i = 0; i < vitals.Length; i++)
		{
			vitals[i] = new Vital();
			//Added by me!
			//vitals[i].Name = ((VitalName)i).ToString();
		}
		SetupVitalModifiers();
	}


	private void SetupSkills()
	{
		for(int i = 0; i < skills.Length; i++)
		{
			skills[i] = new Skill();
			//Added by me!
			//skills[i].Name = ((SkillName)i).ToString();
		}

		SetupSkillModifiers();
	}


	private void SetupVitalModifiers()
	{
		//Health
		ModifyingAttribute healthModifier = new ModifyingAttribute();
		healthModifier.attribute = GetPrimaryAttribute((int)AttributeName.Constitution);
		healthModifier.ratio = 0.5f;

		GetVital((int)VitalName.Health).AddModifier(healthModifier);

		//Energy
		ModifyingAttribute energyModifier = new ModifyingAttribute();
		energyModifier.attribute = GetPrimaryAttribute((int)AttributeName.Constitution);
		energyModifier.ratio = 1;
		
		GetVital((int)VitalName.Energy).AddModifier(energyModifier);

		//Mana
		ModifyingAttribute manaModifier = new ModifyingAttribute();
		manaModifier.attribute = GetPrimaryAttribute((int)AttributeName.Willpower);
		manaModifier.ratio = 1;
		
		GetVital((int)VitalName.Mana).AddModifier(manaModifier);
	}


	private void SetupSkillModifiers()
	{
		ModifyingAttribute MeleeOffenceModifier1 = new ModifyingAttribute();
		ModifyingAttribute MeleeOffenceModifier2 = new ModifyingAttribute();

		MeleeOffenceModifier1.attribute = GetPrimaryAttribute((int)AttributeName.Might);
		MeleeOffenceModifier1.ratio = 0.33f;

		MeleeOffenceModifier2.attribute = GetPrimaryAttribute((int)AttributeName.Nimbleness);
		MeleeOffenceModifier2.ratio = 0.33f;

		//Melee Offence
		GetSkill((int)SkillName.Melee_Offence).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Might), 0.33f));
		GetSkill((int)SkillName.Melee_Offence).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Nimbleness), 0.33f));
		//Melee Deffence
		GetSkill((int)SkillName.Melee_Deffence).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Speed), 0.33f));
		GetSkill((int)SkillName.Melee_Deffence).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Constitution), 0.33f));
		//Magic Offence
		GetSkill((int)SkillName.Magic_Offence).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Concentration), 0.33f));
		GetSkill((int)SkillName.Magic_Offence).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Willpower), 0.33f));
		//Magic Deffence
		GetSkill((int)SkillName.Magic_Defence).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Concentration), 0.33f));
		GetSkill((int)SkillName.Magic_Defence).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Willpower), 0.33f));
		//Ranged Offence
		GetSkill((int)SkillName.Ranged_Offence).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Concentration), 0.33f));
		GetSkill((int)SkillName.Ranged_Offence).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Speed), 0.33f));
		//Ranged Deffence
		GetSkill((int)SkillName.Ranged_Deffence).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Speed), 0.33f));
		GetSkill((int)SkillName.Ranged_Deffence).AddModifier(new ModifyingAttribute(GetPrimaryAttribute((int)AttributeName.Nimbleness), 0.33f));
	}


	public void StatUpdate()
	{
		for(int cnt = 0; cnt < vitals.Length; cnt++)
			vitals[cnt].Update();

		for(int cnt = 0; cnt < skills.Length; cnt++)
			skills[cnt].Update();

	}


	public Item EquipedWeapon
	{
		get { return _equipment[(int)EquipmentSlot.MainHand]; }
		set {
			_equipment[(int)EquipmentSlot.MainHand] = value;
			//_equipedWeapon = value;
			
			if(weaponMount.transform.childCount > 0)
				Destroy(weaponMount.transform.GetChild(0).gameObject);
			//if(_equipedWeapon != null)
			
			if(_equipment[(int)EquipmentSlot.MainHand] != null) 
			{
				GameObject mesh = Instantiate(Resources.Load(GameSettings2.MELEE_WEAPON_MESH_PATH + _equipment[(int)EquipmentSlot.MainHand].Name), weaponMount.transform.position,weaponMount.transform.rotation) as GameObject;
				mesh.transform.localScale = new Vector3(mesh.transform.localScale.x * 3, mesh.transform.localScale.y * 3,mesh.transform.localScale.z * 3);
				mesh.transform.parent = weaponMount.transform;
			}
		}
	}


	public void CalculateMeleeAttackSpeed()
	{

	}



}
