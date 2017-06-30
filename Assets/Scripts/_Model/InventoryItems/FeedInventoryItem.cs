using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FeedInventoryItem : InventoryItem {

	public override InventoryItemTag Tag{
		get{
			return InventoryItemTag.None;
		}
	}

	// NEW
	public abstract FoodEffect FoodEffect {
		get;
	}


	// PUBLIC
	public void Use( IFeed on ){
		if (Time.time - _lastInteract > _waitTime){

			var result = on.Feed( FoodEffect );
			_lastInteract = Time.time;

			if( result ){ 
				Fed();
				Consume();
			}
			else{
				NotFed();
			}
		}
	}
	

	// PRIVATE
	private void Fed(){
	}
	private void NotFed(){
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
