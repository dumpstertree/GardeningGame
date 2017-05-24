using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creature : MonoBehaviour {


	// PROPERTIES
	public Creature Target {
		get{
			return _target;
		}
	}
	public List<Creature> Party = new List<Creature>();
	public Creature LeaderCreature;


	public int BelongsToFactionsBitmask{
		get{
			return _belongsToFactionBitMask;
		}
	}
	public int HatesFactionsBitmask{
		get{
			return _hatesFactionsBitmask;
		}
	}
	public Brain Brain;
	public Body Body;
	public CreatureAllegiance Allegiance;


	// INSTANCE
	private int _belongsToFactionBitMask = 0;
	private int _hatesFactionsBitmask 	 = 0;
	[SerializeField] private Faction[] _belongsToFactions;
	[SerializeField] private Faction[] _hatesFactions;
	[SerializeField] private bool 	   _active;
	[SerializeField] public Animator  Animator;

	public Creature _target;
	private CreatureAllegiance _allegiance;

	// PUBLIC METHODS
	public void Hit( int rawDamage, Creature dealer ){
		Animator.SetTrigger( AnimationTrigger.Hurt );

		Body.Hit( rawDamage );
		Brain.Hit( dealer );
	}
	public bool Feed( Food food ){
		var r = Brain.Feed( food );
		if( r ){
			Body.Feed( food );
		}
		return r;
	}

	public void Create( CreatureAllegiance allegiance ){
		_allegiance = allegiance;

		var data = CreatureData.Roll( allegiance );
	
		Body.Load( data );
		Brain.Load( data );

		_active = true;
	}
	public void Load(){
		_active = true;
	}


	// MONO
	private void Awake(){
		Body  	 = GetComponentInChildren<Body>();
		Brain 	 = GetComponentInChildren<Brain>();
		Animator = GetComponent<Animator>();
	}
	private void Start(){
		if (!_active){
			Debug.LogWarning("Creature ' " + gameObject.name + " ' attempting to run without activating;");
		}

		foreach ( Faction f in _belongsToFactions ){
			_belongsToFactionBitMask += 1 << (int)f;
		}

		foreach ( Faction f in _hatesFactions ){
			_hatesFactionsBitmask += 1 << (int)f;
		}
	}
		

	// DATA TYPE
	private struct AnimationTrigger{
		public static string Hurt = "Hurt";
		public static string Dead = "Dead";
	}
}

public struct CreatureData{
	
	public CreatureAllegiance  Allegiance;
	public CreatureRace		   Race;
	public CreatureSize 	   Size;
	public CreaturePersonality Personality;

	static public CreatureData Roll( CreatureAllegiance allegiance ){

		var cd =  new CreatureData();

		cd.Allegiance 	= allegiance;
		cd.Race			= (CreatureRace)Random.Range( 0, (int)CreatureRace.Count );
		cd.Size 		= (CreatureSize)Random.Range( 0, (int)CreatureSize.Count );
		cd.Personality  = (CreaturePersonality)Random.Range( 0, (int)CreaturePersonality.Count );

		return cd;
	}

}
public enum CreatureAllegiance{
	Player = 0,
	Enemy,
	PartyMember,
	Count
}
public enum CreatureRace{
	RedLyme = 0,
	GreenLyme,
	Count
}
public enum CreatureSize{
	Small = 0,
	Average,
	Large,
	Count
}
public enum CreaturePersonality{
	Agressive = 0,
	Submissive,
	Count
}