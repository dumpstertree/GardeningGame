using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class BulletBehavior : MonoBehaviour {

	private LineRenderer _line;

	private void Awake(){
		_line = GetComponent<LineRenderer>();
	}
	public void Startup( PlayerCreature shooter ){

		// This Needs to be changed to gun
		var gun = FindObjectOfType<CharacterControllerScopeMode>().Gun.transform; 

		RaycastHit hit;        
		if (Physics.Raycast( Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity )) {
			
			// Hit Noise
			Game.Controller.Audio.OneShot(AudioType.TreeHit);


			// Hit Effect
			var e = Instantiate(Game.Resources.TreeHit);
			e.transform.position = hit.point;


			// Bullet Trail
			_line.SetPositions(  new Vector3[2]{gun.position, hit.point} );


			// Alert 
			var c = hit.collider.transform.GetComponent<Creature>();
			if (c != null ){

				// Alert Creature
				c.Hit( 1, shooter );
				shooter.ProjectileHit( c );
			}
		}
		else{
			
			// Bullet Trail
			_line.SetPositions(  new Vector3[2]{gun.position, gun.position + Camera.main.transform.forward*100 } );
		}


		// Destroy
		Destroy( gameObject, 0.1f );
	}
}
