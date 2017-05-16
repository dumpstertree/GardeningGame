using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryItem  {


	// PROPERTIES
	public GameObject 			   HoldItem {
		get{
			return _holdItem;
		}
	}
	public Sprite 				   Sprite {
		get{
			return _sprite;
		}
	}
	public List<InteractorType>    Recievers{
		get{
			return _recievers;
		}
	}
	public InventoryItemActionType Action{
		get{
			return _action;
		}
	}
	public int 					   HitStrength {
		get{
			return _hitStrength;
		}
	}
		
	protected GameObject 			  _holdItem;
	protected Sprite 			  	  _sprite;
	protected List<InteractorType> 	  _recievers;
	protected InventoryItemActionType _action;
	protected int _hitStrength;
	public PlaceableObjectInfo _placeableObject;


	// PRIVATE
	private float _lastInteract;


	// PROTECTED
	protected float _waitTime;
	protected bool _destructable;


	// INIT
	public InventoryItem(){
		_sprite  		= Game.Resources.Axe;
		_holdItem 		= Game.Resources.GenericHoldItem; 
		_recievers 		= new List<InteractorType>();
		_waitTime 		= 1.0f;
		_destructable 	= true;
	}


	// PUBLIC
	/// <summary>
	/// Returns True if the Action was taken and False if the action was ignored due to the action cooldown, this is different than the action Failing
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
	public virtual void Toss(){
	}
		

	// PROTECTED 
	protected virtual void ReturnedMiss(){
		_lastInteract = Time.time;
	}
	protected virtual void ReturnedHit(){
		_lastInteract = Time.time;
		if (_destructable){
			Inventory.RemoveFromEquipment( this );
		}
	}
}

public struct PlaceableObjectInfo{
	public GameObject Prefab;	
}


public enum HoldType{
	LeftHand,
	RightHand
}
public enum InventoryItemActionType{
	None,
	Hit,
	Place,
	FreePlace
}