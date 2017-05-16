using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {

	// PROPERTIES
	static public GameResources Resources{
		get{
			return _resources;
		}
	}
	static private GameResources _resources;

	static public Controller Controller{
		get{
			return _controller;
		}
	}
	static private Controller _controller;

	static public Model Model{
		get{
			return _model;
		}
	}
	static private Model _model;

	// INSTANCE VARIABLES
	public GameObject _resourcesPrefab;
	public GameObject _controllerPrefab;
	public GameObject _modelPrefab;

	private void Awake(){

		// Setup Resources
		if (FindObjectOfType<GameResources>() == null){
			_resources = Instantiate( _resourcesPrefab ).GetComponent<GameResources>();
			_resources.transform.SetParent( transform );  
			_resources.name = _resourcesPrefab.name;
		}
		else{
			_resources = FindObjectOfType<GameResources>();
		}

		// Setup Model
		if (FindObjectOfType<Model>() == null){
			_model = Instantiate( _modelPrefab ).GetComponent<Model>();
			_model.transform.SetParent( transform );  
			_model.name = _modelPrefab.name;
		}
		else{
			_model = FindObjectOfType<Model>();
		}

		// Setup Controller
		if (FindObjectOfType<Controller>() == null){
			_controller = Instantiate( _controllerPrefab ).GetComponent<Controller>();
			_controller.transform.SetParent( transform );  
			_controller.name = _controllerPrefab.name;
		}
		else{
			_controller = FindObjectOfType<Controller>();
		}
	}
}
