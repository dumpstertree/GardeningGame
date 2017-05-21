using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenFence_InventoryItem : InventoryItem {

	public WoodenFence_InventoryItem(){
		_holdItem 		= Game.Resources.GenericHoldItem;
		_sprite   		= Game.Resources.Fence;
		_action 		= InventoryItemActionType.FreePlace;

		_recievers.Add( InteractorType.Ground );

		_placeableObject = new PlaceableObjectInfo();
		_placeableObject.Prefab = Game.Resources.WoodenFencePost;
		_destructable = false;
	}
	protected override void ReturnedHit (){
		base.ReturnedHit ();
		Game.Controller.PlayerSpawner.CharacterController.Animator.SetTrigger("Plant");
		Game.Controller.Audio.OneShot(AudioType.LevelUp);
	}
}
