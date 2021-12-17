using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PushButtonSwitch : MonoBehaviour
{
    [SerializeField] private Sprite pressedSprite;
    [SerializeField] private UnityEvent onPressed;
    [SerializeField] private UnityEvent onReleased;

    private SpriteRenderer _spriteRenderer;
    private Sprite _releasedSprite;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _releasedSprite = _spriteRenderer.sprite;
        
        BecomeReleased();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Player player = col.GetComponent<Player>();
        if (player == null) return;

        BecomePressed();
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (player == null) return;

        BecomeReleased();
    }

    private void BecomePressed()
    {
        _spriteRenderer.sprite = pressedSprite;
        onPressed?.Invoke();
    }
    
    private void BecomeReleased()
    {
        _spriteRenderer.sprite = _releasedSprite;
        onReleased?.Invoke();
    }
}