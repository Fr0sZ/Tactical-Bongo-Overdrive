using UnityEngine;
using System.Collections;

public class PBase : MonoBehaviour 
{
	public AudioClip m_pickUpAudioClip;

	public virtual void OnPickUp(GameObject player)
	{
		if(m_pickUpAudioClip)
			Camera.main.GetComponent<AudioManager>().PlaySoundEffect(m_pickUpAudioClip);

		Destroy(this.gameObject);
	}
}