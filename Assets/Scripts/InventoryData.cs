using System.Collections.Generic;
using UnityEngine;

public class InventoryData {

	private Dictionary<string,InventorySlot> _dataSource;
	private Dictionary<char,int> 			 _buildData;
	private List<IInventory2>				 _listeners;


	public InventoryData(){
		_dataSource = new Dictionary<string,InventorySlot>();
		_buildData  = new Dictionary<char,int>();
		_listeners  = new List<IInventory2>();
	}
	public void AddToDataSource( char prefix, int slots){ 
		AddToDataSource( prefix, slots, new List<InventoryItemTag>() );
	}
	public void AddToDataSource( char prefix, int slots, List<InventoryItemTag> tags ){

		_buildData.Add( prefix, slots );

		for (int i=0; i<slots; i++){

			var key = prefix.ToString() + i;
			if (!_dataSource.ContainsKey( key )){
				
				var slot =  new InventorySlot();
				slot.Item = null;
				slot.Tags = tags;
				
				_dataSource[key] = slot;
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
	public void SwapItems( InventoryItem item1, InventoryItem item2 ){

		string fromSlot = null;
		string toSlot   = null;

		if ( CheckFor(item1) ){
			fromSlot = GetIndex( item1 );
		}
		else{
			Debug.LogWarning( ERROR.ItemNotFound );
			return;
		}

		if ( CheckFor(item2) ){
			toSlot = GetIndex( item2 );
		}
		else{ 
			Debug.LogWarning( ERROR.ItemNotFound );
			return;
		}


		_dataSource[fromSlot].Item = item2;
		_dataSource[toSlot].Item   = item1;

		AlertListeners( fromSlot );
		AlertListeners( toSlot );
	}
	public bool SwapItems( string fromSlot, string toSlot ){

		Debug.Log("Swap");
		
		InventoryItem item1 = null;
		InventoryItem item2 = null;

		if ( _dataSource.ContainsKey(fromSlot) ){
			item1 = _dataSource[fromSlot].Item;
		}
		else{
			Debug.LogWarning( ERROR.IndexDoesNotExist );
			return false;
		}

		if ( _dataSource.ContainsKey(toSlot) ){
			item2 = _dataSource[toSlot].Item;
		}
		else{ 
			Debug.LogWarning( ERROR.IndexDoesNotExist );
			return false;
		}
			

		if ( item1 != null && !CheckForTag( item1.Tag, _dataSource[toSlot]) ){
			return false;
		}
		if ( item2 != null && !CheckForTag( item2.Tag, _dataSource[fromSlot]) ){
			return false;
		}


		_dataSource[fromSlot].Item = item2;
		_dataSource[toSlot].Item   = item1;

		AlertListeners( fromSlot );
		AlertListeners( toSlot );

		return true;
	}


	// ADD
	public void AddItem( InventoryItem item  ){

		foreach( string key in _dataSource.Keys ){
			var slot = _dataSource[ key ];
			if (slot.Item != null){
				if  (slot.Item.GetType() == item.GetType() ){
					if ( slot.Item.AddToStack( 1 ) ){
						AlertListeners( key );
						return;
					}
				}
			}
		}
			
		foreach( string key in _dataSource.Keys ){
			var slot = _dataSource[ key ];
			if (slot.Item == null){
				_dataSource[key].Item = item ;
				AlertListeners( key );
				return;
			}
		}

		Debug.LogWarning(ERROR.NoRoomForItem);
		return;
	}
	public void AddItem( InventoryItem item, string at ){

		if (_dataSource.ContainsKey( at )){
			
			_dataSource[at].Item = item;
			AlertListeners( at );
			return;
		}

		Debug.LogWarning(  ERROR.IndexDoesNotExist );
		return;
	}


	// REMOVE
	public void RemoveItem( InventoryItem item ){
		foreach ( string key in _dataSource.Keys ){
			var i  = _dataSource[key];
			if ( i.Item == item ){
				_dataSource[key].Item = null;
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
		foreach ( InventorySlot v in _dataSource.Values ){
			if ( v.Item != null ){		
				count++;
			}
		}
		return count;
	}
	public int GetContentsCount( System.Type ofType ){

		var count = 0;
		foreach ( InventorySlot v in _dataSource.Values ){
			if ( v != null && v.GetType() == ofType){
				count++;
			}
		}
		return count;
	}
	public string GetIndex( InventoryItem atItem ){

		foreach( string key in _dataSource.Keys ){
			var v = _dataSource[key];
			if (v.Item == atItem){
				return key;
			}
		}

		Debug.LogWarning( ERROR.ItemNotFound );
		return null;
	}
	public InventoryItem GetItem( string atIndex ){

		if (_dataSource.ContainsKey( atIndex )){ 
			return _dataSource[atIndex].Item;
		}
			
		Debug.LogWarning( ERROR.IndexDoesNotExist+atIndex );
		return null;
	}
	public bool CheckForTag( InventoryItemTag tag, InventorySlot inSlot  ){
		
		if ( inSlot.Tags.Count == 0 || inSlot.Tags.Contains( tag )){
			return true;
		}

		return false;
	}


	public bool CheckForPrefix( char prefix ){ 
		return _buildData.ContainsKey(prefix);
	}
	public bool CheckFor( InventoryItem item ){

		foreach ( InventorySlot s in _dataSource.Values ){
			if (s.Item == item){
				return true;
			}
		}
		return false;
	}
	public bool CheckFor( string index ){
		return _dataSource.ContainsKey( index );
	}

	// PRIVATE
	private void AlertListeners( string index ){
		foreach( IInventory2 l in _listeners ){
			l.DataSourceChanged( index );
		}
	}

	// STATIC 
	static public void DistributeItems( string selectionIndex, string hoverIndex, InventoryData data1, InventoryData data2 ){

		var selectionInData1 = data1.CheckFor( selectionIndex );
		var selectionInData2 = data2.CheckFor( selectionIndex );
		var hoverInData1 	 = data1.CheckFor( hoverIndex );
		var hoverInData2 	 = data2.CheckFor( hoverIndex );


		if ( selectionInData1 && selectionInData2 ){ 
			Debug.LogWarning( ERROR.BothItemsInData );
			return;
		}

		if ( hoverInData1 && hoverInData2 ){ 
			Debug.LogWarning( ERROR.BothItemsInData );
			return;
		}

		if ( !selectionInData1 && !selectionInData2 ){ 
			Debug.LogWarning( ERROR.NoItemsInData );
			return;
		}

		if ( !hoverInData1 && !hoverInData2 ){ 
			Debug.LogWarning( ERROR.NoItemsInData );
			return;
		}


		// Swap
		if ( selectionInData1 && selectionInData2 ){  
			data1.SwapItems( selectionIndex, hoverIndex );
			return;
		}
		if ( hoverInData1 && hoverInData2 ){  
			data2.SwapItems( selectionIndex, hoverIndex );
			return;
		}

		// Move
		if ( selectionInData1 && hoverInData2 ){  
			var selection = data1.GetItem(selectionIndex);
			var hover  	  = data2.GetItem(hoverIndex);

			// Hover
			data2.AddItem( selection, hoverIndex);

			// Select
			data1.AddItem( hover, selectionIndex);

			data1.RemoveItem( selection );
			data2.RemoveItem( hover );
			return;
		}
		if ( hoverInData1 && selectionInData2 ){  

			var selection = data2.GetItem(selectionIndex);
			var hover  	  = data1.GetItem(hoverIndex);

			data1.RemoveItem( hover );
			data2.RemoveItem( selection );

			// Hover
			data2.AddItem( hover, selectionIndex);

			// Select
			data1.AddItem( selection, hoverIndex);

			data1.AlertListeners( hoverIndex );
			data2.AlertListeners( selectionIndex );
			return;
		}
	}


	// ERRORS
	private struct ERROR {
		static public string KeyAlreadyInDataSource   = "Key has already been added to the data source;";
		static public string IndexDoesNotExist 		  = "Trying to access an index that does not exist: ";
		static public string OverwrittingPreviousItem = "Trying to place and object in a slot with a previous item; The previous item was overwrittern";
		static public string ItemNotFound			  = "The item you are looking for was never found";
		static public string ListenerNotFound		  = "Listener not found";
		static public string ListenerAlreadyAdded	  = "Listener already added";
		static public string NoRoomForItem			  = "No room for item;";
		static public string BothItemsInData		  = "Both data sources hold a refrence to the given item";
		static public string NoItemsInData			  = "The data sources dont hold any refrence to the given item";
	}
}


public interface IInventory2{
	void DataSourceChanged( string atIndex );
}
public class InventorySlot{
	
	public InventoryItem Item{
		get; set;
	}
	public List<InventoryItemTag> Tags{
		get; set;
	}

}

public enum InventoryItemTag{
	None,
	Bullet
}
