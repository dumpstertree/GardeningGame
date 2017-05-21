using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Plant_InteractableBehavior : InteractableBehavior, IScheduledEvent {

	[SerializeField] private MeshFilter _meshFilter;

	protected int			_level  = 0;
	protected TimeStamp 	_plantedTime;
	protected GrowthPoint[] _growthSchedule;


	// MONO
	private void Start(){
		_growthSchedule = CreateGrowthSchedule();
		ChangeGameObject( _growthSchedule[_level].Mesh );
		ScheduleAlert( _growthSchedule[_level] );
	}


	// OVERRIDABLE METHODS
	protected abstract GrowthPoint[] CreateGrowthSchedule();


	// PRIVATE
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
		ChangeGameObject( gp.Mesh );

		_dropItems = gp.DropItems;

		GameFeel.FeedEffect( transform );
	}
	private void UpdatePlantedTime(){
		_plantedTime = Game.Controller.WorldTime.TimeStamp;
	}
	private void ScheduleAlert( GrowthPoint growthPoint ){
		Game.Controller.WorldTime.ScheduleAlert( this, growthPoint.Hours, growthPoint.Minutes );
	}
	private void ChangeGameObject( Mesh mesh ){
		_meshFilter.mesh = mesh;
	}
		

	// OVERRIDE
	protected override bool Plant (InventoryItem item){
		var r = base.Plant (item);
		GameFeel.FeedEffect(transform);
		return r;
	}
	protected override bool Hit (InventoryItem item) {
		var r = base.Hit (item);
		GameFeel.HitWood( transform );
		return r;
	}
	protected override void Destroy () {
		base.Destroy();
		GameFeel.DestroyWood( transform );
	}


	// SCHEDULE EVENT
	public void RunScheduledEvent(){

		if ( CheckForLevelUp() ){
			LevelUp();
		}
	}

}
// DATA TYPES
public struct GrowthPoint{

	public int Hours;
	public int Minutes;
	public Mesh Mesh;
	public List<DropItem> DropItems;

	public GrowthPoint( Mesh mesh, int hours, int minutes, List<DropItem> dropItems ){
		Mesh = mesh;
		Hours = hours;
		Minutes = minutes;
		DropItems = dropItems;
	}
}