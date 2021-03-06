using UnityEngine;
using System.Collections.Generic;

public class PlayerCharacter : BaseCharacter 
{
	public static GameObject[] weaponMeshes;

	private static List<Item> _inventory = new List<Item>();

	public static List<Item> Inventory
	{
		get { return _inventory; }
	}

	private static Item _equipedWeapon;

	public static Item EquipedWeapon
	{
		get { return _equipedWeapon; }
		set { _equipedWeapon = value; 

//			HideWeaponMeshes();

			if(EquipedWeapon == null)
				return;

		/*
			switch(_equipedWeapon.Name)
			{
			case "Sword":
				Debug.Log ("Default Sword");
				break;
			case "Axe":
				Debug.Log ("Axe");
				weaponMeshes[1].active = true;
				break;
			case "Runic Axe":
				Debug.Log ("Runic Axe");
				weaponMeshes[3].active = true;
				break;
			case "Scythe":
				Debug.Log ("Scythe");
				weaponMeshes[4].active = true;
				break;
			case "Angelic Sword":
				Debug.Log ("Angelic Sword");
				weaponMeshes[0].active = true;
				break;
			default: 
				Debug.Log ("Fist");
				break;
			}
	*/
			if(wm.childCount > 0)
				Destroy(wm.GetChild(0).gameObject);
			GameObject mesh = Instantiate(Resources.Load(GameSettings2.MELEE_WEAPON_MESH_PATH + _equipedWeapon.Name), wm.position, wm.rotation)  as GameObject;
			mesh.transform.parent = wm;
			}
	}

	/*
	public override void Awake()
	{
		base.Awake();

		Transform weaponMount = transform.Find("Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Spine1/Bip001 Neck/Bip001 R Clavicle/Bip001 R UpperArm/Bip001 R Forearm/Bip001 R Hand/HandMount");
		if(weaponMount != null)
		{
			int count = weaponMount.GetChildCount();
			
			weaponMeshes = new GameObject[count];
			for(int cnt = 0; cnt < count; cnt ++)
			{
				weaponMeshes[cnt] = weaponMount.GetChild(cnt).gameObject;
			}
			
			
//			HideWeaponMeshes();
		}

	}
	*/

	void Update()
	{
		//Messenger<int, int>.Broadcast("player health update", 100, 100);		//Es un Trigger. 80, 100 argumentos con que se lama al metodo 'OnChangeHealthBarSize'	 
	}

	private static void HideWeaponMeshes()
	{
		for(int cnt = 0; cnt < weaponMeshes.Length; cnt++)
		{
			weaponMeshes[cnt].active = false;
			Debug.Log(weaponMeshes[cnt].name);
		}
	}

	private static Transform wm;
	public void Start()
	{
		Transform weaponMount = transform.Find("Bip001/Bip001 Pelvis/Bip001 Spine/Bip001 Spine1/Bip001 Neck/Bip001 R Clavicle/Bip001 R UpperArm/Bip001 R Forearm/Bip001 R Hand/HandMount");
		wm = weaponMount;
	}
}
