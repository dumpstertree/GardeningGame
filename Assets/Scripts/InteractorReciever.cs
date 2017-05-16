using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof( InteractableBehavior ))]
public class InteractorReciever : MonoBehaviour {

	public InteractableBehavior InteractableObject{
		get{
			return _behavior;
		}
	}
	[SerializeField] private InteractableBehavior _behavior;

	public InteractorType Type{
		get{
			return _type;
		}
	}
	[SerializeField] private InteractorType _type; 

	public ActiveAction Action;

	public bool Active{
		set{
			if (_active != value){

				if(value){
					PerformAction();
				}
				else{
					RevertAction();
				}

				_active = value;
			}
		}
	}
	private bool _active;

	private void PerformAction(){

		switch(Action){

		case ActiveAction.None:
			break;
		
		case ActiveAction.PropertiesUI:
			Game.Controller.UI.SecondaryUIPanel = UISecondaryPanel.CreatureStats;
			break;
		}
	}
	private void RevertAction(){

		switch(Action){

		case ActiveAction.None:
			break;
		
		case ActiveAction.PropertiesUI:
			Game.Controller.UI.SecondaryUIPanel = UISecondaryPanel.None;
			break;
		}
	}
}

public enum InteractorType {
	Creature,
	PlacedObject,
	Ground,
	All
}

public enum ActiveAction{
	None,
	PropertiesUI
}