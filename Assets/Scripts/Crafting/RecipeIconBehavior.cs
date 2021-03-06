﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecipeIconBehavior : MonoBehaviour, IInventory2 {

	public CraftingUI Delegate{
		set{
			_delegate = value;
		}
	}
	private CraftingUI _delegate;

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
		Inventory.Data.StartListening( this );
		_button.onClick.AddListener(() => ButtonPressed());
	}


	// PRIVATE
	private void ButtonPressed(){

		_delegate.RecipeIconChanged( _recipe );
		/*foreach ( CraftingComponent c in _recipe.Components ){
			for ( int i =0; i<c.Amount-1; i++ ){
				Inventory.RemoveItem( c.Item );
			}
		}
		Inventory.AddItem( _recipe.OutputObject );*/
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
			var count = Inventory.Data.GetContentsCount( c.Item );
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
		//_button.interactable = false;
	}
		

	// IINVENTORY
	public void DataSourceChanged( string atIndex ){
		UpdateVisual();
	}
}
