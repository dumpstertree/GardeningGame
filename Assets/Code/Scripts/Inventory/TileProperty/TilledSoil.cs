using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilledSoil : ITileProperty, IScheduledEvent{

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

	public bool Obstruction{
		get{
			return _obstruction;
		}
	}
	protected bool _obstruction;

	private Tile _tile;


	public TilledSoil( Tile tile ){
		_tile = tile;
		_obstruction = false;

		CreateGameObject( Game.Resources.TilledSoil );
		Game.Controller.WorldTime.ScheduleAlert( this, 5, 0);
	}

	public void RunScheduledEvent(){
	}
	public void Update(){
	}
	public void Start(){
	}

	private void CreateGameObject( GameObject prefab ){

		if (_gameObject){
			GameObject.Destroy(_gameObject);
		}

		var pos = new Vector3 ( _tile._position.x, 0, _tile._position.y );
		if (prefab){
			_gameObject = GameObject.Instantiate( prefab );
			_gameObject.transform.position = pos;
		}
	}
}
