using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodenFence_InventoryItem : InventoryItem {


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
			r.Add( InteractorType.Ground );
			return r;
		}
	}
	public override Sprite Sprite{
		get{
			return Game.Resources.Fence;
		}
	}


	protected override bool Destructable{
		get{
			return true;
		}
	}
	protected override int StackLimit{
		get{
			return 99;
		}
	}

	public WoodenFence_InventoryItem(){
		PlaceableInfo = new PlaceableObjectInfo();
		PlaceableInfo.Prefab = Game.Resources.WoodenFencePost;
	}
	protected override void ReturnedHit (){
		base.ReturnedHit ();
		Game.Controller.PlayerSpawner.CharacterController.Animator.SetTrigger("Plant");
		Game.Controller.Audio.OneShot(AudioType.LevelUp);
	}
}
