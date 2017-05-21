using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eyes : MonoBehaviour {


	// PROPERTIES
	public List<Transform> ObjectsInView{
		get{
			return _objectsInView;
		}
	}
	public List<Creature> EnemiesInView{
		get{
			return _enemiesInView;
		}
	}
		

	// INSTANCE VARIABLES
	private Creature _creature;
	private List<Creature>  _enemiesInView;
	private List<Transform> _objectsInView;
	private List<Transform> _objectsAroundBody;
	private List<IEyes> _listeners = new List<IEyes>();

	[SerializeField] private float _rayDensity = 10;
	[SerializeField] private float _distance   = 10;
	[SerializeField] private float _fov 	   = 100;


	// PUBLIC METHODS
	public void AddListener( IEyes listener ){
		if (!_listeners.Contains(listener)){
			_listeners.Add(listener);
		}
	}
	public void RemoveListener( IEyes listener ){
		if (_listeners.Contains(listener)){
			_listeners.Remove(listener);
		}
	}


	// PRIVATE METHODS
	private void Awake(){

		_objectsInView = new List<Transform>();
		_enemiesInView = new List<Creature>();
		_objectsAroundBody = new List<Transform>();

		_creature = GetComponentInParent<Creature>();
		if (_creature == null){
			Debug.LogWarning( "Stats Component missing Creature in parent" );
		}
	}
	private void Update(){
		Look();
		Percieve();
	}
	private void Look(){

		_objectsInView.Clear();
		_enemiesInView.Clear();

		var numberOfRays = _fov/_rayDensity;
		var minAngle =  -_fov/2;
		for (int i=0; i<numberOfRays; i++){

			float a =	minAngle + _rayDensity*i;
			Quaternion spreadAngle = Quaternion.AngleAxis(a, new Vector3(0, 1, 0));
			Vector3 noAngle = transform.forward *_distance;
			Vector3 newVector = spreadAngle * noAngle;
			Ray ray = new Ray (transform.position, newVector);

			RaycastHit hit;
			if( Physics.Raycast(ray, out hit, Mathf.Infinity)){

				if ( !_objectsInView.Contains( hit.collider.transform) ){ 
					_objectsInView.Add(hit.collider.transform);
				}
			}

		
			Debug.DrawRay(transform.position, newVector);
		}
	}
	private void Percieve(){
		foreach( Transform t in _objectsInView ){
			var c = t.GetComponent<Creature>();
			if ( c != null ){

				if (c.Appearance.CreatureVisiblity.CurrentHealth > 0) {

					// See Hate Creature
					var r = c.Appearance.BelongsToFactionsBitmask & _creature.Appearance.HatesFactionsBitmask;
					if ( r != 0 ){
						_enemiesInView.Add(c);
					}

					// See Player
					r = c.Appearance.BelongsToFactionsBitmask | 1<<(int)Faction.Player ;
					if ( r != 0  ){
						foreach( IEyes l in _listeners ){
							l.SeesPlayer( c );
						}
					}
				}
			}
		}
	}
		
}

public interface IEyes{
	void SeesPlayer( Creature player );
}
