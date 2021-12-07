using UnityEngine;

[AddComponentMenu("Hack And Slash/Item/Weapon")]
public class Weapon : BuffItem 
{
	private int _maxDamage;
	private float _damageVar;
	private float _maxRange;
	public DamageType _dmgType;

	public Weapon()
	{
		_maxRange = 0;
		_maxDamage = 0;
		_maxRange = 0;
		_dmgType = DamageType.Bludgeon;
	}

	public Weapon(int mDmg, float dmgV, float mRange, DamageType dt)
	{
		_maxRange = mRange;
		_maxDamage = mDmg;
		_damageVar = dmgV;
		_dmgType = dt;
	}

	public int MaxDamage
	{
		get { return _maxDamage; }
		set { _maxDamage = value; }
	}

	public float DamageVariance
	{
		get { return _damageVar; }
		set { _damageVar = value; }
	}

	public float MaxRange
	{
		get { return _maxRange; }
		set { _maxRange = value; }
	}

	public DamageType TypeOfDamage
	{
		get { return _dmgType; }
		set { _dmgType = value; }
	}

	public override string ToolTip()
	{
		return Name + "\n" + 
			"Value: " + Value + "\n" +
			"Durability: " + CurDurability + "/" + MaxDurability +"\n" +
			"Damage: " + MaxDamage * DamageVariance + " - " + MaxDamage;
	}

}

public enum DamageType
{
	Bludgeon,
	Pierce,
	Slash,
	Fire,
	Ice,
	Lightning,
	Acid
}
