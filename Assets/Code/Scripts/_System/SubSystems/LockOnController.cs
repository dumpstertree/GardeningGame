using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockOnController : MonoBehaviour, IKeyDown {

	public ILockOn Delegate{
		set{
			_delegates.Add(value);
		}
	}
	private List<ILockOn> _delegates;

	public Creature Target{
		set{
			if (_target != value){
				foreach (ILockOn d in _delegates){
					d.TargetChanged( value, _target );
				}

				_target = value;
			}
		}
	}
	private Creature _target;

	private bool _locked = false;

	private void Awake(){
		_delegates = new List<ILockOn>();
	}
	private void Start(){
		Game.Controller.InputManager.KeyDownDelegate = this;
	}
	private void Update(){

		if (_locked){
			return;
		}

		RaycastHit hit;
		if (Physics.Raycast( Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity )) {

			var c = hit.transform.GetComponent<Creature>();
			if ( c != null ) {

				if (c.Body.GetComponentInChildren<TargetBehavior>() != null){
					Target = c;
					return;
				}
			}
		}

		Target = null;
	}
		
	private void ShiftLockLeft(){

		var start = Camera.main.WorldToScreenPoint( _target.transform.position ).x;
		var lowest = Mathf.Infinity;
		Creature newTarget = _target;

		foreach( Transform t in TargetController._objectsInView ){

			if (t == _target.Body.GetComponentInChildren<TargetBehavior>().transform ){
				continue;
			}

			var pos = Camera.main.WorldToScreenPoint(t.position).x;
			if ( pos <= start ){
				var dist = Mathf.Abs( pos-start );
				if( dist < lowest  ){
					var c = t.GetComponentInParent<Creature>();
					if ( c != null ){
						lowest = pos;
						newTarget = c;
					}
				}
			}
		}

		Target = newTarget;

	}
	private void ShiftLockRight(){
		
		var start = Camera.main.WorldToScreenPoint( _target.transform.position ).x;
		var lowest = Mathf.Infinity;
		Creature newTarget = _target;

		foreach( Transform t in TargetController._objectsInView ){
			if (t == _target.Body.GetComponentInChildren<TargetBehavior>().transform){
				continue;
			}
				
			var pos = Camera.main.WorldToScreenPoint(t.position).x;
			if ( pos >= start ){
				var dist = Mathf.Abs( pos-start );
				if( dist < lowest  ){
					var c = t.GetComponentInParent<Creature>();
					if ( c != null ){
						lowest = pos;
						newTarget = c;
					}
				}
			}
		}

		Target = newTarget;
	}

	// INPUT DELEGATES
	public void KeyDown( InputKey key ){

		if (  key == InputKey.LockOn){
			if (_target){
				_locked = !_locked;
				foreach( ILockOn d in _delegates){
					d.LockOnChanged( _locked );
				}
			}
		}

		if (  key == InputKey.ShiftLockOnLeft){
			if (_target && _locked){
				ShiftLockRight();
			}
		}

		if (  key == InputKey.ShiftLockOnRight){
			if (_target && _locked){
				ShiftLockRight();
			}
		}
	}
}

public interface ILockOn{
	void TargetChanged( Creature newTarget, Creature lastTarget );
	void LockOnChanged( bool locked );
}
