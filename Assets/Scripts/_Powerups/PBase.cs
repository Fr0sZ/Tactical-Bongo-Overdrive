using UnityEngine;
using System.Collections;

public class PBase : MonoBehaviour 
{
	public AudioClip m_pickUpAudioClip;

	public virtual void OnPickUp(GameObject player)
	{
		PlayPickUpSound();
		Destroy(this.gameObject);
	}

	protected void PlayPickUpSound()
	{		
		if(m_pickUpAudioClip)
			Camera.main.GetComponent<AudioManager>().PlaySoundEffect(m_pickUpAudioClip);
	}

	public virtual void OnFire()
	{
	}

	public virtual void OnHit(GameObject shooter, int dmg, Vector2 point, Vector2 dir)
	{
	}

	public virtual void OnDeath()
	{
	}
}