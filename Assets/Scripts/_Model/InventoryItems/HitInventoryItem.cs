using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct HitObjectInfo{
	public int HitStrength;
	public float AnimationDelay;
}
public abstract class HitInventoryItem : InventoryItem {


	public override InventoryItemTag Tag{
		get{
			return InventoryItemTag.None;
		}
	}

	// NEW
	public abstract HitObjectInfo HitInfo {
		get;
	}

	// USE
	public void Use( IHit on ){
		if (Time.time - _lastInteract > _waitTime){

			var result = on.Hit( HitInfo );
			_lastInteract = Time.time;

			if (result){
				Hit();
				Consume();
			}
			else{
				Miss();
			}
		}
	}
		
	private void Hit(){
		GameObject.FindObjectOfType<PlayerCreature>().Animator.SetTrigger("Swing");
		Game.Controller.Audio.OneShot(AudioType.AxeSwing);
	}
	private void Miss(){
		GameObject.FindObjectOfType<PlayerCreature>().Animator.SetTrigger("Swing");
		Game.Controller.Audio.OneShot(AudioType.AxeSwing);
	}
}
