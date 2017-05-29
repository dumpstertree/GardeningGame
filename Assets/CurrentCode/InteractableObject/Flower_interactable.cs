using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flower_interactable : InteractableBehavior, IInteract {

	public void Interact(){

		var r = Inventory.AddItem( new Flower_InventoryItem() );
		if (r){
			Destroy();
		}
		else{
			Debug.LogWarning( " Inventory Full " );
		}
	}
}
