using UnityEngine;

[RequireComponent(typeof(Animator))]
public class EnemyBrain : Brain, IEyes {

	protected override Creature Creature{
		get{
			return _creature;
		}
	}
	[SerializeField] private EnemyCreature _creature;


	// PROPERTIES -- 
	private BrainStateTypes State{ 
		set{
			if (value != _state){

				switch( value ){

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
	[SerializeField] private Eyes 				_eyes;

	private float _timeInCurrentState = 0.0f;



	// PUBLIC
	public void Shoot( ShootInfo info ){

	} 

	// SUPER METHODS
	protected override void Think(){

		_timeInCurrentState += Time.deltaTime;

		switch( _state ){

		case BrainStateTypes.Roaming:
			break;

		case BrainStateTypes.Attacking:
			break;

		case BrainStateTypes.Dead:
			break;
		}
	}


	// MONO
	private void Start(){

		_eyes.AddListener(this);
	}


	// IEYE --
	public void SeesPlayer( Creature c ){
		if (_state == BrainStateTypes.Roaming){
			State = BrainStateTypes.Attacking;
		}
	}


	// DATA TYPES --
	private enum BrainStateTypes{
		Roaming,
		Attacking,
		Dead
	}


	// ANIMATOR TRIGGERS --
	private struct BrainTrigger{
		public static string Roam 		 = "Roam";
		public static string Chase 		 = "Chase";
		public static string Dead 		 = "Dead";
	}
}
