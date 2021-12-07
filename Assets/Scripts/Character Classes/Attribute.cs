/// <summary>
/// Attribute.cs
/// 02/12/2014
/// Nacho Lopez
/// 
/// This is the class for all of the caracter's attributes ingame
/// </summary>

public class Attribute : BaseStat 
{
	//this is the starting exp cost for all of attributes
	new public const int STARTING_EXP_COST = 50;		//new para dar nuevo valor a la constante (que hereda)


	/// <summary>
	/// Initializes a new instance of the <see cref="Attribute"/> class.
	/// </summary>
	public Attribute()
	{
		ExpToLevel = STARTING_EXP_COST;
		LevelModifier = 1.05f;
	}

	
}
