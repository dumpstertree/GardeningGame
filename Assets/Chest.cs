using UnityEngine;

public class Chest : InteractableBehavior, IInteract {
	
	[SerializeField] private Item _item;

	// MONO
	private void Start(){
		
		switch( _item ){
		
		case Item.None:
			break;
		
		case Item.Key:
			_dropItems.Add( new DropItem( new Wood_InventoryItem(), 1, 1, 1 ) );
			break;

		case Item.Pistol:
			_dropItems.Add( new DropItem( new Guns.Pistol(), 1, 1, 1 ) );
			break;

		case Item.Shotgun:
			_dropItems.Add( new DropItem( new Guns.ShotGun(), 1, 1, 1 ) );
			break;

		case Item.Machinegun:
			_dropItems.Add( new DropItem( new Guns.MachineGun(), 1, 1, 1 ) );
			break;
		}
	}

	// SUPER
	override protected void InteractAfterWait(){
		
		DropItems();
		Destroy();

		GameFeel.EggHatched(transform);
	}

	// DATA TYPE
	private enum Item{
		None,
		Key,
		Pistol,
		Shotgun,
		Machinegun
	}
}