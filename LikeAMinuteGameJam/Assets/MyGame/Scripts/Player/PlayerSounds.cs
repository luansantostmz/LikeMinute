using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
	public AudioClip jumpSound;
	public AudioClip damageSound;
	public AudioClip[] randomSpeechSounds;
	public AudioClip deathSound;

	private AudioSource audioSource;

	void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}

	public void PlayJumpSound()
	{
		audioSource.PlayOneShot(jumpSound);
	}

	public void PlayDamageSound()
	{
		audioSource.PlayOneShot(damageSound);
	}

	public void PlayRandomSpeechSound()
	{
		int randomIndex = Random.Range(0, randomSpeechSounds.Length);
		audioSource.PlayOneShot(randomSpeechSounds[randomIndex]);
	}

	public void PlayDeathSound()
	{
		audioSource.PlayOneShot(deathSound);
	}
}

