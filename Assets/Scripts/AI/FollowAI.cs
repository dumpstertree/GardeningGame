using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowAI: BrainStateMachineBehavior {

	// INSTANCE VARIABLES
	private float _maxFollowDistance = 5.0f;
	private float _positionDistance = 3.0f;

	private bool  _following = false;
	private float _mps = 1.0f;


	// OVERRIDE 
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		base.OnStateEnter( animator, stateInfo, layerIndex );
		action = creature.StartCoroutine( EmoteAction() );
	}	
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
		
		var tar = creature.LeaderCreature;
		if (tar != null){

			LookAtTarget( tar.transform );
			var d = GetDistance( tar.transform );

			if (action == null){

				if (d > _maxFollowDistance){
					_following = true;
				}

				if (_following){
					Move();
				}
					
				if (d < _positionDistance){
					Wait();
				}
			}
		}
	}


	// HELPER METHODS
	private float GetDistance( Transform tar ){
		float d = Mathf.Infinity;
		if (tar != null){
			d = Vector3.Distance( transform.position, tar.transform.position );
		}

		return d;
	}
	private void LookAtTarget( Transform tar ){
		transform.LookAt(tar);
	}


	// ACTIONS
	private void Move(){
		creature.Animator.SetBool( "Move", true );
		creature.transform.Translate( new Vector3(0,0,1) * (_mps*Time.deltaTime) );
	}
	private void Position(){	
		var tiles = FindAvailableSurroundingTiles();	
		var t = tiles[ Random.Range(0,tiles.Count) ];
		var s = transform.position;
		var d = new Vector3( t._position.x, s.y, t._position.y );
		var speed = Vector3.Distance( s, d )/_mps;
		action = creature.StartCoroutine( MoveAction(speed, s, d ) );
	}
	private void Wait(){
		action = creature.StartCoroutine( WaitAction( 0.5f) );
	}
}
