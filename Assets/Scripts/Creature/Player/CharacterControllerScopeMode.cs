using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControllerScopeMode : MonoBehaviour, ILockOn, CharacterController {

	public GameObject Gun;

	// PROPERTIES
	public float MouseHorizontal{
		get {
			return _mouseVertical;
		}
	}
	public float MouseVertical{
		get {
			return _mouseHorizontal;
		}
	}
	private float _mouseVertical;
	private float _mouseHorizontal;

	public float BodyHorizontal{
		get{
			return _bodyHorizontal;
		}
	}
	public float BodyVertical{
		get{
			return _bodyVertical;
		}
	}
	private float _bodyHorizontal = 0.0f;
	private float _bodyVertical = 0.0f;

	public Creature Target{
		get{
			return _target;
		}
	}
	private Creature _target;


	// INSTANCE VARIABLES
	[Header("Refrence")]
	[SerializeField] private Transform _mesh;
	[SerializeField] private Transform _camera;
	[SerializeField] private Transform _shoulderCamTarget;

	[Header("Movement Speed")]
	[SerializeField] private float _minMoveSpeed;
	[SerializeField] private float _maxMoveSpeed;
	[SerializeField] private float _fpsCamRotationSpeed = 10;

	[Header("Jump")]
	[SerializeField] private float _jumpStrength = 5.0f;

	[Header("Camera")]
	[SerializeField] private float _minCameraXOffset = 0.0f;
	[SerializeField] private float _maxCameraXOffset = 0.6f;

	[SerializeField] private float _minCameraYOffset = 0.0f;
	[SerializeField] private float _maxCameraYOffset = 0.0f;

	[SerializeField] private float _minCameraZOffset = 0.0f;
	[SerializeField] private float _maxCameraZOffset = 0.0f;

	[SerializeField] private float _camLerpSpeed = 0.5f;
	[SerializeField] private float _shoulderSwitchLerpSpeed = 0.3f;

	private CustomCharacterController _charController;
	private float _curSpeed;
	private bool _locked;

	private AimAssist AimAssist;


	// MONOBEHAVIOR
	private void Awake(){
		_charController = GetComponent<CustomCharacterController>();
		AimAssist = GetComponent<AimAssist>();
	}
	private void Start(){	
		FindObjectOfType<LockOnController>().Delegate = this;
	}
	private void OnEnable(){
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Confined;
	}
	private void OnDisable(){
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}
	private void LateUpdate(){

		MoveXOffset();
		MoveZOffset();
		MoveYOffset();

		LerpCamera();

		if (_target){
			_shoulderCamTarget.rotation = AimAssist.CamRotation;
			_mesh.rotation = AimAssist.PlayerRotation;
		}
			
	}


	// CHARACTER CONTROLLER METHODS
	public void MoveBody( float h, float v ){

		_bodyHorizontal = h;
		_bodyVertical   = v;

		CalcSpeed();

		MoveBodyBasedOnInput();
		RotateBodyBasedOnInput();

		SetAnimation();
	}
	public void MoveMouse( float h, float v ){

		if (_locked){
			h = 0.0f;
			v = 0.0f;
		}

		_mouseHorizontal = h * AimAssist.ReticuleSpeedMult;
		_mouseVertical   = v * AimAssist.ReticuleSpeedMult;

		RotateShoulderCamBasedOnInput();
	}
	public void Jump(){

		var y = Mathf.Infinity;
		var deadZone = 0.1f;

		var ray = -1 * _charController.mesh.up;
		RaycastHit hit;
		if (Physics.Raycast(_charController.mesh.position, ray, out hit)){
			y =  Vector3.Distance( hit.point, _charController.mesh.transform.position);
		}

		if ( y < deadZone && y > -deadZone){
			_charController.Rigidbody.velocity = new Vector3(0,_jumpStrength,0) ;
		}
	}
	  

	// PRIVATE METHODS
	private void CalcSpeed(){

		if (_bodyHorizontal != 0 || _bodyVertical != 0){
			_curSpeed = Mathf.Lerp(_curSpeed, _maxMoveSpeed, .3f);
		}
		else{
			_curSpeed = _minMoveSpeed;
		}
	}
	private void SetAnimation(){
		
		if (_bodyHorizontal != 0 || _bodyVertical != 0){
			_charController.Animator.SetFloat( "Horizontal", _bodyHorizontal );
			_charController.Animator.SetFloat( "Vertical", _bodyVertical );
		}

		else{
			_charController.Animator.SetFloat( "Horizontal", 0 );
			_charController.Animator.SetFloat( "Vertical", 0 );
		}
	}
		
	private void MoveBodyBasedOnInput(){
		
		var x =  _mesh.position + _mesh.right   * (_curSpeed*Time.deltaTime * _bodyHorizontal);
		_charController.Rigidbody.MovePosition( x );

		var y = x + _mesh.forward * (_curSpeed*Time.deltaTime * _bodyVertical);
		_charController.Rigidbody.MovePosition( y );
	}
	private void RotateBodyBasedOnInput(){
		_mesh.Rotate( new Vector3( 0, _mouseHorizontal * _fpsCamRotationSpeed,  0 ) ); 
	}
	private void RotateShoulderCamBasedOnInput(  ){
		_shoulderCamTarget.Rotate( new Vector3(_mouseVertical * -(_fpsCamRotationSpeed/2),0,0) );
	}

	private void MoveCameraAnchor(){
	}
		
	private void MoveXOffset(){
		var m = (_bodyHorizontal >= 0) ? 1 : -1;
		var s = _shoulderCamTarget.localPosition;
		//var x = Mathf.Lerp( m * _minCameraXOffset, m * _maxCameraXOffset, );
		var d = new Vector3( _maxCameraXOffset * m , s.y , s.z);

		_shoulderCamTarget.localPosition = Vector3.Lerp( s, d, _camLerpSpeed ); 
	}
	private void MoveYOffset(){

		var rot = 20;

		var t = .5f;
		if (_charController.Rigidbody.velocity.y > 0.1f){
			t = 0;
		}
		if (_charController.Rigidbody.velocity.y < -0.1f){
			t = 1;
		}

		var s = _shoulderCamTarget.localPosition;
		var y = Mathf.Lerp( _minCameraYOffset, _maxCameraYOffset, t);
		var d = new Vector3( s.x, y, s.z);

		_shoulderCamTarget.localPosition = Vector3.Lerp( s, d, _camLerpSpeed);

		var q = Quaternion.Euler( new Vector3( Mathf.Lerp( -rot, rot, t ), _shoulderCamTarget.rotation.eulerAngles.y ,_shoulderCamTarget.rotation.eulerAngles.z) );
		_shoulderCamTarget.rotation = Quaternion.Slerp( _shoulderCamTarget.rotation, q, _camLerpSpeed);

	}
	private void MoveZOffset(){

		var v = (Mathf.Abs(_bodyHorizontal) > Mathf.Abs(_bodyVertical)) ? _bodyHorizontal : _bodyVertical;
		var s = _shoulderCamTarget.localPosition;
		var z = Mathf.Lerp( _minCameraZOffset, _maxCameraZOffset, Mathf.Abs(v));
		var d = new Vector3( s.x, s.y, z);

		_shoulderCamTarget.localPosition = Vector3.Lerp( s, d, _camLerpSpeed ); 
	}

	private void LerpCamera(){
		_camera.position = Vector3.Lerp(     _camera.position, _shoulderCamTarget.transform.position, _camLerpSpeed );
		_camera.rotation = Quaternion.Slerp( _camera.rotation, _shoulderCamTarget.transform.rotation, _camLerpSpeed );
	}
		

	// LOCKON DELEGATE
	public void TargetChanged( Creature newTarget, Creature lastTarget ){
		_target = newTarget;
	}
	public void LockOnChanged( bool locked ){
		_locked = locked;
	}
}

