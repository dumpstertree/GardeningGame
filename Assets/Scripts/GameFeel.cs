using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFeel : MonoBehaviour {


	// Creature Effects
	static public void GrowHungryEffect( Transform t ){
		Instantiate( Game.Resources.HungerEffect, t.position, Quaternion.Euler(Vector3.zero) );
	} 
	static public void FeedEffect( Transform t ){
		Instantiate( Game.Resources.FeedEffect, t.position, Quaternion.Euler(90,0,0) );
	}


	// MATERIAL EFFECT
	static public void HitWood( Transform t ){
		Game.Controller.Audio.OneShot( AudioType.TreeHit );
		Instantiate( Game.Resources.TreeHit, t.position, Quaternion.Euler(Vector3.zero) );
	}
	static public void DestroyWood( Transform t ){
		Game.Controller.Audio.OneShot( AudioType.TreeDestroy );
		Instantiate( Game.Resources.TreeDestroy, t.position, Quaternion.Euler(Vector3.zero) );
	}
}
