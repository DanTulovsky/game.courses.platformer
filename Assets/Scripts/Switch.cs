using System;
using UnityEngine;
using UnityEngine.Events;

public class Switch : MonoBehaviour
{
    [SerializeField] private Sprite toggleRight;
    [SerializeField] private Sprite toggleLeft;
    [SerializeField] private Sprite toggleCenter;
    
    [SerializeField] private UnityEvent onToggleRight;
    [SerializeField] private UnityEvent onToggleCenter;
    [SerializeField] private UnityEvent onToggleLeft;

    [SerializeField] private ToggleDirection startingDirection = ToggleDirection.Center;

    private SpriteRenderer _spriteRenderer;

    private ToggleDirection _currentDirection = ToggleDirection.Center;

    private enum ToggleDirection
    {
        Left,
        Center,
        Right
    }

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        SetToggleDirection(startingDirection, true);
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        Player player = col.GetComponent<Player>();
        if (!player) return;

        var playerRigidBody = player.GetComponent<Rigidbody2D>();
        if (!playerRigidBody) return;

        bool wasOnRight = col.transform.position.x > transform.position.x;
        bool wasOnLeft = !wasOnRight;

        Vector2 velocity = playerRigidBody.velocity;
        bool playerWalkingRight = velocity.x > 0;
        bool playerWalkingLeft = velocity.x < 0;

        if (wasOnRight && playerWalkingRight)
            SetToggleDirection(ToggleDirection.Right);
        else if (wasOnLeft && playerWalkingLeft)
            SetToggleDirection(ToggleDirection.Left);
    }

    private void SetToggleDirection(ToggleDirection direction, bool force = false)
    {
        if (_currentDirection == direction && !force) return;
        
        _currentDirection = direction;
        
        switch (direction)
        {
            case ToggleDirection.Left:
                _spriteRenderer.sprite = toggleLeft;
                onToggleLeft.Invoke();
                break;
            case ToggleDirection.Center:
                _spriteRenderer.sprite = toggleCenter;
                onToggleCenter.Invoke();
                break;
            case ToggleDirection.Right:
                _spriteRenderer.sprite = toggleRight;
                onToggleRight.Invoke();
                break;
        }
    }

    private void OnValidate()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        switch (startingDirection)
        {
            case ToggleDirection.Left:
                _spriteRenderer.sprite = toggleLeft;
                break;
            case ToggleDirection.Center:
                _spriteRenderer.sprite = toggleCenter;
                break;
            case ToggleDirection.Right:
                _spriteRenderer.sprite = toggleRight;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}