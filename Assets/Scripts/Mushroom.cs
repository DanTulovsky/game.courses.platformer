using UnityEngine;

public class Mushroom : MonoBehaviour
{
    [SerializeField] private float bounceVelocity = 10f;

    private AudioSource _audioSource;
    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider2D;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Player player = col.collider.GetComponent<Player>();
        if (player == null) return;

        Rigidbody2D rb = player.GetComponent<Rigidbody2D>();
        if (rb == null) return;

        rb.velocity = new Vector2(rb.velocity.x, bounceVelocity);

        if (_audioSource != null) _audioSource.Play();
    }
}