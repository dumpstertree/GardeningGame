using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapleTree : Tree {

	public MapleTree( Tile tile ) : base( tile ) {}

	public override GrowthPoint[] CreateGrowthSchedule(){
		var growth = new List<GrowthPoint>();

		var dropItems = new List<DropItem>();
		dropItems.Add( new DropItem( new MapleTreeSeeds(), 1.0f, 1, 6 ) );

		growth.Add( new GrowthPoint( Game.Resources.RedTree_Small,  Game.Resources.Fireworks, 1, 0, dropItems) );
		growth.Add( new GrowthPoint( Game.Resources.RedTree_Medium, Game.Resources.Fireworks, 1, 0, dropItems) );
		growth.Add( new GrowthPoint( Game.Resources.RedTree_Large,  Game.Resources.Fireworks, 0, 0, dropItems) );
		return growth.ToArray();
	}
}
