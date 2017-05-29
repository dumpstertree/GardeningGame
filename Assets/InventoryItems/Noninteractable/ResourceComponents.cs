using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ResourceComponents : InventoryItem{

	// PUBLIC
	public override GameObject HoldItem{
		get{
			return Game.Resources.GenericHoldItem;
		}
	}
		
	// PRIVATE
	protected override int StackLimit{
		get{
			return 99;
		}
	}
	protected override bool Consumable{
		get{
			return false;
		}
	}

}

public class Wood_InventoryItem : ResourceComponents{
	public override string Name {
		get {
			return "Wood Sticks";
		}
	}
	public override Sprite Sprite{
		get{
			return Game.Resources.WoodResourceSprite;
		}
	}
}
