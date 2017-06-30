using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shovel_InventoryItem : FreePlaceInventoryItem {

	public override string Name {
		get {
			return "Shovel";
		}
	}
	public override GameObject HoldItem{
		get{
			return Game.Resources.Prefab.ShovelHoldItem;
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
	protected override bool Consumable{
		get{
			return false;
		}
	}
		
	public override PlaceableObjectInfo PlaceableInfo {
		get{
			var p = new PlaceableObjectInfo();
			p.Prefab = Game.Resources.DirtTile;
			return p;
		}
	}
}
