using UnityEngine;

public abstract class ResourceComponents : InventoryItem{

	public override InventoryItemTag Tag{
		get{
			return InventoryItemTag.None;
		}
	}

	// PUBLIC
	public override GameObject HoldItem{
		get{
			return Game.Resources.GenericHoldItem;
		}
	}
		
	// PRIVATE
	protected override int StackLimit{
		get{
			return 999;
		}
	}
	protected override bool Consumable{
		get{
			return false;
		}
	}

}

public class Wood_InventoryItem : ResourceComponents{
	public override InventoryItemTag Tag{
		get{
			return InventoryItemTag.None;
		}
	}

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

public class Bullet_InventoryItem : ResourceComponents{
	
	public override InventoryItemTag Tag{
		get{
			return InventoryItemTag.Bullet;
		}
	}

	public override string Name {
		get {
			return "Bullet";
		}
	}
	public override Sprite Sprite{
		get{
			return Game.Resources.Sprites.Bullet;
		}
	}
}