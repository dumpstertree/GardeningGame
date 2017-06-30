using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Food{

	public abstract class Food : FeedInventoryItem {
		protected override int StackLimit{
			get{
				return 99;
			}
		}
		protected override bool Consumable{
			get{
				return true;
			}
		}
	}


	public class PowerBerry : Food {
		public override string Name {
			get {
				return "Power Berry";
			}
		}
		public override Sprite Sprite{
			get{
				return Game.Resources.PowerBerrySprite;
			}
		}
		public override GameObject HoldItem{
			get{
				return Game.Resources.GenericHoldItem;
			}
		}
		public override FoodEffect FoodEffect {
			get{
				return new FoodEffect(1,1,0,0,0,0,0);
			}
		}
	}
	public class DefenceBerry : Food {
		public override string Name {
			get {
				return "Defence Berry";
			}
		}
		public override Sprite Sprite{
			get{
				return Game.Resources.DefenceBerrySprite;
			}
		}
		public override GameObject HoldItem{
			get{
				return Game.Resources.GenericHoldItem;
			}
		}
		public override FoodEffect FoodEffect {
			get{
				return new FoodEffect(1,0,0,1,0,0,0);
			}
		}
	}
	public class SpeedBerry : Food {
		public override string Name {
			get {
				return "Speed Berry";
			}
		}
		public override Sprite Sprite{
			get{
				return Game.Resources.SpeedBerrySprite;
			}
		}
		public override GameObject HoldItem{
			get{
				return Game.Resources.GenericHoldItem;
			}
		}
		public override FoodEffect FoodEffect {
			get{
				return new FoodEffect(1,0,0,0,0,1,0);
			}
		}
	}
}
