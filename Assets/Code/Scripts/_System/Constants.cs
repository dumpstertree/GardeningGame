using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Constants : MonoBehaviour {

	public static int InventorySlots{
		get{
			return _inventorySlots;
		}
	}
	private static int _inventorySlots = 15;

	public static string InventoryIndexStart{
		get{
			return _inventoryIndexStart;
		}
	}
	private const string _inventoryIndexStart = "I";


	public static int HotkeySlots{
		get{
			return _hotkeySlots;
		}
	}
	private static int _hotkeySlots = 4;

	public static string HotkeyIndexStart{
		get{
			return _hotkeyIndexStart;
		}
	}
	private const string _hotkeyIndexStart = "H";


	public static int EquipSlots{
		get{
			return _equipSlots;
		}
	}
	private static int _equipSlots = 5; 

	public static string EquipIndexStart{
		get{
			return _equipIndexStart;
		}
	}
	private const string _equipIndexStart = "E";


}
