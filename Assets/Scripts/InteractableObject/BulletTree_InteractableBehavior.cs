
public class BulletTree_InteractableBehavior : InteractableBehavior, IInteract {
	
	private void Start(){
		_dropItems.Add( new DropItem( new Bullet_InventoryItem(), 1, 10, 20 ) );
	}

	protected override void InteractAfterWait(){
		
		var r = true;
		if (r){
			DropItems();
			Destroy();
		}
	}
}
