using UnityEngine;
using System.Collections;

public class Item
{
	private string _name;			//_antesDelNombre significa atributo de la clase
	private int _value;
	private int _curDurabilty;
	private int _maxDurability;
	public Texture2D _icon;
	private RarityTypes _rarity;

	//void Awake()
	//{
	//	Init();
	//}

	//public void Init(){
	//}

	public Item()
	{
		_name = "Need Name";
		_value = 0;
		_rarity = RarityTypes.Common;
		_maxDurability = 50;
		_curDurabilty = _maxDurability; 
	}

	public Item(string name, int value, RarityTypes rarity, int maxDur, int curDur)
	{
		_name = name;
		_value = value;
		_rarity = rarity;
		_maxDurability = maxDur;
		_curDurabilty = curDur; 
	}

	public void Init(string str)
	{

	}

	public string Name
	{
		get{ return _name; }
		set{ _name = value; }
	}

	public int Value
	{
		get{ return _value; }
		set{ _value = value; }
	}

	public RarityTypes Rarity
	{
		get{ return _rarity; }
		set{ _rarity = value; }
	}

	public int MaxDurability
	{
		get{ return _maxDurability; }
		set{ _maxDurability = value; }
	}

	public int CurDurability
	{
		get{ return _curDurabilty; }
		set{ _curDurabilty = value; }
	}

	public Texture2D Icon 
	{
		get { return _icon; }
		set { _icon = value; }
	}

	public virtual string ToolTip()
	{
		return Name + "\n" + 
				"Value: " + Value + "\n" +
				"Durability: " + CurDurability + "/" + MaxDurability +"\n";
	}
}

public enum RarityTypes
{
	Common,
	Uncommon,
	Rare
}
