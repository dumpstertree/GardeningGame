using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCharacterController : MonoBehaviour, IKeyDown, IKeyUp {

	public int X {
		get{
			return (int)Mathf.Round( mesh.position.x );
		}
	}
	public int Y {
		get{
			return (int)Mathf.Floor( mesh.position.z );
		}
	}
		
	public Transform LeftHand;
	public Transform RightHand;

	public Transform mesh;

	public Rigidbody Rigidbody;
	public Animator Animator;

	private ControllerMode Mode{
		set{
			if (_mode != value){
				switch( value ){

				case ControllerMode.Free:
					_freeController.enabled = true;
					_scopeController.enabled = false;
					break;
				
				
				case ControllerMode.Scope:
					_freeController.enabled = false;
					_scopeController.enabled = true;
					break;

				}
					
				_mode = value;
			}
		}
	}
	private ControllerMode _mode;


	// INSTANCE VARIABLES
	[SerializeField] private CharacterControllerGardenMode _freeController;
	[SerializeField] private CharacterControllerScopeMode  _scopeController;

	// PRIVATE METHODS
	private void Awake(){
		_freeController  = GetComponent<CharacterControllerGardenMode>();
		_scopeController = GetComponent<CharacterControllerScopeMode>();
	}
	private void Start(){
		Game.Controller.InputManager.KeyUpDelegate   = this;
		Game.Controller.InputManager.KeyDownDelegate = this;
	}

	private float _horizontal;
	private float _vertical;
	private float _mouseHorizontal;
	private float _mouseVertical;
	private bool  _jump;
	private bool  _run;

	private void Update(){

		GetMouseMovement();
		GetBodyMovement();

		switch( _mode ){
		case ControllerMode.Scope:

			_scopeController.MoveBody( _horizontal, _vertical);
			_scopeController.MoveMouse( _mouseHorizontal, _mouseVertical );

			if (_jump){
				_scopeController.Jump();
			}

			break;
		
		case ControllerMode.Free:
			

			break;
		}

	}
	private void LateUpdate(){
		UpdateAnimator();
	}

	private void GetMouseMovement(){
		_mouseHorizontal = Input.GetAxis("Mouse X");
		_mouseVertical   = Input.GetAxis("Mouse Y");
	}
	private void GetBodyMovement(){

		var h = Input.GetAxis("Horizontal");
		var v = Input.GetAxis("Vertical");

		_horizontal = Mathf.Lerp( _horizontal, h, 0.5f );
		_vertical   = Mathf.Lerp( _vertical,   v, 0.5f );
	}
	private void UpdateAnimator(){
		Animator.SetFloat( "YVelocity", Rigidbody.velocity.y );
	}


	// INPUT DELEGATES
	public void KeyDown( InputKey key ){
		if (key == InputKey.DownScope){
			Mode = ControllerMode.Scope;
		}

		if (key == InputKey.Jump){
			_jump = true;
		}

		if (key == InputKey.Run){
			_run = true;
		}
	}
	public void KeyUp( InputKey key ){
		if (key ==  InputKey.DownScope){
			Mode = ControllerMode.Free;
		}

		if (key == InputKey.Jump){
			_jump = false;
		}

		if (key == InputKey.Run){
			_run = false;
		}
	}


	// DATA TYPES
	private enum ControllerMode{
		Free,
		Scope
	}
}

interface CharacterController{
	void MoveBody( float h, float v );
	void MoveMouse( float h, float v );
	void Jump();
}