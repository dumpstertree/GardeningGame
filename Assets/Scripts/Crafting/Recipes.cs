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
		_knownRecipes.Add( new Bastard_Recipe() );
	}
}


public abstract class CraftingRecipe {
		
	abstract public string RecipeName{
		get;
	}
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

	public override string RecipeName {
		get {
			return "Wooden Fence";
		}
	}

	override public InventoryItem OutputObject{
		get{
			return new WoodenFence_InventoryItem();
		}
	}

	override public Sprite Sprite{
		get{
			return Game.Resources.Fence;
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

public class Bastard_Recipe : CraftingRecipe{

	public override string RecipeName {
		get {
			return "Bastard Recipe";
		}
	}

	override public InventoryItem OutputObject{
		get{
			return new WoodenFence_InventoryItem();
		}
	}

	override public Sprite Sprite{
		get{
			return Game.Resources.Gun;
		}
	}

	override public List<CraftingComponent> Components{
		get{
			var c = new List<CraftingComponent>();
			c.Add( new CraftingComponent( typeof( DefenceBerry_InventoryItem ) , 2 ) );
			c.Add( new CraftingComponent( typeof( PowerBerry_InventoryItem ) , 99 ) );
			return c;
		}
	}
}
