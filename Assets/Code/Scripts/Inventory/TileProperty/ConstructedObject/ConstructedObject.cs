using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ConstructedObject : ITileProperty {

	// PROPERTIES
	public List<DropItem> DropItems{
		get{
			return _dropItems;
		}
	}
	protected List<DropItem> _dropItems;

	public GameObject GameObject{
		get{
			return _gameObject;
		}
		set{
			_gameObject = value;
		}
	}
	private GameObject _gameObject;

	public Tile Tile{
		get{
			return _tile;
		}
		set{
			_tile = value;
		}
	}
	private Tile _tile;

	public bool Obstruction{
		get{
			return _obstruction;
		}
	}
	protected bool _obstruction;

	// INSTANCE VARIABLES
	protected int _health;
	protected int _maxHealth = 3;


	// INIT
	public ConstructedObject( Tile tile ){

		_dropItems = new List<DropItem>();

		_tile = tile;
		_health = _maxHealth;
		_obstruction = true;
	}


	// PUBLIC
	public virtual void Update(){
	}
	public virtual void Start(){
	}

	public virtual void Hit( int damage ){
		if (_health <= 0){
			Destroy();
		}
	}
	public virtual void Destroy(){
		
		var drops = DropRoller.Roll(_dropItems);
		foreach ( InventoryItem item in drops ){
			var go = GameObject.Instantiate( Game.Resources.InventoryItemVisual, _gameObject.transform.position, Quaternion.Euler(Vector3.zero)  );
			var b = go.GetComponent<InventoryItemVisualBehavior>();
			b.InventoryItem = item;
			b.StartVelocity = 3.0f;
		}
	}
}
