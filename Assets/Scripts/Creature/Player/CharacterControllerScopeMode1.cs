using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class CharacterControllerScopeMode : MonoBehaviour {

	public GameObject Gun;

	[Header("Refrence")]
	[SerializeField] private Transform _mesh;
	[SerializeField] private Transform _camera;
	[SerializeField] private Transform _shoulderCamTarget;

	[Header("Movement Speed")]
	[SerializeField] private float _minMoveSpeed;
	[SerializeField] private float _maxMoveSpeed;
	[SerializeField] private float _fpsCamRotationSpeed = 10;

	[Header("Aim Assist")]
	[SerializeField] private float _maxAimAssistRange = 100;
	[SerializeField] private float _maxMagnetRange = 150;
	[SerializeField] private float _maxMagnetStrength = 0.3f;
	[SerializeField] private float _magentBreakStrength = 0.4f;

	[Header("Camera")]
	[SerializeField] private float _cameraHorizontalOffset = 0.6f;
	[SerializeField] private float _camLerpSpeed = 0.5f;
	[SerializeField] private float _shoulderSwitchLerpSpeed = 0.3f;

	private Animator _animator;

	private float _keyHorizontal;
	private float _keyVertical;
	private float _mouseHorizontal;
	private float _mouseVertical;
	private float _curSpeed;

	private void Awake(){
		_animator = GetComponentInChildren<Animator>();
	}
		
	private void OnEnable(){
		//Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Confined;
	}
	private void OnDisable(){
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}

	private void Update(){

		_keyHorizontal = Input.GetAxis("Horizontal");
		_keyVertical   = Input.GetAxis("Vertical");
		_mouseHorizontal = Input.GetAxis("Mouse X");
		_mouseVertical = Input.GetAxis("Mouse Y");


		SetAnimation();
		GetCurSpeed();
		Move();
		//MoveShoulderCam();

		var closest = TargetController.GetClosestTarget();
		if (closest){
			
			var d = GetDistance( closest );

			if (d < _maxAimAssistRange){
				ReticuleSlow( d );
			}

			if (d < _maxMagnetRange && Mathf.Abs(_mouseHorizontal)+Mathf.Abs(_mouseVertical) < _magentBreakStrength ){
				ReticuleMagnet( d, closest );
			}
		}

		RotateMesh();
		RotateShoulderCam();
		LerpCamera();
	}

	private void GetCurSpeed(){
		if (_keyHorizontal != 0 || _keyVertical != 0){
			_curSpeed = Mathf.Lerp(_curSpeed, _maxMoveSpeed, .3f);
		}
		else{
			_curSpeed = _minMoveSpeed;
		}
	}
	private void SetAnimation(){
		if (_keyHorizontal != 0 || _keyVertical != 0){
			_animator.SetFloat( "Horizontal", _keyHorizontal );
			_animator.SetFloat( "Vertical", _keyVertical );
		}
		else{
			_animator.SetFloat( "Horizontal", 0 );
			_animator.SetFloat( "Vertical", 0 );
		}
	}
	private void Move(){
		if (_keyHorizontal != 0 || _keyVertical != 0){
			_mesh.Translate( new Vector3(1,0,0) * (_curSpeed*Time.deltaTime * _keyHorizontal) );
			_mesh.Translate( new Vector3(0,0,1) * (_curSpeed*Time.deltaTime * _keyVertical) );
		}
	}
	private void MoveShoulderCam(){

		var mult = 1;
		if( _keyHorizontal > 0){
			mult = -1;
		}

		var v = Mathf.Lerp( _shoulderCamTarget.transform.localPosition.x, _cameraHorizontalOffset*mult, _shoulderSwitchLerpSpeed );
		_shoulderCamTarget.transform.localPosition = new Vector3( v ,_shoulderCamTarget.transform.localPosition.y,_shoulderCamTarget.transform.localPosition.z );
	}
	private void ReticuleSlow( float distance ){ 
		var speed =  Mathf.Clamp(distance/_maxAimAssistRange, 0.1f, 1.0f) * _fpsCamRotationSpeed;
		_keyHorizontal = _keyHorizontal * speed;
		_keyVertical   = _keyVertical * speed;
	}
	private void ReticuleMagnet( float distance, Transform closest ){

		// Magnet Reticule
		var magnet = _maxMagnetStrength * ( 1 - (distance/_maxMagnetRange) );
		var s = _shoulderCamTarget.rotation;
		_shoulderCamTarget.transform.LookAt( closest );
		var t = _shoulderCamTarget.rotation;
		_shoulderCamTarget.rotation = Quaternion.Slerp( s, t, magnet );

		// Loot At
		var s2 = _mesh.transform.rotation;
		_mesh.transform.LookAt( new Vector3(closest.transform.position.x, _mesh.transform.position.y, closest.transform.position.z) );
	}

	private void RotateMesh(){
		_mesh.Rotate( new Vector3( 0, _mouseHorizontal * _fpsCamRotationSpeed,  0 ) ); 
	}
	private void RotateShoulderCam(  ){
		_shoulderCamTarget.Rotate( new Vector3(_mouseVertical * -(_fpsCamRotationSpeed/2),0,0) );
	}
	private void LerpCamera(){
		_camera.position = Vector3.Lerp(     _camera.position, _shoulderCamTarget.transform.position, _camLerpSpeed );
		_camera.rotation = Quaternion.Slerp( _camera.rotation, _shoulderCamTarget.transform.rotation, _camLerpSpeed );
	}
		
	private float GetDistance( Transform closest ){ 
		return Vector3.Distance( Camera.main.WorldToScreenPoint(closest.position), new Vector3(Screen.width/2, Screen.height/2, 0) );
	}


	// LOCKON DELEGATE
	public void LockOnChanged( Creature newTarget, Creature lastTarget ){
		
	}
}

*/