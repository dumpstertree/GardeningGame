using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour {


	public static  List<Transform> _objectsInView = new List<Transform>();

	public static Transform GetClosestTarget(){

		Transform closest = null;
		float shortest = Mathf.Infinity;
		foreach ( Transform t in _objectsInView ){
			var d = Vector3.Distance( Camera.main.WorldToScreenPoint(t.position), new Vector3(Screen.width/2, Screen.height/2, 0) );
			if (d < shortest){
				shortest = d;
				closest = t;
			}
		}

		return closest;
	}

	public static void ObjectEnteredView( Transform t ){
		_objectsInView.Add(t);
	}

	public static void ObjectLeftView( Transform t ){
		_objectsInView.Remove(t);
	}

}
