using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Egg : InventoryItem {

	protected CreatureRace _race;

	public Egg(): base(){

		PlaceableInfo = new PlaceableObjectInfo();
		AssignPlaceableObject();
	}

	public override InventoryItemActionType Action{
		get{
			return InventoryItemActionType.FreePlace;;
		}
	}
	public override List<InteractorType> Recievers{
		get{
			var r = new List<InteractorType>();
			return r;
		}
	}

	protected override int StackLimit{
		get{
			return 1;
		}
	}
	protected override bool Destructable{
		get{
			return true;
		}
	}
		
	protected abstract void AssignPlaceableObject();
}

public class RedLymeEgg : Egg{
	public override Sprite Sprite{
		get{
			return Game.Resources.Gun;
		}
	}
	protected override void AssignPlaceableObject(){
		PlaceableInfo.Prefab = Game.Resources.GreenLymeEggPrefab;
	}
	public override GameObject HoldItem{
		get{
			return Game.Resources.RedLymeEggPrefab;
		}
	}
}
public class GreenLymeEgg : Egg{
	public override Sprite Sprite{
		get{
			return Game.Resources.Gun;
		}
	}
	protected override void AssignPlaceableObject(){
		PlaceableInfo.Prefab = Game.Resources.GreenLymeEggPrefab;
	}
	public override GameObject HoldItem{
		get{
			return Game.Resources.GreenLymeEggPrefab;
		}
	}
}