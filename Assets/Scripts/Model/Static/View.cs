using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class View : MonoBehaviour {
	static public Transform WorldVisual{
		get{
			return GameObject.Find("WorldVisual").transform;
		}
	}
}
