using UnityEngine;

[RequireComponent(typeof(Animator))]
public class FriendlyBrain : Brain, IStomach, IEyes {

	protected override Creature Creature{
		get{
			return _creature;
		}
	}
	[SerializeField] private FriendlyCreature _creature;


	// PROPERTIES -- 
	private BrainStateTypes State{ 
		set{
			if (value != _state){

				switch( value ){

				case BrainStateTypes.FollowBattle:
					_brainAnimator.SetTrigger( BrainTrigger.Follow );
					break;

				case BrainStateTypes.FollowHappy:
					_brainAnimator.SetTrigger( BrainTrigger.FollowHappy );
					break;

				case BrainStateTypes.Roaming:
					_brainAnimator.SetTrigger( BrainTrigger.Roam );
					break;

				case BrainStateTypes.Attacking:
					_brainAnimator.SetTrigger( BrainTrigger.Chase );
					break;

				case BrainStateTypes.Dead:
					_brainAnimator.SetTrigger( BrainTrigger.Dead );
					break;

				default:
					_brainAnimator.SetTrigger( BrainTrigger.Roam );
					break;
				}

				print( "Creature state changed to : " + value );

				_timeInCurrentState = 0.0f;
				_state = value;
			}
		}
	} 
		

	// INSTANCE --
	[HeaderAttribute("State")]
	[SerializeField] private BrainStateTypes 	_state; 
	[SerializeField] private Animator 			_brainAnimator;

	[HeaderAttribute("Senses")]
	[SerializeField] private Stomach 			_stomach;
	[SerializeField ]private Eyes 				_eyes;

	private float _timeInCurrentState = 0.0f;


	// SUPER METHODS
	protected override void Think(){

		_timeInCurrentState += Time.deltaTime;

		switch( _state ){

		case BrainStateTypes.Roaming:
			break;

		case BrainStateTypes.FollowHappy:
			if (_timeInCurrentState > 5.0f){
				State = BrainStateTypes.Roaming;
			}
			break;

		case BrainStateTypes.FollowBattle:
			break;

		case BrainStateTypes.Attacking:
			break;

		case BrainStateTypes.Dead:
			break;
		}
	}


	// MONO
	private void Start(){
		_stomach.AddListener(this);
		_eyes.AddListener(this);
	}


	// PUBLIC -- 
	public bool Feed( FoodEffect foodEffect ){
		return _stomach.Feed( foodEffect.Hunger );
	}


	// ISTOMACH --
	public void HungerChanged( int curHunger, int MaxHunger ){
	}
	public void BeenFed(){
		State = BrainStateTypes.FollowHappy;
	}


	// IEYE --
	public void SeesPlayer( Creature c ){
		if (_state == BrainStateTypes.Roaming){
			State = BrainStateTypes.FollowHappy;
		}
	}


	// DATA TYPES --
	private enum BrainStateTypes{
		Roaming,
		FollowBattle,
		FollowHappy,
		Attacking,
		Dead
	}


	// ANIMATOR TRIGGERS --
	private struct BrainTrigger{
		public static string Roam 		 = "Roam";
		public static string Chase 		 = "Chase";
		public static string Dead 		 = "Dead";
		public static string Follow 	 = "Follow";
		public static string FollowHappy = "FollowHappy";
	}
}
