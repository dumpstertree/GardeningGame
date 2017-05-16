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
		var tiles = FindAvailableSurroundingTiles();
		var t = tiles[Random.Range(0,tiles.Count)];
		var s = transform.position;
		var d = new Vector3( t._position.x, transform.position.y, t._position.y );

		ActionCleanup();
		action = creature.StartCoroutine( MoveAction( _moveMPS, s, d ) );
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
