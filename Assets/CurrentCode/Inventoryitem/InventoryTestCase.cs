using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTestCase : MonoBehaviour {

	// Use this for initialization
	bool d = false;
	private void Awake(){
		Inventory.Setup();
	}
	private void Start(){

		if (d == false){

			Inventory.AddItem( new PowerPlantSeed_InventoryItem());
			Inventory.AddItem( new DefencePlantSeed_InventoryItem());
			Inventory.AddItem( new SpeedPlantSeed_InventoryItem());

			Inventory.AddItem( new PowerBerry_InventoryItem());
			Inventory.AddItem( new DefenceBerry_InventoryItem());
			Inventory.AddItem( new SpeedBerry_InventoryItem());

			Inventory.AddItem( new RedLymeEgg());
			Inventory.AddItem( new GreenLymeEgg());

			Inventory.AddItem( new Axe_InventoryItem());
			Inventory.AddItem( new WoodenFence_InventoryItem());
			Inventory.AddItem( new Shovel_InventoryItem() );

			d = true;
		}
	}
}
