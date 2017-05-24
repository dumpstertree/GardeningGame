using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyMemberUIBehavior : MonoBehaviour, IStats {

	[SerializeField] private Image _healthBar;
	[SerializeField] private Color _maxColor;
	[SerializeField] private Color _minColor;

	public void Startup( Creature target ){
		target.Body.AddListenerToStats( this );
		HealthChanged( target.Body.Stats.Health, target.Body.Stats.MaxHealth); 
	}

	private void SetFill( float frac ){
		_healthBar.fillAmount = frac;
	}
	private void SetColor( float frac ){
		_healthBar.color = Color.Lerp( _minColor, _maxColor, frac );
	}


	// STAT DELEGATE
	public void HealthChanged( int health, int maxHealth ){
		var frac  = (float)health/(float)maxHealth;
		SetFill( frac );
		SetColor( frac );
	}
	public void StatsChanged( StatsType stat, int value  ){
	}
}
