using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldSpaceTextTest : MonoBehaviour {

	[SerializeField] private Transform _linkedObject;
	[SerializeField] private Text _text;
	void Update () {

		transform.position = Camera.main.WorldToScreenPoint( _linkedObject.position );
		_text.text = "" + (int)(Camera.main.WorldToScreenPoint( _linkedObject.position ).x);
	}
}
