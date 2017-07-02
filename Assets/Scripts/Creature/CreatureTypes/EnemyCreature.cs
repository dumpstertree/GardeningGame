using UnityEngine;

public class EnemyCreature : Creature, IShootable {

	public override Brain Brain{
		get{
			return _brain;
		}
	}
	public override Body Body{
		get{
			return _body;
		}
	}

	[SerializeField] private EnemyBrain _brain;
	[SerializeField] private EnemyBody _body;

	public void Shoot( ShootInfo info ){
		_brain.Shoot( info );
		_body.Shoot( info );
	}
}
