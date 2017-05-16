using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(AudioSource)) ]
	
public class AudioController: MonoBehaviour {

	[SerializeField] private  AudioClip _plant;
	[SerializeField] private  AudioClip _levelUp;
	[SerializeField] private  AudioClip _axeSwing;
	[SerializeField] private  AudioClip _treeHit;
	[SerializeField] private  AudioClip _treeDestroy;
	[SerializeField] private  AudioClip _gunshot;


	[SerializeField] private AudioClip _battleMusic;
	[SerializeField] private AudioClip _freeMusic;

	private AudioSource _source;

	private void Awake(){
		_source = GetComponent<AudioSource>();
	}
	public void OneShot( AudioType type ){
		switch( type ) {
		case AudioType.Planted:
			_source.PlayOneShot(_plant);
			break;
		case AudioType.LevelUp:
			_source.PlayOneShot(_levelUp);
			break;
		case AudioType.AxeSwing:
			_source.PlayOneShot(_axeSwing);
			break;
		case AudioType.TreeHit:
			_source.PlayOneShot(_treeHit);
			break;
		case AudioType.TreeDestroy:
			_source.PlayOneShot(_treeDestroy);
			break;
		case AudioType.Gunshot:
			_source.PlayOneShot(_gunshot);
			break;
		}
	}

	public void Music( MusicType type ){
		switch( type ) {
		case MusicType.Battle:
			_source.clip = _battleMusic;
			_source.Play();
			break;
		case MusicType.Free:
			_source.clip = _freeMusic;
			_source.Play();
			break;
		}
	}
}

public enum AudioType{
	Planted,
	LevelUp,
	AxeSwing,
	TreeHit,
	TreeDestroy,
	Gunshot
}

public enum MusicType{
	Battle,
	Free
}
