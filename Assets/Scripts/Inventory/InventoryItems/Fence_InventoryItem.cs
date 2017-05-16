using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence_InventoryItem : InventoryItem {

	public Fence_InventoryItem(){
		_holdItem =  Game.Resources.WoodenFencePost;
		_sprite = Game.Resources.Fence;
		_recievers.Add( InteractorType.Ground );
	}

	/*public override bool Use (  InteractorReciever receiver  ){

		var tile = Game.Model.World.GetTile( Game.Controller.PlayerSpawner.CharacterController.X, Game.Controller.PlayerSpawner.CharacterController.Y );

		if ( tile.Editable && !tile.CheckForObstruction() ){
			// Add Property
			tile.AddProperty( new Fence( tile ) );
		}
	}*/
	public override void Toss(){

	}
}
