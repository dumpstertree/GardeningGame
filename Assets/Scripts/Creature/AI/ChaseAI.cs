using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseAI : BrainStateMachineBehavior {

	// INSTANCE VARIABLES
	private float _attackDistance = 2.0f;
	private float _maxMoveSpeed = 2;


	// OVERRIDE 
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		base.OnStateEnter( animator, stateInfo, layerIndex );
		action = creature.StartCoroutine( EmoteAction() );
	}	
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){
		
		/*
		var tar = creature.Target;
		if (tar != null){

			LookAtTarget( tar.transform );
			var d = GetDistance( tar.transform );

			if (action == null){

				if (d <= _attackDistance){
					Attack();
				}

				else{
					Move();
				}
			}
		}*/
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
		creature.transform.Translate( new Vector3(0,0,1) * (_maxMoveSpeed*Time.deltaTime) );
	}
	private void Attack(){
		ActionCleanup();
		action = creature.StartCoroutine( AttackAction() );
	}
}
