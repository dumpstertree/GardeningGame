using UnityEngine;
using System.Collections.Generic;

public class Inventory: MonoBehaviour {

	static public InventoryData Data{
		get{
			return _data;
		}
	}
	static private InventoryData _data;
		
	private void Awake(){
		_data = new InventoryData();
		_data.AddToDataSource( Constants.InventoryIndexStart, Constants.InventorySlots );
		_data.AddToDataSource( Constants.HotkeyIndexStart, Constants.HotkeySlots );
		_data.AddToDataSource( Constants.BulletIndexStart, Constants.BulletSlots, new List<InventoryItemTag>(){InventoryItemTag.Bullet } );
		Populate();
	}

	private void Populate(){
		Inventory.Data.AddItem( new PowerPlantSeed_InventoryItem());
		Inventory.Data.AddItem( new DefencePlantSeed_InventoryItem());
		Inventory.Data.AddItem( new SpeedPlantSeed_InventoryItem());
		Inventory.Data.AddItem( new PowerBerry_InventoryItem());
		Inventory.Data.AddItem( new DefenceBerry_InventoryItem());
		Inventory.Data.AddItem( new SpeedBerry_InventoryItem()); 
		Inventory.Data.AddItem( new Axe_InventoryItem());
		Inventory.Data.AddItem( new WoodenFence_InventoryItem());
		Inventory.Data.AddItem( new Shovel_InventoryItem() );
		Inventory.Data.AddItem( new MachineGun());
		Inventory.Data.AddItem( new ShotGun());
		Inventory.Data.AddItem( new Pistol());
	}
}