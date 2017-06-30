using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlantInventoryItem : InventoryItem {		

	public override InventoryItemTag Tag{
		get{
			return InventoryItemTag.None;
		}
	}

	// NEW
	public abstract PlantableObjectInfo PlaceableInfo {
		get;
	}


	// USE
	public void Use( IPlant on ){
		if (Time.time - _lastInteract > _waitTime){

			var result = on.Plant( PlaceableInfo );
			_lastInteract = Time.time;

			if (result){
				Planted();
				Consume();
			}
			else{
				CouldNotPlant();
			}
				
		}
	}

	private void Planted(){
		GameObject.FindObjectOfType<CustomCharacterController>().Animator.SetTrigger("Plant");
	}
	private void CouldNotPlant(){
	}
}
public struct PlantableObjectInfo{
	public GameObject Prefab;	
}