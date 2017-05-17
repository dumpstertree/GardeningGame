using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceBehavior : MonoBehaviour, IRadius {

	// INSTANCE VARIABLES
	[SerializeField] private GameObject _beamPrefab;
	[SerializeField] private RadiusBehavior _radius;
	[SerializeField] private GameObject _fenceBeamPrefab;

	private List<FenceBeamBehavior> _fenceBeams =  new List<FenceBeamBehavior>();
	private bool _skipCollision;

	// PRIVATE METHODS
	private void Start(){
		_radius.Delegate = this;
	}
	private void OnDestroy(){	
		for( int i=_fenceBeams.Count-1; i>=0; i-- ){
			Destroy( _fenceBeams[i].gameObject );
		}
	}

	public void FenceBeamDestroyed( FenceBeamBehavior beam ){
		_fenceBeams.Remove(beam);
	}
	public void AddBeam( FenceBeamBehavior beam ){
		_fenceBeams.Add( beam );
		beam.FencePost2 = this;
		_skipCollision = true;
	}

	// IRADIUS
	public void EnteredTriggerRadius(IRadius radius){

		if( !_skipCollision ){
			if (radius is FenceBehavior){
				var fence = (FenceBehavior)radius;
				var beam = Instantiate( _beamPrefab ).GetComponent<FenceBeamBehavior>();
				beam.FencePost1 = this;
				fence.AddBeam( beam );
			}
		}

		_skipCollision = false;
	}
	public void ExitedTriggerRadius(IRadius radius){
		//print(collider.name);
	}
}
