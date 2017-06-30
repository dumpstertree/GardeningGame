using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractInventoryItem : InventoryItem {

	public override InventoryItemTag Tag{
		get{
			return InventoryItemTag.None;
		}
	}

	public override string Name {
		get {
			return "Interact";
		}
	}
	public override Sprite Sprite {
		get {
			throw new System.NotImplementedException ();
		}
	}
	protected override int StackLimit {
		get {
			throw new System.NotImplementedException ();
		}
	} 
	protected override bool Consumable {
		get {
			throw new System.NotImplementedException ();
		}
	}
	public override GameObject HoldItem {
		get {
			throw new System.NotImplementedException ();
		}
	}

	// USE
	public void Use( IInteract on ){
		if (Time.time - _lastInteract > _waitTime){
			_lastInteract = Time.time;
			on.Interact();

			GameObject.FindObjectOfType<PlayerCreature>().Animator.SetTrigger("Pickup");
			Game.Controller.Audio.OneShot(AudioType.LevelUp);
		}
	}
}
