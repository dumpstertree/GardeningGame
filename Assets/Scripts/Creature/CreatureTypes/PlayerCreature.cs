
using UnityEngine;

public class PlayerCreature : Creature {

	public override Brain Brain{
		get{
			return _brain;
		}
	}
	public override Body Body{
		get{
			return _body;
		}
	}
	
	[SerializeField] private Transform Gun;
	[SerializeField] private PlayerBody _body;
	[SerializeField] private PlayerBrain _brain;

	public void ShootProjectile( BulletSpawnInfo spawnInfo, ShootInfo shootInfo ){
		Game.Controller.ScreenShake.ShakeLight();
		Game.Controller.Audio.OneShot(AudioType.Gunshot);
		BulletBehavior.Create( spawnInfo, shootInfo, this, Gun );
	}
	public void SetInput( PlayerInputPacket packet ){
		
		if (packet.Scoped){
			_animator.SetFloat("Horizontal", packet.Horizontal);
			_animator.SetFloat("Vertical", packet.Vertical);
		}
		else{
			_animator.SetFloat("Horizontal", 0.0f);
			_animator.SetFloat("Vertical", ( Mathf.Abs( packet.Horizontal ) > Mathf.Abs( packet.Vertical)) ?  Mathf.Abs(packet.Horizontal) :  Mathf.Abs(packet.Vertical) );
		}
	}
}

public struct PlayerInputPacket{
	
	public float Horizontal{
		get; set;
	}
	public float Vertical{
		get; set;
	}
	public bool  Scoped{
		get; set;
	}

	public static PlayerInputPacket Create( float h, float v, bool s ){
		var p = new PlayerInputPacket();
		p.Horizontal = h;
		p.Vertical = v;
		p.Scoped = s;
		return p;
	}
}
