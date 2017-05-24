using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_InventoryItem : InventoryItem {

	public override InventoryItemActionType Action{
		get{
			return InventoryItemActionType.None;;
		}
	}
	public override GameObject HoldItem{
		get{
			return Game.Resources.GenericHoldItem;
		}
	}
	public override List<InteractorType> Recievers{
		get{
			var r = new List<InteractorType>();
			r.Add(InteractorType.All);
			return r;
		}
	}
	public override Sprite Sprite{
		get{
			return Game.Resources.Fence;
		}
	}

	protected override int StackLimit{
		get{
			return 1;
		}
	}
	protected override bool Destructable{
		get{
			return false;
		}
	}


	// PUBLIC METHODS
	public override void Toss(){
	}
}
