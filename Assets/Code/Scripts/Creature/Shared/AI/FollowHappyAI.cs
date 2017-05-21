using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHappyAI: BrainStateMachineBehavior {

	// INSTANCE VARIABLES
	private float _followDistance = 1.5f;

	private bool  _following = false;
	private float _mps = 1.0f;
	private GameObject _loveEffect;


	// OVERRIDE 
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		base.OnStateEnter( animator, stateInfo, layerIndex );
		action = creature.StartCoroutine( EmoteAction() );
		_loveEffect = Instantiate( Game.Resources.LoveEffect, transform );
		_loveEffect.transform.localPosition = Vector3.zero;
	}	
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){

		LookAtTarget( Game.Controller.PlayerSpawner.CharacterController.mesh );
		var d = GetDistance(  Game.Controller.PlayerSpawner.CharacterController.mesh );

		if (action == null){

			if (d > _followDistance){
				_following = true;
			}

			if (_following){
				Move();
			}
				
			if (d < _followDistance){
				Wait();
			}
		}
	}
	override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		base.OnStateExit(animator, stateInfo, layerIndex);
		Destroy( _loveEffect );
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

		var s = transform.position;
		var d = transform.position + transform.forward;

		ActionCleanup();
		action = creature.StartCoroutine( MoveAction( _mps, s, d) );
	}
	private void Wait(){
		ActionCleanup();
		action = creature.StartCoroutine( HappyIdleAction() );
	}
}
