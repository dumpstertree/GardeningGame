using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableBehavior : MonoBehaviour {

	protected List<DropItem> _dropItems = new List<DropItem>();
	protected int 		     _maxHealth = 3; // Set to -1 for Invincible
	private int 			 _health = 3;

	protected bool Plant( PlantableObjectInfo info ){
		if ( info.Prefab ){
			Instantiate( info.Prefab, transform.position, Quaternion.identity );
			return true;
		}
		else{
			Debug.LogWarning( "Trying to place an object but none assigned to InventoryItem;" );
			return false;
		}
	}
	protected bool Feed( FoodEffect info ){
		return true;
	}
	protected bool Hit( HitObjectInfo info ){
		StartCoroutine( HitAfterWaitCoroutine( info ) );
		return true;
	}


	// HIT
	private IEnumerator HitAfterWaitCoroutine ( HitObjectInfo info ){

		for (float t=0; t<info.AnimationDelay; t+= Time.deltaTime){
			yield return null;
		}
			
		HitAfterWait( info );
	}
	protected virtual void HitAfterWait( HitObjectInfo info ){

		if (_maxHealth != -1 ){
			_health -= info.HitStrength;
			if (_health <= 0){
				Destroy();
			}
		}
	}
	protected void Destroy(){ 

		var drops = DropRoller.Roll(_dropItems);
		foreach ( InventoryItem item in drops ){
			var go = GameObject.Instantiate( Game.Resources.InventoryItemVisual, transform.position+new Vector3(0,0.2f,0), Quaternion.Euler(Vector3.zero)  );
			var b = go.GetComponent<InventoryItem3D>();
			b.InventoryItem = item;
		}

		var a = GetComponent<Animator>();
		a.SetTrigger("Destroy");
		Destroy(gameObject, a.GetCurrentAnimatorStateInfo(0).length);
	}


}
public interface IHit{
	bool Hit( HitObjectInfo info );
}
public interface IFeed{
	bool Feed( FoodEffect info );
}
public interface IPlant{
	bool Plant( PlantableObjectInfo info );
}
public interface IInteract{
	void Interact();
}