using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoamingAI : BrainStateMachineBehavior {

	[SerializeField] private float _moveMPS = .5f;
	[SerializeField] private float _minWaitTime = 0.5f;
	[SerializeField] private float _maxWaitTime = 2.0f;


	// OVERRIDE METHODS
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){

		if (action == null){
			var a = ChooseAction();
			switch( a ){
			case ActionType.Wait:
				Wait();
				break;
			case ActionType.Move:
				Move();
				break;
			}
		}
	}


	// ACTION SETUP
	private void Wait(){
		var r = Random.Range( _minWaitTime, _maxWaitTime );

		ActionCleanup();
		action = creature.StartCoroutine(WaitAction( r ) );
	}
	private void Move(){

		var minDistance = 1.0f;
		var maxDistance = 5.0f;
		var rays = 8;
		var angle = Random.Range( 0, rays) * 45;
		var distance = 0.0f;

		Vector3 newVector = (Quaternion.AngleAxis(angle, new Vector3(0, 1, 0)) * transform.forward);
		Ray ray = new Ray (transform.position, newVector);
		RaycastHit hit;

		if( Physics.Raycast(ray, out hit, maxDistance)){
			distance = Vector3.Distance( transform.position, hit.point ) - minDistance;
			if ( distance < 0){
				return;
			}
		}
		else{
			distance = maxDistance-minDistance;
		}
			
		var s = transform.position;
		var d = transform.position + (newVector * distance);

		ActionCleanup();
		action = creature.StartCoroutine( MoveAction( distance/_moveMPS, s, d ) );
	}


	// PRIVATE METHODS
	private ActionType ChooseAction(){
		var r = Random.Range( 0,  (int)ActionType.Count );
		return (ActionType)r;
	}


	// DATA TYPES
	private enum ActionType{
		Wait,
		Move,
		Count
	}
}
