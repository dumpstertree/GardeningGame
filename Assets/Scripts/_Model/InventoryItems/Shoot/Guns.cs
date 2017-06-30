using UnityEngine;

namespace Guns{

	public class MachineGun : ShootInventoryItem{

		public override GameObject HoldItem {
			get {
				return Game.Resources.GunHoldItem;
			}
		}
		public override Sprite Sprite {
			get {
				return Game.Resources.Sprites.MachineGun;
			}
		}
	 	protected override ShootInfo ShootInfo {
			get{
				var info = new ShootInfo();
				info.BulletCount = 3;
				info.BulletSpray = 3;
				info.Cooldown = .1f;
				return info;
			}
		}
		protected override int BulletSlotNumber{
			get{
				return 2;
			}
		}

		public override void Shoot(){

			if( !InCooldown() && HasAmmo() ){
				SpawnBullet();
			}
		}
	}

	public class ShotGun : ShootInventoryItem{

		public override GameObject HoldItem {
			get {
				return Game.Resources.GunHoldItem;
			}
		}
		public override Sprite Sprite {
			get {
				return Game.Resources.Sprites.Shotgun;
			}
		}
	 	protected override ShootInfo ShootInfo {
			get{
				var info = new ShootInfo();
				info.BulletCount = 10;
				info.BulletSpray = 10;
				info.Cooldown = 1f;
				return info;
			}
		}
		protected override int BulletSlotNumber{
			get{
				return 1;
			}
		}


		public override void Shoot(){

			if( !InCooldown() && HasAmmo() ){
				SpawnBullet();
			}
		}
	}

	public class Pistol : ShootInventoryItem{

		public override GameObject HoldItem {
			get {
				return Game.Resources.GunHoldItem;
			}
		}
		public override Sprite Sprite {
			get {
				return Game.Resources.Gun;
			}
		}
	 	protected override ShootInfo ShootInfo {
			get{
				var info = new ShootInfo();
				info.BulletCount = 1;
				info.BulletSpray = 1;
				info.Cooldown = .5f;
				return info;
			}
		}
		protected override int BulletSlotNumber{
			get{
				return 0;
			}
		}


		public override void Shoot(){

			if( !InCooldown() && HasAmmo() ){
				SpawnBullet();
			}
		}
	}
}