using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpObject : MonoBehaviour {

	[SerializeField] private LerpType  _lerpType;
	[SerializeField] private Transform _lerpTarget; 
	[SerializeField] private float 	   _deadZone  = 0.01f;
	[SerializeField] private float	   _lerpSpeed = 0.50f;


	void Update () {

		switch( _lerpType ){ 
		case LerpType.Position:
			LerpPosition();
			break;
		case LerpType.Rotation:
			LerpRotation();
			break;
		}
	}

	private void LerpPosition(){
		var d = Vector3.Distance( _lerpTarget.position, transform.position );
		if (d > _deadZone){
			transform.position = Vector3.Lerp( transform.position, _lerpTarget.position, _lerpSpeed );
		}
	}
	private void LerpRotation(){

		if ( Mathf.Abs(transform.rotation.eulerAngles.y - _lerpTarget.rotation.eulerAngles.y) >  _deadZone){
			var y = Vector3.Slerp(transform.rotation.eulerAngles,_lerpTarget.rotation.eulerAngles,_lerpSpeed);
			//Mathf.( transform.rotation.eulerAngles.y, _lerpTarget.rotation.eulerAngles.y, _lerpSpeed );
			transform.rotation = Quaternion.Euler( new Vector3(0,y.y,0) );
		}
	}


	void OnDrawGizmos() {
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, 1.0f);
	}

	private enum LerpType{
		Position,
		Rotation
	}
}

