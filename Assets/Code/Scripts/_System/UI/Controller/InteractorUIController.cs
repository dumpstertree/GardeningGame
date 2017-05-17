using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractorUIController : MonoBehaviour, IInventory, IInteractor {


	[SerializeField] private List<Image> _slots;
	[SerializeField] private InteractorTargetUI _interactorTarget;


	// PRIVATE METHODS
	private void Start(){
		Inventory.Delegate = this;
		Game.Controller.Interactor.Delegate = this;
	}
	private void OnEnable(){
		Reload();
	}
	private void Reload(){
		for ( int i=0; i<_slots.Count; i++ ){
			if (Inventory.GetItemInEquipmentSlot(i) != null){
				_slots[i].enabled = true;
				var e = Inventory.GetItemInEquipmentSlot(i);
				_slots[i].sprite = e.Sprite;
			}
			else{
				_slots[i].enabled = false;
			}
		}
	}


	// INVENTORY DELEGATES
	public void InventoryChanged(){
		Reload();
	}
		
	public void InteractorChanged( InventoryItem interactor ){
	}
	public void InteractorSlotChanged( int slotNumber ){
		_interactorTarget.ChangeTarget( _slots[slotNumber].transform );
	}
}
