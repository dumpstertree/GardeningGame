using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class BulletBehavior : MonoBehaviour {

	[SerializeField] private LayerMask _layermask;
	private LineRenderer _line;

	private void Awake(){
		_line = GetComponent<LineRenderer>();
	}
	public void Startup( ShootInfo info, Creature shooter){

		RaycastHit hit;        
		if (Physics.Raycast( transform.position,transform.forward, out hit, Mathf.Infinity, _layermask)) {
			
			// Hit Noise
			Game.Controller.Audio.OneShot(AudioType.TreeHit);


			// Hit Effect
			var e = Instantiate(Game.Resources.Fireworks);
			e.transform.position = hit.point;


			// Bullet Trail
			_line.SetPositions(  new Vector3[2]{transform.position, hit.point} );


			// Alert 
			var c = hit.collider.transform.GetComponent<IShootable>();
			if (c != null ){
				c.Shoot(info);
			}
		}
		else{
			
			// Bullet Trail
			_line.SetPositions(  new Vector3[2]{transform.position, transform.position + Camera.main.transform.forward*100 } );
		}


		// Destroy
		Destroy( gameObject, 0.1f );
	}


	public static void Create( BulletSpawnInfo spawnInfo, ShootInfo shootInfo, Creature shooter, Transform gun ){
		
		for( int i = 0; i<spawnInfo.BulletCount; i++){
		
			var go = Instantiate( Game.Resources.Bullet );
			var b = go.GetComponent<BulletBehavior>();
			var rot = gun.transform.rotation ;
			
			go.transform.position = gun.transform.position;
			
			rot = rot * Quaternion.AngleAxis(Random.Range(-spawnInfo.BulletSpray,spawnInfo.BulletSpray), gun.up);
			rot = rot * Quaternion.AngleAxis(Random.Range(-spawnInfo.BulletSpray,spawnInfo.BulletSpray), gun.right);
			go.transform.rotation = rot;

			b.Startup( shootInfo, shooter );
		}
	}
}
