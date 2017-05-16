using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof( Stomach) ) ]
public class CreatureGrowth : MonoBehaviour {

	private Stomach _stomach;
	private Creature _creature;

	// MONO
	private void Awake(){
		_stomach = GetComponent<Stomach>();
		_creature = GetComponentInParent<Creature>();
	}


	// ACTIONS
	public bool Feed( Food food ){
		if (_stomach.Feed( food.FoodEffect.Hunger )){
			
			if (food.FoodEffect.Power != 0){
				_creature.CreatureStats.AddBaseStats( StatsType.Power, food.FoodEffect.Power );
			}

			if (food.FoodEffect.Magick != 0){
				_creature.CreatureStats.AddBaseStats( StatsType.Magick, food.FoodEffect.Magick );
			}

			if (food.FoodEffect.Defence != 0){
				_creature.CreatureStats.AddBaseStats( StatsType.Defence, food.FoodEffect.Defence );
			}

			if (food.FoodEffect.MagickDefence != 0){
				_creature.CreatureStats.AddBaseStats( StatsType.MagickDefence, food.FoodEffect.MagickDefence );
			}

			if (food.FoodEffect.Speed != 0){
				_creature.CreatureStats.AddBaseStats( StatsType.Speed, food.FoodEffect.Speed );
			}

			if (food.FoodEffect.Luck != 0){
				_creature.CreatureStats.AddBaseStats( StatsType.Luck, food.FoodEffect.Luck );
			}

			return true;
		}
		else{
			return false;
		}
	}

	public bool Pet(){
		return false;
	}

}
