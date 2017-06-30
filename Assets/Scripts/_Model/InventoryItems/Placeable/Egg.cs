using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// BASE CLASS
public abstract class Egg : FreePlaceInventoryItem {

	// PRIVATE
	protected override int StackLimit{
		get{
			return 1;
		}
	}
	protected override bool Consumable{
		get{
			return true;
		}
	}
		
}


// SUBCLASS
public class RedLymeEgg : Egg{
	public override string Name {
		get {
			return "Red Lyme Egg";
		}
	}
	public override Sprite Sprite{
		get{
			return Game.Resources.Gun;
		}
	}
	public override PlaceableObjectInfo PlaceableInfo {
		get{
			var p = new PlaceableObjectInfo();
			p.Prefab = Game.Resources.RedLymeEggPrefab;
			return p;
		}
	}
	public override GameObject HoldItem{
		get{
			return Game.Resources.RedLymeEggPrefab;
		}
	}
}
public class GreenLymeEgg : Egg{
	public override string Name {
		get {
			return "Green Lyme Egg";
		}
	}
	public override Sprite Sprite{
		get{
			return Game.Resources.Gun;
		}
	}
	public override PlaceableObjectInfo PlaceableInfo {
		get{
			var p = new PlaceableObjectInfo();
			p.Prefab = Game.Resources.GreenLymeEggPrefab;
			return p;
		}
	}
	public override GameObject HoldItem{
		get{
			return Game.Resources.GreenLymeEggPrefab;
		}
	}
}