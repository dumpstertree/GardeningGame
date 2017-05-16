using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {


	// DELEGATES - PROPERTIES
	public IInputMouseButtonDown MouseDownDelegate{
		set{
			_mouseDownDelegate.Add(value);
		}
	}
	private List<IInputMouseButtonDown> _mouseDownDelegate;

	public IInputMouseButtonUp MouseUpDelegate{
		set{
			_mouseUpDelegate.Add(value);
		}
	}
	private List<IInputMouseButtonUp> _mouseUpDelegate;

	public IInputScrollWheelChanged ScrollWheelChangedDelegate{
		set{
			_scrollWheelChangedDelegate.Add(value);
		}
	}
	private List<IInputScrollWheelChanged> _scrollWheelChangedDelegate;

	public IKeyDown KeyDownDelegate{
		set{
			_keyDownDelegate.Add(value);
		}
	}
	private List<IKeyDown> _keyDownDelegate;

	public IKeyUp KeyUpDelegate{
		set{
			_keyUpDelegate.Add(value);
		}
	}
	private List<IKeyUp> _keyUpDelegate;


	// INPUTS - PROPERTIES
	public bool InteractKey{
		set{
			if ( value != _interactKey ){

				if (value){
					AlertKeyDownDelegates( InputKey.Interact );
				}
				else{
					AlertKeyUpDelegates( InputKey.Interact );
				}

				_interactKey = value;
			}
		}
		get{
			return _interactKey;
		}
	}
	private bool _interactKey;

	public bool DownScopeKey{
		set{
			if ( value != _downScopeKey ){
				if (value){
					AlertKeyDownDelegates( InputKey.DownScope );
				}
				else{
					AlertKeyUpDelegates( InputKey.DownScope );
				}

				_downScopeKey = value;
			}
		}
		get{
			return _downScopeKey;
		}
	}
	private bool _downScopeKey;

	public bool InventoryUI{
		set{

			if ( value != _inventoryUI ){
				if (value){
					AlertKeyDownDelegates( InputKey.InventoryUI );
				}
				else{
					AlertKeyUpDelegates( InputKey.InventoryUI );
				}

				_inventoryUI = value;
			}
		}
		get{
			return _inventoryUI;
		}
	}
	private bool _inventoryUI;

	public bool LockOn{
		set{

			if ( value != _lockOn ){
				if (value){
					AlertKeyDownDelegates( InputKey.LockOn );
				}
				else{
					AlertKeyUpDelegates( InputKey.LockOn );
				}

				_lockOn = value;
			}
		}
		get{
			return _lockOn;
		}
	}
	private bool _lockOn;

	public bool ShiftLockOnLeft{
		set{

			if ( value != _shiftLockOnLeft ){
				if (value){
					AlertKeyDownDelegates( InputKey.ShiftLockOnLeft );
				}
				else{
					AlertKeyUpDelegates( InputKey.ShiftLockOnLeft );
				}

				_shiftLockOnLeft = value;
			}
		}
		get{
			return _shiftLockOnLeft;
		}
	}
	private bool _shiftLockOnLeft;

	public bool ShiftLockOnRight{
		set{

			if ( value != _shiftLockOnRight ){
				if (value){
					AlertKeyDownDelegates( InputKey.ShiftLockOnRight );
				}
				else{
					AlertKeyUpDelegates( InputKey.ShiftLockOnRight );
				}

				_shiftLockOnRight = value;
			}
		}
		get{
			return _shiftLockOnRight;
		}
	}
	private bool _shiftLockOnRight;

	public bool Run{
		set{

			if ( value != _run ){
				if (value){
					AlertKeyDownDelegates( InputKey.Run );
				}
				else{
					AlertKeyUpDelegates( InputKey.Run );
				}

				_run = value;
			}
		}
		get{
			return _run;
		}
	}
	private bool _run;

	public bool Jump{
		set{

			if ( value != _jump ){
				if (value){
					AlertKeyDownDelegates( InputKey.Jump );
				}
				else{
					AlertKeyUpDelegates( InputKey.Jump );
				}

				_jump = value;
			}
		}
		get{
			return _jump;
		}
	}
	private bool _jump;


	// PRIVATE METHODS
	private void Awake(){	
		_mouseDownDelegate 			= new List<IInputMouseButtonDown>();
		_mouseUpDelegate   			= new List<IInputMouseButtonUp>();
		_scrollWheelChangedDelegate = new List<IInputScrollWheelChanged>();
		_keyDownDelegate   			= new List<IKeyDown>();
		_keyUpDelegate 				= new List<IKeyUp>();
	}
	private void Update () {


		// KEYS
		InteractKey  	 = (Input.GetAxis("Interact") != 0) ? true : false;  
		DownScopeKey 	 = (Input.GetAxis("DownScope") != 0) ? true : false;  
		InventoryUI   	 = (Input.GetAxis("InventoryUI") != 0) ? true : false;  
		LockOn  		 = (Input.GetAxis("LockOn") != 0) ? true : false;  
		ShiftLockOnLeft  = (Input.GetAxis("ShiftLockOnLeft") != 0) ? true : false;  
		ShiftLockOnRight = (Input.GetAxis("ShiftLockOnRight") != 0) ? true : false;  
		Run 			 = (Input.GetAxis("Run") != 0) ? true : false;  
		Jump 			 = (Input.GetAxis("Jump") != 0) ? true : false;  

		// MOUSE INPUT
		if (Input.GetMouseButtonDown(0)){
			foreach( IInputMouseButtonDown d in _mouseDownDelegate  ){
				d.MouseButtonDown();
			}
		}
		if (Input.GetMouseButtonUp(0)){
			foreach( IInputMouseButtonUp d in _mouseUpDelegate  ){
				d.MouseButtonUp();
			}
		}
		if(Input.GetAxis("Mouse ScrollWheel") != 0 ){
			foreach ( IInputScrollWheelChanged d in _scrollWheelChangedDelegate ){
				d.ScrollWheelChanged( Input.GetAxis("Mouse ScrollWheel") );
			}
		}
			
	}

	private void AlertKeyDownDelegates( InputKey k ){
		foreach( IKeyDown d in _keyDownDelegate ){
			d.KeyDown( k );
		}
	}
	private void AlertKeyUpDelegates( InputKey k ){
		foreach( IKeyUp d in _keyUpDelegate ){
			d.KeyUp( k );
		}
	}
}

// INTERFACE
public interface IInputMouseButtonDown{
	void MouseButtonDown();
}
public interface IInputMouseButtonUp{
	void MouseButtonUp();
}
public interface IInputScrollWheelChanged{
	void ScrollWheelChanged( float scrollValue );
}
public interface IKeyDown{
	void KeyDown( InputKey key );
}
public interface IKeyUp{
	void KeyUp( InputKey key );
}


// DATA TYPES
public enum InputKey {
	Interact,
	DownScope,
	InventoryUI,
	LockOn,
	ShiftLockOnLeft,
	ShiftLockOnRight,
	Run,
	Jump
}
