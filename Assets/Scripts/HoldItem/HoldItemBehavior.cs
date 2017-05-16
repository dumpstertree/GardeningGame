using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoldItemBehavior : MonoBehaviour {

	[SerializeField] private HoldType _hold;

	void Update () {
		
		switch(_hold){

		case HoldType.LeftHand:
			transform.position = Game.Controller.PlayerSpawner.CharacterController.LeftHand.position;
			transform.rotation = Game.Controller.PlayerSpawner.CharacterController.LeftHand.rotation;
			break;
		
		case HoldType.RightHand:
			transform.position = Game.Controller.PlayerSpawner.CharacterController.RightHand.position;
			transform.rotation = Game.Controller.PlayerSpawner.CharacterController.RightHand.rotation;
			break;
		
		}
	}
}
