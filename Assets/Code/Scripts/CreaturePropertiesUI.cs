using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreaturePropertiesUI : MonoBehaviour, IStomach, StatsDelegate {

	[SerializeField] private Image _hungerBar;
	[SerializeField] private Text _powerText;
	[SerializeField] private Text _magickText;
	[SerializeField] private Text _defenceText;
	[SerializeField] private Text _magickDefenceText;
	[SerializeField] private Text _speedText;
	[SerializeField] private Text _luckText;


	private Stomach _stomach;
	private Stats _stats;


	// MONO
	private void OnEnable(){
		
		var i = FindObjectOfType<Interactor>();
		if ( i._reciever){
			
			_stomach = i._reciever.GetComponent<Creature>().Growth.GetComponent<Stomach>();
			_stats   = i._reciever.GetComponent<Creature>().CreatureStats;

			_stomach.StartListening(this);
			_stats.Delegate = this;

			HardPullHunger();
			HardPullStats();
		}
	}
	private void OnDisable(){

		if (_stomach != null){
			_stomach.StopListening(this);
			_stomach = null;
		}

		if (_stats != null){
			// Remove Stats Delegate
			_stats = null;
		}
	}


	// PRIVATE METHODS	
	private void HardPullHunger(){
	
		if (_stomach){
			_hungerBar.transform.localScale = new Vector3( 1-(float)_stomach.CurHungerMeter/(float)_stomach.MaxHungerMeter,1,1);
		}

	}
	private void HardPullStats(){

		if (_stats){
			_powerText.text   		 = _stats.Power.ToString();
			_magickText.text  		 = _stats.Magick.ToString();
			_defenceText.text 		 = _stats.Defence.ToString();
			_magickDefenceText.text  = _stats.MagickDefence.ToString();
			_speedText.text    		 = _stats.Speed.ToString();
			_luckText.text 	   		 = _stats.Luck.ToString();
		}

	}


	// ISTOMACH
	public void HungerChanged( int cur, int max ){
		_hungerBar.transform.localScale = new Vector3( 1-(float)cur/(float)max,1,1);
	}


	// STATS DELEGATE
	public void HealthChanged( int health, int maxHealth ){
	}
	public void StatsChanged( StatsType stat, int value  ){
		switch( stat ){
		case StatsType.Power:
			_powerText.text = value.ToString();
			break;
		case StatsType.Magick:
			_magickText.text = value.ToString();
			break;
		case StatsType.Defence:
			_defenceText.text = value.ToString();
			break;
		case StatsType.MagickDefence:
			_magickDefenceText.text = value.ToString();
			break;
		case StatsType.Speed:
			_speedText.text = value.ToString();
			break;
		case StatsType.Luck:
			_luckText.text = value.ToString();
			break;
		}
	}
}
