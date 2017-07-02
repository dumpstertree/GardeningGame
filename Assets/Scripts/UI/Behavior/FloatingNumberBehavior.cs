using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingNumberBehavior : MonoBehaviour {

	[SerializeField] private Text  _text;
	[SerializeField] private float _floatHeight;
	[SerializeField] private float _floatTime;
	[SerializeField] private float _startOffset;
	[SerializeField] private float _horizontalRandom;
	[SerializeField] private Color _positiveColor; 
	[SerializeField] private Color _negativeColor; 

	private Vector3 _startPos;
	private Vector3 _endPos;
	private float _time;

	public void Startup( Transform t, int number, bool positive ){
		transform.SetParent( GameObject.FindGameObjectWithTag("WorldSpaceCanvas").transform, false );
		transform.LookAt(Camera.main.transform);
		transform.position = new Vector3( t.position.x +Random.Range(-_horizontalRandom,_horizontalRandom), t.position.y+_startOffset, t.position.z);
		_text.text = number.ToString();
		if (positive){
			_text.color = _positiveColor;
		}
		else{
			_text.color = _negativeColor;
		}

		_time  = 0;
		_startPos = transform.position;
		_endPos =  new Vector3( transform.position.x, transform.position.y + _startOffset + _floatHeight, transform.position.z );
	}
	private void Update(){

		transform.LookAt(Camera.main.transform);
		transform.Rotate( 0,180,0 );

		_time += Time.deltaTime;
		transform.position = Vector3.Lerp( _startPos, _endPos, _time/_floatTime );

		if( _time > _floatTime ){
			Destroy( gameObject );
		}
	}
}
