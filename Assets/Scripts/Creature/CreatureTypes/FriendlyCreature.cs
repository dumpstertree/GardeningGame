using UnityEngine;

public class FriendlyCreature : Creature {
	

	// PROPERTIES
	public override Brain Brain{
		get {
			return _brain;
		}
	}
	public override Body Body{
		get{
			return _body;
		}
	}


	// INSTANCE
	[SerializeField] private FriendlyBrain _brain;
	[SerializeField] private FriendlyBody  _body;


	// PUBLIC
	public bool Feed( FoodEffect food ){
		var r = _brain.Feed( food );
		if( r ){
			_body.Feed( food );
		}
		return r;
	}
}
