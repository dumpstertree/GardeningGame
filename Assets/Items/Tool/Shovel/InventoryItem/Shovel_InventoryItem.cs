using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel_InventoryItem : InventoryItem {

	public override InventoryItemActionType Action{
		get{
			return InventoryItemActionType.FreePlace;;
		}
	}
	public override GameObject HoldItem{
		get{
			return Game.Resources.GenericHoldItem;
		}
	}
	public override List<InteractorType> Recievers{
		get{
			var r = new List<InteractorType>();
			return r;
		}
	}
	public override Sprite Sprite{
		get{
			return Game.Resources.Hoe;
		}
	}

	protected override int StackLimit{
		get{
			return 1;
		}
	}
	protected override bool Destructable{
		get{
			return false;
		}
	}


	// PUBLIC METHODS
	public Shovel_InventoryItem(){
		PlaceableInfo = new PlaceableObjectInfo();
		PlaceableInfo.Prefab = Game.Resources.DirtTile;
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
