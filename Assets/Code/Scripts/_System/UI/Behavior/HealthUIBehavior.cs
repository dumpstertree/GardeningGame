using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUIBehavior : MonoBehaviour {

	[Header("Refrence")]
	[SerializeField] private Image _image;
	[SerializeField] private Animator _animator;

	[Header("Sizing")]
	[SerializeField] private float _maxDistance;
	[SerializeField] private float _maxSize;
	[SerializeField] private float _minSize;

	[Header("Color")]
	[SerializeField] private Color _maxColor;
	[SerializeField] private Color _minColor;

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
		SetFill();

		_animator.SetBool( _animatorTrigger, true );
	}
	public void Teardown(){
		_animator.SetBool( _animatorTrigger, false );
		Destroy( gameObject, _killTime );
	}


	// PRIVATE METHODS
	private void Update () {
		Move();
		SetFill();
	}
	private void Move(){

		if (_target == null){
			Destroy(gameObject);
		}
		else{
			transform.position = Camera.main.WorldToScreenPoint(_target.CreatureTargetBehavior.transform.position);
		}
	}
	private void SetFill(){
		var f =  (float)_target.CreatureStats.Health / (float)_target.CreatureStats.MaxHealth;
		_image.fillAmount = f;
		_image.color = Color.Lerp( _maxColor, _minColor, 1-f );
	}
}
