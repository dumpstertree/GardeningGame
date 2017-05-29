using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingUI : MonoBehaviour, IInventory, IInventoryEmptySlot, IInputMouseButtonDown, IInputMouseButtonUp {

	protected const string _craftingSlotPrefix = "C";
	protected const int    _craftingSlotAmount = 3;

	private const float _maxMoveTime = 0.2f;
	private const float _pps = 500;
	private const string IconChildName   = "Icon";
	private const string NameChildName   = "Name";
	private const string AmountChildName = "Amount";


	[SerializeField] private GameObject 	_recipeIconPrefab;
	[SerializeField] private int 			_collumns = 4;
	[SerializeField] private int 			_spacing  = 50;
	[SerializeField] private int 			_offsetFromTop = 150;
	[SerializeField] private Text 		   _titleText;
	[SerializeField] private Transform[]   _reqComponentSlots;
	[SerializeField] private RectTransform _recipeWorkspace;
	[SerializeField] private Transform 	   _inventorySlotContainer;
	[SerializeField] private Transform 	   _craftingSlotContainer;
	[SerializeField] private Transform 	   _itemVisualsContainer;


	private string _curHover 	 = "-1";
	private string _curSelection = "-1";


	private Dictionary<string,UIInventoryEmptySlotBehavior> _slotEmptyObjects;
	private Dictionary<string,ItemSlotFill> 			    _slotVisuals; 
	private CraftingTable 									_craftingTableInventory;
	private List<RecipeIconBehavior> _recipeSlotVisuals;


	// MONO
	private void Awake(){

		_slotVisuals 					 = new Dictionary<string,ItemSlotFill>();
		_slotEmptyObjects 				 = new Dictionary<string,UIInventoryEmptySlotBehavior>();
		_craftingTableInventory 		 = new CraftingTable();
		_recipeSlotVisuals 				 = new List<RecipeIconBehavior>();
		_craftingTableInventory.Delegate = this;

		var index = 0;
		foreach( UIInventoryEmptySlotBehavior slot in _inventorySlotContainer.GetComponentsInChildren<UIInventoryEmptySlotBehavior>() ){

			slot.Index = Constants.InventoryIndexStart+index;
			slot.Delegate = this;

			_slotVisuals.Add( Constants.InventoryIndexStart+index, null );
			_slotEmptyObjects.Add( Constants.InventoryIndexStart+index, slot );

			index++;
		}

		index = 0;
		foreach( UIInventoryEmptySlotBehavior slot in _craftingSlotContainer.GetComponentsInChildren<UIInventoryEmptySlotBehavior>() ){

			slot.Index = _craftingSlotPrefix+index;
			slot.Delegate = this;

			_slotVisuals.Add( _craftingSlotPrefix+index, null );
			_slotEmptyObjects.Add( _craftingSlotPrefix+index, slot );

			index++;
		}
	}
	private void Start () {
		Inventory.Delegate =  this;
		Game.Controller.InputManager.MouseDownDelegate = this;
		Game.Controller.InputManager.MouseUpDelegate = this;
	}
	private void OnEnable(){
		ReloadRecipes();
		ReloadRequirements( new Recipes().KnowRecipes[0] );
		ReloadInventory();
		ReloadCraftingSlots();
	}


	// PRIVATE
	private void ReloadRecipes(){

		var x = 0;
		var y = 0;
		var width = (_recipeWorkspace.sizeDelta.x - (_spacing*(_collumns+1))) / _collumns;

		for( int i=_recipeSlotVisuals.Count-1; i>=0; i--){
			
			Destroy( _recipeSlotVisuals[i].gameObject );
			_recipeSlotVisuals.RemoveAt( i );
		}

		foreach( CraftingRecipe r in new Recipes().KnowRecipes ){
			
			var iconRT = Instantiate( _recipeIconPrefab ).GetComponent<RectTransform>();
			var icon = iconRT.GetComponent<RecipeIconBehavior>();
			_recipeSlotVisuals.Add( icon );


			iconRT.SetParent( _recipeWorkspace, false );
			iconRT.sizeDelta = new Vector2( width, width );
			var xPos =  _spacing + x*_spacing + x*width + width/2 + -(_recipeWorkspace.sizeDelta.x/2);
			var yPos =  -(_spacing + y*_spacing + y*width + width/2) +  (_recipeWorkspace.sizeDelta.y/2) + -_offsetFromTop;

			iconRT.anchoredPosition = new Vector2( xPos, yPos );
			var b = iconRT.GetComponent<RecipeIconBehavior>();
			b.Recipe = r;
			b.Delegate = this;

			x++;
			if ( x >= _collumns ){
				x = 0;
				y+= 1;
			}
		}
	}
	private void ReloadCraftingSlots(){

		for( int i=0; i< _craftingSlotAmount; i++ ){

			var key = _craftingSlotPrefix+i;
			var item = _craftingTableInventory.GetItem( key );

			if ( _slotVisuals[key]){
				Destroy( _slotVisuals[key].gameObject );
			}

			if ( item != null ){
				var visualGO = Instantiate( Game.Resources.Prefab.InventorySlotObject );
				var visual   = visualGO.GetComponent<ItemSlotFill>();

				visualGO.transform.SetParent( _itemVisualsContainer, false );
				visualGO.transform.position = _slotEmptyObjects[key].transform.position;
				visual.InventoryItem = item;

				_slotVisuals[key] = visual;
			}
		}
	}
	private void ReloadRequirements( CraftingRecipe loadedRecipe ){

		_titleText.text = loadedRecipe.RecipeName;

		for( int i=0; i < _reqComponentSlots.Length; i++){

			var slot = _reqComponentSlots[i];

			if ( i >= loadedRecipe.Components.Count ){
				slot.gameObject.SetActive( false );
			}
			else{
				slot.gameObject.SetActive( true );
				var component = loadedRecipe.Components[i];
				var inst = System.Activator.CreateInstance(component.Item) as InventoryItem;

				slot.FindChild( IconChildName ).GetComponent<Image>().sprite = inst.Sprite;
				slot.FindChild( NameChildName ).GetComponent<Text>().text = inst.Name;
				slot.FindChild( AmountChildName ).GetComponent<Text>().text = component.Amount.ToString()+"x";
			}
		}
	}
	private void ReloadInventory(){

		for( int i=0; i< Constants.InventorySlots; i++ ){

			var key = Constants.InventoryIndexStart+i;
			var item = Inventory.GetItemInSlot( key );

			if ( _slotVisuals[key]){
				Destroy( _slotVisuals[key].gameObject );
			}

			if ( item != null ){
				var visualGO = Instantiate( Game.Resources.Prefab.InventorySlotObject );
				var visual   = visualGO.GetComponent<ItemSlotFill>();

				visualGO.transform.SetParent( _itemVisualsContainer, false );
				visualGO.transform.position = _slotEmptyObjects[key].transform.position;
				visual.InventoryItem = item;

				_slotVisuals[key] = visual;
			}
		}
	}

	private IEnumerator MoveVisual(){

		var selectionIndex = _curSelection;
		var hoverIndex 	   = "";
		var visualS 	   = _slotVisuals[ _curSelection ];


		visualS.Action = visualS.StartCoroutine( visualS.TrackMouse() );

		while (_curSelection != "-1"){
			yield return null;
		}

		hoverIndex = _curHover;


		// Revert
		if ( hoverIndex == "-1" ){
			var a = visualS.StartCoroutine( visualS.MoveToPos( _slotEmptyObjects[selectionIndex].transform.position ) );
			visualS.Action = a;
			yield return a;
		}

		// Lock Into Place
		else{

			var a = visualS.StartCoroutine( visualS.MoveToPos(  _slotEmptyObjects[hoverIndex].transform.position  ) );
			visualS.Action = a;
			yield return a;

			MoveItems( selectionIndex, hoverIndex );
			Destroy( visualS.gameObject );
		}
	}
	private void MoveItems( string selectionIndex, string hoverIndex ){

		var craftingS = (selectionIndex[0].ToString() == _craftingSlotPrefix) ? true : false;
		var craftingH = (hoverIndex[0].ToString()     == _craftingSlotPrefix) ? true : false;

		// Swap Craft
		if ( craftingS && craftingH ){
			_craftingTableInventory.SwapItems( selectionIndex, hoverIndex );
		}

		//Move to crafting
		else if ( !craftingS && craftingH ){
			var item = Inventory.GetItemInSlot( selectionIndex );
			_craftingTableInventory.AddItemToSlot( item, hoverIndex );
			Inventory.RemoveItem( selectionIndex );
		}

		// Move to inventory
		else if( craftingS && !craftingH ){
			var item = _craftingTableInventory.GetItem( selectionIndex );
			_craftingTableInventory.RemoveItemFromSlot( selectionIndex );
			Inventory.AddItem( item, hoverIndex );
		}

		// Swap Inventory
		else if (!craftingS && !craftingH){
			Inventory.SwapItem( selectionIndex, hoverIndex );
		}

		// Catch
		else{
			Debug.LogWarning( "Uncaught movement" );
		}
	}


	// IINVENTORY
	public void InventoryChanged(){
		ReloadInventory();
	}


	//RECIPEICON
	public void RecipeIconChanged( CraftingRecipe recipe ){
		ReloadRequirements( recipe );
	}

	// INPUT DELEGATES
	public void MouseButtonDown(){

		if (_curHover != "-1"){
			_curSelection = _curHover;
			StartCoroutine( MoveVisual() );
		}

	}
	public void MouseButtonUp(){

		if (_curSelection != "-1"){
			_curSelection = "-1";
		}
	}


	// EMPTY SLOT DELEGATES
	public void HoverStarted( string index ){
		_curHover = index;
	}
	public void HoverEnded( string index ){
		if (_curHover == index){
			_curHover = "-1";
		}
	}


	private class CraftingTable{

		public CraftingUI Delegate{
			set{
				_delegate = value;
			}
		}
		private CraftingUI _delegate;
		private Dictionary<string,InventoryItem> _slots;

		public CraftingTable (){
			_slots = new  Dictionary<string,InventoryItem>();
			for( int i=0; i<_craftingSlotAmount; i++ ){
				_slots.Add( _craftingSlotPrefix+i, null );
			}
		} 

		public void AddItemToSlot( InventoryItem item, string index  ){
			_slots[ index ] = item;
			Reload();
		}
		public void RemoveItemFromSlot( string index ){
			_slots[ index ] = null;
			Reload();
		}
		public void SwapItems( string from, string to ){

			var firstSlot  = _slots[ from ];
			var secondSlot = _slots[ to ];

			_slots[to]   = firstSlot;
			_slots[from] = secondSlot;

			Reload();
		} 
		public InventoryItem GetItem( string at ){
			if (_slots.ContainsKey( at )){
				return _slots[at];
			}
			else{
				return null;
			}
		}

		private void Reload(){
			_delegate.ReloadCraftingSlots();
		}
	}
}
