using UnityEngine;

public abstract class Body : MonoBehaviour {

	public Stats Stats;


	// PROPERTIES
	public Visibility CreatureVisiblity{
		get{
			return new Visibility( this );
		}
	}


	// PUBLIC
	public void Load( CreatureData data ){
	}
	public void Hit( int rawDamage ){
	}
	
	
	// LISTENER REQUESTS
	public void AddListenerToStats( IStats l ){
		Stats.AddListenerToStats( l );
	}
	public void RemoveListenerFromStats( IStats l ){
		Stats.RemoveListenerFromStats( l );
	}


	// MONO 
	private void Start(){
		Stats = GetComponentInChildren<Stats>();
	}
}

public struct Visibility{

	public int CurrentHealth;
	public Visibility( Body body ){
		CurrentHealth = body.Stats.Health;
	}
}