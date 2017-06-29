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

	public static char InventoryIndexStart{
		get{
			return _inventoryIndexStart;
		}
	}
	private const char _inventoryIndexStart = 'I';


	public static int HotkeySlots{
		get{
			return _hotkeySlots;
		}
	}
	private static int _hotkeySlots = 5;

	public static char HotkeyIndexStart{
		get{
			return _hotkeyIndexStart;
		}
	}
	private const char _hotkeyIndexStart = 'H';


	public static int EquipSlots{
		get{
			return _equipSlots;
		}
	}
	private static int _equipSlots = 5; 

	public static char EquipIndexStart{
		get{
			return _equipIndexStart;
		}
	}
	private const char _equipIndexStart = 'E';


	public static int BulletSlots{
		get{
			return _bulletSlots;
		}
	}
	private static int _bulletSlots = 5; 

	public static char BulletIndexStart{
		get{
			return _bulletIndexStart;
		}
	}
	private const char _bulletIndexStart = 'B';
}
