using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree_InteractableBehavior : InteractableBehavior, IHit {

	private void Start(){
		_dropItems = new List<DropItem>();
		_dropItems.Add( new DropItem( new Wood_InventoryItem(), 1,1,4 ) );
	}

	public bool Hit (HitObjectInfo info ){
		base.Hit( info );
		return true;
	}
	protected override void HitAfterWait( HitObjectInfo info ){
		base.HitAfterWait( info );
		GameFeel.HitWood(transform);
	}
}
