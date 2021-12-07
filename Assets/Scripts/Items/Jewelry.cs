using UnityEngine;
using System.Collections;

public class Jewelry : BuffItem 
{
	private JewelrySlot _slot;					//store the slot the jewelry is in

	public Jewelry()
	{
		_slot = JewelrySlot.PocketItem;
	}

	public Jewelry(JewelrySlot slot)
	{
		_slot = slot;
	}

	public JewelrySlot Slot
	{
		get { return _slot; }
		set { _slot = value; }
	}
}
