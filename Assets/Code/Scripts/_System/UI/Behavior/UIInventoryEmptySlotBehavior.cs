using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIInventoryEmptySlotBehavior : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IInputMouseButtonDown, IInputMouseButtonUp {

	// PROPERTIES
	public InventoryItem InventoryItem{
		set{
			_item = value;
			Set();
		}
	}
	private InventoryItem _item;

	public int Index{
		get{
			return _index;
		}
		set{
			_index = value;
		}
	}
	private int _index = -1;

	public IInventoryEmptySlot Delegate{
		set{
			_delegate = value;
		}
		get{
			return _delegate;
		}
	}
	private IInventoryEmptySlot _delegate;


	// INSTANCE VARIABLES
	[SerializeField] private GameObject _inventorySlotItemPrefab;

	private  UIInventoryFilledSlotBehavior _filledSlotItem;
	private bool _mouseDown;
	private bool _hovering;
	private bool _selected;


	// PRIVATE METHODS
	private void Start(){
		FindObjectOfType<InputManager>().MouseDownDelegate = this;
		FindObjectOfType<InputManager>().MouseUpDelegate = this;
	}
	private void Set(){

		if (_filledSlotItem){
			Destroy(_filledSlotItem.gameObject);
		}

		if (_item != null){
			_filledSlotItem = Instantiate( _inventorySlotItemPrefab ).GetComponent<UIInventoryFilledSlotBehavior>();
			_filledSlotItem.transform.SetParent( transform, false );
			_filledSlotItem.InventoryItem = _item;
		}
	}
	private void IsSelected(){

		if ( _filledSlotItem ){

			if (_selected && _mouseDown ){
				return;
			}

			var s = false;
			if (_hovering && _mouseDown){
				s = true;
			}

			if (s != _selected){

				if (_hovering && _mouseDown){
					_selected = true;
					_filledSlotItem.Selected = true;
					_delegate.SelectionStarted();
				}
				else{
					_selected = false;
					_filledSlotItem.Selected = false;
					_delegate.SelectionEnded();
				}

			}
		}
	}


	// INPUT DELEGATES
	public void MouseButtonDown(){
		if (_hovering){
			_mouseDown = true;
			IsSelected();
		}
	}
	public void MouseButtonUp(){
		_mouseDown = false;
		IsSelected();
	}


	// POINTER DELEGATES
	public void OnPointerEnter(PointerEventData eventData){
		_hovering = true;
		IsSelected();
		_delegate.HoverStarted( _index );
	}
	public void OnPointerExit(PointerEventData eventData){
		_hovering = false;
		IsSelected();
		_delegate.HoverEnded( _index );
	}
}

public interface IInventoryEmptySlot{
	void SelectionStarted();
	void SelectionEnded();
	void HoverStarted( int index );
	void HoverEnded( int index );
}



