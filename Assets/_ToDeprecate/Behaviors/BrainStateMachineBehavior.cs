﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrainStateMachineBehavior : StateMachineBehaviour {

	public Creature   creature;
	public GameObject gameObject;
	public Transform  transform;
	public Coroutine  action;


	// STATE OVERRIDES
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		creature = animator.gameObject.GetComponentInParent<Creature>();
		gameObject = creature.gameObject;
		transform = creature.transform;
	}
	override public void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		if (action != null){
			creature.StopCoroutine( action );
			action = null;
		}
	}


	// HELPERS
	protected void ActionCleanup(){
		if (action != null){
			creature.StopCoroutine( action );
			action = null;
		}
	}


	// ACTIONS
	protected IEnumerator MoveAction( float moveMPS, Vector3 s, Vector3 d  ) {

		creature.Animator.SetBool( "Move", true );

		for ( float t = 0.0f; t < moveMPS; t += Time.deltaTime) {

			// Position
			//creature.gameObject.GetComponent<Rigidbody>().MovePosition( Vector3.Lerp( s, d, t/moveMPS) );
			creature.transform.position = Vector3.Lerp( s, d, t/moveMPS);

			// Rotation
			var sR = creature.transform.rotation;
			creature.transform.LookAt( d );
			var tR = creature.transform.rotation;
			creature.transform.rotation = Quaternion.Slerp( sR, tR, t/moveMPS );
			yield return null;
		}

		action = null;
		yield break;
	}
	protected IEnumerator WaitAction( float waitTime ) {

		creature.Animator.SetBool( "Move", false );

		for ( float t = 0.0f; t < waitTime; t += Time.deltaTime) {
			yield return null;
		}

		action = null;
		yield break;
	}
	protected IEnumerator AttackAction() {

		creature.Animator.SetTrigger( "Attack" );

		var l = 1f;
		var e = false;
		for ( float t=0; t<l; t+=Time.deltaTime ){

			if (e == false && t < 0.5f){
				/*if (creature.Target){
					creature.Target.Hit( 1, creature );
				} */
				e = true;
			}

			yield return null;
		}

		action = null;
		yield break;
	}
	protected IEnumerator EmoteAction(  ){

		creature.Animator.SetTrigger( "Emote" );

		var l = 1f;
		for ( float t=0; t<l; t+=Time.deltaTime ){
			yield return null;
		}

		action = null;
		yield break;
		
	}

	protected IEnumerator HappyIdleAction(  ){

		creature.Animator.SetTrigger( "HappyIdle" );

		var l = 1f;
		for ( float t=0; t<l; t+=Time.deltaTime ){
			yield return null;
		}

		action = null;
		yield break;

	}
}
