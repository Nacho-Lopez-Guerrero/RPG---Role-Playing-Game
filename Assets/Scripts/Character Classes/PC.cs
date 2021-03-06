using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[AddComponentMenu("Hack And Slash/Player/Player Characer Stats")]
public class PC : BaseCharacter 
{
	private static PC instance = null;

	private static Item _equipedWeapon;

	private static List<Item> _inventory = new List<Item>();

	private static Item[] _equipment = new Item[(int)EquipmentSlot.COUNT];

	
	private bool _initialized = false;


	public List<Item> Inventory
	{
		get { return _inventory; }
		set { _inventory = value; }
	}


	public static PC Instance
	{
		get { 
			if(instance == null)
			{
				Debug.Log("Instancing a new PC");
				GameObject go = Instantiate(Resources.Load(GameSettings2.MALE_MODEL_PATH + GameSettings2.maleModels[GameSettings2.LoadCharacterModelIndex()]),
				                			GameSettings2.LoadPlayerPosition(), 
				                       		Quaternion.identity) as GameObject;

				//go.transform.localScale = new Vector3(4, 4, 4);

				PC temp = go.GetComponent<PC>();
				if(temp == null)
					Debug.LogError("Player Prefab does not contain an PC script. Please add and configure");

				instance = go.GetComponent<PC>();

				go.name = "pc";
				go.tag = "Player";
			}

			return instance;
		}
	}


	public Item EquipedShield
	{
		get { return _equipment[(int)EquipmentSlot.OffHand]; }
		set {
			_equipment[(int)EquipmentSlot.OffHand] = value;
			//_equipedWeapon = value;
			
			if(offHandMount.transform.childCount > 0)
				Destroy(offHandMount.transform.GetChild(0).gameObject);
			//if(_equipedWeapon != null)
			
			if(_equipment[(int)EquipmentSlot.OffHand] != null)
			{
				GameObject mesh = Instantiate(Resources.Load(GameSettings2.SHIELDS_MESH_PATH + _equipment[(int)EquipmentSlot.OffHand].Name), offHandMount.transform.position, offHandMount.transform.rotation) as GameObject;

				mesh.transform.localScale = new Vector3(mesh.transform.localScale.x / 2, mesh.transform.localScale.y / 2,mesh.transform.localScale.z / 2);
				mesh.transform.parent = offHandMount.transform;


			}
		}
	}


	public Item EquipedHelmet
	{
		get { return _equipment[(int)EquipmentSlot.Head]; }
		set {
			_equipment[(int)EquipmentSlot.Head] = value;
			//_equipedWeapon = value;
			
			if(helmetMount.transform.childCount > 0)
				Destroy(helmetMount.transform.GetChild(0).gameObject);
			//if(_equipedWeapon != null)
			
			if(_equipment[(int)EquipmentSlot.Head] != null)
			{
				GameObject mesh = Instantiate(Resources.Load(GameSettings2.HELMETS_MESH_PATH + _equipment[(int)EquipmentSlot.Head].Name), helmetMount.transform.position, helmetMount.transform.rotation) as GameObject;
			
				//scale
				mesh.transform.localScale = new Vector3(mesh.transform.localScale.x / 2, mesh.transform.localScale.y / 2,mesh.transform.localScale.z / 2);

				//hide player's  hair
				hairMount.transform.GetChild(0).gameObject.active = false;

				mesh.transform.parent = helmetMount.transform;
			}
		}
	}


	public void LoadCharacter()
	{
		GameSettings2.LoadAttributes();

		ClearModifiers();

		GameSettings2.LoadSkills();
		GameSettings2.LoadVitals();

		Debug.Log (primaryAttributes[0].ExpToLevel);
	
		LoadScale();
		LoadHair();
		LoadSkinColor();

		_initialized = true;
	}


	public void LoadHairMesh()
	{	
		GameObject hairStyle;

		if(PC.Instance.hairMount.transform.childCount > 0)
			Object.Destroy(PC.Instance.hairMount.transform.GetChild(0).gameObject);


		int hairMeshIndex = GameSettings2.LoadHairMesh();
			
		hairStyle = Object.Instantiate( Resources.Load(GameSettings2.HUMAN_MALE_HAIR_MESH_PATH + "Hair_" + hairMeshIndex), 
		                               hairMount.transform.position, 
		                               hairMount.transform.rotation) as GameObject;				//Use Object class because class dont descend from monobeahviour

		hairStyle.transform.parent = hairMount.transform;

		LoadHairColor();

		MeshOffset mo = hairStyle.GetComponent<MeshOffset>();
		if(mo == null)
			return;
		
		hairStyle.transform.localPosition = mo.positionalOffset;
		hairStyle.transform.localRotation = Quaternion.Euler(mo.rotationalOffset);
		hairStyle.transform.localScale = mo.scaleOffset;
	}


	public void LoadHairColor()
	{
		Texture temp = Resources.Load(GameSettings2.HUMAN_MALE_HAIR_COLOR_PATH + ((HairColorNames)GameSettings2.LoadHairColor()).ToString()) as Texture;

		hairMount.transform.GetChild(0).GetComponent<Renderer>().material.mainTexture = temp;
	}


	public void LoadHair()
	{
		LoadHairMesh();
		LoadHairColor();

	}


	public void LoadSkinColor()
	{
		PC.Instance.characterMaterialMesh.GetComponent<Renderer>().materials[(int)CharacterMaterialIndex.Face].mainTexture = Resources.Load(GameSettings2.HEAD_TEXTURE_PATH + "head_" + GameSettings2.LoadHeadIndex() + "_" + GameSettings2.LoadSkinColor() + ".human") as Texture;
	}


	public void LoadScale()
	{
		Vector2 scale = GameSettings2.LoadCharacterScale();

		Debug.Log("Scale1 = " + transform.localScale);
		transform.localScale = new Vector3((transform.localScale.x + 0.2f) * scale.x,
		                                   (transform.localScale.y + 0.2f) * scale.y,
		                                   (transform.localScale.z + 0.2f) * scale.x);

		Debug.Log("Scale2 = " + transform.localScale);

	}


	public void LoadStats()
	{

	}


	public void LoadTorso()
	{

	}


	public void LoadTorsoArmor()
	{

	}

	public void LoadGloves()
	{

	}


	public void LoadLegArmor()
	{

	}


	public void LoadBoots()
	{

	}


	public static void DisplayWeaponMountName()
	{
		Debug.Log(instance.weaponMount.name);
	}


	public void Initialize()
	{
		if(!_initialized)
			LoadCharacter();
	}


	#region Unity Monobehaviour Functions
	public void Awake()
	{	
		base.Awake();
		instance = this;
	}


	void Update()
	{
		//Messenger<int, int>.Broadcast("player health update", 100, 100);		//Es un Trigger. 80, 100 argumentos con que se lama al metodo 'OnChangeHealthBarSize'	 
	}
	#endregion

}
