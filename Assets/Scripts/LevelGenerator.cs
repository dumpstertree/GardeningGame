using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour {


	[SerializeField] private GameObject[] wallPrefabs;
	[SerializeField] private Texture2D map;
	private void Start(){
	
		var w = map.width;
		var h = map.height;

		for ( int x=0; x<w; x++ ){
			for ( int y=0; y<h; y++ ){
				if ( map.GetPixel(x,y) == Color.white ){
					var num = Random.Range( 0, wallPrefabs.Length );
					var i = Instantiate( wallPrefabs[num], new Vector3( x, 0, y ), Quaternion.Euler( new Vector3 (0, Random.Range(0,360), 0 ) )) ;
					var r = Random.Range( 0.5f, 1f );
					i.transform.localScale =  new Vector3( r, r, r );
				}
			}
		}
	}
}
