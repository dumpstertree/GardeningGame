using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapleTreeSeeds : InventoryItem {

	public MapleTreeSeeds(){
		_sprite = Game.Resources.MapleSeed;
	}
	/*public override bool Use ( InteractorReciever receiver ){

		var tile = Game.Model.World.GetTile( Game.Controller.PlayerSpawner.CharacterController.X, Game.Controller.PlayerSpawner.CharacterController.Y );

		if (tile.Editable && !tile.CheckForObstruction()){

			// Add Property
			tile.AddProperty( new MapleTree( tile ) );

			// Remove From Inventory
			Inventory.RemoveFromEquipment( this );
		}
	}*/
	public override void Toss(){
		Inventory.RemoveFromEquipment( this );
	}
}
