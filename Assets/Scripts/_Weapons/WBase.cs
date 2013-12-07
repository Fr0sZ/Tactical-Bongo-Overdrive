using UnityEngine;
using System.Collections;

public abstract class WBase : MonoBehaviour {

	protected Vector3 m_WeaponOffset = new Vector3(0.7F,-0.2F,0);

	public float  FireCooldown;
	public int Ammo;
	public abstract void Fire();

	private float m_lastFire;
	protected virtual bool CanFire(){
		float timeSinceLastFire = Time.timeSinceLevelLoad - m_lastFire;

		if (Ammo>0 && timeSinceLastFire >= FireCooldown){
			m_lastFire = Time.timeSinceLevelLoad;
			return true;
		}
		return false;
	}

	public AudioClip m_pickUpAudioClip;
	
	public virtual void OnPickUp(GameObject player)
	{
		PlayPickUpSound();
		
		Destroy(GetComponent<Rigidbody2D>());
		Destroy(GetComponent<BoxCollider2D>());
		
		transform.parent = player.transform;
		transform.localPosition = m_WeaponOffset;
		transform.localRotation = Quaternion.Euler(0,0,0);
		
		player.GetComponent<Player>().Weapon = this;
	}
	
	protected void PlayPickUpSound()
	{		
		if(m_pickUpAudioClip)
			Camera.main.GetComponent<AudioManager>().PlaySoundEffect(m_pickUpAudioClip);
	}

	
}
