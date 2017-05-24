using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stomach : MonoBehaviour, IScheduledEvent {


	// INSTANCE VARIABLES
	public int CurHungerMeter{
		set{
			if (value != _curHungerMeter){
				_curHungerMeter = value;
				AlertListeners();
			}
		}
		get{
			return _curHungerMeter;
		}
	}
	public int MaxHungerMeter{
		get{
			return _maxHungerMeter;
		}
	}


	private int _maxHungerMeter = 10;
	private int _curHungerMeter = 5; // 0  = Full -> 10 = Empty
	private int _hungerIncreaseHours  = 1;
	private int _hungerInceaseMinutes = 0;

	private List<IStomach> _listeners;


	// MONO
	private void Awake(){
		_listeners = new List<IStomach>();
	}
	private void Start(){
		Game.Controller.WorldTime.ScheduleAlert(this, _hungerIncreaseHours, _hungerInceaseMinutes);
	}


	// PUBLIC METHODS
	public void AddListener( IStomach listener ){
		if ( !_listeners.Contains( listener) ){
			_listeners.Add(listener);
		}
	}
	public void StopListening( IStomach listener ){
		if ( _listeners.Contains( listener) ){
			_listeners.Remove(listener);
		}
	}
	public bool Feed( int amount ){

		if ( _curHungerMeter - amount < 0 ){
			return false;
		}

		DecreaseHunger( amount );
		GameFeel.FeedEffect( transform.parent );
		// Need To reschedule alert

		GetComponentInParent<Creature>().Animator.SetTrigger("Emote");

		foreach( IStomach l in _listeners ){ l.BeenFed(); }

		return true;

	}
	public bool GrowHungry( int amount ){

		if ( _curHungerMeter + amount > _maxHungerMeter ){
			return false;
		}

		IncreaseHunger( amount );
		GameFeel.GrowHungryEffect( transform.parent );
		Game.Controller.WorldTime.ScheduleAlert(this, _hungerIncreaseHours, _hungerInceaseMinutes);

		// Hunger Animation
		return true;
	}


	// PRIVATE METHODS
	private void IncreaseHunger( int amount ){
		CurHungerMeter += amount;
		Mathf.Clamp(_curHungerMeter, 0, _maxHungerMeter);
	}
	private void DecreaseHunger( int amount ){
		CurHungerMeter -= amount;
		Mathf.Clamp(_curHungerMeter, 0, _maxHungerMeter);
	}
	private void AlertListeners(){
		foreach( IStomach l in _listeners ){
			l.HungerChanged( CurHungerMeter, _maxHungerMeter );
		}
	}


	// SCHEDULED EVENT LISTENER
	public void RunScheduledEvent(){
		GrowHungry( 1 );
	}
}

public interface IStomach{
	void HungerChanged( int curHunger, int MaxHunger );
	void BeenFed();
}


public enum HungerState{
	Full,
	Normal,
	Hungry,
	Starving
}