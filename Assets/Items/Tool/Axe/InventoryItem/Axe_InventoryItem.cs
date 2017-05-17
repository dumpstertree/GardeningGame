using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe_InventoryItem : InventoryItem {


	// PUBLIC METHODS
	public Axe_InventoryItem(){
		_holdItem 		= Game.Resources.AxeHoldItem;
		_sprite   		= Game.Resources.Axe;
		_recievers.Add( InteractorType.PlacedObject );

		_action 		= InventoryItemActionType.Hit;
		_hitStrength 	= 1;
		_destructable 	= false;
	}
	public override bool Miss(){
		var actionTaken = base.Miss();

		if(actionTaken){
			Game.Controller.PlayerSpawner.CharacterController.Animator.SetTrigger("Swing");
			Game.Controller.Audio.OneShot(AudioType.AxeSwing);
		}

		return actionTaken;
	}
	protected override void ReturnedHit (){
		base.ReturnedHit ();
		Game.Controller.PlayerSpawner.CharacterController.Animator.SetTrigger("Swing");
		Game.Controller.Audio.OneShot(AudioType.AxeSwing);
	}
	protected override void ReturnedMiss (){
		base.ReturnedMiss ();
		Game.Controller.PlayerSpawner.CharacterController.Animator.SetTrigger("Swing");
		Game.Controller.Audio.OneShot(AudioType.AxeSwing);
	}
	public override void Toss(){
	}
}
