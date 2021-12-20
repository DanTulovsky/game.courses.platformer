using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBox : MonoBehaviour
{
    [SerializeField] private Sprite usedSprite;
    [SerializeField] private GameObject item;
    [SerializeField] private Vector2 itemLaunchVelocity = Vector2.up;

    private SpriteRenderer _spriteRenderer;
    private bool used;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (item)
            item.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (used) return;
        
        Player player = col.collider.GetComponent<Player>();
        if (player == null) return;


        // Hit from below
        if (col.GetContact(0).normal.y > 0)
        {
            _spriteRenderer.sprite = usedSprite;
            if (!item) return;

            used = true;
            item.SetActive(true);
            
            if (item.TryGetComponent(out Rigidbody2D rigidbody2D))
            {
                rigidbody2D.velocity = itemLaunchVelocity;
            }
        }
    }
}