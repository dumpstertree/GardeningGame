using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UIController : MonoBehaviour, IKeyUp {


	// PROPERTIES
	public UIPanel UIPanel{
		get{
			return _uipanel;
		}
		set{
			if (_uipanel != value){
				_uipanel = value;
				OpenUI();
			}
			else{
				_uipanel = UIPanel.None;
				OpenUI();
			}
		}
	}
	private UIPanel _uipanel;

	public UISecondaryPanel SecondaryUIPanel{
		get{
			return _secondaryUIpanel;
		}
		set{

			if (_uipanel != UIPanel.None){
				value = UISecondaryPanel.None;
			}

			if (_secondaryUIpanel != value){
				_secondaryUIpanel = value;
				OpenSecondaryUI();
			}
		}
	}
	private UISecondaryPanel _secondaryUIpanel;


	// INSTANCE VARIABLES
	[SerializeField] private GameObject _inventoryControllerPrefab;
	[SerializeField] private GameObject _interactorControllerPrefab;
	[SerializeField] private GameObject _CreaturePropertiesControllerPrefab;

	private UIInventoryController  _inventoryController;
	private InteractorUIController _interactorController;
	private CreaturePropertiesUI   _creaturePropertiesController;


	// PRIVATE NETHODS
	private void Awake(){

		// Setup Inventory Controller
		if (FindObjectOfType<UIInventoryController>() == null){
			_inventoryController = Instantiate( _inventoryControllerPrefab ).GetComponent<UIInventoryController>();
			_inventoryController.transform.SetParent( transform, false );  
			_inventoryController.name = _inventoryControllerPrefab.name;
		}
		else{
			_inventoryController = FindObjectOfType<UIInventoryController>();
		}

		// Setup Interactor Controller
		if (FindObjectOfType<InteractorUIController>() == null){
			_interactorController = Instantiate( _interactorControllerPrefab ).GetComponent<InteractorUIController>();
			_interactorController.transform.SetParent( transform, false );  
			_interactorController.name = _interactorControllerPrefab.name;
		}
		else{
			_interactorController = FindObjectOfType<InteractorUIController>();
		}

		// Setup Interactor Controller
		if (FindObjectOfType<CreaturePropertiesUI>() == null){
			_creaturePropertiesController = Instantiate( _CreaturePropertiesControllerPrefab ).GetComponent<CreaturePropertiesUI>();
			_creaturePropertiesController.transform.SetParent( transform, false );  
			_creaturePropertiesController.name = _CreaturePropertiesControllerPrefab.name;
		}
		else{
			_creaturePropertiesController = FindObjectOfType<CreaturePropertiesUI>();
		}
	}
	private void Start(){
		Game.Controller.InputManager.KeyUpDelegate = this;
		UIPanel = UIPanel.None;
		SecondaryUIPanel = UISecondaryPanel.None;
	}
	private void OpenUI(){

		switch( _uipanel ){

		case UIPanel.None:
			_inventoryController.gameObject.SetActive(false);
			_interactorController.gameObject.SetActive(true);

			Camera.main.GetComponent<UnityStandardAssets.ImageEffects.Blur>().enabled = false;
			break;
		
		case UIPanel.Inventory:
			_inventoryController.gameObject.SetActive(true);
			_interactorController.gameObject.SetActive(false);

			Camera.main.GetComponent<UnityStandardAssets.ImageEffects.Blur>().enabled = true;
			break;
		}
	}
	private void OpenSecondaryUI(){
		
		switch( _secondaryUIpanel ){

		case UISecondaryPanel.None:
			_creaturePropertiesController.gameObject.SetActive(false);
			break;

		case UISecondaryPanel.CreatureStats:
			_creaturePropertiesController.gameObject.SetActive(true);
			break;
		}
	}


	// INPUT MANAGER DELEGATES
	public void KeyUp( InputKey key ){
		if (key == InputKey.InventoryUI){
			UIPanel = UIPanel.Inventory;
		}
	}

}

public enum UIPanel{
	Invalid,
	None,
	Inventory
}
public enum UISecondaryPanel{
	Invalid,
	None,
	CreatureStats
}