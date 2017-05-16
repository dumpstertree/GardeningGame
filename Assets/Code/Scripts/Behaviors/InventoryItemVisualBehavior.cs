using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(SpriteRenderer)) ]

public class InventoryItemVisualBehavior : MonoBehaviour {

	// CONSTANTS
	private const float _collisionRadius = 0.5f;
	private const float _magnetDistance  = 1.0f;
	private const float _magnetLerpSpeed = 0.5f;
	private const float _nonPickupTime   = 0.5f;

	// PROPERTIES
	public InventoryItem InventoryItem{
		get{
			return _inventoryItem;
		}
		set{
			_inventoryItem = value;
			UpdateVisual();
		}
	}
	private InventoryItem _inventoryItem;

	public float StartVelocity{
		set{
			_startVelocity = value;
		}
	}
	private float _startVelocity = 0.0f;


	// INSTANCE VARIABLES
	float _time = 0.0f;


	// PRIVATE METHODS
	private void UpdateVisual(){
		GetComponent<SpriteRenderer>().sprite = _inventoryItem.Sprite;
	}
	private void Awake(){
		GetComponent<Rigidbody>().AddExplosionForce( _startVelocity, transform.position, 1.0f);
	}
	private void Update(){

		Billboard();

		if (_time > _nonPickupTime){

			var c = Game.Controller.PlayerSpawner.CharacterController.mesh;
			var d = Vector3.Distance( transform.position, c.position );

			Magnet( d, c );
			Collision( d );
		}

		else{
			
			_time+=Time.deltaTime;
		
		}
	}

	private void Billboard(){
		transform.LookAt( Camera.main.transform );
	}
	private void Magnet( float d, Transform c ){
		if (d < _magnetDistance && Inventory.ContentsCount < Inventory.Capactiy ){
			var magStrength = (1 - d/_magnetDistance) * _magnetLerpSpeed;
			transform.position = Vector3.Lerp( transform.position, c.position, magStrength );
		}
	}
	private void Collision( float d ){
		if (d < _collisionRadius){
			if (Inventory.AddToInventory( _inventoryItem )){
				Destroy( gameObject );
			}
		}
	}
}
