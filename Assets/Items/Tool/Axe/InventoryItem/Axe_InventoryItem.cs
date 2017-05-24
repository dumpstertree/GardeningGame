using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe_InventoryItem : InventoryItem {

	public override InventoryItemActionType Action{
		get{
			return InventoryItemActionType.Hit;;
		}
	}
	public override GameObject HoldItem{
		get{
			return Game.Resources.AxeHoldItem;
		}
	}
	public override List<InteractorType> Recievers{
		get{
			var r = new List<InteractorType>();
			r.Add( InteractorType.PlacedObject );
			return r;
		}
	}
	public override Sprite Sprite{
		get{
			return Game.Resources.Axe;
		}
	}

	protected override int StackLimit{
		get{
			return 1;
		}
	}
	protected override bool Destructable{
		get{
			return false;;
		}
	}

	// PUBLIC METHODS
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
	