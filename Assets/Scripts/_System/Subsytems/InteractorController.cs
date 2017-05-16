using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractorController : MonoBehaviour, IInventory {


	// PROPERTIES
	public int InteractorSlot{
		set{
			if (value != _interactorSlot){
				Interactor = Inventory.GetItemInEquipmentSlot(value);
				_interactorSlot = value;
				AlertDelegatesSlotChanged();
			}
		}
	}
	private int _interactorSlot = 0;

	public InventoryItem Interactor{
		set{
			if (_interactor != value){
				_interactor = value;
				AlertDelegatesInteractorChange();
			}
		}
	}
	private InventoryItem _interactor;

	public IInteractor Delegate{
		set{
			_delegates.Add(value);
		}
	}
	private List<IInteractor> _delegates;


	// PRIVATE METHODS
	private void Awake(){
		_delegates = new List<IInteractor>();
		Inventory.Delegate = this;
	}
	private void Start(){
		AlertDelegatesInteractorChange();
		AlertDelegatesSlotChanged();
	}
	private void Update(){
		
		if ( Input.GetKeyDown(KeyCode.I) ){
			InteractorSlot = 0;
		}

		if ( Input.GetKeyDown(KeyCode.J) ){
			InteractorSlot = 2;
		}

		if ( Input.GetKeyDown(KeyCode.L) ){
			InteractorSlot = 3;
		}

		if ( Input.GetKeyDown(KeyCode.K) ){
			InteractorSlot = 1;
		}
	}
	private void AlertDelegatesInteractorChange(){
		foreach( IInteractor d in _delegates ){
			d.InteractorChanged( _interactor );
		}
	}
	private void AlertDelegatesSlotChanged(){
		foreach( IInteractor d in _delegates ){
			d.InteractorSlotChanged( _interactorSlot );
		}
	}
		

	// INVENTORY DELEGATE
	public void InventoryChanged(){
		Interactor = Inventory.GetItemInEquipmentSlot( _interactorSlot );
	}
}

// DELEGATE INTERFACE
public interface IInteractor{
	void InteractorChanged( InventoryItem interactor );
	void InteractorSlotChanged( int slotNumber );
}	

