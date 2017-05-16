using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fence : ConstructedObject {

	public Fence( Tile tile ) : base( tile ) {

		_dropItems.Add( new DropItem( new Fence_InventoryItem(), 1.0f, 1, 1 ) );

		// Object
		TileProperty.CreateGameObject( this, Game.Resources.WoodenFencePost );

		// Effect
		TileProperty.CreateEffect( this, Game.Resources.Fireworks );

		// Audio
		Game.Controller.Audio.OneShot(AudioType.Planted);
	} 

	public override void Update (){
		base.Update();
		UpdateFenceBehavior();
	}
	public override void Start(){
		base.Start();
		Tile.LeftTile.UpdateWholeTile();
		Tile.RightTile.UpdateWholeTile();
		Tile.UpTile.UpdateWholeTile();
		Tile.DownTile.UpdateWholeTile();
	}

	private void UpdateFenceBehavior(){

		var l = Tile.LeftTile.FindProperties(  typeof(ConstructedObject) );
		var u = Tile.UpTile.FindProperties(    typeof(ConstructedObject) );

		// LEFT
		if ( l.Count > 0 ){
			GameObject.GetComponent<FenceBehavior>().Left = true;
		}
			

		// UP
		if ( u.Count > 0 ){
			GameObject.GetComponent<FenceBehavior>().Up = true;
		}
	}
		
}
