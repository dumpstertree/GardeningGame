using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class BulletBehavior : MonoBehaviour {

	private LineRenderer _line;

	private void Awake(){
		_line = GetComponent<LineRenderer>();
	}
	public void Startup( ShootInfo info, Creature shooter){

		RaycastHit hit;        
		if (Physics.Raycast( transform.position,transform.forward, out hit, Mathf.Infinity )) {
			
			// Hit Noise
			Game.Controller.Audio.OneShot(AudioType.TreeHit);


			// Hit Effect
			var e = Instantiate(Game.Resources.Fireworks);
			e.transform.position = hit.point;


			// Bullet Trail
			_line.SetPositions(  new Vector3[2]{transform.position, hit.point} );


			// Alert 
			var c = hit.collider.transform.GetComponent<Creature>();
			if (c != null ){
				c.Hit( 1, shooter );
			}
		}
		else{
			
			// Bullet Trail
			_line.SetPositions(  new Vector3[2]{transform.position, transform.position + Camera.main.transform.forward*100 } );
		}


		// Destroy
		Destroy( gameObject, 0.1f );
	}


	public static void Create( ShootInfo info, Creature shooter, Transform gun ){
		
		for( int i = 0; i<info.BulletCount; i++){
		
			var go = Instantiate( Game.Resources.Bullet );
			var b = go.GetComponent<BulletBehavior>();
			var rot = gun.transform.rotation ;
			
			go.transform.position = gun.transform.position;
			
			rot = rot * Quaternion.AngleAxis(Random.Range(-info.BulletSpray,info.BulletSpray), gun.up);
			rot = rot * Quaternion.AngleAxis(Random.Range(-info.BulletSpray,info.BulletSpray), gun.right);
			go.transform.rotation = rot;

			b.Startup( info, shooter );
		}
	}
}
