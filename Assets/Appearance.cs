using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Appearance : MonoBehaviour {


	// PROPERTIES
	public Visibility CreatureVisiblity{
		get{
			return new Visibility( GetComponentInParent<Creature>() );
		}
	}
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


	// INSTANCE VARAIBLES
	private int _belongsToFactionBitMask = 0;
	private int _hatesFactionsBitmask 	 = 0;
	[SerializeField] private Faction[] BelongsToFactions;
	[SerializeField] private Faction[] HatesFactions;


	// MONO
	private void Start(){

		foreach ( Faction f in BelongsToFactions ){
			_belongsToFactionBitMask += 1 << (int)f;
		}

		foreach ( Faction f in HatesFactions ){
			_hatesFactionsBitmask += 1 << (int)f;
		}
	}
}

public struct Visibility{

	public int CurrentHealth;
	public Visibility( Creature creature ){
		CurrentHealth = creature.CreatureStats.Health;
	}
}

public enum Faction{
	Player 	  = 0,
	GreenLyme = 1,
	RedLyme	  = 2
}
