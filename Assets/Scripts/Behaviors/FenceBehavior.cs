using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceBehavior : MonoBehaviour {

	// PROPERTIES
	public bool Left{
		set{
			if( value != _left){
				_left = value;

				if (_left){
					_leftGO = Instantiate( _fenceBeamPrefab );
					_leftGO.transform.position = transform.position;
					_leftGO.transform.SetParent( transform );
					_leftGO.transform.rotation = Quaternion.Euler( new Vector3( 0, 0, 0 ) );
				}
				else{
					Destroy( _leftGO );
				}
			}
		}
	}
	public bool Up{
		set{
			if( value != _up){
				_up = value;

				if (_up){
					_upGO = Instantiate( _fenceBeamPrefab );
					_upGO.transform.position = transform.position;
					_upGO.transform.SetParent( transform );
					_upGO.transform.rotation = Quaternion.Euler( new Vector3( 0, 90, 0 ) );
				}
				else{
					Destroy( _upGO );
				}
			}
		}
	}

	private bool _left 	= false;
	private bool _up 	= false;


	// INSTANCE VARIABLES
	[SerializeField] private GameObject _fenceBeamPrefab;

	private GameObject _leftGO;
	private GameObject _rightGO;
	private GameObject _upGO;
	private GameObject _downGO;
}
