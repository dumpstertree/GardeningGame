using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShootInventoryItem : InventoryItem{
	
	public override InventoryItemTag Tag{
		get{
			return InventoryItemTag.None;
		}
	}
	public override string Name {
		get {
			return "Shoot";
		}
	}
	protected override int StackLimit {
		get {
			return 1;
		}
	} 
	protected override bool Consumable {
		get {
			return false;
		}
	}
	protected abstract BulletSpawnInfo BulletSpawnInfo{
		get;
	}
	protected abstract ShootInfo ShootInfo{
		get;
	}


	protected abstract int BulletSlotNumber{
		get;
	}

	// USE
	public abstract void Shoot();


	// HELPER
	protected bool InCooldown(){
		return (Time.time - _lastInteract > BulletSpawnInfo.Cooldown) ? false : true;
	}
	protected bool HasAmmo(){
		var bullets  = Inventory.Data.GetItem( Constants.BulletIndexStart+BulletSlotNumber.ToString());
		if (bullets != null && bullets.StackAmount > 0){
			return true;
		}
		return false;
	}
	protected void SpawnBullet(){
		var bullets  = Inventory.Data.GetItem( Constants.BulletIndexStart+BulletSlotNumber.ToString());
		bullets.RemoveFromStack(1);
		_lastInteract = Time.time;
		GameObject.FindObjectOfType<PlayerCreature>().ShootProjectile( BulletSpawnInfo, ShootInfo );
	}
}

public struct BulletSpawnInfo {
	public float Cooldown;
	public float BulletSpray;
	public int   BulletCount;
}

public struct ShootInfo{
	public int Power;
}
