using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour 
{	
	public void PlaySoundEffect(AudioClip soundEffect)
	{
		AudioSource audioSource = gameObject.AddComponent<AudioSource>() as AudioSource;
		audioSource.clip = soundEffect;
		audioSource.Play();
		Destroy(audioSource, soundEffect.length);
	}

	public void PlaySoundEffect(AudioClip soundEffect, float volume)
	{
		AudioSource audioSource = gameObject.AddComponent<AudioSource>() as AudioSource;
		audioSource.clip = soundEffect;
		audioSource.Play();
		audioSource.volume = volume;
		Destroy(audioSource, soundEffect.length);
	}
}
