using UnityEngine;
using System.Collections;

public static class ItemGenerator 
{
	public const int BASE_MELEE_RANGE = 1;
	public const int BASE_RANGED_RANGE = 5;

	private const string MELEE_WEAPON_PATH = "Weapons/Melee/";

	public static Item CreateItem()
	{
		int rand = Random.Range(0, (int)ItemType.COUNT);


		//item = CreateWeapon();
		Item item = CreateItem((ItemType)rand);

		return item;
	}


	public static Item CreateItem(ItemType type)
	{
		Item item = new Item();
			
		switch(type)
		{
		case ItemType.MeleeWeapon:
			item = CreateMeleeWeapon();
			break;
		//case ItemType.RangedWeapon:
		//	item = CreateRangedWeapon();
		//	break;
		case ItemType.Armor:
			item = CreateArmor();
			break;
		}

		item.Value = Random.Range(1, 101);

		item.Rarity = RarityTypes.Common;

		item.MaxDurability = Random.Range(50, 61);
		item.CurDurability = item.MaxDurability;

		return item;
	}


	private static Weapon CreateRangedWeapon()
	{
		Weapon rw = new Weapon();

		return rw;
	}


	public static Weapon CreateMeleeWeapon()
	{
		Weapon meleeWeapon = new Weapon();

		string[] weaponNames = new string[] {
											"Axe",
											"Angelic Sword",
											"Runic Axe",
											"Scythe", };

		//Assign name to the wepons 
		meleeWeapon.Name = weaponNames[Random.Range(0, weaponNames.Length)];

		//Assign the icon for the weapon
		meleeWeapon.Icon = Resources.Load(GameSettings2.MELEE_WEAPON_PATH + meleeWeapon.Name) as Texture2D;

		meleeWeapon.MaxDamage = Random.Range(5, 11);
		meleeWeapon.DamageVariance = Random.Range(2f, 0.76f);
		meleeWeapon.TypeOfDamage = DamageType.Slash;
		meleeWeapon.MaxRange = BASE_MELEE_RANGE;


		return meleeWeapon;

	}


	private static Armor CreateArmor()
	{
		int temp = Random.Range(0, 2);
		Armor armor = new Armor();

		switch(temp)
		{
			case 0:
				armor = CreateShield();
				break;
			case 1:
				Debug.Log("Creating Helemt");
				armor = CreateHelmet();
				break;
		}



		return armor;
	}


	private static Armor CreateShield()
	{
		Armor armor = new Armor();
		
		string[] shieldNames = new string[] {
			"Metal Shield"
			 };
		
		//Assign name to the wepons 
		armor.Name = shieldNames[Random.Range(0, shieldNames.Length)];
		
		//Assign the icon for the weapon (item)
		armor.Icon = Resources.Load(GameSettings2.SHIELDS_PATH + armor.Name) as Texture2D;
		
		//Assign Armor self properties
		armor.ArmorLevel = Random.Range(10, 50);
		armor.Slot = EquipmentSlot.OffHand;

		return armor;
	}


	private static Armor CreateHelmet()
	{
		Armor armor = new Armor();
		
		string[] helmetNames = new string[] {
			"Santa Hat"
		};
		
		//Assign name to the wepons 
		armor.Name = helmetNames[Random.Range(0, helmetNames.Length)];
		
		//Assign the icon for the weapon (item)
		armor.Icon = Resources.Load(GameSettings2.HELMETS_PATH + armor.Name) as Texture2D;
		
		//Assign Armor self properties
		armor.ArmorLevel = Random.Range(10, 50);
		armor.Slot = EquipmentSlot.Head;

		return armor;
	}
	

}


public enum ItemType
{
	MeleeWeapon,
	Armor,
	//Potion,
	//Scroll,
	//RangedWeapon
	COUNT
}
