using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent ( typeof(BoxCollider) )]
public class Interactor : MonoBehaviour, IInteractor, IKeyUp, IKeyDown {

	[SerializeField] private LayerMask _collisionsToIgnore;

	// INSTANCE VARIABLES
	private InventoryItem _interactor;
	private GameObject _interactorVisual;
	private GameObject _tooltipVisual;
	private bool _keyDown   = false;

	private List<InteractorReciever> _interactorRecieversInRange;

	public InteractorReciever Reciever{
		get{
			return _reciever;
		}
		set{
			if ( value != _reciever || !_nulled && value == null ){

				_reciever = value;

				Destroy( _tooltipVisual );

				if (value != null){
					_nulled = false;
					_tooltipVisual = Instantiate( Game.Resources.WorldSpaceTooltipVisual );
					_tooltipVisual.GetComponent<InteractorWorldSpaceTooltipBehavior>().Setup( _reciever.transform, _interactor );
				}
				else{
					_nulled = true;
				}
			}
		}
		
	}

	public bool _nulled = false; // This value tracks weather or not the object was destroyed last frame
	public InteractorReciever _reciever;
	public bool _valid = true;
	public Sprite _validSprite;
	public Sprite _invalidSprite;


	// MONO
	private void Awake(){
		_interactorRecieversInRange = new List<InteractorReciever>();
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
		case InventoryItemActionType.Feed:
			Interact();
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

		if (_reciever == null && !_nulled){
			Reciever = null;
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


	// PRIVATE METHODS
	private void Free(){

		Reciever = null;

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
		if(Reciever){
			_valid = true;
		}
		else{
			_valid = false;
		}

		if (_keyDown && _valid){
			_interactor.Use( Reciever );
			var mesh = FindObjectOfType<CustomCharacterController>().mesh;
			mesh.LookAt( new Vector3( Reciever.transform.position.x, mesh.transform.position.y, Reciever.transform.position.z) );
		}
	}
	private void None(){
		Reciever = null;
		_valid = true;
	}

	private void GetRecieverBasedOnInteractor(){

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
					Reciever = available[ k[0] ];
					return;
				}
			}
		}
			
		Reciever = null;
			
	}
	private void GetValidity(){
		if(Reciever == null && _interactorRecieversInRange.Count > 0){
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

		if (Reciever){
			_interactorVisual.transform.position = Vector3.Lerp( _interactorVisual.transform.position, new Vector3( Reciever.transform.position.x, 0.01f, Reciever.transform.position.z ), 0.5f );
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
