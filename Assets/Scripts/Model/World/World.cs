using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {

	// PROPERTIES
	public int Width{
		get{
			return width;
		}
	}
	public int Height{
		get{
			return height;
		}
	}


	// INSTANCE VARIABLES
	[SerializeField] private Tile[,] world;
	[SerializeField] private int width;
	[SerializeField] private int height;

	private int FarmableRange = 20;


	// PUBLIC METHODS
	public Tile GetTile( int x, int y ){

		if (x > width || x < 0){
			return null;
		}
		if (y > height || y < 0){
			return null;
		}

		return world[x,y];
	}


	// PRIVATE METHODS
	private void Awake(){
		Generate();
	}
	private void Generate(){

		world = new Tile[width,height];

		for( int x=0; x<width; x++ ){
			for( int y=0; y<height; y++ ){

				if ( y >= Height/2-FarmableRange/2 && y <= Height/2+FarmableRange/2 &&
					x >= Width/2-FarmableRange/2 && x <= Width/2+FarmableRange/2){

					var t = new Tile(x,y, TerrainType.Dirt, true );
					world[ x, y ] = t;
				}
				else{
					
					var t = new Tile(x,y, TerrainType.Grass, false );
					world[ x, y ] = t;
				}
			}
		}
	}
}
