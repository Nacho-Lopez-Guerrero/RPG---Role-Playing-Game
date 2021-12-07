/// <summary>
/// Base stat.cs
/// Nacho Lopez
/// 02/12/2014
/// 
/// This is the base class for a stat ingame
/// </summary>

using UnityEngine;								//Necessary to use 'Debug' class

public class BaseStat {

	public const int STARTING_EXP_COST = 100;	//Value for all stats to start at 

	private int _baseValue;						//The base value of this stat (level)
	private int _buffValue;						//The amount of the buff to this stat
	private int _expToLevel;					//The total amount of exp needed to raise skill
	private float _levelModifier;				//The modifier applied to the exp needed to raise skill

	private string _name;						//this is the name of the attribute


	/// <summary>
	/// Initializes a new instance of the <see cref="BaseStat"/> class.
	/// </summary>
	public BaseStat()
	{
		_name = "";
		_baseValue = 0;
		_buffValue = 0;
		_levelModifier = 1.1f;
		_expToLevel = STARTING_EXP_COST;
	}



#region Basic Setters and Getters
	//Basic Setters and Getters
	/// <summary>
	/// Gets or sets the _baseValue.
	/// </summary>
	/// <value>The _baseValue.</value>
	public int BaseValue		//Cabeceras de funciones sin { } = Funcion set/get
	{
		get{ return _baseValue; }
		set{ _baseValue = value; }
	}

	/// <summary>
	/// Gets or sets the _buffValue.
	/// </summary>
	/// <value>The _buffValue.</value>
	public int BuffValue
	{
		get{ return _buffValue; }
		set{ _buffValue = value; }
	}

	/// <summary>
	/// Gets or sets the _expToLevel.
	/// </summary>
	/// <value>The exp to level.</value>
	public int ExpToLevel
	{
		get{ return _expToLevel; }
		set{ _expToLevel = value; }
	}

	/// <summary>
	/// Gets or sets the _levelModifier.
	/// </summary>
	/// <value>The _levelModifier.</value>
	public float LevelModifier
	{
		get{ return _levelModifier; }
		set{ _levelModifier = value; }
	}

	/// <summary>
	/// Gets or sets the _name.
	/// </summary>
	/// <value>
	/// The _name.
	/// </value>
	public string Name {
		
		get{ return _name; }
		set { _name = value; }
	}
#endregion

	/// <summary>
	/// Calculate the exp to level and return it
	/// </summary>
	/// <retunrs>
	/// The exp to level
	/// </returns>
	private int CalculateExpToLevel()
	{
		return (int)(_expToLevel * _levelModifier);
	}

	/// <summary>
	/// Assign the new value to _expToLevel and then increase the _baseValue by one.
	/// </summary>
	public void LevelUp()
	{
		_expToLevel = CalculateExpToLevel();
		_baseValue++;		//BaseValue = nivel
	}

	/// <summary>
	/// Recalculate adjusted base value and return it
	/// </summary>
	/// <returns>
	/// The adjusted base value.
	/// </returns>
	public int AdjustedBaseValue
	{
		get{ return _baseValue + _buffValue; }
	}
}
