using UnityEngine;

public class Armor : Clothing 
{

	private int _armorLevel;		//the elvel of this piece of armor

	public Armor()
	{
		_armorLevel = 0;
	}

	public Armor(int level)
	{
		_armorLevel = level;
	}

	public int ArmorLevel
	{
		get { return _armorLevel; }
		set { _armorLevel = value; }
	}
}
