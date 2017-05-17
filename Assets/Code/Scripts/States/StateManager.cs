using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager {

	public static GameState GameState{
		get{
			return _gameState; 
		}
		set{
			if (_gameState != value){
				_gameState = value;

				if (_gameState == GameState.InBattle){
				//	Game.Controller.Audio.Music(MusicType.Battle);
				}

				if (_gameState == GameState.Free){
				//	Game.Controller.Audio.Music(MusicType.Free);
				}
				if (_gameState == GameState.Farm){
				
				}
			}
		}

	}
	private static GameState _gameState = GameState.Free;
}

public enum GameState{
	Free,
	InBattle,
	Farm
}