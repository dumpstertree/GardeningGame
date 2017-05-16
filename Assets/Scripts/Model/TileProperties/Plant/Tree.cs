using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tree : ITileProperty, IScheduledEvent {

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

	protected int 			_health;
	protected int 		    _maxHealth = 3;

	protected int			_level  = 0;
	protected TimeStamp 	_plantedTime;
	protected GrowthPoint[] _growthSchedule;


	// PUBLIC METHODS
	public Tree( Tile tile ){
		
		_health = _maxHealth;

		_obstruction = true;
		_tile = tile;
		_growthSchedule = CreateGrowthSchedule();

		var gp = _growthSchedule[_level];

		UpdatePlantedTime();
		ScheduleAlert( gp );
		CreateGameObject( gp.Prefab );
		CreateEffect( gp.Effect );

		Game.Controller.Audio.OneShot(AudioType.LevelUp);
	}
	public void RunScheduledEvent(){
		Update();
	}
	public void Update(){

		if ( CheckForLevelUp() ){
			LevelUp();
		}
	}
	public void Start(){
		Update();
	}
	public void Hit( int health ){

		_health -= health;

		Game.Controller.Audio.OneShot( AudioType.TreeHit );
		CreateEffect( Game.Resources.TreeHit );

		if (_health <= 0){
			Destroy();
		}
	}
	public void Destroy(){

		var drops = DropRoller.Roll( _growthSchedule[_level].DropItems );
		foreach ( InventoryItem i in drops ){

			var go = GameObject.Instantiate( Game.Resources.InventoryItemVisual  );
			go.transform.position = new Vector3( _tile._position.x, 0, _tile._position.y );

			var b = go.GetComponent<InventoryItemVisualBehavior>();
			b.InventoryItem = i;
			b.StartVelocity = 3.0f;
		}

		CreateGameObject( null ); // Should Later be replaced with animation
		CreateEffect( Game.Resources.TreeDestroy );
		Game.Controller.Audio.OneShot(AudioType.TreeDestroy);

		_tile.RemoveProperty( this );
		Game.Controller.WorldTime.RemoveAlerts( this );
	}


	// PRIVATE METHODS
	private bool CheckForLevelUp() {

		var gp = _growthSchedule[_level];
		var pastH = Game.Controller.WorldTime.Hour-_plantedTime.Hour;
		var pastM = Game.Controller.WorldTime.Minute-_plantedTime.Minute;

		if (pastH >= gp.Hours && pastM >= gp.Minutes && _level<_growthSchedule.Length-1){
			return true;
		}

		return false;

	}
	private void LevelUp(){

		_level++;

		var gp = _growthSchedule[_level];

		UpdatePlantedTime();
		ScheduleAlert( gp );
		CreateGameObject( gp.Prefab );
		CreateEffect( gp.Effect );

		Game.Controller.Audio.OneShot(AudioType.LevelUp);
	}

	private void UpdatePlantedTime(){
		_plantedTime = Game.Controller.WorldTime.TimeStamp;
	}

	private void ScheduleAlert( GrowthPoint growthPoint ){
		Game.Controller.WorldTime.ScheduleAlert( this, growthPoint.Hours, growthPoint.Minutes );
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
	private void CreateEffect( GameObject effect ){

		var pos = new Vector3 ( _tile._position.x, 0, _tile._position.y );
		if (effect){
			var e = GameObject.Instantiate( effect );
			e.transform.position = pos;
		}
	}


	// OVERRIDABLE METHODS
	public abstract GrowthPoint[] CreateGrowthSchedule();


	// DATA TYPES
	public struct GrowthPoint{

		public int Hours;
		public int Minutes;
		public GameObject Prefab;
		public GameObject Effect;
		public List<DropItem> DropItems;

		public GrowthPoint( GameObject prefab, GameObject effect, int hours, int minutes, List<DropItem> dropItems ){
			Prefab = prefab;
			Effect = effect;
			Hours = hours;
			Minutes = minutes;
			DropItems = dropItems;
		}
	}
}