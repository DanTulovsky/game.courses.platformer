using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PushButtonSwitch : MonoBehaviour
{
    [SerializeField]
    private Sprite downSprite;

    [SerializeField]
    private UnityEvent onEnter;

    private SpriteRenderer _spriteRenderer;
    private Sprite _upSprite;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _upSprite = _spriteRenderer.sprite;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Player player = col.GetComponent<Player>();
        if (player == null) return;

        _spriteRenderer.sprite = downSprite;

        onEnter?.Invoke();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (player == null) return;

        _spriteRenderer.sprite = _upSprite;
    }
}