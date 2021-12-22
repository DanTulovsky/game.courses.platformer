using UnityEngine;

public class Breakable : MonoBehaviour, ITakeDamage
{
    private ParticleSystem _particleSystem;
    private AudioSource _audioSource;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.GetComponent<Player>() == null) return;

        if (col.GetContact(0).normal.y > 0)
        {
            // Hit from below
            TakeHit();
        }
        
    }

    private void TakeHit()
    {
        _particleSystem.Play();
        if (_audioSource != null) _audioSource.Play();

        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }

    public void TakeDamage()
    {
        TakeHit();
    }
}
