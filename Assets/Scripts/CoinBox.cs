using System;
using UnityEngine;

public class CoinBox : MonoBehaviour
{
    [SerializeField] private int totalCoins = 3;
    [SerializeField] private Sprite usedSprite;

    private SpriteRenderer _spriteRenderer;
    
    private int _remainingCoins;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _remainingCoins = totalCoins;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Player player = col.collider.GetComponent<Player>();
        if (player == null) return;


        // Hit from below
        if (col.GetContact(0).normal.y > 0 && _remainingCoins > 0)
        {
            _remainingCoins--;
            Coin.CoinsCollected++;

            if (_remainingCoins <= 0)
                _spriteRenderer.sprite = usedSprite;
        }
    }
}