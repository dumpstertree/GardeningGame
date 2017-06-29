using UnityEngine;

public class PlayerCamera : MonoBehaviour, IInteractor {

	[HeaderAttribute("Cam Properties")]
	[SerializeField] private Transform _lerpObject;
	[SerializeField] private Transform _cameraTargetPos;
	[SerializeField] private Transform _cameraFocus;


	[HeaderAttribute("Idle Cam")]
	[SerializeField] private float _lerpspeed;
	[SerializeField] private float _followTriggerDistance;


	[HeaderAttribute("Scope Cam")]
	[SerializeField] private float _scopeCamLerpSpeed;


	// INSTANCE
	private bool  _scopeMode;
	private bool _following;
	
	
	// MONO
	private void Start(){

		Game.Controller.Interactor.Delegate = this;
	}
	private void Update () {
		Move();		
		Rotate();
	}
	private void OnDrawGizmos(){
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere( transform.position, _followTriggerDistance);
		Gizmos.DrawWireSphere( _lerpObject.position, 1.0f);
		Gizmos.DrawWireSphere( _cameraTargetPos.position, 1.0f);
	}
	

	//PRIVATE 
	private void Move(){
		
		if (_scopeMode){
			
			_lerpObject.position = Vector3.Lerp( transform.position, _cameraTargetPos.position, _scopeCamLerpSpeed);
			transform.position = Vector3.Lerp( transform.position, _lerpObject.position, _scopeCamLerpSpeed);
			_following = true;
		}
		else{

			if ( Vector3.Distance(transform.position, _cameraFocus.position) > _followTriggerDistance){
				_following = true;
			}
			
			if( Vector3.Distance(transform.position, _cameraTargetPos.position) < 0.1f){
				_following = false;
			}
			
			if (_following){
				_lerpObject.position = Vector3.Lerp( transform.position, _cameraTargetPos.position, _lerpspeed);
			}
			
			transform.position = Vector3.Lerp( transform.position, _lerpObject.position, _lerpspeed);
		}
	}
	private void Rotate(){
		
		if (_scopeMode){
			
			var startRot = transform.rotation;
			var targetRot = _cameraTargetPos.rotation;
			transform.rotation = Quaternion.Slerp( startRot, targetRot, _scopeCamLerpSpeed);
		}
		else{

			var startRot = transform.rotation;
			transform.LookAt( _cameraFocus );
			var targetRot = transform.rotation;
			transform.rotation = Quaternion.Slerp( startRot, targetRot, _lerpspeed);
		}
	}



	// IINTERACTOR
	public void InteractorChanged( InventoryItem interactor ){
		_scopeMode = (interactor is ShootInventoryItem) ? true : false;
	}
	public void InteractorSlotChanged( int slotNumber ){
	}
}
