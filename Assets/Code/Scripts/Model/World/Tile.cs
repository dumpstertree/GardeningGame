using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile {

	// INSTANCE VARIABLES
	public TerrainType Terrain{
		get{
			return _terrain;
		}
	}
	public Tile LeftTile{
		get{
			return Game.Model.World.GetTile( (int)_position.x-1, (int)_position.y );
		}
	}
	public Tile RightTile{
		get{
			return Game.Model.World.GetTile( (int)_position.x+1, (int)_position.y );
		}
	}
	public Tile UpTile{
		get{
			return Game.Model.World.GetTile( (int)_position.x, (int)_position.y+1 );
		}
	}
	public Tile DownTile{
		get{
			return Game.Model.World.GetTile( (int)_position.x, (int)_position.y-1 );
		}
	}

	public bool Editable{
		get{
			return _editable;
		}
	}
	private bool _editable;

	public Vector2	  		 	_position;
	public TerrainType 			_terrain;
	private List<ITileProperty> _properties;


	// PUBLIC METHODS
	public Tile( int x, int y, TerrainType terrain, bool editable ){

		_editable = editable;

		_properties = new List<ITileProperty>();
		_position = new Vector2(x,y);
		_terrain = terrain;

		if (_terrain == TerrainType.Dirt){
			GameObject.Instantiate( Game.Resources.DirtTile, new Vector3(_position.x, 0.0f, _position.y), Quaternion.Euler(Vector3.zero) );
		}
	}

	public void AddProperty( ITileProperty prop ){
		_properties.Add( prop );
		prop.Start();
		UpdateWholeTile();
	}
	public void RemoveProperty( ITileProperty prop ){
		_properties.Remove( prop );
		UpdateWholeTile();
	}
		
	public bool CheckForObstruction(){

		foreach( ITileProperty p in  _properties){
			if (p.Obstruction){
				return true;
			}
		}

		return false;
	}
	public List<ITileProperty> FindProperties( System.Type ofType ){

		var r = new List<ITileProperty>();
		foreach ( ITileProperty p in _properties ){
			if ( p.GetType().BaseType == ofType ){
				r.Add( p );
			}
		}
		return r;
	}

	public void UpdateWholeTile(){
		foreach( ITileProperty p in _properties ){
			p.Update();
		}
	}
}

public interface ITileProperty{

	GameObject GameObject{
		get; set;
	}

	Tile Tile{
		get; set;
	}

	bool Obstruction{
		get;
	}


	void Update();
	void Start();
}
public class TileProperty{
	static public void CreateGameObject( ITileProperty property , GameObject prefab ){

		if (property.GameObject){
			GameObject.Destroy(property.GameObject);
		}

		var pos = new Vector3 ( property.Tile._position.x, 0, property.Tile._position.y );
		if (prefab){
			property.GameObject = GameObject.Instantiate( prefab );
			property.GameObject.transform.position = pos;
		}
	}
	static public void CreateEffect( ITileProperty property, GameObject effect ){

		var pos = new Vector3 ( property.Tile._position.x, 0, property.Tile._position.y );
		if (effect){
			var e = GameObject.Instantiate( effect );
			e.transform.position = pos;
		}
	}
}

public enum TerrainType{ Invalid, Grass, Sand, Dirt }