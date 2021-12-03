using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceSoundBehaviour : MonoBehaviour
{
	[SerializeField] private AudioSource _collideSound;
	[SerializeField] private AudioSource _thrownSound;

	private void OnEnable()
	{
        Dice dice = GetComponentInParent<Dice>();
        dice.ThrownEvent += PlayThrownSound;
	}
	
	private void OnDisable()
	{
        Dice dice = GetComponentInParent<Dice>();
        if (dice != null)
        {
			dice.ThrownEvent -= PlayThrownSound;
        }
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.transform.TryGetComponent(out Ground _))
		{
			_collideSound.Play();
		}
	}
	private void OnCollisionEnter(Collision collision)
	{
		if (collision.transform.TryGetComponent(out Ground _))
		{
			_collideSound.volume= Random.Range(0.8f, 1.1f);
			_collideSound.pitch = Random.Range(0.8f, 1.1f);
			_collideSound.Play();
		}
	}

	private void PlayThrownSound(object sender, DiceEventArgs e)
	{
		_thrownSound.volume = Random.Range(0.8f, 1.1f);
		_thrownSound.pitch = Random.Range(0.8f, 1.1f);
		_thrownSound.Play();
	}
}
