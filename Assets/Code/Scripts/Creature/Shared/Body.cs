using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour {

	public Stats Stats;


	// PROPERTIES
	public Visibility CreatureVisiblity{
		get{
			return new Visibility( this );
		}
	}


	// PUBLIC
	public void Load( CreatureData data ){
		// Load Race
		// Load Scale
		// Load Animator
	}
	public void Hit( int rawDamage ){
	}
	public void Feed( Food food ){

		if (food.FoodEffect.Power != 0){
			Stats.AddBaseStats( StatsType.Power, food.FoodEffect.Power );
		}

		if (food.FoodEffect.Magick != 0){
			Stats.AddBaseStats( StatsType.Magick, food.FoodEffect.Magick );
		}

		if (food.FoodEffect.Defence != 0){
			Stats.AddBaseStats( StatsType.Defence, food.FoodEffect.Defence );
		}

		if (food.FoodEffect.MagickDefence != 0){
			Stats.AddBaseStats( StatsType.MagickDefence, food.FoodEffect.MagickDefence );
		}

		if (food.FoodEffect.Speed != 0){
			Stats.AddBaseStats( StatsType.Speed, food.FoodEffect.Speed );
		}

		if (food.FoodEffect.Luck != 0){
			Stats.AddBaseStats( StatsType.Luck, food.FoodEffect.Luck );
		}

	}

		// LISTENER REQUESTS
	public void AddListenerToStats( IStats l ){
		Stats.AddListenerToStats( l );
	}
	public void RemoveListenerFromStats( IStats l ){
		Stats.RemoveListenerFromStats( l );
	}


	// MONO 
	private void Start(){
		Stats = GetComponentInChildren<Stats>();
	}
}

public struct Visibility{

	public int CurrentHealth;
	public Visibility( Body body ){
		CurrentHealth = body.Stats.Health;
	}
}

public enum Faction{
	Player 	  = 0,
	GreenLyme = 1,
	RedLyme	  = 2
}
