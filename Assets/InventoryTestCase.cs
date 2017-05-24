using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTestCase : MonoBehaviour {

	// Use this for initialization
	bool d = false;
	private void Update(){

		if (Time.time < .1f){
			return;
		}

		if (d == false){

			Inventory.AddToInventory( new PowerPlantSeed_InventoryItem());
			Inventory.AddToInventory( new DefencePlantSeed_InventoryItem());
			Inventory.AddToInventory( new SpeedPlantSeed_InventoryItem());

			Inventory.AddToInventory( new PowerBerry_InventoryItem());
			Inventory.AddToInventory( new DefenceBerry_InventoryItem());
			Inventory.AddToInventory( new SpeedBerry_InventoryItem());

			Inventory.AddToInventory( new RedLymeEgg());
			Inventory.AddToInventory( new GreenLymeEgg());

			Inventory.AddToInventory( new Axe_InventoryItem());
			Inventory.AddToInventory( new WoodenFence_InventoryItem());
			Inventory.AddToInventory( new Shovel_InventoryItem() );

			d = true;
		}
	}
}
