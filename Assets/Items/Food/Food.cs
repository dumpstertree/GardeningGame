using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : InventoryItem {

	// PROPERTIES
	public FoodEffect FoodEffect {
		get{
			return _effect;
		}
	}
	protected FoodEffect _effect;


	// CONSTRUCTOR
	public Food() : base(){
		_sprite = null;
		_holdItem = Game.Resources.GenericHoldItem;
		_recievers.Add( InteractorType.Creature );
		_effect = new FoodEffect(0,0,0,0,0,0,0);
		_action = InventoryItemActionType.Feed;
	}
		
}

public class PowerBerry : Food {

	public PowerBerry() : base(){
		_sprite = Game.Resources.PowerBerrySprite;
		_effect = new FoodEffect(1,1,0,0,0,0,0);
	}
}
public class DefenceBerry : Food {

	public DefenceBerry() : base(){
		_sprite = Game.Resources.DefenceBerrySprite;
		_effect = new FoodEffect(1,0,0,1,0,0,0);
	}
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