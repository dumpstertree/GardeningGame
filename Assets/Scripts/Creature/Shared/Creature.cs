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

	public int X{
		get{
			return (int)Mathf.Round( transform.position.x );
		}
	}
	public int Y{ 
		get{
			return (int)Mathf.Round( transform.position.z );
		}
	}
	public Creature Target {
		get{
			return _target;
		}
	}
	public int BelongsToFactionsBitmask = 0;
	public int HatesFactionsBitmask = 0;
	public List<Creature> Party = new List<Creature>();

	// INSTANCE VARIABLES
	public bool UseAI = true;
	public Creature LeaderCreature;
	public Personality CreaturePersonality;

	public CreatureGrowth Growth;
	public Animator Animator;
	public Animator CreatureBrain;
	public Stats CreatureStats;
	public Eyes CreatureEyes;
	public TargetBehavior CreatureTargetBehavior;
	public Faction[] BelongsToFactions;
	public Faction[] HatesFactions;

	public Creature _target;


	// PUBLIC METHODS
	public void Hit( int rawDamage, Creature dealer ){
		_target = dealer;

		CreateEffect(Game.Resources.HitEffect);
		if (Animator)
			Animator.SetTrigger(AnimationTrigger.Hurt);
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
		CreateEffect(Game.Resources.Fireworks);
		Destroy( gameObject, 2 );
	}
		

	// PRIVATE METHODS
	private void Start(){
		foreach ( Faction f in BelongsToFactions ){
			BelongsToFactionsBitmask += 1 << (int)f;
		}
		foreach ( Faction f in HatesFactions ){
			HatesFactionsBitmask += 1 << (int)f;
		}
	}
	private void Update(){
		if (UseAI){

			GetGameState();

			if (StateManager.GameState == GameState.Free){
				FreeGameStateThink();
			}
			if (StateManager.GameState == GameState.InBattle){
				InBattleGameStateThink();
			}
		}
	}

	private void GetGameState(){
		var b = false;
		foreach( Creature c in Party ){
			if( c.Target != null){
				b = true;
				break;
			}
		}
		if (_target != null){
			b = true;
		}

		if (!b){
			StateManager.GameState = GameState.Free;
		}
		else{
			StateManager.GameState = GameState.InBattle;
		}
	}
	private void FreeGameStateThink(){

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
	private void InBattleGameStateThink(){

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
	private void CreateEffect( GameObject prefab ){
		var e = Instantiate( prefab );
		e.transform.position = transform.position;
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
	}


	public struct Visibility{
		
		public int CurrentHealth;
		public Visibility( Creature creature ){
			CurrentHealth = creature.CreatureStats.Health;
		}
	}
}
	
public enum Personality{
	Agressive,
	Submissive
}
public enum Faction{
	Player = 0,
	GreenLyme = 1,
	RedLyme = 2,
}