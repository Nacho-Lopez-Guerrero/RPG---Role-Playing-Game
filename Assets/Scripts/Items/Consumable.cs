using UnityEngine;

public class Consumable : BuffItem 
{
	private Vital[] _vitals;			//a list of vital to heal
	private int[] _amountToHeal;		//the amunt to heal each vital

	private float _buffTime;			//how long buff lasts if there is one

	public Consumable()
	{
		Reset();
	}

	public Consumable(Vital[] v, int[] a, float b)
	{
		_vitals = v;
		_amountToHeal = a;
		_buffTime = b;
	}


	public void Reset()
	{
		_buffTime = 0;

		for(int i = 0; i < _vitals.Length; i++)
		{
			_vitals[i] = new Vital();
			_amountToHeal[i] = 0;
		}
	}

	public int VitalCount()
	{
		return	_vitals.Length;
	}

	public Vital VitalAtIndex(int index)
	{
		if(index < _vitals.Length && index > -1)
			return _vitals[index];
		else
			return new Vital();
	}

	public int HealAtIndex(int index)
	{
		if(index < _amountToHeal.Length && index > -1)
			return _amountToHeal[index];
		else
			return 0;
	}

	public void SetVitalAt(int index, Vital vital)
	{
		if(index < _vitals.Length && index > -1)
			_vitals[index] = vital;
	}

	public void setHealAt(int index, int heal)
	{
		if(index < _amountToHeal.Length && index > -1)
			_amountToHeal[index] = heal;
	}

	public float BuffTime
	{
		get { return _buffTime; }
		set { _buffTime = value; }
	}

	public void SetVitalAndHealAt(int index, Vital vital, int heal)
	{
			SetVitalAt(index, vital);
			setHealAt(index, heal);
	}
}
