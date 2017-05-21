using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour {


	// PROPERTIES
	public Visibility CreatureVisiblity{
		get{
			return new Visibility( this );
		}
	}
	public Creature Target {
		get{
			return _target;
		}
	}
	public List<Creature> Party = new List<Creature>();

	// INSTANCE VARIABLES
	public bool UseAI = true;

	public Creature LeaderCreature;
	public Personality CreaturePersonality;

	public Appearance Appearance;
	public CreatureGrowth Growth;
	public Animator Animator;

	public Stats CreatureStats;
	public TargetBehavior CreatureTargetBehavior;
	public Creature _target;

	protected Eyes CreatureEyes; 
	protected Animator CreatureBrain;
	private BrainStateMachine Brain;

	// PUBLIC METHODS
	public void Hit( int rawDamage, Creature dealer ){

		//_target = dealer;

		Animator.SetTrigger( AnimationTrigger.Hurt );
		CreatureStats.SubtractHealth( Stats.CalculateDamage(rawDamage) );
	}
	public void Kill(){

		Animator.SetTrigger(AnimationTrigger.Dead);
		CreatureBrain.SetTrigger(BrainTrigger.Dead);

		if (CreatureTargetBehavior)
			CreatureTargetBehavior.enabled = false;

		CreatureBrain.enabled = false;
		enabled = false;

		DestroyCorpse();
	}
	public void DestroyCorpse(){
		Destroy( gameObject, 2 );
	}


	// MONO METHODS
	private void Awake(){
		Brain = new Creature.BrainStateMachine( this );
	}


	// DATA TYPE
	private struct AnimationTrigger{
		public static string Hurt = "Hurt";
		public static string Dead = "Dead";
	}
	private struct BrainTrigger{
		public static string Roam = "Roam";
		public static string Chase = "Chase";
		public static string Dead = "Dead";
		public static string Follow = "Follow";
		public static string FollowHappy = "FollowHappy";

	}



	public enum BrainStateTypes{
		Roaming,
		FollowBattle,
		FollowHappy,
		Attacking,
		Dead
	}
	private class BrainStateMachine : IStomach, IEyes {


		public BrainStateMachine( Creature c ){
			_creature = c;
			_state = BrainStateTypes.Roaming;
			_creature.gameObject.GetComponentInChildren<Stomach>().StartListening( this );
			_creature.CreatureEyes.AddListener( this );
		}

		public BrainStateTypes State
		{ 
			set{
				if (value != _state){

					switch( value ){

					case BrainStateTypes.FollowBattle:
						_creature.CreatureBrain.SetTrigger( BrainTrigger.Follow );
						break;

					case BrainStateTypes.FollowHappy:
						_creature.CreatureBrain.SetTrigger( BrainTrigger.FollowHappy );
						break;

					case BrainStateTypes.Roaming:
						_creature.CreatureBrain.SetTrigger( BrainTrigger.Roam );
						break;
					
					case BrainStateTypes.Attacking:
						_creature.CreatureBrain.SetTrigger( BrainTrigger.Chase );
						break;
					
					case BrainStateTypes.Dead:
						_creature.CreatureBrain.SetTrigger( BrainTrigger.Dead );
						break;

					default:
						_creature.CreatureBrain.SetTrigger( BrainTrigger.Roam );
						break;
					}

					print( "State changed to :" + value );
					_state = value;
				}
			}
		} 

		private BrainStateTypes _state;
		private Creature _creature;


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
	}

}
	
public enum Personality{
	Agressive,
	Submissive
}

public enum CreatureType{
	Player,
	Enemy,
	PartyMember
}

/*private void FarmThink(){
		Brain.State = BrainStateTypes.Roaming;
	}
	private void FreeThink(){

		// Passive Abilities
		CreatureStats.Regenerate();


		// Brain Triggers
		if (LeaderCreature != null){
			CreatureBrain.SetTrigger( BrainTrigger.Follow );
		}
		else{
			CreatureBrain.SetTrigger( BrainTrigger.Roam );
		}


		// Battle Triggers
		switch(CreaturePersonality){

		case Personality.Agressive:
			StateManager.GameState = GameState.InBattle;
			FindTarget();
			break;

		case Personality.Submissive:
			break;
		}
	}
	private void BattleThink(){

		if (_target == null){
			switch(CreaturePersonality){
			case Personality.Agressive:
				_target = FindTarget();
				break;

			case Personality.Submissive:
				if (LeaderCreature != null){
					_target = LeaderCreature.Target;
				}
				else{
					_target = FindTarget();
				}
				break;
			}
		}

		if (_target != null){
			var v = _target.CreatureVisiblity;
			if (v.CurrentHealth <= 0){
				_target = null;
			}
			else{
				CreatureBrain.SetTrigger( BrainTrigger.Chase );
			}
		} 
	}

	private Creature FindTarget(){

		var enemies = CreatureEyes.EnemiesInView;
		if (enemies.Count > 0){
			return  enemies[0];
		}

		foreach( Creature c in Party){
			if (c.Target != null){
				return c.Target;
			}
		}

		return null;
	}
*/