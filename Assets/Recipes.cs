using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Recipes{

	public List<CraftingRecipe> KnowRecipes{
		get{
			return _knownRecipes;
		}
	}
	private List<CraftingRecipe> _knownRecipes;


	public Recipes() {
		_knownRecipes = new List<CraftingRecipe>();
		_knownRecipes.Add( new WoodenFence_Recipe() );
	}
}


public abstract class CraftingRecipe {
		
	abstract public InventoryItem OutputObject{
		get;
	}
	abstract public Sprite Sprite{
		get;
	}
	abstract public List<CraftingComponent> Components{
		get;
	}

}
public struct CraftingComponent{

	public int Amount;
	public System.Type Item;

	public CraftingComponent( System.Type item, int amount ) {
		Item = item;
		Amount = amount;
	}
}

public class WoodenFence_Recipe : CraftingRecipe{

	override public InventoryItem OutputObject{
		get{
			return new WoodenFence_InventoryItem();
		}
	}

	override public Sprite Sprite{
		get{
			return Game.Resources.Axe;
		}
	}

	override public List<CraftingComponent> Components{
		get{
			var c = new List<CraftingComponent>();
			c.Add( new CraftingComponent( typeof( Wood_InventoryItem ) , 1 ) );
			return c;
		}
	}
}
