using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldTime : MonoBehaviour {


	// PROPERTIES
	public TimeStamp TimeStamp{
		get{
			return new TimeStamp( Hour, Minute, (int)Mathf.Floor( Time.time/_secondsPerMinute ));
		}
	}
	public int Minute{
		get{
			return _minute;
		}
	}
	public int Hour{
		get{
			return _hour;
		}
	}


	// INSTANCE VARIABLES
	private int _secondsPerMinute = 1;
	private int _minutesPerHour = 5;
	private int _hour   = 0;
	private int _minute = 0;
	private Dictionary<int, List<IScheduledEvent>> _scheduledAlerts;


	// MONO
	private void Awake(){
		_scheduledAlerts = new Dictionary<int,List<IScheduledEvent>>();
	}


	// PUBLIC METHODS
	public void ScheduleAlert( IScheduledEvent eventListener,  int hour, int minutes ){

		var totalMinutes = (int)Mathf.Floor( Time.time/_secondsPerMinute )+1;
		var t = totalMinutes + hour*_minutesPerHour+minutes;

		if (_scheduledAlerts.ContainsKey(t)){
			_scheduledAlerts[t].Add( eventListener );
		}
		else{
			_scheduledAlerts.Add( t, new List<IScheduledEvent>{eventListener} );
		}
	}
	public void RemoveAlerts( IScheduledEvent forProperty ){
		foreach( List<IScheduledEvent> tp in _scheduledAlerts.Values ){
			if (tp.Contains( forProperty ) ){
				tp.Remove( forProperty );
			}
		}
	}


	// PRIVATE METHODS
	private void Update () {
		
		var totalMinutes = (int)Mathf.Floor( Time.time/_secondsPerMinute );
		_minute = (int)Mathf.Floor( totalMinutes % _minutesPerHour) ;
		_hour   = (int)Mathf.Floor( totalMinutes / _minutesPerHour) ;

		var keys =  new List<int>(_scheduledAlerts.Keys);
		foreach ( int key in keys ){

			if (key <= totalMinutes){
				foreach (IScheduledEvent t in _scheduledAlerts[key]){
					t.RunScheduledEvent();
				}
					
				_scheduledAlerts.Remove(key);
			}
		}
	}
		
}
public struct TimeStamp {

	public int Hour;
	public int Minute;
	public int TotalMinutes;

	public TimeStamp( int hour, int minute, int totalMinutes ){
		Hour = hour;
		Minute = minute;
		TotalMinutes = totalMinutes;
	}
}
public interface IScheduledEvent{
	void RunScheduledEvent();
}