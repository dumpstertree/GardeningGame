using System.Collections.Generic;

public class Tree_InteractableBehavior : InteractableBehavior, IHit {

	private void Start(){
		_dropItems = new List<DropItem>();
		_dropItems.Add( new DropItem( new Wood_InventoryItem(), 1,1,4 ) );
	}

	protected override void HitAfterWait( HitObjectInfo info ){
		base.HitAfterWait( info );
		GameFeel.HitWood(transform);
	}
}