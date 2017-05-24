using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Seeds : InventoryItem {

	public Seeds(){
		AssignPlaceableObject();
	}
	protected override void ReturnedHit (){
		base.ReturnedHit ();
		Game.Controller.PlayerSpawner.CharacterController.Animator.SetTrigger("Plant");
		Game.Controller.Audio.OneShot(AudioType.LevelUp);
	}

	public override InventoryItemActionType Action{
		get{
			return InventoryItemActionType.Plant;;
		}
	}
	public override List<InteractorType> Recievers{
		get{
			var r = new List<InteractorType>();
			r.Add( InteractorType.Ground );
			return r;
		}
	}

	protected override int StackLimit{
		get{
			return 99;
		}
	}
	protected override bool Destructable{
		get{
			return true;
		}
	}
		
 	protected abstract void AssignPlaceableObject();

}
	
public class PowerPlantSeed_InventoryItem : Seeds {
	public override Sprite Sprite{
		get{
			return Game.Resources.PowerPlantSeedSprite;
		}
	}
	public override GameObject HoldItem{
		get{
			return Game.Resources.GenericHoldItem;
		}
	}
	protected override void AssignPlaceableObject(){
		PlaceableInfo = new PlaceableObjectInfo();
		PlaceableInfo.Prefab = Game.Resources.PowerBerryTreePrefab;
	}
}
public class DefencePlantSeed_InventoryItem : Seeds {
	public override Sprite Sprite{
		get{
			return Game.Resources.DefencePlantSeedSprite;
		}
	}
	public override GameObject HoldItem{
		get{
			return Game.Resources.GenericHoldItem;
		}
	}
	protected override void AssignPlaceableObject(){
		PlaceableInfo = new PlaceableObjectInfo();
		PlaceableInfo.Prefab = Game.Resources.PowerBerryTreePrefab;
	}
}
public class SpeedPlantSeed_InventoryItem : Seeds {
	public override Sprite Sprite{
		get{
			return Game.Resources.SpeedPlantSeedSprite;
		}
	}
	public override GameObject HoldItem{
		get{
			return Game.Resources.GenericHoldItem;
		}
	}
	protected override void AssignPlaceableObject(){
		PlaceableInfo = new PlaceableObjectInfo();
		PlaceableInfo.Prefab = Game.Resources.PowerBerryTreePrefab;
	}
}
