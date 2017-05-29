using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemSlotFill : MonoBehaviour {

	private const float _maxMoveTime = 0.2f;
	private const float _pps = 500;

	public Coroutine Action{
		set{

			if (_action != null){
				StopCoroutine( _action );
				_action = null;
			}

			_action = value;
		}
	}
	private Coroutine _action = null;

	// PROPERTIES
	public InventoryItem InventoryItem{
		set{
			_item = value;
			UpdateVisual();
		}
	}
	private InventoryItem _item;



	// INSTANCE VARIABLES
	[SerializeField] private Image _iconImage;
	[SerializeField] private Text  _countText;
	[SerializeField] private Image _countBubble;


	public IEnumerator TrackMouse(){

		GetComponent<Animator>().SetTrigger("Pressed");
		transform.SetAsLastSibling();

		var originalPos = transform.position;
		var mouseOffset = originalPos-Input.mousePosition;

		while (true){
			transform.position = Input.mousePosition+mouseOffset;
			yield return null;
		}

	}
	public IEnumerator MoveToPos( Vector3 targetPos ){

		var startPos = transform.position;
		var time = Mathf.Clamp( Vector3.Distance( transform.position, targetPos )/_pps, 0, _maxMoveTime );

		for ( float i=0; i<time; i+=Time.deltaTime ){ 
			transform.position = Vector3.Lerp( startPos, targetPos, i/time);	 
			yield return null;
		}
			
		transform.position = targetPos;
		GetComponent<Animator>().SetTrigger("Normal");

		Action = null;
	}

	// PRIVATE METHODS
	private void UpdateVisual(){
		
		_iconImage.sprite = _item.Sprite;

		var count = _item.StackAmount;
		if (count > 1){
			_countText.text	= _item.StackAmount.ToString();
			_countBubble.gameObject.SetActive(true);
		}
		else{
			_countText.text = "";
			_countBubble.gameObject.SetActive(false);
		}
	}
		
}