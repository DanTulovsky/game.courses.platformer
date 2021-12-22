using System;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public event Action OnPickedUp;

    private AudioSource _audioSource;
    private SpriteRenderer _spriteRenderer;
    private Collider2D _collider2D;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider2D = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Player player = col.GetComponent<Player>();
        if (player == null) return;
        
        Disable();
        
        OnPickedUp?.Invoke();

        if (_audioSource != null) _audioSource.Play();
    }

    private void Disable()
    {
        _spriteRenderer.enabled = false;
        _collider2D.enabled = false;
    }
}