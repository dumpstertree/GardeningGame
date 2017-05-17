using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingUIController : MonoBehaviour, ILockOn {

	[SerializeField] private GameObject _targetLockOnUIPrefab;
	[SerializeField] private GameObject _targetHealthUIPrefab;

	private GameObject _targetHealthUIInstance;
	private GameObject _targetLockOnUIInstance;

	private Creature _target;
	private bool _lockedOn;

	private void Start(){
		FindObjectOfType<LockOnController>().Delegate = this;
	}
	public void TargetChanged( Creature newTarget, Creature lastTarget ){

		if (_targetHealthUIInstance){
			_targetHealthUIInstance.GetComponent<HealthUIBehavior>().Teardown();
		}

		if (newTarget){
			_targetHealthUIInstance =  Instantiate( _targetHealthUIPrefab );
			_targetHealthUIInstance.GetComponent<HealthUIBehavior>().Startup( newTarget );
		}

		_target = newTarget;

		if (_targetLockOnUIInstance){
			LockOnChanged( _lockedOn );
		}
	}
	public void LockOnChanged( bool locked ){

		_lockedOn = locked;

		if (_targetLockOnUIInstance){
			_targetLockOnUIInstance.GetComponent<LockOnUIBehavior>().Teardown();
		}

		if (locked){
			_targetLockOnUIInstance = Instantiate( _targetLockOnUIPrefab );
			_targetLockOnUIInstance.GetComponent<LockOnUIBehavior>().Startup( _target );
		}
	}
}
