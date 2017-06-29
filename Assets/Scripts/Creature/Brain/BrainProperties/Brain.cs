using UnityEngine;

public abstract class Brain : MonoBehaviour {

	protected abstract Creature Creature{
		get;
	}


	// MONO
	private void Update(){
		Think();
	}


	// PUBLIC
	public void Load( CreatureData data ){
	}
	public void Hit( Creature dealer ){
	}


	protected abstract void Think();
}

