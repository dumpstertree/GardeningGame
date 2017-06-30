using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public struct PlaceableObjectInfo{
	public GameObject Prefab;	
}
public abstract class FreePlaceInventoryItem : InventoryItem {


	public override InventoryItemTag Tag{
		get{
			return InventoryItemTag.None;
		}
	}

	// NEW
	public abstract PlaceableObjectInfo PlaceableInfo {
		get;
	}


	// METHOD
	public void Use ( Interactor interactor ){

		if (Time.time - _lastInteract > _waitTime){
			_lastInteract = Time.time;
			Debug.Log( Time.time );

			if (PlaceableInfo.Prefab){
				GameObject.Instantiate( PlaceableInfo.Prefab, interactor.transform.position, Quaternion.Euler(Vector3.zero));
				Placed();
				Consume();
			}
			else{
				NotPlaced();
			}
		}
	}

	private void Placed(){
		GameObject.FindObjectOfType<CustomCharacterController>().Animator.SetTrigger("Dig");
	}

	private void NotPlaced(){
		Debug.LogWarning("Tried to create a placeable object that did no exist;");
	}
		
}