using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof( InteractorReciever ))]
public class WoodenFence_InteractableBehavior : InteractableBehavior {

	private void Start(){
		_maxHealth = 3;
		_dropItems.Add( new DropItem( new WoodenFence_InventoryItem(), 1,1,1 ) );
	}

	protected override bool Place (InventoryItem item){
		var r = base.Place (item);
		GameFeel.FeedEffect(transform);
		return r;
	}
	protected override bool Hit (InventoryItem item) {
		var r = base.Hit (item);
		GameFeel.HitWood( transform );
		return r;
	}
	protected override void Destroy () {
		base.Destroy();
		GameFeel.DestroyWood( transform );
	}
}
