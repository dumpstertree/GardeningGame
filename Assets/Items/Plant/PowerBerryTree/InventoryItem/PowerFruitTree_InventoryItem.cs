using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerFruitTree_InventoryItem : InventoryItem {

	public PowerFruitTree_InventoryItem(){
		_holdItem = Game.Resources.GenericHoldItem;
		_sprite   = Game.Resources.PowerBerrySprite;
		_action	  = InventoryItemActionType.FreePlace;

		_recievers.Add( InteractorType.Ground );

		_placeableObject = new PlaceableObjectInfo();
		_placeableObject.Prefab = Game.Resources.PowerBerryTreePrefab;
	}
	protected override void ReturnedHit (){
		base.ReturnedHit ();
		Game.Controller.PlayerSpawner.CharacterController.Animator.SetTrigger("Plant");
		Game.Controller.Audio.OneShot(AudioType.LevelUp);
	}
}
