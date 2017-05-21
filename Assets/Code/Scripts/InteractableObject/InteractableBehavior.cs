using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Animator)) ]
public class InteractableBehavior : MonoBehaviour {

	protected List<DropItem> _dropItems = new List<DropItem>();
	protected int 		     _maxHealth = 3; // Set to -1 for Invincible

	private int 			 _health = 3;
	private Animator 		_animator;

	private void Awake(){
		_animator = GetComponent<Animator>();
	}
	public bool Interact( InventoryItem item  ){

		switch( item.Action	){

		case InventoryItemActionType.Feed:
			return Feed( item );

		case InventoryItemActionType.Hit:
			return Hit( item );

		case InventoryItemActionType.Plant:
			return Plant( item );

		default:
			return false;
		}
	}

	protected virtual bool Plant( InventoryItem item ){
		if ( item._placeableObject.Prefab ){
			Instantiate( item._placeableObject.Prefab, transform.position, Quaternion.identity );
			return true;
		}
		else{
			Debug.LogWarning( "Trying to place an object but none assigned to InventoryItem;" );
			return false;
		}
	}
	protected virtual bool Feed( InventoryItem item ){
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
