using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehavior : MonoBehaviour {

	[SerializeField] private Renderer _renderer;
	private bool _visible;

	private void Update(){
		if (_renderer.isVisible != _visible ){
			_visible = _renderer.isVisible;

			if (_visible){
				TargetController.ObjectEnteredView( transform );
			}
			else{
				TargetController.ObjectLeftView( transform );
			}
		}
	}
	private void OnDestroy(){
		TargetController.ObjectLeftView( transform );
	}
}
