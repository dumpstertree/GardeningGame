using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericFood_InventoryItem : Food {
	public GenericFood_InventoryItem() : base(){
		_sprite = Game.Resources.Hoe;
		_effect = new FoodEffect(1,1,1,1,1,1,1);
	}
}
