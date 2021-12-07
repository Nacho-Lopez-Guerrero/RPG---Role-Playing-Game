using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MyGUI : MonoBehaviour 
{
	public GUISkin mySkin;

	/***************************************************/
					/*LOOT WINDOW VARIABLES*/
	/***************************************************/
	
	public float lootWindowHeight = 80;

	public float buttonWidth = 40;
	public float buttonHeight = 40;
	public float closeButtonWidth = 20;
	public float closeButtonHeight = 20;

	private bool _displayLootWindow = false;
	private float _offset = 10;
	private const int LOOT_WINDOW_ID = 0;
	private Rect _lootWindowRect =  new Rect(0,0,0,0);
	private Vector2 _lootWindowSlider = Vector2.zero;
	public static Chest chest;
	
	private string _toolTip = "";

	/***************************************************/
				/*INVENTORY WINDOW VARIABLES*/
	/***************************************************/

	private bool _displayInventory = false;
	private const int INVENTORY_WINDOW_ID = 1;
	private Rect _inventoryWindowRect =  new Rect(10, 10, 170, 265);
	private int _inventoryRows = 6;
	private int _inventoryCols = 4;

	private float _doubleClickTimer = 0;
	private const float DOUBLE_CLICK_TIMER_THRESHOLD = 0.5f;
	private Item _selectedItem;

	/***************************************************/
				/*CHARACTER WINDOW VARIABLES*/
	/***************************************************/
	
	private bool _displayCharacterWindow = false;
	private const int CHARACTER_WINDOW_ID = 2;
	private Rect _characterWindowRect =  new Rect(10, 10, 170, 265);
	private int _characterPanel = 0;
	private string[] _characterPanelNames = new string[] {"Equipment","Attributes","Skills"};

	// Use this for initialization
	void Start () 
	{
		PC.Instance.Initialize();
		PC.DisplayWeaponMountName();
	}

	private void OnEnable()
	{
//		Messenger<int>.AddListener("PopulateChest", PopulateChest);
		Messenger.AddListener("CloseChest", ClearWindow);
		Messenger.AddListener("DisplayLoot", DisplayLoot);
		Messenger.AddListener("ToggleInventory", ToggleInventoryWindow);
		Messenger.AddListener("ToggleCharacterWindow", ToggleCharacterWindow);



	}

	private void OnDisable()
	{
//		Messenger<int>.RemoveListener("PopulateChest", PopulateChest);
		Messenger.RemoveListener("CloseChest", ClearWindow);
		Messenger.RemoveListener("DisplayLoot", DisplayLoot);
		Messenger.RemoveListener("ToggleInventory", ToggleInventoryWindow);
		Messenger.RemoveListener("ToggleCharacterWindow", ToggleCharacterWindow);

	}

	void OnGUI()
	{
		GUI.skin = mySkin;

		if(_displayCharacterWindow)
			_characterWindowRect = GUI.Window(CHARACTER_WINDOW_ID, _characterWindowRect, CharacterWindow, PlayerPrefs.GetString("Name", "No name"));

		if(_displayInventory)
			_inventoryWindowRect = GUI.Window(INVENTORY_WINDOW_ID, _inventoryWindowRect, InventoryWindow, "Inventory");

		if(_displayLootWindow)
			_lootWindowRect = GUI.Window(LOOT_WINDOW_ID, new Rect(_offset, Screen.height - (_offset + lootWindowHeight), Screen.width - (_offset * 2), lootWindowHeight), LootWindow, "Chest content");


		DisplayToolTip();
	}

	private void LootWindow(int id)
	{	
		if(GUI.Button(new Rect(_lootWindowRect.width - _offset * 2, 0, closeButtonWidth, closeButtonHeight), "x"))
		{
			ClearWindow();
		}

		if(chest == null)
			return;

		if(chest.loot.Count == 0)
		{
			ClearWindow();
			return;
		}

		_lootWindowSlider = GUI.BeginScrollView(new Rect(_offset * 0.5f, 15, _lootWindowRect.width - _offset, 70), _lootWindowSlider, new Rect(0, 0, (chest.loot.Count * buttonWidth) + _offset, buttonHeight + _offset));

		for(int cnt = 0; cnt < chest.loot.Count; cnt ++)
		{
			if(GUI.Button(new Rect(_offset * 0.5f + (buttonWidth * cnt), _offset, buttonWidth, buttonHeight), new GUIContent(chest.loot[cnt].Icon, chest.loot[cnt].ToolTip()), "Inventory Slot Common"))
			{
//				Debug.Log(chest.loot[cnt].ToolTip());
				PC.Instance.Inventory.Add(chest.loot[cnt]);
				chest.loot.RemoveAt(cnt);
			}
		}

		GUI.EndScrollView();

		SetToolTip();

	}

//	private void PopulateChest(int x)
//	{
//		for(int cnt = 0; cnt < x; cnt++)
//		{
//			_lootItems.Add(new Item());
//			_displayLootWindow = true;
//		}
//	}

	private void ClearWindow()
	{
		chest.OnMouseUp();

		_displayLootWindow = false;
		_displayInventory = false;
		_displayCharacterWindow = false;
		chest = null;

		//_lootItems.Clear();
	}

	private void DisplayLoot()
	{
		_displayLootWindow = true;
	}

	private void InventoryWindow(int id)
	{
		int cnt = 0;


		for(int y = 0; y < _inventoryRows; y++)
		{
			for(int x = 0; x < _inventoryCols; x++)
			{
				if(cnt < PC.Instance.Inventory.Count)
				{
					if(GUI.Button(new Rect(5 + (x * buttonWidth), 20 + (y * buttonHeight), buttonWidth, buttonHeight), 
					              new GUIContent(PC.Instance.Inventory[cnt].Icon, 
					               PC.Instance.Inventory[cnt].ToolTip()), 
					              "Inventory Slot Common"))
					{
						if(_doubleClickTimer != 0 && _selectedItem != null)
						{
							if(Time.time - _doubleClickTimer < DOUBLE_CLICK_TIMER_THRESHOLD)
							{
								Debug.Log("Double Click! " + PC.Instance.Inventory[cnt].Name);

								if(typeof(Weapon) == PC.Instance.Inventory[cnt].GetType())
								{
									if(PC.Instance.EquipedWeapon == null)
									{
										PC.Instance.EquipedWeapon = PC.Instance.Inventory[cnt];
										PC.Instance.Inventory.RemoveAt(cnt);
									}
									else
									{
										Item temp = PC.Instance.EquipedWeapon;
										PC.Instance.EquipedWeapon = PC.Instance.Inventory[cnt];
										PC.Instance.Inventory[cnt] = temp;
									}
								}
								else if(typeof(Armor) == PC.Instance.Inventory[cnt].GetType())
								{
									Armor armor = (Armor)PC.Instance.Inventory[cnt];

									switch(armor.Slot)
									{
										case EquipmentSlot.Head:
											if(PC.Instance.EquipedHelmet == null)
											{
											PC.Instance.EquipedHelmet = PC.Instance.Inventory[cnt];
												PC.Instance.Inventory.RemoveAt(cnt);
											}
											else
											{
												Item temp = PC.Instance.EquipedHelmet;
												PC.Instance.EquipedHelmet = PC.Instance.Inventory[cnt];
												PC.Instance.Inventory[cnt] = temp;
											}

											break;

										case EquipmentSlot.OffHand:
											if(PC.Instance.EquipedShield == null)
											{
												PC.Instance.EquipedShield = PC.Instance.Inventory[cnt];
												PC.Instance.Inventory.RemoveAt(cnt);
											}
											else
											{
												Item temp = PC.Instance.EquipedShield;
												PC.Instance.EquipedShield = PC.Instance.Inventory[cnt];
												PC.Instance.Inventory[cnt] = temp;
											}

											break;
									}



								}

								_doubleClickTimer = 0;
								_selectedItem = null;
							}
							else
							{
								Debug.Log("Reset 2 click timer");
								_doubleClickTimer = Time.time;
							}
						}
						else
						{
							_doubleClickTimer -= Time.time;
							_selectedItem = PC.Instance.Inventory[cnt];
						}
					}

				}
				else
					GUI.Label(new Rect(5 + (x * buttonWidth), 20 + (y * buttonHeight), buttonWidth, buttonHeight), (x + y * _inventoryCols).ToString(), "Inventory Window");

				cnt++;				
			}
		}

		SetToolTip();
		GUI.DragWindow();
	}

	public void ToggleInventoryWindow()
	{
		_displayInventory = !_displayInventory;
	}

	
	public void ToggleCharacterWindow()
	{
		_displayCharacterWindow = !_displayCharacterWindow;
	}

	private void SetToolTip()
	{
		if(Event.current.type == EventType.Repaint && GUI.tooltip != _toolTip)
		{
			 if(_toolTip != "")
				_toolTip = "";

			if(GUI.tooltip != "")
				_toolTip = GUI.tooltip;
		}
	}

	public void CharacterWindow(int id)
	{
		_characterPanel = GUI.Toolbar(new Rect(5, 25, _characterWindowRect.width - 10, 50), _characterPanel, _characterPanelNames);

		switch(_characterPanel)
		{
		case 0:
			DisplayEquipment();
			break;
		case 1:
			DisplayAttributes();
			break;
		case 2:
			DisplaySkills();
			break;
		}

		GUI.DragWindow();
	}

	private void DisplayEquipment()
	{
		GUI.skin = mySkin;

		if(PC.Instance.EquipedWeapon == null)
		{
			GUI.Button(new Rect(5, 100, 40, 40), "", "Inventory Window");
		}
		else
		{
			if(GUI.Button(new Rect(5, 100, 40, 40), new GUIContent(PC.Instance.EquipedWeapon.Icon, PC.Instance.EquipedWeapon.ToolTip()), "Inventory Slot Common"))
			{
				PC.Instance.Inventory.Add(PC.Instance.EquipedWeapon);
				PC.Instance.EquipedWeapon = null;
			}

		}

		SetToolTip();
	}
	
	private void DisplayAttributes()
	{
		int lineHeight = 17;
		int valueDisplayWidth = 50;

		Debug.Log("Display Attributes");

		GUI.BeginGroup(new Rect(5, 80, _characterWindowRect.width - (_offset * 2), _characterWindowRect.height - 50));

		//Display the attributes
		for(int cnt = 0; cnt < PC.Instance.primaryAttributes.Length; cnt++)
		{
			GUI.Label(new Rect(2, cnt * lineHeight, _characterWindowRect.width - (_offset * 2) - valueDisplayWidth, 25), ((AttributeName)cnt).ToString());
			GUI.Label(new Rect(_characterWindowRect.width - (_offset * 2) - valueDisplayWidth, cnt * lineHeight, valueDisplayWidth, 25), PC.Instance.GetPrimaryAttribute(cnt).BaseValue.ToString());

		}

		//Display the Vitals
		for(int cnt = 0; cnt < PC.Instance.vitals.Length; cnt++)
		{
			GUI.Label(new Rect(2, (cnt + PC.Instance.primaryAttributes.Length) * lineHeight, _characterWindowRect.width - (_offset * 2) - valueDisplayWidth, 25), ((VitalName)cnt).ToString());
			GUI.Label(new Rect(_characterWindowRect.width - (_offset * 2) - valueDisplayWidth, (cnt + PC.Instance.primaryAttributes.Length) * lineHeight, valueDisplayWidth, 25), PC.Instance.GetVital(cnt).CurValue.ToString() + "/" +  PC.Instance.GetVital(cnt).AdjustedBaseValue.ToString());	
		}

		GUI.EndGroup();
	}
	
	private void DisplaySkills()
	{
		int lineHeight = 17;
		int valueDisplayWidth = 50;

		GUI.BeginGroup(new Rect(5, 80, _characterWindowRect.width - (_offset * 2), _characterWindowRect.height - 50));
		
		//Display the attributes
		for(int cnt = 0; cnt < PC.Instance.skills.Length; cnt++)
		{
			GUI.Label(new Rect(2, cnt * lineHeight, _characterWindowRect.width - (_offset * 2) - valueDisplayWidth, 25), ((SkillName)cnt).ToString());
			GUI.Label(new Rect(_characterWindowRect.width - (_offset * 2) - valueDisplayWidth, cnt * lineHeight, valueDisplayWidth, 25), PC.Instance.GetSkill(cnt).AdjustedBaseValue.ToString());
		}

		GUI.EndGroup();
		
		//Display the Vitals
		/*
		for(int cnt = 0; cnt < PC.Instance.vitals.Length; cnt++)
		{
			GUI.Label(new Rect(2, (cnt + PC.Instance.primaryAttributes.Length) * lineHeight, _characterWindowRect.width - (_offset * 2) - valueDisplayWidth, 25), ((VitalName)cnt).ToString());
			GUI.Label(new Rect(_characterWindowRect.width - (_offset * 2) - valueDisplayWidth, (cnt + PC.Instance.primaryAttributes.Length) * lineHeight, valueDisplayWidth, 25), PC.Instance.GetVital(cnt).CurValue.ToString() + "/" +  PC.Instance.GetVital(cnt).AdjustedBaseValue.ToString());	
		}
		*/
	}
	
	private void DisplayToolTip()
	{
		if(_toolTip != "")
		{
			GUI.Box(new Rect(Screen.width / 2 - 100, 10, 200, 100), _toolTip);
		}
	}
}
