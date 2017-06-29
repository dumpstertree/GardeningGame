using UnityEngine;

public class PlayerBrain : Brain, IInteractor {

	// PROPERTIES
	protected override Creature Creature{
		get{
			return _creature;
		}
	}


	// REFRENCE
	[SerializeField] private AimAssist _aimAssist;
	[SerializeField] private PlayerCreature _creature;
	[SerializeField] private Rigidbody _rigidBody;
	[SerializeField] private float _speed;

	[HeaderAttribute("Camera Positions")]
	[SerializeField] private Transform _cameraPos;
	[SerializeField] private Transform _cameraBasePos;
	[SerializeField] private Transform _scopeCameraBasePos;

	[SerializeField] private float _camRotateSpeed = 90;


	// INSTANCE
    private float _horizontal;
	private float _vertical;
	private bool  _scopeMode;

	
	// SUPER
	protected override void Think() {

		GetAxis();
		
		Rotate();
		Move();
		
		MoveCameraPos();
		RotateCameraPos();

		_creature.SetInput( PlayerInputPacket.Create( _horizontal, _vertical, _scopeMode));
    }


    // MONO
    private void Start(){

    	Game.Controller.Interactor.Delegate = this;
    }


    // PRIVATE
	private void GetAxis(){

		var h = Input.GetAxis("Horizontal");
		var v = Input.GetAxis("Vertical");

		_horizontal = Mathf.Lerp( _horizontal, h, 0.5f );
		_vertical   = Mathf.Lerp( _vertical,   v, 0.5f );
	}
	private void Move(){

		if (_scopeMode){

			var startPos = _rigidBody.transform.position;
			var speed = _speed * Time.deltaTime;
			var forward  = _rigidBody.transform.forward * _vertical * speed;
			var right 	 = _rigidBody.transform.right * _horizontal * speed;
			
			_rigidBody.MovePosition( startPos + forward + right );
		}
		else{
			if ( Mathf.Abs(_horizontal) > 0.1f || Mathf.Abs(_vertical) > 0.1f){
				var startPos = _rigidBody.transform.position;
				var input  = Mathf.Abs( ( Mathf.Abs(_vertical) > Mathf.Abs(_horizontal) ) ? _vertical : _horizontal );
				var forward  = _rigidBody.transform.forward;
				var speed = _speed * Time.deltaTime;
			
				_rigidBody.MovePosition( startPos + ( forward * speed * input ) );
			}
		}
	}
	private void Rotate(){

		if (_scopeMode){

			var mouseHorizontal = Input.GetAxis("Mouse X") * _aimAssist.ReticuleSpeedMult;
			var up = _rigidBody.transform.up;
			var y = mouseHorizontal * _camRotateSpeed;
			_rigidBody.MoveRotation( _rigidBody.rotation * Quaternion.AngleAxis( y, up));
		}
		else{
			if ( Mathf.Abs(_horizontal) > 0.1f || Mathf.Abs(_vertical) > 0.1f){
				var rads = Mathf.Atan2(_vertical,_horizontal);
				var degrees = rads * Mathf.Rad2Deg;
				var adjusted = (degrees-90) * -1;
				var y = Camera.main.transform.rotation.eulerAngles.y + adjusted;

				_rigidBody.MoveRotation( Quaternion.AngleAxis( y, Vector3.up));
			}
		}
	}	
	private void MoveCameraPos(){
		
		if (_scopeMode){

			Vector2 xOffset = new Vector2(  1,  -1 );
			Vector2 yOffset = new Vector2(  0,  0 );
			Vector2 zOffset = new Vector2(  0,  -1 );
			
			var x =  Mathf.Lerp( xOffset.x, xOffset.y, (_horizontal+1)/2 );
			var y =  Mathf.Lerp( yOffset.x, yOffset.y, (_vertical+1)/2 );
			var z =  Mathf.Lerp( zOffset.x, zOffset.y, (_vertical+1)/2 );

			_cameraPos.localPosition = _scopeCameraBasePos.localPosition + new Vector3( x, y, z );
		}
		else{

			Vector2 xOffset = new Vector2( -3,  3 );
			Vector2 yOffset = new Vector2(  0,  0 );
			Vector2 zOffset = new Vector2(  0, -1 );
			
			var x =  Mathf.Lerp( xOffset.x, xOffset.y, (_horizontal+1)/2 );
			var y =  Mathf.Lerp( yOffset.x, yOffset.y, (_vertical+1)/2 );
			var z =  Mathf.Lerp( zOffset.x, zOffset.y, (_vertical+1)/2 );

			_cameraPos.localPosition = _cameraBasePos.localPosition + new Vector3( x, y, z );
		}
	}
	private void RotateCameraPos(){
		
		if (_scopeMode){
		
			var mouseVertical = -Input.GetAxis("Mouse Y") * _camRotateSpeed * _aimAssist.ReticuleSpeedMult;
			_scopeCameraBasePos.Rotate(new Vector3( mouseVertical, 0, 0));
			_cameraPos.transform.rotation = _scopeCameraBasePos.rotation;
		}
		else{

			_cameraPos.transform.rotation = _cameraBasePos.rotation;
		}
	}


	// IINTERACTOR
	public void InteractorChanged( InventoryItem interactor ){

		_scopeMode = (interactor is ShootInventoryItem) ? true : false;
	}
	public void InteractorSlotChanged( int slotNumber ){
	}
}
