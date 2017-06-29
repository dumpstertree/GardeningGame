using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnController : MonoBehaviour {

	[SerializeField] private GameObject _characterControllerPrefab;

	public CustomCharacterController CharacterController{
		get{
			return _characterController;
		}
	}
	private CustomCharacterController _characterController;

	void Awake () {
		
		// Setup Audio Controller
		/*if (FindObjectOfType<CustomCharacterController>() == null){
			_characterController = Instantiate( _characterControllerPrefab ).GetComponent<CustomCharacterController>();
			_characterController.transform.SetParent( transform );  
			_characterController.name = _characterControllerPrefab.name;
		}
		else{
			_characterController = FindObjectOfType<CustomCharacterController>();
		}*/

		//_characterController.transform.position = new Vector3( 256, 0, 50 );
	}
}
