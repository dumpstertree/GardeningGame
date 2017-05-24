using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree_InteractableBehavior : InteractableBehavior {

	private void Start(){
		_dropItems = new List<DropItem>();
		_dropItems.Add( new DropItem( new Wood_InventoryItem(), 1,1,4 ) );
	}

	protected override bool Hit (InventoryItem item)
	{
		var r = base.Hit (item);
		GameFeel.HitWood(transform);
		return r;
	}
	protected override void Destroy ()
	{
		base.Destroy ();
		GameFeel.DestroyWood(transform);
	}
}
