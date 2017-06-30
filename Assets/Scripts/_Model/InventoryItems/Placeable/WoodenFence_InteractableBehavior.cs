using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenFence_InteractableBehavior : InteractableBehavior, IHit{

	private void Start(){
		_maxHealth = 3;
		_dropItems.Add( new DropItem( new WoodenFence_InventoryItem(), 1,1,1 ) );
	}
		
	public bool Hit (HitObjectInfo info) {
		base.Hit ( info );
		return true;
	}
	protected override void HitAfterWait( HitObjectInfo info ){
		base.HitAfterWait( info );
		GameFeel.HitWood(transform);
	}
}
