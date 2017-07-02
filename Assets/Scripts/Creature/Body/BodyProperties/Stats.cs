using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour {

	// PROPERTIES

	public List<IStats> Listeners{
		get{
			return _listeners;
		}
	}
	private List<IStats> _listeners = new List<IStats>();

	public void AddListenerToStats( IStats l ){
		if (!_listeners.Contains(l)){
			_listeners.Add(l);
		}
	}
	public void RemoveListenerFromStats( IStats l ){
		if (_listeners.Contains(l)){
			_listeners.Remove(l);
		}
	}


	public int MaxHealth {
		get{
			return _maxHealth;
		}
	}
	[SerializeField] private int _maxHealth;

	public int Health {
		get{
			return _health;
		}
	}
	[SerializeField] private int _health;


	public int Power{
		get{
			return _power;
		}
		set{
			_power = value;
			AlertDelegatesStatChanged( StatsType.Power, _power );
		}
	}
	private int _power = 5;

	public int Magick{
		get{
			return _magick;
		}
		set{
			_magick = value;
			AlertDelegatesStatChanged( StatsType.Magick, _magick );
		}
	}
	private int _magick = 5;

	public int Defence{
		get{
			return _defence;
		}
		set{
			_defence = value;
			AlertDelegatesStatChanged( StatsType.Defence, _defence );
		}
	}
	private int _defence = 5;

	public int MagickDefence{
		get{
			return _magickDefence;
		}
		set{
			_magickDefence = value;
			AlertDelegatesStatChanged( StatsType.MagickDefence, _magickDefence );
		}
	}
	private int _magickDefence = 5;

	public int Speed{
		get{
			return _speed;
		}
		set{
			_speed = value;
			AlertDelegatesStatChanged( StatsType.Speed, _speed );
		}
	}
	private int _speed = 5;

	public int Luck{
		get{
			return _luck;
		}
		set{
			_luck = value;
			AlertDelegatesStatChanged( StatsType.Luck, _luck );
		}
	}
	private int _luck = 5;

	// INSTANCE VARIABLES
	private float _regenerateCooldown = 1.0f;
	private float _regenTime = 0.0f;


	// PUBLIC METHODS
	public void HitWithProjectile( ShootInfo info ){

		var damage = Stats.CalculateDamage(info);
		_health -= damage;

		if (_health <= 0){
			Debug.LogWarning( "No Kill currently itergrated" );
		}

		AlertDelegatesHealthChanged();

		var n = Instantiate( Game.Resources.FloatingNumber).GetComponent<FloatingNumberBehavior>();
		n.Startup( transform, damage, false );
	}

	
	public void AddBaseStats( StatsType stat, int value ){

		switch( stat ){

		case StatsType.Power:
			Power += value;
			break;
		
		case StatsType.Magick:
			Magick += value;
			break;
		
		case StatsType.Defence:
			Defence += value;
			break;
		
		case StatsType.MagickDefence:
			MagickDefence += value;
			break;
		
		case StatsType.Speed:
			Speed += value;
			break;
		
		case StatsType.Luck:
			Luck += value;
			break;
		}
	}


	// STATIC METHODS
	public static int CalculateDamage( ShootInfo info ){
		return info.Power;
	}

	private void AlertDelegatesHealthChanged(){
		foreach( IStats l in _listeners ){
			l.HealthChanged( _health, _maxHealth );		
		}
	}

	private void AlertDelegatesStatChanged( StatsType stat, int value  ){
		foreach( IStats l in _listeners ){
			l.StatsChanged( stat, value );		
		}
	}
}


public interface IStats{
	void HealthChanged( int health, int maxHealth );
	void StatsChanged( StatsType stat, int value  );
}

public enum StatsType{
	Power,
	Magick,
	Defence,
	MagickDefence,
	Speed,
	Luck
}