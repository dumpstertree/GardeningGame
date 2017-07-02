using UnityEngine;

public class EnemyBody : Body {
	
	[SerializeField] private Stats _stats;

	public void Shoot( ShootInfo info ){
		_stats.HitWithProjectile( info);	
	} 
}
