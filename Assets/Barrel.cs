using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : InteractableBehavior, IShootable, IHit {
	
	[SerializeField] private Item _item;

	
	private void Start(){
		
		_maxHealth = 1;
		base.Start();

		switch( _item ){
		
		case Item.None:
			break;
		
		case Item.Bullet:
			_dropItems.Add( new DropItem( new Bullet_InventoryItem(), 1, 10, 30 ) );
			break;
		}
	}

	private enum Item{
		None,
		Bullet
	}

	protected override void Destroy(){
		base.Destroy();
		GameFeel.DestroyWood(transform);
	}

}
