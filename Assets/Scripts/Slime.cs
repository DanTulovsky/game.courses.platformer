using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] private Transform leftSensor;
    [SerializeField] private Transform rightSensor;

    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;

    private float _direction = -1f;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _rigidbody2D.velocity = new Vector2(_direction, _rigidbody2D.velocity.y);

        if (_direction < 0)
        {
            Debug.DrawRay(leftSensor.position, Vector2.down * 0.1f, Color.red);

            var result = Physics2D.Raycast(leftSensor.position, Vector2.down, 0.1f);
            if (result.collider == null)
                TurnAround();
        }
        else
        {
            Debug.DrawRay(rightSensor.position, Vector2.down * 0.1f, Color.red);

            var result = Physics2D.Raycast(rightSensor.position, Vector2.down, 0.1f);
            if (result.collider == null)
                TurnAround();
        }
    }

    private void TurnAround()
    {
        _direction *= -1;
        _spriteRenderer.flipX = _direction > 0;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        Player player = col.collider.GetComponent<Player>();
        if (player == null) return;

        player.ResetToStart();
    }
}