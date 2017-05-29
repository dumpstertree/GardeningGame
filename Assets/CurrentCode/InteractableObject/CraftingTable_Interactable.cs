using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingTable_Interactable : InteractableBehavior, IInteract  {

	private void Awake(){
		_maxHealth = -1;
	}

	public void Interact(){
		print("interact");
		Game.Controller.UI.UIPanel = UIPanel.Crafting;
	}
}
