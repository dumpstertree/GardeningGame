using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeldItemSwapper : MonoBehaviour, IInteractor {

	private GameObject _holdItemInstance;


	// PRIVATE METHODS
	private void Start(){
		Game.Controller.Interactor.Delegate = this;
	}


	// INTERACTOR DELEGATES
	public void InteractorChanged( InventoryItem interactor ){

		if (_holdItemInstance){
			Destroy( _holdItemInstance );
		}

		if (interactor != null){
			_holdItemInstance =  Instantiate( interactor.HoldItem );
		}
	}
	public void InteractorSlotChanged( int slotNumber ){
	
	}
}
