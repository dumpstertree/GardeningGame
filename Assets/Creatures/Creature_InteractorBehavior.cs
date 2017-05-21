using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature_InteractorBehavior : InteractableBehavior {

	[SerializeField] private Creature _creature;
	protected override bool Feed( InventoryItem item ){

		if (item is Food){
			_creature.Growth.Feed( (Food)item );
			return true;
		}

		return false;
	}

	protected override bool Plant( InventoryItem item ){
		Debug.LogWarning( "Tried to PLACE on creature;" );
		return false;
	}

	protected override bool Hit( InventoryItem item ){
		Debug.LogWarning( "Tried to HIT on creature;" );
		return false;
	}
}
