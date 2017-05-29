using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory: MonoBehaviour {


	// PROPERTIES
	static public  IInventory Delegate {
		set{
			_delegates.Add( value );
		}
	}
	static private List<IInventory> _delegates = new List<IInventory>();

	static private Dictionary<string,InventoryItem> InventoryDic{
		get{
			if (_inventoryDic == null){
				Setup();
			}

			return _inventoryDic;
		}
		set{
			_inventoryDic = value;
		}
	}
	static private Dictionary<string,InventoryItem> _inventoryDic;

	static public int ContentsCount{
		get{

			var count = 0;
			for ( int i=0; i<Constants.InventorySlots; i++ ){

				var slot = _inventoryDic[ Constants.InventoryIndexStart+i ];
				if (slot != null){
					count++;
				}

			}
			return count;
		}
	}

	static public void Setup(){

		_inventoryDic = new Dictionary<string,InventoryItem>();

		for( int i=0; i<Constants.InventorySlots; i++ ){
			_inventoryDic.Add( Constants.InventoryIndexStart+i, null);
		}
		for( int i=0; i<Constants.HotkeySlots; i++ ){
			_inventoryDic.Add( Constants.HotkeyIndexStart+i, null);
		}
		for( int i=0; i<Constants.EquipSlots; i++ ){
			_inventoryDic.Add( Constants.EquipIndexStart+i, null);
		}

	}

	static public void SwapItem( string fromSlot, string toSlot ){

		var firstSlot  = InventoryDic[ fromSlot ];
		var secondSlot = InventoryDic[ toSlot ];

		InventoryDic[ toSlot ]   = firstSlot;
		InventoryDic[ fromSlot ] = secondSlot;

		AlertDelegate();
	}

	static public bool AddItem( InventoryItem item  ){

		for ( int i=0; i<Constants.InventorySlots; i++ ){

			var slot = InventoryDic[ Constants.InventoryIndexStart+i ];
			if (slot != null){
				if (slot.GetType() == item.GetType()){
					if ( slot.AddToStack( 1 ) ){
						AlertDelegate();
						return true;
					}
				}
			}
		}
			
		if ( ContentsCount < Constants.InventorySlots ){

			for ( int i=0; i<Constants.InventorySlots; i++ ){
				var slot = InventoryDic[ Constants.InventoryIndexStart+i ];

				if (slot == null){
					InventoryDic[ Constants.InventoryIndexStart+i ] = item ;
					AlertDelegate();
					return true;
				}
			}
		}

		return false;

	}
	static public void AddItem( InventoryItem item, string at ){
		_inventoryDic[ at ] = item;
		AlertDelegate();
	}

	static public bool RemoveItem( InventoryItem item ){

		foreach ( string key in InventoryDic.Keys ){

			if (InventoryDic[key] == item){
				InventoryDic[key] = null;
				AlertDelegate();
				return true;
			}
		}

		return false;

	}
	static public bool RemoveItem( string slot ){

		if ( InventoryDic.ContainsKey(slot) ){
			InventoryDic[slot] = null;
			return true;
		}

		return false;
		
	}
	static public bool RemoveItem( System.Type type ){
		return true;
	}


	static public InventoryItem GetItemInSlot( string slot ){
		if (InventoryDic.ContainsKey(slot)){
			return _inventoryDic[ slot ];
		}
		else{
			return null;
		}
	}
		

	// PUBLIC METHODS
	static public void StackChanged( InventoryItem item ){
		AlertDelegate();
	}
	static public int  GetAmountInInventory( System.Type ofType ){

		var count = 0;
		foreach( InventoryItem i in InventoryDic.Values ){
			if (i != null && i.GetType() == ofType ){
				count += i.StackAmount;
			}
		}
			
		return count;
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