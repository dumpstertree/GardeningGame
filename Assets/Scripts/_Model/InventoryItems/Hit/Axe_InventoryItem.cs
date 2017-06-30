using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe_InventoryItem : HitInventoryItem {

	public override string Name {
		get {
			return "Axe";
		}
	}

	public override HitObjectInfo HitInfo {
		get {
			var h = new HitObjectInfo();
			h.HitStrength = 1;
			h.AnimationDelay = 0.8f;
			return h;
		}
	}
	public override GameObject HoldItem{
		get{
			return Game.Resources.AxeHoldItem;
		}
	}
	public override Sprite Sprite{
		get{
			return Game.Resources.Axe;
		}
	}

	protected override int StackLimit{
		get{
			return 1;
		}
	}
	protected override bool Consumable{
		get{
			return false;
		}
	}
}
	