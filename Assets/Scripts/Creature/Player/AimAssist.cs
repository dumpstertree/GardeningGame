using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimAssist : MonoBehaviour {

	public float ReticuleSpeedMult{
		get{
			if (_scopeMode.Target){
				return ReticuleSlow( GetDistance() );
			}

			return 1.0f;
		}
	}
	public Quaternion CamRotation{
		get{
			if (_scopeMode.Target){
				return ReticuleMagnet( GetDistance() );
			}

			return _shoulderCamTarget.rotation;
		}
	}
	public Quaternion PlayerRotation{
		get{
			if(_scopeMode.Target){
				return FaceTarget();
			}

			return _mesh.rotation;
		}
	}


	[Header("Aim Assist")]
	[SerializeField] private float _maxAimAssistRange 	= 100;
	[SerializeField] private float _maxMagnetRange 		= 150;
	[SerializeField] private float _maxMagnetStrength   = 0.3f;
	[SerializeField] private float _magnetBreakStrength = 0.4f;
	[SerializeField] private float _fpsCamRotationSpeed = 10;

	[SerializeField] private Transform _mesh;
	[SerializeField] private Transform _camera;
	[SerializeField] private Transform _shoulderCamTarget;

	private CharacterControllerScopeMode _scopeMode;


	// PRIVATE METHODS
	private void Awake(){
		_scopeMode = GetComponent<CharacterControllerScopeMode>();	
	}

	private float ReticuleSlow( float distance ){ 
		return Mathf.Clamp(distance/_maxAimAssistRange, 0.1f, 1.0f) * _fpsCamRotationSpeed;
	}
	private Quaternion ReticuleMagnet( float distance ){

		var magnet = _maxMagnetStrength * ( 1 - (distance/_maxMagnetRange) );
		var s = _shoulderCamTarget.rotation;
		_shoulderCamTarget.transform.LookAt( _scopeMode.Target.transform );
		var t = _shoulderCamTarget.rotation;

		return  Quaternion.Slerp( s, t, magnet );

	}
	private Quaternion FaceTarget(){
		var s2 = _mesh.transform.rotation;
		_mesh.transform.LookAt( new Vector3( _mesh.transform.position.x, _mesh.transform.position.y, _scopeMode.Target.transform.position.z));
		return _mesh.rotation;
	}

	private float GetDistance(){ 

		var s = Camera.main.WorldToScreenPoint( _scopeMode.Target.transform.position );
		var t = new Vector3(Screen.width/2, Screen.height/2, 0); 
		return Vector3.Distance( s, t );
	}
}
