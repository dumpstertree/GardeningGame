using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractorTargetUI : MonoBehaviour {

	[SerializeField] private float _lerpSpeed;

	private Vector3 _targetPos;
	private bool _moving = false;

	public void ChangeTarget( Transform t ){

		if ( Vector3.Distance( t.position, transform.position ) > 1 ){
			_targetPos = t.position;
			_moving = true;
		}
	}

	void Update () {

		if (_moving){
			transform.position = Vector3.Lerp( transform.position, _targetPos, _lerpSpeed );

			if ( Vector3.Distance( _targetPos, transform.position ) <= 1 ){
				_moving = false;
			}
		}
	}
}
