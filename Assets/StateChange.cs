using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateChange : MonoBehaviour {

	[SerializeField] private GameState StateToTransitionTo;
	private List<IGameState> _listeners;

	void Start () {
		StateManager.GameState = StateToTransitionTo;
	}

	public void AddListener( IGameState listener ){
		if (!_listeners.Contains(listener)){
			_listeners.Add( listener );
		}
	}
	public void RemoveListener( IGameState listener ){
		if (_listeners.Contains(listener)){
			_listeners.Remove( listener );
		}
	}
}

public interface IGameState{
	void StateChanged( GameState state ); 
}
