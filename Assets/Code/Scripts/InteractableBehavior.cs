using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBehavior : MonoBehaviour {

	protected List<DropItem> _dropItems = new List<DropItem>();
	protected int 		     _maxHealth = 3; // Set to -1 for Invincible

	private int 			 _health = 3;

	public bool Interact( InventoryItem item  ){

		switch( item.Action	){

		case InventoryItemActionType.Hit:
			return Hit( item );

		case InventoryItemActionType.Place:
			return Place( item );

		default:
			return false;
		}
	}
		
	protected virtual bool Place( InventoryItem item ){
		Instantiate( item._placeableObject.Prefab, transform.position, Quaternion.Euler(Vector3.zero) );
		return true;
	}
	protected virtual bool Hit( InventoryItem item ){
		if (_maxHealth != -1 ){
			_health -= item.HitStrength;
			if (_health <= 0){
				Destroy();
			}
			return true;
		}
		else{
			return false;
		}
	}
	protected virtual void Destroy(){ 

		GameObject.Destroy(gameObject);

		var drops = DropRoller.Roll(_dropItems);
		foreach ( InventoryItem item in drops ){
			var go = GameObject.Instantiate( Game.Resources.InventoryItemVisual, transform.position, Quaternion.Euler(Vector3.zero)  );
			var b = go.GetComponent<InventoryItemVisualBehavior>();
			b.InventoryItem = item;
			b.StartVelocity = 3.0f;
		}


	}
}
