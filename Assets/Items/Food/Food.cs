using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Food : InventoryItem {

	// PROPERTIES
	public FoodEffect FoodEffect {
		get{
			return _effect;
		}
	}
	protected FoodEffect _effect;


	// CONSTRUCTOR
	public Food() : base(){
		AssignFoodEffect();
	}

	public override InventoryItemActionType Action{
		get{
			return InventoryItemActionType.Feed;;
		}
	}
	public override List<InteractorType> Recievers{
		get{
			var r = new List<InteractorType>();
			r.Add( InteractorType.Creature );
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
		
	protected abstract void AssignFoodEffect();
}

public struct FoodEffect{

	public int Hunger;		 
	public int Power; 		 
	public int Magick; 		 
	public int Defence; 		 
	public int MagickDefence; 
	public int Speed; 		 
	public int Luck;	

	public FoodEffect( int hunger, int power, int magick, int defence, int magickDefence, int speed, int luck ){
		Hunger = hunger;
		Power = power;
		Magick = magick;
		Defence = defence;
		MagickDefence = magickDefence;
		Speed = speed;
		Luck = luck;
	}
}



public class PowerBerry_InventoryItem : Food {
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
	protected override void AssignFoodEffect(){
		_effect = new FoodEffect(1,1,0,0,0,0,0);
	}
}
public class DefenceBerry_InventoryItem : Food {
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
	protected override void AssignFoodEffect(){
		_effect = new FoodEffect(1,0,0,1,0,0,0);
	}
}
public class SpeedBerry_InventoryItem : Food {
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
	protected override void AssignFoodEffect(){
		_effect = new FoodEffect(1,0,0,0,0,1,0);
	}
}
