using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FenceBeamBehavior : MonoBehaviour {

	public FenceBehavior FencePost1;
	public FenceBehavior FencePost2;

	private void OnDestroy(){
		if (FencePost1){
			FencePost1.FenceBeamDestroyed(this);
		}
		if (FencePost2){
			FencePost2.FenceBeamDestroyed(this);
		}
	}
	private void Start(){
		if (FencePost1 && FencePost2){
			transform.position =  Vector3.Lerp( FencePost1.transform.position, FencePost2.transform.position, .5f );
			transform.localScale = new Vector3( 1, 1, Vector3.Distance(FencePost1.transform.position, FencePost2.transform.position)  );
			transform.LookAt(FencePost1.transform);
		}
	}
}
