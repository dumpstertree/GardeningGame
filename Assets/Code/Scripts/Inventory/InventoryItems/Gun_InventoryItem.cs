﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun_InventoryItem : InventoryItem{

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
}