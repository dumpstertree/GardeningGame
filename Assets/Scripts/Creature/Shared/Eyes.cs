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

	[SerializeField] private float _rayDensity = 10;
	[SerializeField] private float _distance   = 10;
	[SerializeField] private float _fov 	   = 100;


	// PRIVATE METHODS
	private void Awake(){

		_objectsInView = new List<Transform>();
		_enemiesInView = new List<Creature>();

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

				if ( !_objectsInView.Contains( hit.transform )){ 
					_objectsInView.Add(hit.transform);
				}
			}


			// Debug
			//Debug.DrawRay(transform.position, newVector);
		}
	}
	private void Percieve(){
		foreach( Transform t in _objectsInView ){
			var c = t.GetComponent<Creature>();
			if ( c != null ){

				if (c.CreatureVisiblity.CurrentHealth > 0) {

					var r = c.BelongsToFactionsBitmask & _creature.HatesFactionsBitmask;
					if ( r != 0 ){
						_enemiesInView.Add(c);
					}
				}
			}
		}
	}
}
