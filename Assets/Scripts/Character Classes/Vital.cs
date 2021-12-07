/// <summary>
/// Vital.cs
/// 03/12/2014
/// Nacho Lopez
/// 
/// This class contains the extra functions for a character's vital
/// </summary>
public class Vital : ModifiedStat 
{
	private int _curValue;			//This is the current value of this vital

	/// <summary>
	/// Initializes a new instance of the <see cref="Vital"/> class.
	/// </summary>
	public Vital()
	{
		_curValue = 0;
		ExpToLevel = 50;
		LevelModifier = 1.1f;
	}

	/// <summary>
	/// When getting the curValue make sure that it os not greater than our AdjustedBaseValue
	/// If it is, make it the same as our adjustedBaseValue
	/// </summary>
	/// <value>The current value.
	/// </value>
	public int CurValue
	{
		get{ 
			if(_curValue > AdjustedBaseValue)
				_curValue = AdjustedBaseValue;

			return _curValue;
		}
		set{ _curValue = value; }
	}
}
