using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// BASE CLASS
public abstract class ConstructedObject : FreePlaceInventoryItem{
	protected override bool Consumable{
		get{
			return true;
		}
	}
	protected override int StackLimit{
		get{
			return 99;
		}
	}
}


// SUBCLASS
public class WoodenFence_InventoryItem : ConstructedObject {

	public override string Name {
		get {
			return "Wooden Fence";
		}
	}
	public override PlaceableObjectInfo PlaceableInfo {
		get{
			var p = new PlaceableObjectInfo();
			p.Prefab = Game.Resources.WoodenFencePost;
			Debug.Log(p.Prefab);
			return p;
		}
	}
	public override GameObject HoldItem{
		get{
			return Game.Resources.GenericHoldItem;
		}
	}
	public override Sprite Sprite{
		get{
			return Game.Resources.Fence;
		}
	}
}
public class Flower_InventoryItem : ConstructedObject {

	protected override int StackLimit{
		get{
			return 1;
		}
	}

	public override string Name {
		get {
			return "Flower";
		}
	}
	public override PlaceableObjectInfo PlaceableInfo {
		get{
			var p = new PlaceableObjectInfo();
			p.Prefab = Game.Resources.Prefab.Flower;
			return p;
		}
	}
	public override GameObject HoldItem{
		get{
			return Game.Resources.GenericHoldItem;
		}
	}
	public override Sprite Sprite{
		get{
			return Game.Resources.Sprites.Flower;
		}
	}
}
