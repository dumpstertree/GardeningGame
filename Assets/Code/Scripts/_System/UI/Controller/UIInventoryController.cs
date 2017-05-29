using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIInventoryController : MonoBehaviour, IInventory, IInventoryEmptySlot, IInputMouseButtonDown, IInputMouseButtonUp {

	// CONSTANTS
	private const float _maxMoveTime = 0.2f;
	private const float _pps = 500;

	// INSTANCE VARIABLES
	[SerializeField] private Transform _inventorySlotContainer;
	[SerializeField] private Transform _equipSlotContainer;
	[SerializeField] private Transform _itemVisualsContainer;

	private Dictionary<string,UIInventoryEmptySlotBehavior> _slotEmptyObjects;
	private Dictionary<string,ItemSlotFill> _slotVisuals;
	private string _curHover 	 = "-1";
	private string _curSelection = "-1";


	// MONO
	private void Awake(){
		
		_slotVisuals = new Dictionary<string,ItemSlotFill>();
		_slotEmptyObjects = new Dictionary<string,UIInventoryEmptySlotBehavior>();

		var index = 0;
		foreach( UIInventoryEmptySlotBehavior slot in _equipSlotContainer.GetComponentsInChildren<UIInventoryEmptySlotBehavior>() ){

			slot.Index = Constants.HotkeyIndexStart+index;
			slot.Delegate = this;

			_slotVisuals.Add( Constants.HotkeyIndexStart+index, null );
			_slotEmptyObjects.Add( Constants.HotkeyIndexStart+index, slot );

			index++;
		}

		index = 0;
		foreach( UIInventoryEmptySlotBehavior slot in _inventorySlotContainer.GetComponentsInChildren<UIInventoryEmptySlotBehavior>() ){

			slot.Index = Constants.InventoryIndexStart+index;
			slot.Delegate = this;

			_slotVisuals.Add( Constants.InventoryIndexStart+index, null );
			_slotEmptyObjects.Add( Constants.InventoryIndexStart+index, slot );

			index++;
		}
	}
	private void OnEnable(){
		Reload();
	}
	private void OnDisable(){
		// Remove inventory delegate
	}
	private void Start(){

		Game.Controller.InputManager.MouseDownDelegate = this;
		Game.Controller.InputManager.MouseUpDelegate   = this;
		Inventory.Delegate = this;
	}


	// PRIVATE METHODS
	private void Reload(){

		foreach ( string key in _slotEmptyObjects.Keys ){
			var item = Inventory.GetItemInSlot( key );

			if ( _slotVisuals[key]){
				Destroy( _slotVisuals[key].gameObject );
			}
				
			if ( item != null ){
				var visualGO = Instantiate( Game.Resources.Prefab.InventorySlotObject );
				var visual   = visualGO.GetComponent<ItemSlotFill>();

				visualGO.transform.SetParent( _itemVisualsContainer, false );
				visualGO.transform.position = _slotEmptyObjects[key].transform.position;
				visual.InventoryItem = item;

				_slotVisuals[key] = visual;
			}
		}
	}
	private IEnumerator MoveVisual(){

		var selectionIndex = "";
		var hoverIndex 	   = "";

		var visualS 	= _slotVisuals[ _curSelection ];
		var originalPos = visualS.transform.position;
		var mouseOffset = originalPos-Input.mousePosition;

		visualS.GetComponent<Animator>().SetTrigger("Pressed");
		visualS.transform.SetAsLastSibling();

		selectionIndex = _curSelection;

		while (_curSelection != "-1"){
			visualS.transform.position = Input.mousePosition+mouseOffset;
			yield return null;
		}

		hoverIndex = _curHover;



		var selectionS = Vector3.zero;
		var selectionT = Vector3.zero;

		if ( hoverIndex == "-1" ){
			selectionS = Input.mousePosition+mouseOffset;
			selectionT = originalPos;
		}
		else{
			selectionS = Input.mousePosition+mouseOffset;
			selectionT = _slotEmptyObjects[hoverIndex].transform.position;
		}



		var time = Mathf.Clamp( Vector3.Distance( selectionS, selectionT )/_pps, 0, _maxMoveTime );

		for ( float i=0; i<time; i+=Time.deltaTime ){ 
			visualS.transform.position = Vector3.Lerp( selectionS, selectionT, i/time);	 
			yield return null;
		}



		visualS.transform.position = selectionT;
		visualS.GetComponent<Animator>().SetTrigger("Normal");

		if( hoverIndex != "-1" ){
			Inventory.SwapItem( selectionIndex, hoverIndex );
		}
	}
		

	// INVENTORY DELEGATES
	public void InventoryChanged(){
		Reload();
	}
		

	// INPUT DELEGATES
	public void MouseButtonDown(){
		
		if (_curHover != "-1"){
			_curSelection = _curHover;
			StartCoroutine( MoveVisual() );
		}

	}
	public void MouseButtonUp(){

		if (_curSelection != "-1"){
			_curSelection = "-1";
		}
	}


	// EMPTY SLOT DELEGATES
	public void HoverStarted( string index ){
		_curHover = index;
	}
	public void HoverEnded( string index ){
		if (_curHover == index){
			_curHover = "-1";
		}
	}
}
