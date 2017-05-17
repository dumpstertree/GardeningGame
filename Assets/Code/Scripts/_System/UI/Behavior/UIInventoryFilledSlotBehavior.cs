using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIInventoryFilledSlotBehavior : MonoBehaviour {

	[SerializeField] private  Image _iconImage;


	// PROPERTIES
	public InventoryItem InventoryItem{
		set{
			_item = value;
			UpdateVisual();
		}
	}
	private InventoryItem _item;

	public bool Selected{
		set{
			if (value != _selected){
				_selected = value;
				_rectTransform.anchoredPosition = _startPos;
			}
		}
	}
	private bool _selected = false;


	// INSTANCE VARIABLES
	private RectTransform _rectTransform;
	private Vector3 _startPos;


	// PRIVATE METHODS
	private void Awake(){
		_rectTransform = GetComponent<RectTransform>();
		_startPos = _rectTransform.anchoredPosition;
	}
	private void UpdateVisual(){
		_iconImage.sprite = _item.Sprite;
	}
	private void Update(){
		if (_selected){
			_rectTransform.position = Input.mousePosition;
		}
	}
}
