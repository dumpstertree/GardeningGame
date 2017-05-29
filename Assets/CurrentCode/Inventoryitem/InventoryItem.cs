using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InventoryItem  {


	// PROPERTIES
	public abstract string Name{
		get;
	}
	public int StackAmount{
		get{
			return _stackAmount;
		}
	}
	public abstract Sprite Sprite {
		get;
	}
	public abstract GameObject HoldItem{
		get;
	}
	public virtual string HudDescription{
		get;
	}


	// PRIVATE PROPERTIES
	protected abstract int  StackLimit{
		get;
	}
	protected abstract bool Consumable{
		get;
	}
		

	// PRIVATE
	protected int   _stackAmount;
	protected static float _lastInteract;
	protected float _waitTime;


	// INIT
	public InventoryItem(){
		_stackAmount    = 1;
		_waitTime 		= 2f;
		HudDescription = "Use Item";
	}
	public bool AddToStack( int amount ){

		if ( _stackAmount+amount <= StackLimit ){
			_stackAmount += amount;
			Inventory.StackChanged( this );
			return true;
		}

		else{
			return false;
		}
	}
	public void RemoveFromStack( int amount ){
		
		_stackAmount -= amount;
		Inventory.StackChanged( this );

		if ( _stackAmount < 1 ){
			Inventory.RemoveItem( this );
		}
	}
		
	protected void Consume(){

		if( Consumable ){
			_stackAmount -= 1;
			if ( _stackAmount < 1 ){
				Inventory.RemoveItem( this );
			} 
		}
	}

}

public struct PlaceableObjectInfo{
	public GameObject Prefab;	
}
public abstract class FreePlaceInventoryItem : InventoryItem {


	// NEW
	public abstract PlaceableObjectInfo PlaceableInfo {
		get;
	}


	// METHOD
	public void Use ( Interactor interactor ){

		if (Time.time - _lastInteract > _waitTime){
			_lastInteract = Time.time;
			Debug.Log( Time.time );

			if (PlaceableInfo.Prefab){
				GameObject.Instantiate( PlaceableInfo.Prefab, interactor.transform.position, Quaternion.Euler(Vector3.zero));
				Placed();
				Consume();
			}
			else{
				NotPlaced();
			}
		}
	}

	private void Placed(){
		GameObject.FindObjectOfType<CustomCharacterController>().Animator.SetTrigger("Dig");
	}

	private void NotPlaced(){
		Debug.LogWarning("Tried to create a placeable object that did no exist;");
	}
		
}


public struct PlantableObjectInfo{
	public GameObject Prefab;	
}
public abstract class PlantInventoryItem : InventoryItem {		

	// NEW
	public abstract PlantableObjectInfo PlaceableInfo {
		get;
	}


	// USE
	public void Use( IPlant on ){
		if (Time.time - _lastInteract > _waitTime){

			var result = on.Plant( PlaceableInfo );
			_lastInteract = Time.time;

			if (result){
				Planted();
				Consume();
			}
			else{
				CouldNotPlant();
			}
				
		}
	}

	private void Planted(){
		GameObject.FindObjectOfType<CustomCharacterController>().Animator.SetTrigger("Plant");
	}
	private void CouldNotPlant(){
	}
}


public struct HitObjectInfo{
	public int HitStrength;
	public float AnimationDelay;
}
public abstract class HitInventoryItem : InventoryItem {


	// NEW
	public abstract HitObjectInfo HitInfo {
		get;
	}

	// USE
	public void Use( IHit on ){
		if (Time.time - _lastInteract > _waitTime){

			var result = on.Hit( HitInfo );
			_lastInteract = Time.time;

			if (result){
				Hit();
				Consume();
			}
			else{
				Miss();
			}
		}
	}
		
	private void Hit(){
		GameObject.FindObjectOfType<CustomCharacterController>().Animator.SetTrigger("Swing");
		Game.Controller.Audio.OneShot(AudioType.AxeSwing);
	}
	private void Miss(){
		GameObject.FindObjectOfType<CustomCharacterController>().Animator.SetTrigger("Swing");
		Game.Controller.Audio.OneShot(AudioType.AxeSwing);
	}
}

public struct FoodEffect{

	public int Hunger;		 
	public int Power; 		 
	public int Magick; 		 
	public int Defence; 		 
	public int MagickDefence; 
	public int Speed; 		 
	public int Luck;	

	public FoodEffect( int hunger, int power, int magick, int defence, int magickDefence, int speed, int luck ){
		Hunger = hunger;
		Power = power;
		Magick = magick;
		Defence = defence;
		MagickDefence = magickDefence;
		Speed = speed;
		Luck = luck;
	}
}
public abstract class FeedInventoryItem : InventoryItem {


	// NEW
	public abstract FoodEffect FoodEffect {
		get;
	}


	// USE
	public void Use( IFeed on ){
		if (Time.time - _lastInteract > _waitTime){

			var result = on.Feed( FoodEffect );
			_lastInteract = Time.time;

			if( result ){ 
				Fed();
				Consume();
			}
			else{
				NotFed();
			}

		}
	}

	private void Fed(){
	}
	private void NotFed(){
	}

}

public class InteractInventoryItem : InventoryItem {

	public override string Name {
		get {
			return "Interact";
		}
	}
	public override Sprite Sprite {
		get {
			throw new System.NotImplementedException ();
		}
	}
	protected override int StackLimit {
		get {
			throw new System.NotImplementedException ();
		}
	} 
	protected override bool Consumable {
		get {
			throw new System.NotImplementedException ();
		}
	}
	public override GameObject HoldItem {
		get {
			throw new System.NotImplementedException ();
		}
	}

	// USE
	public void Use( IInteract on ){
		if (Time.time - _lastInteract > _waitTime){
			Debug.Log( Time.time );
			_lastInteract = Time.time;
			on.Interact();

			GameObject.FindObjectOfType<CustomCharacterController>().Animator.SetTrigger("Pickup");
			Game.Controller.Audio.OneShot(AudioType.LevelUp);
		}
	}
}