using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeIconBehavior : MonoBehaviour, IInventory {


	public CraftingRecipe Recipe{
		set{
			_recipe = value;
			UpdateVisual();
		}
	}
	private CraftingRecipe _recipe;

	[SerializeField] Image _icon;
	private Button _button;


	// MONO
	private void Awake(){
		_button = GetComponent<Button>();
	}
	private void Start(){
		Inventory.Delegate = this;
		_button.onClick.AddListener(() => ButtonPressed());
	}


	// PRIVATE
	private void ButtonPressed(){
		foreach ( CraftingComponent c in _recipe.Components ){
			Inventory.RemoveItemFromInventory( c.Item, c.Amount );
		}
		Inventory.AddToInventory( _recipe.OutputObject );
	}
	private void UpdateVisual(){

		_icon.sprite =  _recipe.Sprite;
	
		if ( CheckSupplies() ){
			SetActive();
		}
		else{
			SetInactive();
		}
	}

	private bool CheckSupplies(){
		foreach ( CraftingComponent c in _recipe.Components ){
			var count = Inventory.GetAmountInInventory( c.Item );
			if ( count < c.Amount ){
				return false;
			}
		}
		return true;
	}
	private void SetActive(){
		_button.interactable = true;
	}
	private void SetInactive(){
		_button.interactable = false;
	}
		

	// IINVENTORY
	public void InventoryChanged(){
		UpdateVisual();
	}
}
