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
	}


	// PRIVATE METHODS
	private void UpdateVisual(){

		_renderer.material.mainTexture = InventoryItem.Sprite.texture;
	}
	private void Billboard(){

		_renderer.transform.rotation =  Camera.main.transform.rotation;
	}


	// IINTERACT
	public void Interact(){
		Debug.Log( "Need to check if room" );
		Inventory.Data.AddItem( _inventoryItem );
		Destroy();
	}


	// STATIC
	public static void Create( InventoryItem item, Vector3 pos ){
		var spawnOffset = 0.8f;
		var go = Instantiate( Game.Resources.InventoryItemVisual);
		var b =  go.GetComponent<InventoryItem3D>();
		go.transform.position = pos + Vector3.up*spawnOffset; 
		b.InventoryItem = item;
	}
}
