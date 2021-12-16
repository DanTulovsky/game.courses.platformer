using System;
using UnityEngine;

public class SpringBoard : MonoBehaviour
{
    [SerializeField] private float bounceVelocity = 10f;
    [SerializeField] private Sprite downSprite;
    private Sprite _upSprite;

    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _upSprite = _spriteRenderer.sprite;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        var player = col.collider.GetComponent<Player>();
        if (player == null) return;

        var rb = player.GetComponent<Rigidbody2D>();
        if (rb == null) return;

        rb.velocity = new Vector2(rb.velocity.x, bounceVelocity);
        _spriteRenderer.sprite = downSprite;
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        var player = other.collider.GetComponent<Player>();
        if (player == null) return;

        _spriteRenderer.sprite = _upSprite;
    }
}