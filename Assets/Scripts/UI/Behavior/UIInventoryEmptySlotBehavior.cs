using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIInventoryEmptySlotBehavior : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

	// PROPERTIES
	public string Index{
		get{
			return _index;
		}
		set{
			_index = value;
		}
	}
	private string _index = "-1";

	public IInventoryEmptySlot Delegate{
		set{
			_delegate = value;
		}
		get{
			return _delegate;
		}
	}
	private IInventoryEmptySlot _delegate;

	public bool Hovering{
		get{
			return _hovering;
		}
	}
	private bool _hovering;
 

	// POINTER DELEGATES
	public void OnPointerEnter(PointerEventData eventData){
		_hovering = true;
		_delegate.HoverStarted( _index );
	}
	public void OnPointerExit(PointerEventData eventData){
		_hovering = false;
		_delegate.HoverEnded( _index );
	}
}

public interface IInventoryEmptySlot{
	void HoverStarted( string index );
	void HoverEnded( string index );
}



