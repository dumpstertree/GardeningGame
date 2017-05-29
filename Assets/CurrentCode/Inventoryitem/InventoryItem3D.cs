using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem3D : InteractableBehavior, IInteract {


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


	// INSTANCE VARIABLES
	[SerializeField] private Renderer _renderer;
	private Rigidbody _rigidbody;

	// MONO
	private void Awake(){
		_rigidbody = GetComponent<Rigidbody>();
	}
	private void Update(){
		Billboard();
		_rigidbody.velocity = Vector3.Lerp( _rigidbody.velocity, Vector3.zero, .1f );
	}


	// PRIVATE METHODS
	private void UpdateVisual(){
		_renderer.material.mainTexture = InventoryItem.Sprite.texture;;
	}
	private void Billboard(){
		_renderer.transform.rotation =  Camera.main.transform.rotation;
	}


	// IINTERACT
	public void Interact(){
		Inventory.AddItem( _inventoryItem );
		Destroy();
	}
}
