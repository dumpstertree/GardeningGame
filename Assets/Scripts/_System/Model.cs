using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour {

	[SerializeField] private GameObject _worldPrefab;

	public World World{
		get{
			return _world;
		}
	}
	private World _world;

	private void Awake(){

		// Setup World
		if (FindObjectOfType<World>() == null){
			_world = Instantiate( _worldPrefab ).GetComponent<World>();
			_world.transform.SetParent( transform );  
			_world.name = _worldPrefab.name;
		}
		else{
			_world = FindObjectOfType<World>();
		}

	}
}
