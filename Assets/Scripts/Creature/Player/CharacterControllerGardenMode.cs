using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerGardenMode : MonoBehaviour {

	[SerializeField] private float _minSpeed;
	[SerializeField] private float _maxSpeed;
	[SerializeField] private float _followDistance;
	[SerializeField] private float _lerpSpeed;
	[SerializeField] private Transform _cameraTarget; 
	[SerializeField] private Transform _cameraLerpObject;
	[SerializeField] private Transform _camera;


	public Transform mesh;
	private Animator _animator;
	private float _horizontal;
	private float _vertical;
	private float _curSpeed;
	public bool  _lerping;


	private void Awake(){
		_animator = GetComponentInChildren<Animator>();
	}
	private void Update() {

		_horizontal = Input.GetAxis("Horizontal");
		_vertical   = Input.GetAxis("Vertical");

		NormalView();

		CheckForLerp();
		if (_lerping){
			LerpLerpNode();
		}
	}

	private void NormalView(){

		if (_horizontal != 0 || _vertical != 0){

			// ANIMATIONS
			_animator.SetFloat( "Horizontal", 0 );
			_animator.SetFloat( "Vertical", _curSpeed/_maxSpeed );

			//MOVEMENT
			GetCurSpeed();
			Rotate();
			MoveBodyBasedOnInput();
		}

		else{
			// ANIMATIONS
			_animator.SetFloat( "Horizontal", 0 );
			_animator.SetFloat( "Vertical", 0 );

			// MOVEMENT
			_curSpeed = _minSpeed;
		}

		// CAMERA LERP
		_camera.position = Vector3.Lerp( _camera.position, _cameraLerpObject.position, _lerpSpeed/2);
		LookAtPlayer();
	}
	private void GetCurSpeed(){
		_curSpeed = Mathf.Lerp(_curSpeed, _maxSpeed, .3f);
	}
	private void MoveBodyBasedOnInput(){
		mesh.Translate( new Vector3(0,0,1) * (_curSpeed*Time.deltaTime) );
	}
	private void Rotate(){

		var rads = Mathf.Atan2(_vertical,_horizontal);
		var degrees = rads * Mathf.Rad2Deg;
		var adjusted = (degrees-90) * -1;
		var y = _camera.eulerAngles.y + adjusted;

		mesh.rotation = Quaternion.Euler( new Vector3( 0, y, 0 ) );
	}
	private void CheckForLerp(){
		var d = Vector3.Distance( _camera.position, mesh.position );

		if ( d > _followDistance ){
			_lerping = true;
		}
		else if ( d <= _followDistance && _lerping && _horizontal+_vertical != 0 ){
			_lerping = true;
		}
		else{
			_lerping = false;
		}
	}
	private void LerpLerpNode(){
		_cameraLerpObject.position = Vector3.Lerp( _cameraLerpObject.position, _cameraTarget.position, _lerpSpeed);
	}
	private void LookAtPlayer(){

		var s = _camera.rotation;
		_camera.LookAt(mesh);
		var t = _camera.rotation;

		_camera.rotation = Quaternion.Slerp( s, t, 0.5f );
	}


	void OnDrawGizmos() {
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(_cameraLerpObject.position, 1.0f);
		Gizmos.DrawWireSphere(_cameraTarget.position, 1.0f);
		Gizmos.DrawWireSphere(_camera.position, 1.0f);
	}

	public void MoveBodyBasedOnInput( float h, float v ){
		
		
	}
	public void Jump(){
	
	}
}
