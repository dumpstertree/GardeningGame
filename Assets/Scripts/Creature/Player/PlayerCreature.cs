using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCreature : Creature {

	public void ShootProjectile(){
		Game.Controller.ScreenShake.ShakeLight();
		Game.Controller.Audio.OneShot(AudioType.Gunshot);
		var b = Instantiate( Game.Resources.Bullet ).GetComponent<BulletBehavior>();
		b.Startup( this );
	}
	public void ProjectileHit( Creature creature ){

		var r = BelongsToFactionsBitmask & creature.BelongsToFactionsBitmask;
		if ( r == 0 ){
			_target = creature;
			StateManager.GameState = GameState.InBattle;
		}

	}
}
