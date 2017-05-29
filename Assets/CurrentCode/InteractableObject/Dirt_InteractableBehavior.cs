using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dirt_InteractableBehavior : InteractableBehavior, IPlant {

	public bool Plant( PlantableObjectInfo info ){
		base.Plant( info );
		return true;
	}
}
