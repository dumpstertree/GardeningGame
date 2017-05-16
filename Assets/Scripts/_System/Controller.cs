using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

	[SerializeField] private GameObject _audioControllerPrefab;
	[SerializeField] private GameObject _worldTimePrefab;
	[SerializeField] private GameObject _uiControllerPrefab;
	[SerializeField] private GameObject _playerSpawnControllerPrefab;
	[SerializeField] private GameObject _inputManagerPrefab;
	[SerializeField] private GameObject _screenShakePrefab;
	[SerializeField] private GameObject _interactorPrefab;

	public AudioController Audio{
		get{
			return _audio;
		}
	}
	private AudioController _audio;

	public WorldTime WorldTime{
		get{
			return _worldTime;
		}
	}
	private WorldTime _worldTime;

	public UIController UI{
		get{
			return _ui;
		}
	}
	private UIController _ui;

	public PlayerSpawnController PlayerSpawner{
		get{
			return _playerSpawner;
		}
	}
	private PlayerSpawnController _playerSpawner;

	public InputManager InputManager{
		get{
			return _inputManager;
		}
	}
	private InputManager _inputManager;

	public ScreenShake ScreenShake{
		get{
			return _screenShake;
		}
	}
	private ScreenShake _screenShake;

	public InteractorController Interactor{
		get{
			return _interactor;
		}
	}
	private InteractorController _interactor;

	private void Awake(){

		// Setup Audio Controller
		if (FindObjectOfType<AudioController>() == null){
			_audio = Instantiate( _audioControllerPrefab ).GetComponent<AudioController>();
			_audio.transform.SetParent( transform );  
			_audio.name = _audioControllerPrefab.name;
		}
		else{
			_audio = FindObjectOfType<AudioController>();
		}

		// Setup World Time
		if (FindObjectOfType<WorldTime>() == null){
			_worldTime = Instantiate( _worldTimePrefab ).GetComponent<WorldTime>();
			_worldTime.transform.SetParent( transform );  
			_worldTime.name = _worldTimePrefab.name;
		}
		else{
			_worldTime = FindObjectOfType<WorldTime>();
		}

		// Setup UI Controller
		if (FindObjectOfType<UIController>() == null){
			_ui = Instantiate( _uiControllerPrefab ).GetComponent<UIController>();
			_ui.transform.SetParent( transform );  
			_ui.name = _uiControllerPrefab.name;
		}
		else{
			_ui = FindObjectOfType<UIController>();
		}

		// Setup Player Spawner Controller
		if (FindObjectOfType<PlayerSpawnController>() == null){
			_playerSpawner = Instantiate( _playerSpawnControllerPrefab ).GetComponent<PlayerSpawnController>();
			_playerSpawner.transform.SetParent( transform );  
			_playerSpawner.name = _playerSpawnControllerPrefab.name;
		}
		else{
			_playerSpawner = FindObjectOfType<PlayerSpawnController>();
		}

		// Setup Input Manager
		if (FindObjectOfType<InputManager>() == null){
			_inputManager = Instantiate( _inputManagerPrefab ).GetComponent<InputManager>();
			_inputManager.transform.SetParent( transform );  
			_inputManager.name = _inputManagerPrefab.name;
		}
		else{
			_inputManager = FindObjectOfType<InputManager>();
		}

		// Setup Input Manager
		if (FindObjectOfType<ScreenShake>() == null){
			_screenShake = Instantiate( _screenShakePrefab ).GetComponent<ScreenShake>();
			_screenShake.transform.SetParent( transform );  
			_screenShake.name = _screenShakePrefab.name;
		}
		else{
			_screenShake = FindObjectOfType<ScreenShake>();
		}

		// Interactor Controller
		if (FindObjectOfType<InteractorController>() == null){
			_interactor = Instantiate( _interactorPrefab ).GetComponent<InteractorController>();
			_interactor.transform.SetParent( transform );  
			_interactor.name = _interactorPrefab.name;
		}
		else{
			_interactor = FindObjectOfType<InteractorController>();
		}
	}
}
