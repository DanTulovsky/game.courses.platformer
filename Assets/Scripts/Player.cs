using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private float jumpForce = 200;
    private Vector2 _startPosition;
    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _startPosition = transform.position;
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        var horizontal = Input.GetAxis("Horizontal") * speed;

        if (Mathf.Abs(horizontal) >= 1)
        {
            rigidBody2D.velocity = new Vector2(horizontal, rigidBody2D.velocity.y);
        }

        var animator = GetComponent<Animator>();
        bool walking = horizontal != 0;
        animator.SetBool("Walk", walking);

        if (horizontal != 0)
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.flipX = horizontal < 0;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            rigidBody2D.AddForce(Vector2.up * jumpForce);
        }
    }

    public void ResetToStart()
    {
        transform.position = _startPosition;
        _rigidbody2D.velocity = Vector2.zero;
    }
}