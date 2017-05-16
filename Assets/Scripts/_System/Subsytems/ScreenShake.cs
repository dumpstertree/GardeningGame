using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShake : MonoBehaviour {

	private float _maxOffset = 0.5f;
	private float _maxTime   = 1.0f;

	private float _maxCurTime   = 0.0f;
	private float _maxCurOffset = 0.0f;
	private float _curTime	    = 0.0f;


	public void Shake( float offsetPercent, float timePrecent ){
		_maxCurTime   = _maxTime*timePrecent;
		_maxCurOffset = _maxOffset*offsetPercent;
		_curTime 	  = _maxCurTime;
	}

	public void ShakeLight(){
		Shake(0.25f,0.25f);
	}

	public void ShakeMedium(){
		Shake(0.5f,0.5f);
	}

	public void ShakeHeavy(){
		Shake(1f,1f);
	}
		
	private void Update(){

		if (_curTime > 0){

			var frac  = _curTime/_maxCurTime;
			var offset = frac*_maxCurOffset;
			var x = Random.Range( -offset, offset );
			var y = Random.Range( -offset, offset );

			Camera.main.transform.localPosition = new Vector3( x, y, 0 );

			_curTime -= Time.deltaTime;

			if (_curTime < 0){
				Camera.main.transform.localPosition = Vector3.zero;
			}

		}
	}
}
