using UnityEngine;

public class PlayerSoundBehaviour : MonoBehaviour
{
    private AudioSource _collideSound;
    private PlayersMover _playersMover;

    private void Awake()
    {
        _collideSound = GetComponentInChildren<AudioSource>();
    }

    private void OnEnable()
    {
        _playersMover = GetComponentInParent<PlayersMover>();
        _playersMover.MovedEvent += PlaySound;
    }

    private void OnDisable()
    {
        _playersMover.MovedEvent -= PlaySound;
    }

    private void PlaySound(object sender, PlayerEvent e)
    {
        _collideSound.volume = Random.Range(0.8f, 1.1f);
        _collideSound.pitch = Random.Range(0.8f, 1.1f);
        _collideSound.Play();
    }
}
