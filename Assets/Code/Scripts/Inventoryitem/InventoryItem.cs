using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryItem  {


	// PROPERTIES
	public int 					   StackAmount{
		get{
			return _stackAmount;
		}
		set{
			Debug.Log(value);
			_stackAmount = value;
			RemoveEmptyItem(); 
			Inventory.StackChanged( this );
		}
	}
		
	public HitObjectInfo 			HitInfo;
	public PlaceableObjectInfo 		PlaceableInfo;


	// PUBLIC PROPERTIES
	public abstract Sprite Sprite {
		get;
	}
	public abstract InventoryItemActionType Action {
		get;
	}
	public abstract List<InteractorType> Recievers {
		get;
	}
	public abstract GameObject HoldItem{
		get;
	}

	public virtual string HudDescription{
		get;
	}


	// PRIVATE PROPERTIES
	protected abstract int  StackLimit{
		get;
	}
	protected abstract bool Destructable{
		get;
	}
		

	// PRIVATE
	private float 	_lastInteract;
	private int   	_stackAmount;
	protected float _waitTime;


	// INIT
	public InventoryItem(){
		
		_stackAmount    = 1;
		_waitTime 		= 1.0f;

		HudDescription = "Use Item";
	}


	// PUBLIC
	/// <summary>
	/// Add to item stack. Returns true if the item can be added and false if it cannot.
	/// </summary>
	/// <param name="receiver">Receiver.</param>
	public bool AddToStack( int amount ){

		if ( _stackAmount+amount <= StackLimit ){
			_stackAmount += amount;
			return true;
		}

		else{
			return false;
		}
	}
	public void RemoveFromStack( int amount ){
	}


	public virtual void Use ( Interactor interactor ){

		if (Time.time - _lastInteract > _waitTime){

			if (PlaceableInfo.Prefab){
				GameObject.Instantiate( PlaceableInfo.Prefab, interactor.transform.position, Quaternion.Euler(Vector3.zero));
				ReturnedHit();
			}
			else{
				Debug.LogWarning("Tried to create a placeable object that did no exist;");
			}
		}
	}
	/// <summary>
	/// Used To use an item that has the Place Action
	/// </summary>
	/// <param name="receiver">Receiver.</param>
	public virtual bool Use(  InteractorReciever receiver ){

		if (Time.time - _lastInteract > _waitTime){
			var result = receiver.InteractableObject.Interact( this );

			if (result){
				ReturnedHit();
			}
			else{
				ReturnedMiss();
			}
			return true;
		}

		else{
			return false;
		}
	}
	/// <summary>
	/// Returns True if the Action was taken and False if the action was ignored due to the action cooldown, this is different than the action Failing
	/// </summary>
	/// <param name="receiver">Receiver.</param>
	public virtual bool Miss(){

		if (Time.time - _lastInteract > _waitTime){
			_lastInteract = Time.time;
			return true;
		}

		else{
			return false;
		}
	}
	/// <summary>
	/// Used to toss an object on the ground
	/// </summary>
	/// <param name="receiver">Receiver.</param>
	public virtual void Toss(){
	}


	// PROTECTED 
	protected virtual void ReturnedMiss(){
		_lastInteract = Time.time;
	}
	protected virtual void ReturnedHit(){
		_lastInteract = Time.time;
		if (Destructable){
			_stackAmount -= 1;
			if (_stackAmount <= 0){
				Inventory.RemoveFromEquipment( this );
			}
		}
	}
	private void RemoveEmptyItem(){
		if (_stackAmount <= 0){
			Inventory.RemoveFromInventory( this );
		}
	}
}

public struct PlaceableObjectInfo{
	public GameObject Prefab;	
}
public struct HitObjectInfo{
	public int HitStrength;
}
	
public enum HoldType{
	LeftHand,
	RightHand
}
public enum InventoryItemActionType{
	None,
	Hit,
	Water, 
	Plant,
	Feed,
	FreePlace
}

/*
public FreePlaceInventoryItem : InventoryItem {
	
	
}*/