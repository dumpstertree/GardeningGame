using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel_InventoryItem : InventoryItem {


	// PUBLIC METHODS
	public Shovel_InventoryItem(){
		_holdItem 		= Game.Resources.GenericHoldItem;
		_sprite   		= Game.Resources.Hoe;
		_action 		= InventoryItemActionType.FreePlace;
		_placeableObject = new PlaceableObjectInfo();
		_placeableObject.Prefab = Game.Resources.DirtTile;
		_destructable 	= false;
	}
	public override bool Miss(){
		var actionTaken = base.Miss();

		if(actionTaken){
			Game.Controller.PlayerSpawner.CharacterController.Animator.SetTrigger("Dig");
			Game.Controller.Audio.OneShot(AudioType.AxeSwing);
		}

		return actionTaken;
	}
	protected override void ReturnedHit (){
		base.ReturnedHit ();
		Game.Controller.PlayerSpawner.CharacterController.Animator.SetTrigger("Dig");
		Game.Controller.Audio.OneShot(AudioType.AxeSwing);
	}
	protected override void ReturnedMiss (){
		base.ReturnedMiss ();
		Game.Controller.PlayerSpawner.CharacterController.Animator.SetTrigger("Dig");
		Game.Controller.Audio.OneShot(AudioType.AxeSwing);
	}
	public override void Toss(){
	}
}
