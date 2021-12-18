using UnityEngine;

public class Breakable : MonoBehaviour
{
    private ParticleSystem _particleSystem;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
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
        
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }
}
