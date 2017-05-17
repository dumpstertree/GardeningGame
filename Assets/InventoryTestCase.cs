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

			Inventory.AddToInventory( new PowerFruitTree_InventoryItem());
			Inventory.AddToInventory( new Axe_InventoryItem());
			Inventory.AddToInventory( new WoodenFence_InventoryItem());
			Inventory.AddToInventory( new WoodenFence_InventoryItem());
			Inventory.AddToInventory( new WoodenFence_InventoryItem());
			Inventory.AddToInventory( new WoodenFence_InventoryItem());
		
			d = true;
		}
	}
}
