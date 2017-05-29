using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Food : FeedInventoryItem {

	// PRIVATE
	protected override int StackLimit{
		get{
			return 99;
		}
	}
	protected override bool Consumable{
		get{
			return true;
		}
	}
}


public class PowerBerry_InventoryItem : Food {
	public override string Name {
		get {
			return "Power Berry";
		}
	}
	public override Sprite Sprite{
		get{
			return Game.Resources.PowerBerrySprite;
		}
	}
	public override GameObject HoldItem{
		get{
			return Game.Resources.GenericHoldItem;
		}
	}
	public override FoodEffect FoodEffect {
		get{
			return new FoodEffect(1,1,0,0,0,0,0);
		}
	}
}
public class DefenceBerry_InventoryItem : Food {
	public override string Name {
		get {
			return "Defence Berry";
		}
	}
	public override Sprite Sprite{
		get{
			return Game.Resources.DefenceBerrySprite;
		}
	}
	public override GameObject HoldItem{
		get{
			return Game.Resources.GenericHoldItem;
		}
	}
	public override FoodEffect FoodEffect {
		get{
			return new FoodEffect(1,0,0,1,0,0,0);
		}
	}
}
public class SpeedBerry_InventoryItem : Food {
	public override string Name {
		get {
			return "Speed Berry";
		}
	}
	public override Sprite Sprite{
		get{
			return Game.Resources.SpeedBerrySprite;
		}
	}
	public override GameObject HoldItem{
		get{
			return Game.Resources.GenericHoldItem;
		}
	}
	public override FoodEffect FoodEffect {
		get{
			return new FoodEffect(1,0,0,0,0,1,0);
		}
	}
}
