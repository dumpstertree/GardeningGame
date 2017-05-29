using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature_InteractorBehavior : InteractableBehavior, IFeed {
	
	[SerializeField] private Creature _creature;
	public bool Feed( FoodEffect info ){
		
		//return _creature.Feed( (Food)item );
		return true;
	}
}
