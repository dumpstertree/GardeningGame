using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadAI : StateMachineBehaviour {

	private float t = 0.0f;
	private float l = 0.0f;

	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		animator.gameObject.GetComponentInParent<Creature>().Animator.SetBool("Dead",true);
		l = animator.gameObject.GetComponentInParent<Creature>().Animator.GetCurrentAnimatorStateInfo(0).length;
	}
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex){

		t += Time.deltaTime;
		if (t > l){
			var e = Instantiate( Game.Resources.Fireworks );
			e.transform.position = animator.transform.position;
			//animator.gameObject.GetComponentInParent<Creature>().DestroyCorpse();
		}
	}
}