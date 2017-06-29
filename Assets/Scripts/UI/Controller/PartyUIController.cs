using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyUIController : MonoBehaviour {


	[SerializeField] private float 	_spacing;
	[SerializeField] GameObject 	_partyMemberUIPrefab;


	// INSTANCE VARIABLES
	private List<GameObject> _partyMemberUIInstances;


	// PRIVATE METHODS
	private void Awake(){
		_partyMemberUIInstances = new List<GameObject>();
	}
	private void Start(){
		CreatePlayerUI();
		CreatePartyUI();
	}
	private void CreatePlayerUI(){
		var go = InstantiateUI();
		go.GetComponent<PartyMemberUIBehavior>().Startup(  Game.Controller.PlayerSpawner.CharacterController.GetComponentInChildren<PlayerCreature>() );
		_partyMemberUIInstances.Add( go );
	}
	private void CreatePartyUI(){
/*
		var party = Game.Controller.PlayerSpawner.CharacterController.GetComponentInChildren<PlayerCreature>().Party;

		foreach( Creature c in party ){
			var go = InstantiateUI();
			go.GetComponent<PartyMemberUIBehavior>().Startup( c );
			_partyMemberUIInstances.Add( go );
		}*/
	}
	private GameObject InstantiateUI(){
		var go =  Instantiate( _partyMemberUIPrefab );
		var rect = go.GetComponent<RectTransform>();
		go.transform.SetParent( GameObject.FindWithTag("ScreenSpaceCanvas").transform );
		rect.anchoredPosition3D = new Vector3( _spacing, -((_spacing+rect.sizeDelta.y) * _partyMemberUIInstances.Count + _spacing), 0);
		return go;
	}
}
