using UnityEngine;
using System.Collections;

public class BuffItem : Item 
{

//	private int[] buffMods;
//	private BaseStat[] stat;

	private Hashtable buffs;
/// <summary>
///	Might, 50
/// Melee Offence, 100 
/// </summary>

	public BuffItem()
	{
		buffs = new Hashtable();
	}

	public BuffItem(Hashtable ht)
	{
		buffs = ht;
	}

	public void AddBuff(BaseStat stat, int mod)
	{
		try
		{
			buffs.Add(stat.Name, mod);
		}
		catch(UnityException e)
		{
			Debug.LogWarning(e);
		}
	}

	public void RemoveBuff(BaseStat stat)
	{
		buffs.Remove(stat.Name);
	}

	public int BuffCount()
	{
		return buffs.Count;
	}

	public Hashtable GetBuff()
	{
		return buffs;
	}

}
