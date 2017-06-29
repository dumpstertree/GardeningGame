using UnityEngine;

public class EnemyCreature : Creature {

	public override Brain Brain{
		get{
			return null;
		}
	}
	public override Body Body{
		get{
			return null;
		}
	}


	// INSTANCE
	[SerializeField] private EnemyBrain _brain;
	[SerializeField] private EnemyBody  _body;
}
