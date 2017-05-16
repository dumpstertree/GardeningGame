using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerFruitTree_InteractableBehavior : Plant_InteractableBehavior {

	protected override GrowthPoint[] CreateGrowthSchedule(){

		var growth = new List<GrowthPoint>();

		var dropItems = new List<DropItem>();
		dropItems.Add( new DropItem( new PowerFruitTree_InventoryItem(), 1.0f, 1, 6 ) );

		growth.Add( new GrowthPoint( Game.Resources.PowerFruitSmallMesh, 1, 0, new List<DropItem>() ));
		growth.Add( new GrowthPoint( Game.Resources.PowerFruitMediumMesh,1, 0, new List<DropItem>() ));
		growth.Add( new GrowthPoint( Game.Resources.PowerFruitLargeMesh, 0, 0, dropItems) );

		return growth.ToArray();
	}
}
