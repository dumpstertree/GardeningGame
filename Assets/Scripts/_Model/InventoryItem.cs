using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryItem  {


	// PROPERTIES
	public int StackAmount{
		get{
			return _stackAmount;
		}
	}


	public abstract string Name{
		get;
	}
	public abstract Sprite Sprite {
		get;
	}
	public abstract GameObject HoldItem{
		get;
	}
	public virtual string HudDescription{
		get;
	}
	public abstract InventoryItemTag Tag{
		get;
	}


	// PRIVATE PROPERTIES
	protected abstract int  StackLimit{
		get;
	}
	protected abstract bool Consumable{
		get;
	}
		

	// PRIVATE
	protected static float _lastInteract;
	protected int   _stackAmount;
	protected float _waitTime;


	// INIT
	protected InventoryItem(){
		_stackAmount    = 1;
		_waitTime 		= 2f;
		HudDescription = "Use Item";
	}
	public bool AddToStack( int amount ){

		if ( _stackAmount+amount <= StackLimit ){
			_stackAmount += amount;
			return true;
		}
		
		return false;
	}
	public void RemoveFromStack( int amount ){
		
		_stackAmount -= amount;

		if ( _stackAmount < 1 ){
			Inventory.Data.RemoveItem( this );
		}
	}
		
	protected void Consume(){

		if( Consumable ){
			
			_stackAmount -= 1;
			if ( _stackAmount < 1 ){
				Inventory.Data.RemoveItem( this );
			} 
		}
	}

}