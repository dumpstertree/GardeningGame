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

	private List<Transform> 		 _collisions;
	private List<InteractableBehavior> _interactorRecieversInRange;

	public InteractableBehavior Reciever{
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
	public InteractableBehavior _reciever;

	public bool _nulled = false;
	public bool _interacting = false;

	public void StartInteracting(){
		_interacting = true;
	}
	public void EndInteraction(){
		_interacting = false;
	}


	// MONO
	private void Awake(){
		_collisions = new List<Transform>();
		_interactorRecieversInRange = new List<InteractableBehavior>();
	}
	private void Start(){
		Game.Controller.InputManager.KeyUpDelegate   = this;
		Game.Controller.InputManager.KeyDownDelegate = this;
		Game.Controller.Interactor.Delegate = this;

		_interactorVisual = Instantiate( Game.Resources.InteractorVisual );
	}
	private void Update(){

		CheckNulledObject();
		UpdateTileVisual();
		GetReciever();

		if (!_interacting){


			if ( _keyDown  ){

				if ( _interactor is FreePlaceInventoryItem ){
					var i = (FreePlaceInventoryItem)_interactor;
					i.Use( this );
				}

				if ( Reciever ){

					if ( _interactor is InteractInventoryItem && Reciever is IInteract ){
						var r = (IInteract)Reciever;
						var i = (InteractInventoryItem)_interactor;
						i.Use( r );
					}

					if ( _interactor is FeedInventoryItem && Reciever is IFeed ){
						var r = (IFeed)Reciever;
						var i = (FeedInventoryItem)_interactor;
						i.Use( r );
					}
						
					if ( _interactor is HitInventoryItem && Reciever is IHit ){
						var r = (IHit)Reciever;
						var i = (HitInventoryItem)_interactor;
						i.Use( r );
					}

					if ( _interactor is PlantInventoryItem && Reciever is IPlant ){
						var r = (IPlant)Reciever;
						var i = (PlantInventoryItem)_interactor;
						i.Use( r );
					}

					Game.Controller.PlayerSpawner.CharacterController.mesh.transform.LookAt( new Vector3( Reciever.transform.position.x, Game.Controller.PlayerSpawner.CharacterController.mesh.position.y, Reciever.transform.position.z ));
				}
			}
		}
	}
	private void OnTriggerEnter(Collider c) {

		if (_collisionsToIgnore != (_collisionsToIgnore | (1 << c.gameObject.layer ))){
			_collisions.Add( c.transform );

			var reciever = c.GetComponent<InteractableBehavior>();
			if ( reciever != null ){
				_interactorRecieversInRange.Add(reciever);
			}
		}
	}
	private void OnTriggerExit(Collider c) {

		if (_collisions.Contains( c.transform ) ){
			_collisions.Remove( c.transform );
		}

		var reciever = c.GetComponent<InteractableBehavior>();
		if ( _interactorRecieversInRange.Contains( reciever ) ){
			_interactorRecieversInRange.Remove(reciever);
		}
	}


	// PRIVATE METHODS
	private void GetReciever(){

		Vector3 playerPos = FindObjectOfType<CustomCharacterController>().mesh.transform.position;
		InteractableBehavior closest = null;
		var distance = Mathf.Infinity;

		for( int i=_interactorRecieversInRange.Count-1; i>=0; i-- ){

			var reciever = _interactorRecieversInRange[i];
			if (reciever == null){
				_interactorRecieversInRange.RemoveAt(i);
				continue;
			}


			if ( _interactor is PlantInventoryItem){
				if (reciever is IPlant){
					var d = Vector3.Distance( reciever.transform.position, playerPos );
					if (d<distance){
						closest = reciever;
					}
				}
			}
			if ( _interactor is HitInventoryItem){
				if (reciever is IHit){
					var d = Vector3.Distance( reciever.transform.position, playerPos );
					if (d<distance){
						closest = reciever;
					}
				}
			}
			if ( _interactor is FeedInventoryItem){
				if (reciever is IFeed){
					var d = Vector3.Distance( reciever.transform.position, playerPos );
					if (d<distance){
						closest = reciever;
					}
				}
			}
			if ( _interactor is InteractInventoryItem){
				if (reciever is IInteract){
					var d = Vector3.Distance( reciever.transform.position, playerPos );
					if (d<distance){
						closest = reciever;
					}
				}
			}
		}

		Reciever = closest;
	}
	private void CheckNulledObject(){
		if (_reciever == null && !_nulled){
			Reciever = null;
		}
	}
		
	private void UpdateTileVisual(){
			
		if ( _interactor is FreePlaceInventoryItem ){
			SetTileVisibility( true );
			SetTileValidity( CheckTileValidityFree() );
			SetTilePosition( transform.position );
		}

		else if (  
			_interactor is FeedInventoryItem  ||
			_interactor is HitInventoryItem   ||	  
			_interactor is PlantInventoryItem ||
			_interactor is InteractInventoryItem){

			var valid = CheckTileValidityInteract();
			if (valid){
				SetTileVisibility( true );
				SetTilePosition( _reciever.transform.position );
			} else{
				SetTileVisibility( false );
			}
		}

		else{
			SetTileVisibility( false );
		}
	}
	private void SetTileVisibility( bool v ){
		_interactorVisual.gameObject.SetActive( v );
	}
	private void SetTileValidity( bool v ){
		if (v){
			_interactorVisual.GetComponentInChildren<SpriteRenderer>().sprite = Game.Resources.Sprites.ValidInteractor;	
		}
		else{
			_interactorVisual.GetComponentInChildren<SpriteRenderer>().sprite = Game.Resources.Sprites.InvalidInteractor;	
		}
	}
	private void SetTilePosition( Vector3 pos ){
		_interactorVisual.transform.position = Vector3.Lerp( _interactorVisual.transform.position, pos, 0.25f);
	}
	private bool CheckTileValidityFree(){
		if (_collisions.Count > 0){
			return false;
		}
		return true;
	}
	private bool CheckTileValidityInteract(){
		if (_reciever == null ){
			return false;
		}
		return true;
	}
		

	// INTERACTOR DELEGATE
	public void InteractorChanged( InventoryItem interactor ){
		if(interactor == null){
			_interactor = new InteractInventoryItem();
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
