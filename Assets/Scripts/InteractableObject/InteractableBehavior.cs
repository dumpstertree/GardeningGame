using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class InteractableBehavior : MonoBehaviour {

	protected List<DropItem> _dropItems;
	protected int _maxHealth = 3; // Set to -1 for Invincible
	private int _health;
	private bool _ignoreInput;

	// MONO
	protected void Awake(){
		_dropItems = new List<DropItem>();
	}
	protected void Start(){
		_health  = _maxHealth;
	}


	// PROTECTED
	public bool Plant( PlantableObjectInfo info ){
		
		if (_ignoreInput){
			return false;
		}

		StartCoroutine( PlantAfterWaitCoroutine( info, .5f ) );
		return false;
	}
	public bool Hit( HitObjectInfo info ){
		
		if (_ignoreInput){
			return false;
		}

		StartCoroutine( HitAfterWaitCoroutine( info ) );
		return true;
	}
	public bool Feed( FoodEffect info ){

		if (_ignoreInput){
			return false;
		}

		StartCoroutine( FeedAfterWaitCoroutine( info, .5f ) );
		return true;
	}
	public void Interact(){

		if (_ignoreInput){
			return;
		}

		StartCoroutine( InteractAfterWaitCoroutine( 0.5f ) );
	}
	public void Shoot( ShootInfo info ){

		if (_ignoreInput){
			return;
		}

		if (_maxHealth != -1 ){
			_health -= 1;
			if (_health <= 0){
				DropItems();
				Destroy();
			}
		}

		GameFeel.HitWood(transform);
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


	protected virtual void DropItems(){
		
		var pos = transform.position;
		var drops = DropRoller.Roll(_dropItems);
		if (drops.Count > 0){
			var item =  drops[0];
			item.AddToStack( drops.Count-1 );
			InventoryItem3D.Create( item, pos);
		}
	}
	protected virtual void Destroy(){ 
		
		_ignoreInput = true;

		var c = GetComponent<Collider>();
		if ( c ){
			c.enabled = false;
		}

		var rb = GetComponent<Rigidbody>();
		if ( rb ){
			rb.isKinematic = true;
		}
	
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
public interface IShootable{
	void Shoot( ShootInfo info );
}