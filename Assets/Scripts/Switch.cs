using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Switch : MonoBehaviour
{
    [SerializeField] private Sprite toggleRight;
    [SerializeField] private Sprite toggleLeft;
    [SerializeField] private UnityEvent onToggleRight;
    [SerializeField] private UnityEvent onToggleLeft;

    private SpriteRenderer _spriteRenderer;

    private Sprite _toggleCenter;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _toggleCenter = _spriteRenderer.sprite;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        Player player = col.GetComponent<Player>();
        if (!player) return;
        
        var playerRigidBody = player.GetComponent<Rigidbody2D>();
        if (!playerRigidBody) return;
        
        bool wasOnRight = col.transform.position.x > transform.position.x;
        bool wasOnLeft = !wasOnRight;
        
        bool playerWalkingRight = playerRigidBody.velocity.x > 0;
        bool playerWalkingLeft = playerRigidBody.velocity.x < 0;

        if (wasOnRight && playerWalkingRight)
            ToggleRight();
        else if (wasOnLeft && playerWalkingLeft)
            ToggleLeft();
    }

    private void ToggleRight()
    {
        _spriteRenderer.sprite = toggleRight;
        onToggleRight.Invoke();
    }

    private void ToggleLeft()
    {
        _spriteRenderer.sprite = toggleLeft;
        onToggleLeft.Invoke();
    }

    private void ToggleCenter()
    {
        _spriteRenderer.sprite = _toggleCenter;
    }
}