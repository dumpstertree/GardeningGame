using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof(Animator) )]
public class Brain : MonoBehaviour, IStomach, IEyes {

	private Animator 			_brainAnimator;
	private BrainStateTypes 	_state;
	//private CreaturePersonality _personality;
	private Creature 			_creature;

	// SENSORS
	private Stomach _stomach;
	private Eyes _eyes;


	// MONO
	private void Start(){

		_stomach = GetComponentInChildren<Stomach>();
		_stomach.AddListener( this );

		_eyes	 = GetComponentInChildren<Eyes>();
		_eyes.AddListener( this );

		_creature = GetComponentInParent<Creature>();
		_brainAnimator = GetComponent<Animator>();

		_state = BrainStateTypes.Roaming;
	}
	private void Update(){
		Think();
	}


	// PUBLIC
	public void Load( CreatureData data ){
		//_personality = data.Personality;
	}
	public void Hit( Creature dealer ){
	}
	public bool Feed( Food food ){
		return _stomach.Feed( food.FoodEffect.Hunger );
	}


	// PRIVATE METHODS
	// This should be made more private because it should not be touched 
	private float _timeInCurrentState = 0.0f;
	private void Think(){

		_timeInCurrentState += Time.deltaTime;

		if( _creature.Allegiance == CreatureAllegiance.PartyMember ){

			switch( _state ){

			case BrainStateTypes.Roaming:
				break;

			case BrainStateTypes.FollowHappy:
				if (_timeInCurrentState > 5.0f){
					State = BrainStateTypes.Defualt;
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

	}
	private BrainStateTypes State
	{ 
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
		

	// ISTOMACH
	public void HungerChanged( int curHunger, int MaxHunger ){
	}
	public void BeenFed(){

		if (StateManager.GameState == GameState.Farm){
			State = BrainStateTypes.FollowHappy;
		}
	}


	// IEYE
	public void SeesPlayer( Creature c ){

		if (StateManager.GameState == GameState.Farm){
			State = BrainStateTypes.FollowHappy;
		}
	}


	// DATA TYPES
	private enum BrainStateTypes{
		Defualt,
		Roaming,
		FollowBattle,
		FollowHappy,
		Attacking,
		Dead
	}


	// ANIMATOR TRIGGERS
	private struct BrainTrigger{
		public static string Roam 		 = "Roam";
		public static string Chase 		 = "Chase";
		public static string Dead 		 = "Dead";
		public static string Follow 	 = "Follow";
		public static string FollowHappy = "FollowHappy";

	}
}

