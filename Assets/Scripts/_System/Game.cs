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
