using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class Gun_InventoryItem : InventoryItem{

	public override InventoryItemActionType Action{
		get{
			return InventoryItemActionType.None;;
		}
	}

	protected override int StackLimit{
		get{
			return 1;
		}
	}

	public Gun_InventoryItem(){

		_holdItem = Game.Resources.GunHoldItem;
		_sprite   = Game.Resources.Gun;
	}

	/*public override bool Use ( InteractorReciever receiver ){
		Game.Controller.PlayerSpawner.CharacterController.mesh.GetComponent<PlayerCreature>().ShootProjectile();
	}
	public override void Toss(){
		Inventory.RemoveFromEquipment( this );
	}*/
//}
