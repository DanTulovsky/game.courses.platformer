using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float launchForce = 5;
    [SerializeField] private int maxBounces = 3;
    [SerializeField] private float bounceForce = 4f;

    private Rigidbody2D _rigidbody2D;
    private int _bounces;
    public float Direction { get; set; }

    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _rigidbody2D.velocity = new Vector2(launchForce * Direction, 0);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        _bounces++;
        if (_bounces > maxBounces)
            Destroy(gameObject);
        else
            _rigidbody2D.velocity = new Vector2(launchForce * Direction, bounceForce);
    }

    private void Update()
    {
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}