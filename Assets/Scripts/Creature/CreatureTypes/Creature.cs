using UnityEngine;

public abstract class Creature : MonoBehaviour {


	// PROPERTIES
	public int BelongsToFactionsBitmask {
		get{
			return _belongsToFactionBitMask;
		}
	}
	public int HatesFactionsBitmask {
		get{
			return _hatesFactionsBitmask;
		}
	}
	public Animator Animator{
		get{
			return _animator;
		}
	}
	public abstract Brain Brain{
		get;
	}
	public abstract Body Body{
		get;
	}


	// INSTANCE
	private int _belongsToFactionBitMask = 0;
	private int _hatesFactionsBitmask 	 = 0;

	[SerializeField] private Faction[] _belongsToFactions;
	[SerializeField] private Faction[] _hatesFactions;
	[SerializeField] public Animator _animator;


	// PUBLIC METHODS
	public void Hit( int rawDamage, Creature dealer ){
		//Body.Hit( rawDamage );
		//Brain.Hit( dealer );
	}
	public void Create(){
		var data = CreatureData.Roll();
		Body.Load( data );
		Brain.Load( data );
	}


	// MONO
	private void Awake(){

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
	private struct ERROR{
		public static string NoBrain 	= "No Brain found on creature;";
		public static string NoBody 	= "No Body found on creature;";
		public static string NoAnimator = "No Animator found on creature;";
	}
}
public enum Faction{
	Player 	  = 0,
	GreenLyme = 1,
	RedLyme	  = 2
}
public struct CreatureData{

	public CreatureRace		   Race;
	public CreatureSize 	   Size;
	public CreaturePersonality Personality;

	static public CreatureData Roll(){

		var cd =  new CreatureData();

		cd.Race			= (CreatureRace)Random.Range( 0, (int)CreatureRace.Count );
		cd.Size 		= (CreatureSize)Random.Range( 0, (int)CreatureSize.Count );
		cd.Personality  = (CreaturePersonality)Random.Range( 0, (int)CreaturePersonality.Count );

		return cd;
	}
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