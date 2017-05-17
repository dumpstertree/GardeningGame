using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent ( typeof(BoxCollider) )]
public class Interactor : MonoBehaviour, IInteractor, IKeyUp, IKeyDown {

	[SerializeField] private LayerMask _collisionsToIgnore;

	// INSTANCE VARIABLES
	private InventoryItem _interactor;
	private GameObject _interactorVisual;
	private bool _keyDown   = false;

	private List<InteractorReciever> _interactorRecieversInRange;
	private SphereCollider _interactRange;

	public InteractorReciever _reciever;
	public bool _valid = true;
	public Sprite _validSprite;
	public Sprite _invalidSprite;

	// MONO
	private void Awake(){
		_interactorRecieversInRange = new List<InteractorReciever>();
		_interactRange = GetComponent<SphereCollider>();
	}
	private void Start(){
		Game.Controller.InputManager.KeyUpDelegate = this;
		Game.Controller.InputManager.KeyDownDelegate = this;
		Game.Controller.Interactor.Delegate = this;
	}
	private void Update(){

		switch( _interactor.Action ){

		case InventoryItemActionType.FreePlace:
			Free();
			break;
		case InventoryItemActionType.Hit:
			Interact();
			break;
		case InventoryItemActionType.Water:
			Interact();
			break;
		case InventoryItemActionType.Plant:
			Interact();
			break;
		case InventoryItemActionType.None:
			None();
			break;
		}

		UpdateTileVisual();


		for( int i = 0; i < _interactorRecieversInRange.Count; i++){
			if (i == 0){
				_interactorRecieversInRange[i].Active = true;
			}
			else{
				_interactorRecieversInRange[i].Active = false;
			}
		}

	}
	private void OnTriggerEnter(Collider c) {

		if (_collisionsToIgnore != (_collisionsToIgnore | (1 << c.gameObject.layer ))){
			var reciever = c.GetComponent<InteractorReciever>();
			if ( reciever != null ){

				_interactorRecieversInRange.Add(reciever);
			}
		}
	}
	private void OnTriggerExit(Collider c) {

		var reciever = c.GetComponent<InteractorReciever>();
		if ( _interactorRecieversInRange.Contains( reciever ) ){
		
			if (reciever){
				reciever.Active = false;
			}
			
			_interactorRecieversInRange.Remove(reciever);
		}
	}

	private void Free(){

		_reciever = null;

		if (_interactorRecieversInRange.Count > 0){
			_valid = false;
		}
		else{
			_valid = true;
		}

		if (_keyDown && _valid){
			_interactor.Use( this );
		}
	}
	private void Interact(){

		GetRecieverBasedOnInteractor();
		if(_reciever){
			_valid = true;
		}
		else{
			_valid = false;
		}

		if (_keyDown && _valid){
			_interactor.Use( _reciever );
			var mesh = FindObjectOfType<CustomCharacterController>().mesh;
			mesh.LookAt( new Vector3( _reciever.transform.position.x, mesh.transform.position.y, _reciever.transform.position.z) );
		}
	}
	private void None(){
		_reciever = null;
		_valid = true;
	}

	// PRIVATE METHODS
	public void GetRecieverBasedOnInteractor(){

		if (_interactor != null) {
			
			for (int i = _interactorRecieversInRange.Count-1; i >= 0; i-- ){
				if(_interactorRecieversInRange[i] == null){
					_interactorRecieversInRange.RemoveAt(i);
				}
			}

			if (_interactorRecieversInRange.Count > 0){

				Dictionary<float,InteractorReciever> available = new Dictionary<float,InteractorReciever>();
				foreach (InteractorReciever r in _interactorRecieversInRange){
					if( _interactor.Recievers.Contains( r.Type ) || _interactor.Recievers.Contains( InteractorType.All ) ){

						var d = Vector3.Distance( r.transform.position, FindObjectOfType<CustomCharacterController>().mesh.transform.position );
						if ( !available.ContainsKey( d ) ){
							available.Add( d, r );
						}
					}
				}

				if (available.Count > 0){
					var k = new List<float>( available.Keys );
					k.Sort();
					_reciever = available[ k[0] ];
					return;
				}
			}
		}
			
		_reciever = null;
			
	}
	private void GetValidity(){
		if(_reciever == null && _interactorRecieversInRange.Count > 0){
			_valid = false;
		}
		else{
			_valid = true;
		}
	
	}
	private void UpdateTileVisual(){

		if (!_interactorVisual){
			_interactorVisual = Instantiate( Game.Resources.InteractorVisual );
			_interactorVisual.transform.position = transform.position;
		}

		if (_reciever){
			_interactorVisual.transform.position = Vector3.Lerp( _interactorVisual.transform.position, new Vector3( _reciever.transform.position.x, 0.01f, _reciever.transform.position.z ), 0.5f );
		}
		else{
			_interactorVisual.transform.position = Vector3.Lerp( _interactorVisual.transform.position, transform.position, 0.5f );
		}

		if (_valid){
			_interactorVisual.GetComponentInChildren<SpriteRenderer>().sprite = _validSprite;
		}
		else{
			_interactorVisual.GetComponentInChildren<SpriteRenderer>().sprite = _invalidSprite;
		}
	}
		

	// INTERACTOR DELEGATE
	public void InteractorChanged( InventoryItem interactor ){

		if (interactor == null){
			_interactor = new Hand_InventoryItem(); 
		}
		else{
			_interactor = interactor;
		}
	}
	public void InteractorSlotChanged( int slotNumber ){
	}


	// INPUT MANAGER DELEGATE
	public void KeyUp( InputKey key ){
		if (key == InputKey.Interact){
			_keyDown = false;
		}
	}
	public void KeyDown( InputKey key ){
		if (key == InputKey.Interact){
			_keyDown = true;
		}
	}
}
