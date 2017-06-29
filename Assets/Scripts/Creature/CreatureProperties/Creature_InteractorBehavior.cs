using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature_InteractorBehavior : InteractableBehavior, IFeed {
	
	private FriendlyCreature _creature;
	private void Awake(){
		_creature = GetComponent<FriendlyCreature>();
	}
	public bool Feed( FoodEffect info ){
		return _creature.Feed( info );
	}
}
