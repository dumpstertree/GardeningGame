using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory {

	// PROPERTIES
	static public  IInventory Delegate {
		set{
			_delegates.Add( value );
		}
	}
	static private List<IInventory> _delegates = new List<IInventory>();
	//
	static public int Capactiy{
		get{
			return _capacity;
		}
	}
	static private int _capacity = 12;
	//
	static public int ContentsCount{
		get{
			var i = 0;
			foreach ( InventoryItem item in _inventory ){
				if (item != null){
					i++;
				}
			}
			return i;
		}
	}
	// 
	static public int EquipSlots{
		get{
			return _equipSlots;
		}
	}
	static private int _equipSlots = 4;

	// INSTANCE VARIABLES
	static private InventoryItem[] _inventory = new InventoryItem[ _capacity ];
	static private InventoryItem[] _equipment = new InventoryItem[ _equipSlots ];

	// PUBLIC METHODS
	static public void StackChanged( InventoryItem item ){
		AlertDelegate();
	}
	static public int GetAmountInInventory( System.Type ofType ){

		var count = 0;
		foreach( InventoryItem i in _inventory ){
			if (i != null && i.GetType() == ofType ){
				count += i.StackAmount;
			}
		}
			
		return count;
	}
	static public void RemoveItemFromInventory(  System.Type ofType, int ofAmount ){

		var amountLeft = ofAmount;

		foreach( InventoryItem i in _inventory ){
			if (i != null && i.GetType() == ofType ){
				
				if (i.StackAmount > amountLeft){
					i.StackAmount -= amountLeft;
					amountLeft -= amountLeft;
				}

				else{
					i.StackAmount -= i.StackAmount;
					amountLeft -= i.StackAmount;
				}

				if( amountLeft <= 0 ){
					return;
				}
			}
		}
	}
		
	static public bool AddToInventory( InventoryItem item ){


		foreach ( InventoryItem i in _inventory ){
			if (i != null){
				if (i.GetType() == item.GetType()){
					var r = i.AddToStack( 1 );
					if ( r ){
						AlertDelegate();
						return true;
					}
				}
			}
		}


		if ( ContentsCount < _capacity ){

			var index = 0;
			foreach ( InventoryItem i in _inventory ){


				if (i == null){
					_inventory[index] = item ;
					AlertDelegate();
					break;
				}
				index++;
			}
			return true;
		}
		return false;
	}
	static public bool RemoveFromInventory( InventoryItem item ){

		var index = 0;
		foreach ( InventoryItem i in _inventory ){
			if (i == item){
				_inventory[index] = null;
				AlertDelegate();
				return true;
			}
			index++;
		}

		return false;
	}
	static public bool RemoveFromEquipment( InventoryItem item ){
		var index = 0;
		foreach ( InventoryItem i in _equipment ){
			if (i == item){
				_equipment[index] = null;
				AlertDelegate();
				return true;
			}
			index++;
		}
		return false;
	}
	//
	static public InventoryItem GetItemInInventorySlot( int slot ){
		return _inventory[ slot ];
	}
	static public InventoryItem GetItemInEquipmentSlot( int slot ){
		return _equipment[ slot ];
	}
	//
	static public void SwapItemsInInventory( int fromSlot, int toSlot ){

		var firstSlot  = GetItemInInventorySlot( fromSlot );
		var secondSlot = GetItemInInventorySlot( toSlot );

		_inventory[toSlot]   = firstSlot;
		_inventory[fromSlot] = secondSlot;

		AlertDelegate();
	}
	static public void EquipItem( int fromInventorySlot, int toEquipSlot  ){

		var t = _equipment[ toEquipSlot ];
		var f = _inventory[ fromInventorySlot ];

		_equipment[toEquipSlot] = f;
		_inventory[fromInventorySlot] = t;

		AlertDelegate();
	}
	static public void UnequipItem( int fromEquipSlot, int toInventorySlot  ){
		
		var f = _equipment[ fromEquipSlot ];
		var t = _inventory[ toInventorySlot ];

		_equipment[fromEquipSlot] = t;
		_inventory[toInventorySlot] = f;

		AlertDelegate();
	}


	// PRIVATE METHODS
	static private void AlertDelegate(){
		foreach( IInventory i in _delegates ){
			i.InventoryChanged();
		}
	}
}


public interface IInventory{
	void InventoryChanged();
}