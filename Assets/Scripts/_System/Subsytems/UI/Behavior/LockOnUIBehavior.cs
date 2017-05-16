using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockOnUIBehavior : MonoBehaviour {



	[Header("Refrence")]
	[SerializeField] private Image _image;
	[SerializeField] private Animator _animator;

	[Header("Destroy")]
	[SerializeField] private float _killTime = 0.1f;

	// INSTANCE VARIABLES
	private Creature _target;
	private RectTransform _rect;
	private Vector2 _originalSize;


	// CONSTANTS
	const string _animatorTrigger  = "Open";


	// PUBLIC METHODS
	public void Startup( Creature target ){

		transform.SetParent( GameObject.FindGameObjectWithTag("ScreenSpaceCanvas").transform );

		_target = target;
		_rect = _image.GetComponent<RectTransform>();
		_originalSize = _rect.sizeDelta;

		Move();

		_animator.SetBool( _animatorTrigger, true );
	}
	public void Teardown(){
		_animator.SetBool( _animatorTrigger, false );
		Destroy( gameObject, _killTime);
	}


	// PRIVATE METHODS
	private void Update () {
		Move();
	}
	private void Move(){

		if (_target == null){
			Destroy(gameObject);
		}
		else{
			transform.position = Camera.main.WorldToScreenPoint(_target.CreatureTargetBehavior.transform.position);
		}
	}
}