using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingUI : MonoBehaviour, IInventory2, IInventoryEmptySlot, IInputMouseButtonDown, IInputMouseButtonUp {

	protected const char   _craftingSlotPrefix = 'C';
	protected const int    _craftingSlotAmount =  3;

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
	[SerializeField] private Button 	   _buildButton;


	private string _curHover 	 = "-1";
	private string _curSelection = "-1";


	private Dictionary<string,UIInventoryEmptySlotBehavior> _itemSlots;
	private Dictionary<string,ItemSlotFill> 			    _itemVisuals; 
	private InventoryData 									_craftingTableInventory;
	private List<RecipeIconBehavior> 						_recipeVisuals;
	private CraftingRecipe									_loadedRecipe;


	// MONO
	private void Awake(){

		_itemSlots 	   = new Dictionary<string,UIInventoryEmptySlotBehavior>();
		_itemVisuals   = new Dictionary<string,ItemSlotFill>();
		_recipeVisuals = new List<RecipeIconBehavior>();

		_craftingTableInventory = new InventoryData();
		_craftingTableInventory.AddToDataSource(_craftingSlotPrefix,_craftingSlotAmount);

		BuildSlots();
	}
	private void Start () {
		_craftingTableInventory.StartListening( this );
		Inventory.Data.StartListening( this );
		Game.Controller.InputManager.MouseDownDelegate = this;
		Game.Controller.InputManager.MouseUpDelegate = this;
	}
	private void OnEnable(){

		_loadedRecipe =  new Recipes().KnowRecipes[0];

		ReloadRecipes();
		ReloadRequirements();
		ReloadInventory();
		ReloadCraftingSlots();
		ReloadBuildButton();
	}


	// PRIVATE
	private void BuildSlots(){

		var index = 0;

		index = 0;
		var inventorySlots = _inventorySlotContainer.GetComponentsInChildren<UIInventoryEmptySlotBehavior>();
		foreach( UIInventoryEmptySlotBehavior slot in inventorySlots ){

			var id = Constants.InventoryIndexStart.ToString()+index;

			slot.Index = id;
			slot.Delegate = this;

			_itemVisuals.Add( id, null );
			_itemSlots.Add( id, slot );

			index++;
		}

		index = 0;
		var craftingSlots = _craftingSlotContainer.GetComponentsInChildren<UIInventoryEmptySlotBehavior>();
		foreach( UIInventoryEmptySlotBehavior slot in craftingSlots){

			var id = _craftingSlotPrefix.ToString()+index;

			slot.Index = id;
			slot.Delegate = this;

			_itemVisuals.Add( id, null );
			_itemSlots.Add( id, slot );

			index++;
		}
	}

	private void ReloadRecipes(){
		var x = 0;
		var y = 0;
		var width = (_recipeWorkspace.sizeDelta.x - (_spacing*(_collumns+1))) / _collumns;

		for( int i=_recipeVisuals.Count-1; i>=0; i--){
			
			Destroy( _recipeVisuals[i].gameObject );
			_recipeVisuals.RemoveAt( i );
		}

		foreach( CraftingRecipe r in new Recipes().KnowRecipes ){
			
			var iconRT = Instantiate( _recipeIconPrefab ).GetComponent<RectTransform>();
			var icon = iconRT.GetComponent<RecipeIconBehavior>();
			_recipeVisuals.Add( icon );


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
	private void ReloadRequirements(){
		_titleText.text = _loadedRecipe.RecipeName;

		for( int i=0; i < _reqComponentSlots.Length; i++){

			var slot = _reqComponentSlots[i];

			if ( i >= _loadedRecipe.Components.Count ){
				slot.gameObject.SetActive( false );
			}
			else{
				slot.gameObject.SetActive( true );
				var component = _loadedRecipe.Components[i];
				var inst = System.Activator.CreateInstance(component.Item) as InventoryItem;

				slot.FindChild( IconChildName ).GetComponent<Image>().sprite = inst.Sprite;
				slot.FindChild( NameChildName ).GetComponent<Text>().text = inst.Name;
				slot.FindChild( AmountChildName ).GetComponent<Text>().text = component.Amount.ToString()+"x";
			}
		}
	}
		
	private void ReloadCraftingSlots(){
		for( int i=0; i <  _craftingSlotAmount; i++ ){
			var key =_craftingSlotPrefix.ToString()+i;
			ReloadCraftingSlots( key );
		}
	}
	private void ReloadCraftingSlots( string atIndex ){
		if ( _itemVisuals.ContainsKey( atIndex ) ){
			var visual = _itemVisuals[atIndex];
			var item   = _craftingTableInventory.GetItem( atIndex );

			// Reload
			if ( item!=null && visual!=null ){
				visual.InventoryItem = item;
				return;
			}

			// Destroy
			else if ( item==null && visual!=null ){
				Destroy( visual.gameObject );
				return;
			}

			// Create
			else if ( item!=null && visual==null ){
				var go = Instantiate( Game.Resources.Prefab.InventorySlotObject );
				var v  = go.GetComponent<ItemSlotFill>();

				go.transform.SetParent( _itemVisualsContainer, false );
				go.transform.position = _itemSlots[atIndex].transform.position;
				v.InventoryItem = item;

				_itemVisuals[ atIndex ] = v;
				return;
			}

			// Catch
			else{
				return; 
			}
		}

		Debug.LogWarning( ERROR.IndexDoesNotExist + atIndex );
		return;
	}

	private void ReloadInventory(){

		for( int i=0; i< Constants.InventorySlots; i++ ){
			var key = Constants.InventoryIndexStart.ToString()+i;
			ReloadInventory( key );
		}
	}
	private void ReloadInventory( string atIndex ){
		if ( _itemVisuals.ContainsKey( atIndex ) ){

			var visual = _itemVisuals[atIndex];
			var item = Inventory.Data.GetItem( atIndex );

			// Reload
			if ( item!=null && visual!=null ){
				//print("relaod");
				visual.InventoryItem = item;
				return;
			}

			// Destroy
			else if ( item==null && visual!=null ){
				//print("Destory");
				_itemVisuals[ atIndex ] = null;
				Destroy( visual.gameObject );
				return;
			}

			// Create
			else if ( item!=null && visual==null ){
				//print("Create");
				var go = Instantiate( Game.Resources.Prefab.InventorySlotObject );
				var v  = go.GetComponent<ItemSlotFill>();

				go.transform.SetParent( _itemVisualsContainer, false );
				go.transform.position = _itemSlots[atIndex].transform.position;
				v.InventoryItem = item;

				_itemVisuals[ atIndex ] = v;
				return;
			}
			// Catch
			else{
				//print("catch");
				return; 
			}
		}

		Debug.LogWarning( ERROR.IndexDoesNotExist + atIndex );
		return;
	}
		
	private void ReloadBuildButton(){
			foreach ( CraftingComponent cc in _loadedRecipe.Components ){
			var inInventory = _craftingTableInventory.GetContentsCount( cc.GetType() );
			//print(inInventory);
			//print(cc.Amount);
			if ( inInventory < cc.Amount ){
				_buildButton.interactable = false;
				return;
			}
		}

		_buildButton.interactable = true;
	}

	private IEnumerator MoveVisual(){

		var selectionIndex = _curSelection;
		var hoverIndex 	   = "";
		var visualS 	   = _itemVisuals[ _curSelection ];


		// TRACK MOUSE
		visualS.Action = visualS.StartCoroutine( visualS.TrackMouse() );
		while (_curSelection != "-1"){
			yield return null;
		}
		hoverIndex = _curHover;


		// REVERT
		if ( hoverIndex == "-1" ){
			var a = visualS.StartCoroutine( visualS.MoveToPos( _itemSlots[selectionIndex].transform.position ) );
			visualS.Action = a;
			yield return a;
		}

		// LOCK
		else{

			var a = visualS.StartCoroutine( visualS.MoveToPos(  _itemSlots[hoverIndex].transform.position  ) );
			visualS.Action = a;
			yield return a;

			var hoverVisual     = _itemVisuals[hoverIndex];
			var selectionVisual = _itemVisuals[selectionIndex];

			if (hoverVisual){
				Destroy( hoverVisual.gameObject );
				_itemVisuals[ hoverIndex ] = null;
			}

			if (selectionVisual){
				Destroy( selectionVisual.gameObject );
				_itemVisuals[ selectionIndex ] = null;
			}

			InventoryData.DistributeItems( selectionIndex, hoverIndex, _craftingTableInventory, Inventory.Data );
		}
	}


	// IINVENTORY
	public void DataSourceChanged( string atIndex ){
		var prefix = atIndex[0];

		if( _craftingTableInventory.CheckForPrefix( prefix ) ){
			ReloadCraftingSlots( atIndex );
			ReloadBuildButton();
		}
		if( Inventory.Data.CheckForPrefix( prefix ) ){
			ReloadInventory( atIndex );
		}
	}


	//RECIPEICON
	public void RecipeIconChanged( CraftingRecipe recipe ){
		_loadedRecipe = recipe;
		ReloadRequirements();
		ReloadBuildButton();
	}


	// INPUT DELEGATES
	public void MouseButtonDown(){

		if (_curHover != "-1"){
			if ( _craftingTableInventory.CheckFor(_curHover) || Inventory.Data.CheckFor(_curHover) ){
				_curSelection = _curHover;
				StartCoroutine( MoveVisual() );
			}
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


	// ERROR
	private struct ERROR{
		static public string IndexDoesNotExist = "Trying to acces an index that does not exist: ";
	}
}
