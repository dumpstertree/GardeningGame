using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableBehavior : MonoBehaviour {

	protected List<DropItem> _dropItems;
	protected int 		     _maxHealth = 3; // Set to -1 for Invincible
	private int 			 _health = 3;


	// MONO
	private void Awake(){

		_dropItems = new List<DropItem>();
	}


	// PROTECTED
	public bool Plant( PlantableObjectInfo info ){
		StartCoroutine( PlantAfterWaitCoroutine( info, .5f ) );
		return false;
	}
	public bool Hit( HitObjectInfo info ){
		StartCoroutine( HitAfterWaitCoroutine( info ) );
		return true;
	}
	public bool Feed( FoodEffect info ){
		StartCoroutine( FeedAfterWaitCoroutine( info, .5f ) );
		return true;
	}
	public void Interact(){

		StartCoroutine( InteractAfterWaitCoroutine( 0.5f ) );
	} 


	protected virtual void PlantAfterWait( PlantableObjectInfo info ){
		if ( info.Prefab ){
			Instantiate( info.Prefab, transform.position, Quaternion.identity );
		}
	}
	protected virtual void HitAfterWait( HitObjectInfo info ){

		if (_maxHealth != -1 ){
			_health -= info.HitStrength;
			if (_health <= 0){
				DropItems();
				Destroy();
			}
		}
	}
	protected virtual void FeedAfterWait( FoodEffect info ){	
	}
	protected virtual void InteractAfterWait(){
	}


	protected void DropItems(){
		var pos = transform.position;
		var drops = DropRoller.Roll(_dropItems);
		var item =  drops[0];
		item.AddToStack( drops.Count );
		InventoryItem3D.Create( item, pos);
	}
	protected void Destroy(){ 
		var a = GetComponent<Animator>();
		a.SetTrigger("Destroy");
		Destroy(gameObject, a.GetCurrentAnimatorStateInfo(0).length);
	}


	// PRIVATE
	private IEnumerator PlantAfterWaitCoroutine ( PlantableObjectInfo info, float delay ){

		for (float t=0; t<delay; t+= Time.deltaTime){
			yield return null;
		}
			
		PlantAfterWait( info );
	}
	private IEnumerator HitAfterWaitCoroutine ( HitObjectInfo info ){

		for (float t=0; t<info.AnimationDelay; t+= Time.deltaTime){
			yield return null;
		}
			
		HitAfterWait( info );
	}
	private IEnumerator FeedAfterWaitCoroutine ( FoodEffect info, float delay ){

		for (float t=0; t<delay; t+= Time.deltaTime){
			yield return null;
		}
			
		FeedAfterWait( info );
	}
	private IEnumerator InteractAfterWaitCoroutine ( float delay ){

		for (float t=0; t<delay; t+= Time.deltaTime){
			yield return null;
		}
			
		InteractAfterWait( );
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