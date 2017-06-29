using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractorController : MonoBehaviour, IInventory2 {


	// PROPERTIES
	public int InteractorSlot{
		set{
			if (value != _interactorSlot){
				Interactor = Inventory.Data.GetItem( Constants.HotkeyIndexStart.ToString() + value);
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
	}
	private void Start(){
		AlertDelegatesInteractorChange();
		AlertDelegatesSlotChanged();
		Inventory.Data.StopListening( this );
	}
	private void Update(){
		
		if ( Input.GetKey(KeyCode.I) ){
			InteractorSlot = 0;
		}

		else if ( Input.GetKey(KeyCode.J) ){
			InteractorSlot = 2;
		}

		else if ( Input.GetKey(KeyCode.L) ){
			InteractorSlot = 3;
		}

		else if ( Input.GetKey(KeyCode.K) ){
			InteractorSlot = 1;
		}
		else{
			InteractorSlot = Constants.HotkeySlots-1;
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
	public void DataSourceChanged( string key ){
		if (_interactorSlot != Constants.HotkeySlots){
			Interactor = Inventory.Data.GetItem( Constants.HotkeyIndexStart.ToString() + _interactorSlot);
		}
		else{
			Interactor = null;
		}
	}
}

// DELEGATE INTERFACE
public interface IInteractor{
	void InteractorChanged( InventoryItem interactor );
	void InteractorSlotChanged( int slotNumber );
}	

