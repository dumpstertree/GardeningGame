using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIInventoryController : MonoBehaviour, IInventory, IInventoryEmptySlot {

	// CONSTANTS
	private const int _equipIndexStart = 100;

	// INSTANCE VARIABLES
	[SerializeField] private Transform _inventorySlotContainer;
	[SerializeField] private Transform _equipSlotContainer;

	private UIInventoryEmptySlotBehavior[]  _inventorySlots;
	private UIInventoryEmptySlotBehavior[]  _equipSlots;
	private int _curHover 	  = -1;
	private int _curSelection = -1;


	// PRIVATE METHODS
	private void Awake(){
		_inventorySlots = _inventorySlotContainer.GetComponentsInChildren<UIInventoryEmptySlotBehavior>();
		_equipSlots     = _equipSlotContainer.GetComponentsInChildren<UIInventoryEmptySlotBehavior>();
	}
	private void Start(){

		var index = _equipIndexStart;
		foreach( UIInventoryEmptySlotBehavior slot in _equipSlots ){
			slot.Index = index;
			slot.Delegate = this;
			index++;
		}

		index = 0;
		foreach( UIInventoryEmptySlotBehavior slot in _inventorySlots ){
			slot.Index = index;
			slot.Delegate = this;
			index++;
		}

		// Testing //
		Inventory.Delegate = this;
		Inventory.AddToInventory( new MapleTreeSeeds());
		Inventory.AddToInventory( new MapleTreeSeeds());
		Inventory.SwapItemsInInventory( 0, 10);
		//
	}
	private void OnEnable(){
		Reload();
	}
	private void Reload(){

		var index = 0;
		foreach ( UIInventoryEmptySlotBehavior i in _inventorySlots ){
			var item = Inventory.GetItemInInventorySlot( index );
			i.InventoryItem = item;
			index++;
		}

		index = 0;
		foreach ( UIInventoryEmptySlotBehavior i in _equipSlots ){
			var item = Inventory.GetItemInEquipmentSlot( index );
			i.InventoryItem = item;
			index++;
		}
	}
	private void SwapItems(){

		if (_curSelection >= _equipIndexStart || _curHover >= _equipIndexStart){

			if (_curSelection >= _equipIndexStart){
				Inventory.UnequipItem( _curSelection-_equipIndexStart, _curHover );
			}

			if (_curHover >= _equipIndexStart){
				Inventory.EquipItem( _curSelection, _curHover-_equipIndexStart );
			}
		}
		else{
			Inventory.SwapItemsInInventory( _curSelection, _curHover );
		}
	}	
		
	// INVENTORY DELEGATES
	public void InventoryChanged(){
		Reload();
	}


	// EMPTY SLOT DELEGATES
	public void SelectionStarted(){
		_curSelection = _curHover;
	}
	public void SelectionEnded(){
		
		if (_curHover != _curSelection && _curHover != -1 && _curSelection != -1){
			SwapItems();
		}
		else{
			
		}
		_curSelection = -1;
	}
	public void HoverStarted( int index ){
		_curHover = index;
	}
	public void HoverEnded( int index ){
		if (_curHover == index){
			_curHover = -1;
		}
	}
}
