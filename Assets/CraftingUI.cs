using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingUI : MonoBehaviour {

	private List<CraftingRecipe> _recipes;
	[SerializeField] private GameObject _recipeIconPrefab;

	private int _rows = 10;
	private int _collumns = 4;
	private int _spacing = 100;

	void Start () {
		_recipes = new Recipes().KnowRecipes;

		var x = 0;
		var y = 0;

		foreach( CraftingRecipe r in _recipes ){

			var icon = Instantiate( _recipeIconPrefab ).GetComponent<RectTransform>();
			icon.SetParent( transform, false );
			icon.anchoredPosition3D = new Vector3( _spacing + _spacing*x + 100*x, -(_spacing + _spacing*y + 100*y), 0 );

			icon.GetComponent<RecipeIconBehavior>().Recipe = r;

			x++;
			if ( x >= _collumns ){
				x = 0;
				y+= 1;
			}
		}
	}
}
