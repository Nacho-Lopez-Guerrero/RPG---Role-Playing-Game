/// <summary>
/// Skill.cs
/// 03/12/2014
/// Nacho Lopez
/// 
/// This class contains all the extra functions that are needed for a skill
/// </summary>
public class Skill : ModifiedStat 
{
	private bool _known;		//A boolean variable to toggle if a character knows a skill

	/// <summary>
	/// Initializes a new instance of the <see cref="Skill"/> class.
	/// </summary>
	public Skill()
	{
		_known = false;
		ExpToLevel = 25;
		LevelModifier = 1.1f;
	}

	/// <summary>
	/// Gets or sets a value indicating whether this <see cref="Skill"/> is known.
	/// </summary>
	/// <value>
	/// <c>true</c> if known; otherwise, <c>false</c>.
	/// </value>
	public bool Known
	{
		get{ return _known; }
		set{ _known = value; }
	}
}
