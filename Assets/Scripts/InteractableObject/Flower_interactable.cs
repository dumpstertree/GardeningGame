using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower_interactable : InteractableBehavior, IInteract {

	public void Interact(){

		Inventory.Data.AddItem( new Flower_InventoryItem() );
		Debug.Log("need to check avaialability");
		var r = true;
		if (r){
			Destroy();
		}
		else{
			Debug.LogWarning( " Inventory Full " );
		}
	}
}
