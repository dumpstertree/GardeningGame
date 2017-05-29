using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractorWorldSpaceTooltipBehavior : MonoBehaviour {

	private float _height = 1.5f;
	private Transform _transform;

	[SerializeField] private Text _text;

	public void Setup( Transform t, InventoryItem i ){
		_transform = t;
		_text.text = i.HudDescription;
	}

	private void Start(){
		transform.SetParent( GameObject.FindGameObjectWithTag("WorldSpaceCanvas").transform );
		transform.localScale = new Vector3(1,1,1);
	}

	private void Update(){
		transform.LookAt( Camera.main.transform.parent ) ;
		transform.position = new Vector3( _transform.position.x, _transform.position.y + _height, _transform.position.z);
	}

}
