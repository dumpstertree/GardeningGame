using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCreature : Creature {

	public void ShootProjectile(){
		Game.Controller.ScreenShake.ShakeLight();
		Game.Controller.Audio.OneShot(AudioType.Gunshot);
		var b = Instantiate( Game.Resources.Bullet ).GetComponent<BulletBehavior>();
		b.Startup( this );
	}
	public void ProjectileHit( Creature creature ){

		var r = BelongsToFactionsBitmask & creature.BelongsToFactionsBitmask;
		if ( r == 0 ){
			_target = creature;
			StateManager.GameState = GameState.InBattle;
		}
	}


	public class Stats : MonoBehaviour {

		// PROPERTIES
		public StatsDelegate Delegate{ 
			set{
				_delegates.Add( value );
			}
		}
		private List<StatsDelegate> _delegates;

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
				foreach( StatsDelegate d in _delegates ){
					d.StatsChanged( StatsType.Power, _power );
				}
			}
		}
		private int _power = 5;

		public int Magick{
			get{
				return _magick;
			}
			set{
				_magick = value;
				foreach( StatsDelegate d in _delegates ){
					d.StatsChanged( StatsType.Magick, _magick );
				}
			}
		}
		private int _magick = 5;

		public int Defence{
			get{
				return _defence;
			}
			set{
				_defence = value;
				foreach( StatsDelegate d in _delegates ){
					d.StatsChanged( StatsType.Defence, _defence );
				}
			}
		}
		private int _defence = 5;

		public int MagickDefence{
			get{
				return _magickDefence;
			}
			set{
				_magickDefence = value;
				foreach( StatsDelegate d in _delegates ){
					d.StatsChanged( StatsType.MagickDefence, _magickDefence );
				}
			}
		}
		private int _magickDefence = 5;

		public int Speed{
			get{
				return _speed;
			}
			set{
				_speed = value;
				foreach( StatsDelegate d in _delegates ){
					d.StatsChanged( StatsType.Speed, _speed );
				}
			}
		}
		private int _speed = 5;

		public int Luck{
			get{
				return _luck;
			}
			set{
				_luck = value;
				foreach( StatsDelegate d in _delegates ){
					d.StatsChanged( StatsType.Luck, _luck );
				}
			}
		}
		private int _luck = 5;

		// INSTANCE VARIABLES
		private Creature _creature;
		private float _regenerateCooldown = 1.0f;
		private float _regenTime = 0.0f;



		// PRIVATE METHODS
		private void Awake(){

			_delegates = new List<StatsDelegate>();

			_creature = GetComponentInParent<Creature>();
			if (_creature == null){
				Debug.LogWarning( "Stats Component missing Creature in parent" );
			}
		}


		// PUBLIC METHODS
		public void SubtractHealth( int health ){

			_health -= health;

			AlertDelegatesHealthChanged();

			var n = Instantiate( Game.Resources.FloatingNumber).GetComponent<FloatingNumberBehavior>();
			n.Startup( transform, Stats.CalculateDamage(health), false );
		}
		public void AddHealth( int health ){

			_health += health;

			if (_health >= _maxHealth){
				_health = _maxHealth;
			}

			AlertDelegatesHealthChanged();

			var n = Instantiate( Game.Resources.FloatingNumber).GetComponent<FloatingNumberBehavior>();
			n.Startup( transform, Stats.CalculateDamage(health), true );
		}
		public void Regenerate(){	
			if( _health < _maxHealth){
				_regenTime += Time.deltaTime;
				if (_regenTime > _regenerateCooldown){
					AddHealth( (int)Mathf.Ceil( (float)_maxHealth * .01f) );
					_regenTime = 0;
				}
			}
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
		public static int CalculateDamage( int rawDamage ){
			return rawDamage;
		}

		private void AlertDelegatesHealthChanged(){
			foreach( StatsDelegate d in _delegates ){
				d.HealthChanged( _health, _maxHealth );		
			}
		}
	}


	public interface StatsDelegate{
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
}
