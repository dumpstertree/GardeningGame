using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent ( typeof(SphereCollider) )]
public class Interactor : MonoBehaviour, IInteractor, IKeyUp, IKeyDown {

	// INSTANCE VARIABLES
	private InventoryItem _interactor;
	private GameObject _interactorVisual;
	private bool _keyDown   = false;

	private List<InteractorReciever> _interactorRecieversInRange;
	private SphereCollider _interactRange;

	public InteractorReciever _reciever;


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

		MoveTileVisual();
		GetRecieverBasedOnInteractor();

		if (_keyDown){
			if (_reciever){
				_interactor.Use( _reciever );
				var mesh = FindObjectOfType<CustomCharacterController>().mesh;
				mesh.LookAt( new Vector3( _reciever.transform.position.x, mesh.transform.position.y, _reciever.transform.position.z) );
			}
			else{
				_interactor.Miss();
			}
		}
			
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

		var reciever = c.GetComponent<InteractorReciever>();
		if ( reciever != null ){

			_interactorRecieversInRange.Add(reciever);
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
	private void MoveTileVisual(){

		if (_reciever){
			if (!_interactorVisual){
				_interactorVisual = Instantiate( Game.Resources.InteractorVisual );
				_interactorVisual.transform.position = new Vector3( _reciever.transform.position.x, 0.01f, _reciever.transform.position.z );
			}

			_interactorVisual.transform.position = Vector3.Lerp( _interactorVisual.transform.position, new Vector3( _reciever.transform.position.x, 0.01f, _reciever.transform.position.z ), 0.5f );
		}
		else{
			if (_interactorVisual){
				Destroy(_interactorVisual);
			}
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
