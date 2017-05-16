using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDistributor : MonoBehaviour {

	public GameObject InventoryItemVisual;

	private void Start(){
		for ( int i =0; i< Random.Range(10, 15); i++ ){
			var go = Instantiate( InventoryItemVisual );
			go.GetComponent<InventoryItemVisualBehavior>().InventoryItem = new MapleTreeSeeds();
			go.transform.position =  new Vector3( go.transform.position.x + Random.Range(-10,10), 0.5f, go.transform.position.z + Random.Range(-10,10) );
		}
	}
}
