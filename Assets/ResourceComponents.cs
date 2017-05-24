using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ResourceComponents : InventoryItem{

	// PUBLIC
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
		
	// PRIVATE
	protected override int StackLimit{
		get{
			return 99;
		}
	}
	protected override bool Destructable{
		get{
			return false;
		}
	}

}

public class Wood_InventoryItem : ResourceComponents{
	public override Sprite Sprite{
		get{
			return Game.Resources.WoodResourceSprite;
		}
	}
}
