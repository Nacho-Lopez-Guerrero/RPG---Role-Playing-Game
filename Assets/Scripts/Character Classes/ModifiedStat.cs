/// <summary>
/// Modified stat.cs
/// 0/12/2014
/// Nacho Lopez
/// 
/// This is the class for all stats that will be modificable by attributes
/// </summary>

using System.Collections.Generic;				//Generic Added so can use List<> class

public class ModifiedStat : BaseStat {

	private List<ModifyingAttribute> _mods;		//A list of attributes that modify this stat
	private int _modValue;						//The amount added to the baseValue from the modifiers

	/// <summary>
	/// Initializes a new instance of the <see cref="ModifiedStat"/> class.
	/// </summary>
	public ModifiedStat()
	{
		_mods = new List<ModifyingAttribute>();
		_modValue = 0;
	}

	/// <summary>
	/// Adds the modifier to the list of mods for this modifiesStat.
	/// </summary>
	/// <param name="mod">
	/// Mod.
	/// </param>
	public void AddModifier(ModifyingAttribute mod)
	{
		_mods.Add(mod);
	}

	/// <summary>
	/// Reset _modValue to 0
	/// Check if we have at least onne ModifyingAttribute in our list of mods.
	/// If we do, then iterate through the list and add the AdjustedBaseValue * ratio to our _modValue.
	/// </summary>
	private void CalculateModValue()
	{
		_modValue = 0;

		if(_mods.Count > 0)
			foreach(ModifyingAttribute att in _mods)
				_modValue += (int)(att.attribute.AdjustedBaseValue * att.ratio);
	}

	/// <summary>
	/// Overrides father class (BaseStat.cs) method
	/// Calculates the AdjustedBaseValue from the BaseValue + BufValue + _modValue
	/// </summary>
	/// <returns>
	/// The adjusted base value.
	/// </returns>
	/// <value>The adjusted base value.</value>
	public new int AdjustedBaseValue
	{
		get{ return BaseValue + BuffValue + _modValue; }	//Usamos Getters para acceder a atributos privados
	}														//de la clase heredada BaseStat

	/// <summary>
	/// NO ES COMO UN UPDATE() DE MONOBEHAVIOUR!!
	/// </summary>
	public void Update()
	{
		CalculateModValue();
	}


	public string GetModifiyingAttributesString()
	{
		string temp = "";

//		UnityEngine.Debug.Log(_mods.Count);

		for(int cnt = 0; cnt < _mods.Count; cnt ++)
		{
			temp += _mods[cnt].attribute.Name;
			temp += "_";
			temp += _mods[cnt].ratio;

			if(cnt < _mods.Count - 1)
				temp += "|";
//			UnityEngine.Debug.Log(_mods[cnt].attribute.Name);
//			UnityEngine.Debug.Log(_mods[cnt].ratio);
		}

//		UnityEngine.Debug.Log(temp);

		return temp;
	}

	public void ClearModifiers()
	{
		_mods.Clear();
	}
}

/// <summary>
/// A struct that will hold an attribute and a ratio that willl be added as a modifying attribute to our ModifiedStats
/// Estructura para indicar el ratio que modifica el atributo la ModifiedStat
/// </summary>
public struct ModifyingAttribute 
{
	public Attribute attribute;			//The attribute that modifies this ModifiedStat (modifier)
	public float ratio;					//the percent of the attribute's adjustedBaseValue that will be applied to this ModifiedStat


	/// <summary>
	/// Initializes a new instance of the <see cref="ModifyingAttribute"/> struct.
	/// </summary>
	/// <param name="att">
	/// Att. the attribute to be used
	/// </param>
	/// <param name="rat">
	/// Rat. ratio to be used
	/// </param>
	public ModifyingAttribute(Attribute att, float rat)
	{
		attribute = att;
		ratio = rat;
	}

}