using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractorUI : MonoBehaviour, IInventory2, IInteractor {

	[Header( "Slot" )]
	[SerializeField] private List<Image> 		_slots;
	[SerializeField] private Transform 			_slotVisualsContainer;

	[Header( "HotkeySelection" )]
	[SerializeField] private Transform 			_hotkeySelection;
	[SerializeField] private float 				_lerpSpeed;
	[SerializeField] private float 				_deadZone;


	private ItemSlotFill[] _visuals;
	private bool _moving = false;
	private Vector3 _targetPos;


	// MONO
	private void Awake(){
		_visuals = new ItemSlotFill[_slots.Count];
	}
	private void Start(){
		Inventory.Data.StartListening( this );
		Game.Controller.Interactor.Delegate = this;
	}
	private void OnEnable(){
		Reload();
	}
	private void Update () {

		if (_moving){
		
			_hotkeySelection.position = Vector3.Lerp( _hotkeySelection.position, _targetPos, _lerpSpeed );
	
			if ( Vector3.Distance( _targetPos, _hotkeySelection.position ) <= _deadZone ){
				_moving = false;
			}
		}
	}


	// PRIVATE
	private void ChangeTarget( int slotNumber ){
		var t = _slots[slotNumber].transform;
		_targetPos = t.position;
		_moving = true;
	}
	private void Reload(){

		for ( int i=0; i<_slots.Count; i++ ){

			var forSlot = Constants.HotkeyIndexStart.ToString()+i;
			var item = Inventory.Data.GetItem( forSlot );

			if (item != null){
				CreateInstance(i);
				_visuals[i].InventoryItem = item;
			}
			else{
				DestroyInstance( i );
			}
		}
	}
	private void CreateInstance( int slot ){
		if ( !_visuals[slot] ){
			var visualGo = Instantiate( Game.Resources.Prefab.InventorySlotObject );
			var visual   = visualGo.GetComponent<ItemSlotFill>();
			var rectTran = visualGo.GetComponent<RectTransform>();

			rectTran.SetParent( _slotVisualsContainer, false );
			rectTran.position = _slots[slot].transform.position;

			_visuals[slot] = visual;
		}
	}
	private void DestroyInstance( int slot ){
		if ( _visuals[slot] ){
			Destroy( _visuals[slot].gameObject );
		}
	}


	// INVENTORY DELEGATES
	public void DataSourceChanged( string atIndex ){
		Reload();
	}
		

	// INTERACTOR DELEGATES
	public void InteractorChanged( InventoryItem interactor ){
	}
	public void InteractorSlotChanged( int slotNumber ){
		ChangeTarget( slotNumber );
	}
}
