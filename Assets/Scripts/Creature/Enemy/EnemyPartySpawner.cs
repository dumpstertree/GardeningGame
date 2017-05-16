using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPartySpawner : MonoBehaviour {

	[SerializeField] private CreatureSpawnTemplate[] Creatures;

	private void Start(){

		Creature leader = null;
		List<Creature> party = new List<Creature>();

		var i = 0;
		foreach( CreatureSpawnTemplate st in Creatures ){

			GameObject go;

			switch (st.Type){
			case CreatureType.SmallGreenSlime:
				go = Instantiate( Game.Resources.SmallGreenSlime );
				break;

			case CreatureType.MedGreenSlime:
				go = Instantiate( Game.Resources.MedGreenSlime );
				break;
			
			case CreatureType.LargeGreenSlime:
				go = Instantiate( Game.Resources.LargeGreenSlime );
				break;
			
			case CreatureType.SmallRedSlime:
				go = Instantiate( Game.Resources.SmallRedSlime );
				break;
			
			case CreatureType.MedRedSlime:
				go = Instantiate( Game.Resources.MedRedSlime );
				break;
			
			case CreatureType.LargeRedSlime:
				go = Instantiate( Game.Resources.LargeRedSlime );
				break;
			
			default:
				go = Instantiate( Game.Resources.SmallGreenSlime );
				break;
			}

			go.transform.position = new Vector3( transform.position.x, 0, transform.position.z + i );
			var c = go.GetComponent<Creature>();
			c.CreaturePersonality = st.Personality;
			if (st.Leader){
				leader = c;
			}
			party.Add( c );
			i++;
		}

		foreach( Creature c in party ){
			c.Party = party;
			c.LeaderCreature = leader;
		}
	}

	[System.Serializable]
	private class CreatureSpawnTemplate{

		public CreatureType Type;
		public bool Leader;
		public Personality Personality;
	}

	public enum CreatureType{
		SmallGreenSlime,
		MedGreenSlime,
		LargeGreenSlime,
		SmallRedSlime,
		MedRedSlime,
		LargeRedSlime,
	}

}
