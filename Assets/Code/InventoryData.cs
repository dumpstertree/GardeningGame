using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryData : MonoBehaviour {

	private Dictionary<string,InventoryItem> _dataSource;
	private Dictionary<char,int> 			 _buildData;
	private List<IInventory2>				 _listeners;


	public InventoryData(){
		_dataSource =  new Dictionary<string,InventoryItem>();
		_buildData  = new Dictionary<char,int>();
	}
	public void AddToDataSource( char prefix, int slots ){

		_buildData.Add( prefix, slots );

		for (int i=0; i<slots; i++){

			var key = prefix.ToString() + i;
			if (!_dataSource.ContainsKey( key )){
				_dataSource[key] = null;
				continue;
			}
	
			Debug.LogWarning( ERROR.KeyAlreadyInDataSource );
			continue;
		}
	}


	// Listeners
	public void StartListening( IInventory2 listener ){

		if ( !_listeners.Contains( listener ) ){
			_listeners.Add(listener);
			return;
		}

		Debug.LogWarning( ERROR.ListenerAlreadyAdded );
		return;
	}
	public void StopListening( IInventory2 listener ){

		if ( _listeners.Contains( listener ) ){
			_listeners.Remove(listener);
			return;
		}

		Debug.LogWarning( ERROR.ListenerNotFound );
		return;
	}


	// SWAP
	public void SwapItems( InventoryItem item1, InteractInventoryItem item2 ){

		string fromSlot = null;
		string toSlot   = null;

		if ( _dataSource.ContainsValue(item1) ){
			fromSlot = GetIndex( item1 );
		}
		else{
			Debug.LogWarning( ERROR.ItemNotFound );
			return;
		}

		if ( _dataSource.ContainsValue(item2) ){
			toSlot = GetIndex( item2 );
		}
		else{ 
			Debug.LogWarning( ERROR.ItemNotFound );
			return;
		}

		_dataSource[fromSlot] = item2;
		_dataSource[toSlot]   = item1;
	}
	public void SwapItem( string fromSlot, string toSlot ){

		InventoryItem item1 = null;
		InventoryItem item2 = null;

		if ( _dataSource.ContainsKey(fromSlot) ){
			item1 = _dataSource[fromSlot];
		}
		else{
			Debug.LogWarning( ERROR.IndexDoesNotExist );
			return;
		}

		if ( _dataSource.ContainsKey(toSlot) ){
			item2 = _dataSource[toSlot];
		}
		else{ 
			Debug.LogWarning( ERROR.IndexDoesNotExist );
			return;
		}
			
		_dataSource[fromSlot] = item2;
		_dataSource[toSlot]   = item1;
	}


	// ADD
	public void AddItem( InventoryItem item  ){

		foreach( string key in _dataSource.Keys ){
			var slot = _dataSource[ key ];
			if (slot != null){
				if  (slot.GetType() == item.GetType() ){
					if ( slot.AddToStack( 1 ) ){
						AlertListeners( key );
						return;
					}
				}
			}
		}
			
		foreach( string key in _dataSource.Keys ){
			var slot = _dataSource[ key ];
			if (slot == null){
				_dataSource[key] = item ;
				AlertListeners( key );
				return;
			}
		}

		Debug.LogWarning(ERROR.NoRoomForItem);
		return;
	}
	public void AddItem( InventoryItem item, string at ){

		if (!_dataSource.ContainsKey( at )){

			if ( _dataSource[at] == null ){
				_dataSource[at] = item;
				AlertListeners( at );
				return;
			}

			Debug.LogWarning(  ERROR.OverwrittingPreviousItem );
			return;
		}

		Debug.LogWarning(  ERROR.IndexDoesNotExist );
		return;
	}


	// REMOVE
	public void RemoveItem( InventoryItem item ){

		foreach ( string key in _dataSource.Keys ){
			var i  = _dataSource[key];
			if ( i == item ){
				_dataSource[key] = null;
				AlertListeners( key );
				return;
			}
		}

		Debug.LogWarning( ERROR.ItemNotFound );
		return;
	}
	public void RemoveItem( string at ){

		if ( _dataSource.ContainsKey(at) ){
			_dataSource[at] = null;
			AlertListeners( at );
			return;
		}

		Debug.LogWarning(  ERROR.IndexDoesNotExist );
		return;
	}


	// GET
	public int GetContentsCount(){

		var count = 0;
		foreach ( InventoryItem value in _dataSource.Values ){
			if ( value != null ){		
				count++;
			}
		}
		return count;
	}
	public int GetContentsCount( System.Type ofType ){

		var count = 0;
		foreach ( InventoryItem value in _dataSource.Values ){
			if ( value.GetType() ==  ofType){
				count++;
			}
		}
		return count;
	}
	public string GetIndex( InventoryItem atItem ){

		foreach( string key in _dataSource.Keys ){
			var value = _dataSource[key];
			if (value == atItem){
				return key;
			}
		}

		Debug.LogWarning( ERROR.ItemNotFound );
		return null;
	}
	public InventoryItem GetItem( string atIndex ){

		if (!_dataSource.ContainsKey( atIndex )){ 
			return _dataSource[atIndex];
		}

		Debug.LogWarning( ERROR.IndexDoesNotExist );
		return null;
	}


	// PRIVATE
	private void AlertListeners( string index ){
		foreach( IInventory2 l in _listeners ){
			l.DataSourceChanged( index );
		}
	}


	// ERRORS
	private struct ERROR {
		static public string KeyAlreadyInDataSource   = "Key has already been added to the data source;";
		static public string IndexDoesNotExist 		  = "Trying to access an index that does not exist;";
		static public string OverwrittingPreviousItem = "Trying to place and object in a slot with a previous item; The previous item was overwrittern";
		static public string ItemNotFound			  = "The item you are looking for was never found";
		static public string ListenerNotFound		  = "Listener not found";
		static public string ListenerAlreadyAdded	  = "Listener already added";
		static public string NoRoomForItem			  = "No room for item;";
	}
}

public interface IInventory2{
	void DataSourceChanged( string atIndex );
}
