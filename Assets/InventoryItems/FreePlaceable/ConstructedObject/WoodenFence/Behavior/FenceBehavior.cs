 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceBehavior : MonoBehaviour, IRadius {

	// INSTANCE VARIABLES
	[SerializeField] private GameObject _beamPrefab;
	[SerializeField] private RadiusBehavior _radius;

	[SerializeField] private List<FenceBeamBehavior> _fenceBeams = new List<FenceBeamBehavior>();
	[SerializeField] private List<FenceBehavior> 	 _fencePostsInRange = new List<FenceBehavior>();

	private FenceBehavior Post1{
		set{
			
			if (value != _post1 && _post1 != null){
				for( int i = _fenceBeams.Count-1; i>=0; i-- ){
					var beam = _fenceBeams[i];
					if (  _post1 == beam.FencePost1 || _post1 == beam.FencePost2){
						Destroy( beam );
					}
				}
			}
				
			if ( value != null && !CheckIfBeamAlreadyExists( value ) ){
				var beam = Instantiate( _beamPrefab ).GetComponent<FenceBeamBehavior>();
				beam.FencePost1 = this;
				beam.FencePost2 =  value;
				_fenceBeams.Add( beam );

				value.AddBeam( beam );
	
				_post1 = value;
			}
		}
	}
	private FenceBehavior Post2{
		set{
			
			if (value != _post2  && _post1 != null){
				for( int i = _fenceBeams.Count-1; i>=0; i-- ){
					var beam = _fenceBeams[i];
					if (  _post2 == beam.FencePost1 || _post2 == beam.FencePost2){
						Destroy( beam );
					}
				}
			}
				
			if ( value != null && !CheckIfBeamAlreadyExists( value ) ){
				var beam = Instantiate( _beamPrefab ).GetComponent<FenceBeamBehavior>();
				beam.FencePost1 = this;
				beam.FencePost2 =  value;
				_fenceBeams.Add( beam );

				value.AddBeam( beam );

				_post2 = value;
			}
		}
	}
	private FenceBehavior _post1;
	private FenceBehavior _post2;

	// PRIVATE METHODS
	private void Start(){
		_radius.Delegate = this;
	}
	private void OnDestroy(){	
		foreach( FenceBeamBehavior f in _fenceBeams ){
			Destroy( f.gameObject );
		}
	}

	public void FenceBeamDestroyed( FenceBeamBehavior beam ){

		// Remove Beam Refrence
		for( int i = _fenceBeams.Count-1; i>=0; i-- ){
			var fence = _fenceBeams[i];
			if ( beam == fence ){
				_fenceBeams.Remove( fence );
			}
		}

		/*// Remove Post Refrence
		for( int i = _fencePostsInRange.Count-1; i>=0; i-- ){
			var post = _fencePostsInRange[i];
			if ( beam.FencePost1 == post || beam.FencePost2 == post ){
				_fencePostsInRange.Remove( post );
			}
		}*/

	}
	public void AddBeam( FenceBeamBehavior beam ){
		_fenceBeams.Add( beam );
		beam.FencePost2 = this;
	}

	private void AddFencePost( FenceBehavior fence ){
		_fencePostsInRange.Add( fence );
		SortFencePostsByRange();
	}
	private void RemoveFencePost( FenceBehavior fence ){
		_fencePostsInRange.Remove( fence );
		SortFencePostsByRange();
	}
	private void SortFencePostsByRange(){

		var fenceDistance = new Dictionary<float,FenceBehavior>();
		foreach( FenceBehavior f in _fencePostsInRange ){
			var d = Vector3.Distance( f.transform.position, transform.position );
			while ( fenceDistance.ContainsKey(d)  ){
				d += 0.001f;
			}
			
			fenceDistance.Add( d , f );
		}
			
		var keys = new List<float>( fenceDistance.Keys );
		keys.Sort();

		Post1 = (keys.Count >= 1) ? fenceDistance[ keys[0] ]: null;
		Post2 = (keys.Count >= 2) ? fenceDistance[ keys[1] ]: null;
	}
	private bool CheckIfBeamAlreadyExists( FenceBehavior beam ){

		foreach( FenceBeamBehavior b in _fenceBeams ){
			if (b.FencePost1 == beam || b.FencePost2 == beam){
				return true;
			}
		}
		return false;
	}


	// IRADIUS
	public void EnteredTriggerRadius(IRadius radius){

		if (radius is FenceBehavior){
			var fence = (FenceBehavior)radius;
			if( !_fencePostsInRange.Contains( fence ) ){
				AddFencePost( fence );
			}
		}
	}
	public void ExitedTriggerRadius(IRadius radius){
	
		if (radius is FenceBehavior){
			var fence = (FenceBehavior)radius;
			if( _fencePostsInRange.Contains( fence ) ){
				RemoveFencePost( fence );
			}
		}
	}
}

