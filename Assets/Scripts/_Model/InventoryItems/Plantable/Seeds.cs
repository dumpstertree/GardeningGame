using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Seeds{


	public abstract class Seeds : PlantInventoryItem {

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
		

	public class PowerPlantSeed: Seeds {
		public override string Name {
			get {
				return "Power Plant Seed";
			}
		}
		public override Sprite Sprite{
			get{
				return Game.Resources.PowerPlantSeedSprite;
			}
		}
		public override GameObject HoldItem{
			get{
				return Game.Resources.GenericHoldItem;
			}
		}
		public override PlantableObjectInfo PlaceableInfo {
			get{
				var p = new PlantableObjectInfo();
				p.Prefab = Game.Resources.PowerBerryTreePrefab;
				return p;
			}
		}
	}
	public class DefencePlantSeed : Seeds {
		public override string Name {
			get {
				return "Defence Plant Seed";
			}
		}
		public override Sprite Sprite{
			get{
				return Game.Resources.DefencePlantSeedSprite;
			}
		}
		public override GameObject HoldItem{
			get{
				return Game.Resources.GenericHoldItem;
			}
		}
		public override PlantableObjectInfo PlaceableInfo {
			get{
				var p = new PlantableObjectInfo();
				p.Prefab = Game.Resources.PowerBerryTreePrefab;
				return p;
			}
		}
	}
	public class SpeedPlantSeed : Seeds {
		public override string Name {
			get {
				return "Speed Plant Seed";
			}
		}
		public override Sprite Sprite{
			get{
				return Game.Resources.SpeedPlantSeedSprite;
			}
		}
		public override GameObject HoldItem{
			get{
				return Game.Resources.GenericHoldItem;
			}
		}
		public override PlantableObjectInfo PlaceableInfo {
			get{
				var p = new PlantableObjectInfo();
				p.Prefab = Game.Resources.PowerBerryTreePrefab;
				return p;
			}
		}
	}
}
