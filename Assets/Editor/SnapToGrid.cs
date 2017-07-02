using UnityEngine;
using UnityEditor;
using System.Collections;
 
public class SnapToGrid : ScriptableObject {
 
	[MenuItem ("Window/Snap to Grid %g")]
	static void MenuSnapToGrid() {
		foreach (Transform t in Selection.GetTransforms(SelectionMode.TopLevel | SelectionMode.OnlyUserModifiable)) {
			var p =  t.position;
			var x = Mathf.Round( p.x );
			var y = Mathf.Round( p.y );
			var z = Mathf.Round( p.z );
			t.position = new Vector3 ( x,y,z);
		}
	}
 
}