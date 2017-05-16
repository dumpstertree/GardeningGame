using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_InventoryItem : InventoryItem {


	// INIT
	public Hand_InventoryItem() : base(){
		_recievers.Add( InteractorType.All );
	}


	// PUBLIC METHODS
	public override void Toss(){
	}
}
