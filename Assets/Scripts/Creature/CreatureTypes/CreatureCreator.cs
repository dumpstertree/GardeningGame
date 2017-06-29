using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureCreator : MonoBehaviour, IScheduledEvent {

	[SerializeField] private GameObject _creaturePrefab;

	void Start () {
		Game.Controller.WorldTime.ScheduleAlert( this, 1, 0 );
	}
		
	public void RunScheduledEvent(){
		
		var c = Instantiate(_creaturePrefab);
		c.transform.position = transform.position;
		c.GetComponent<Creature>().Create();

		GameFeel.EggHatched( transform );
		Destroy( gameObject );
	}

}
