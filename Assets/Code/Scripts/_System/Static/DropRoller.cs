using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropRoller {

	static public List<InventoryItem> Roll( List<DropItem> dropList ){

		var itemList = new List<InventoryItem>();
		var roll = Random.Range(0.0f,1f);
		var maxSuccess = 0.0f;

		foreach (DropItem i in dropList){
			maxSuccess += i.SpawnRate;
		}

		if (roll <= maxSuccess){

			// Get Dropped Item
			var r = 0.0f;
			DropItem droppedItem = new DropItem(null,0,0,0) ;
			foreach (DropItem i in dropList){
				r += i.SpawnRate;
				if ( roll <= r ){
					droppedItem = i;
					break;
				}
			}

			// Get Quantity
			var quantityRand = Random.Range(0.0f,1f);
			var quantityCurve = quantityRand*quantityRand;
			var itemDelta = droppedItem.MaxDropQuantity-droppedItem.MinDropQuantity;
			var q = droppedItem.MinDropQuantity + Mathf.Floor( quantityCurve*itemDelta ) ;

			// Return
			for ( int i=0; i<q; i++  ){
				itemList.Add( droppedItem.Item );
			}
		}
			
		return itemList;
	}
}
	
public struct DropItem{

	public InventoryItem Item{
		get{
			return _item;
		}
	}
	public float SpawnRate{
		get{
			return _spawnRate;
		}
	}
	public float MinDropQuantity{
		get{
			return _minDropQuantity;
		}
	}
	public float MaxDropQuantity{
		get{
			return _maxDropQuantity;
		}
	}

	private InventoryItem _item;
	private float 	_spawnRate;
	private int 	_minDropQuantity;
	private int 	_maxDropQuantity;

	public DropItem( InventoryItem item, float spawnRate, int minDropQuantity, int maxDropQuantity ){
		_item = item;
		_spawnRate = spawnRate;
		_minDropQuantity = minDropQuantity;
		_maxDropQuantity = maxDropQuantity;
	}
}