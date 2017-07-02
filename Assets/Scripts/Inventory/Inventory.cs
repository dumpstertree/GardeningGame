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
		
		//Inventory.Data.AddItem( new Seeds.PowerPlantSeed());
		//Inventory.Data.AddItem( new Seeds.DefencePlantSeed());
		//Inventory.Data.AddItem( new Seeds.SpeedPlantSeed());
		
		//Inventory.Data.AddItem( new Food.PowerBerry());
		//Inventory.Data.AddItem( new Food.DefenceBerry());
		//Inventory.Data.AddItem( new Food.SpeedBerry()); 
		
		//Inventory.Data.AddItem( new Guns.MachineGun());
		//Inventory.Data.AddItem( new Guns.ShotGun());
		//Inventory.Data.AddItem( new Guns.Pistol());

		Inventory.Data.AddItem( new Axe_InventoryItem());
		Inventory.Data.AddItem( new WoodenFence_InventoryItem());
		Inventory.Data.AddItem( new Shovel_InventoryItem() );

		for (int i=0; i<999; i++){
			Inventory.Data.AddItem( new Bullet_InventoryItem());
		}
	}
}