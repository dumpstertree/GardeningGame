using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(SphereCollider))]
public class RadiusBehavior : MonoBehaviour {

	public IRadius Delegate{
		get{
			return _delegate;
		}
		set{
			_delegate = value;
		}
	}
	private IRadius _delegate;

	void OnTriggerEnter(Collider other) {
		if(_delegate != null ){
			if (other.gameObject.layer == this.gameObject.layer){
				_delegate.EnteredTriggerRadius( other.GetComponent<RadiusBehavior>().Delegate );
			}
		}
	}
	void OnTriggerExit(Collider other) {
		if(_delegate != null){
			if (other.gameObject.layer == this.gameObject.layer){
				_delegate.EnteredTriggerRadius( other.GetComponent<RadiusBehavior>().Delegate );
			}
		}
	}
}

public interface IRadius{
	void EnteredTriggerRadius(IRadius radius);
	void ExitedTriggerRadius(IRadius radius);
}