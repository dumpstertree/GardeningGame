using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameResources: MonoBehaviour {
		
	[Header ("Prefabs") ]
	public GameObject InteractorVisual;
	public GameObject WorldSpaceTooltipVisual;
	public GameObject InventoryItemVisual;
	public GameObject FloatingNumber;

	public GameObject WoodenFencePost;
	public GameObject DirtTile;

	public GameObject Bullet;

	public GameObject PowerBerryTreePrefab;


	[Header ("Hold Item Prefabs")]
	public GameObject GenericHoldItem;
	public GameObject GunHoldItem;
	public GameObject AxeHoldItem;



	[Header ("Sprites") ]
	public Sprite MapleSeed;
	public Sprite Axe;
	public Sprite Hoe;
	public Sprite Fence;
	public Sprite Gun;
	public Sprite DefenceBerrySprite;
	public Sprite PowerBerrySprite;


	[Header ("Effects") ]
	public GameObject Fireworks;
	public GameObject TreeHit;
	public GameObject TreeDestroy;
	public GameObject HitEffect;
	public GameObject HungerEffect;
	public GameObject FeedEffect;
	public GameObject LoveEffect;

	public GameObject SmallGreenSlime;
	public GameObject MedGreenSlime;
	public GameObject LargeGreenSlime;
	public GameObject SmallRedSlime;
	public GameObject MedRedSlime;
	public GameObject LargeRedSlime;

	[Header ("Mesh")]
	public Mesh PowerFruitSmallMesh;
	public Mesh PowerFruitMediumMesh;
	public Mesh PowerFruitLargeMesh;
}
