using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float speed = 1;

    [SerializeField]
    private float jumpVelocity = 10;

    [SerializeField]
    private int maxJumps = 2;

    [SerializeField]
    private Transform feet;

    [SerializeField]
    private float downPull = 0.1f;

    [SerializeField]
    private float maxJumpDuration = 0.1f;

    private Vector2 _startPosition;
    private Rigidbody2D _rigidbody2D;

    private float _fallTimer;
    private float _jumpTimer;
    private int _jumpsRemaining;
    private float _horizontal;

    private static readonly int Walk = Animator.StringToHash("Walk");
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;
    private bool _isGrounded;

    private void Start()
    {
        _startPosition = transform.position;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        _jumpsRemaining = maxJumps;
    }

    private void Update()
    {
        UpdateIsGrounded();

        ReadHorizontalInput();
        MoveHorizontal();
        UpdateAnimator();
        UpdateSpriteDirection();

        if (ShouldStartJump()) Jump();
        else if (ShouldContinueJump()) ContinueJump();

        _jumpTimer += Time.deltaTime;

        if (_isGrounded && _fallTimer > 0)
        {
            _fallTimer = 0;
            _jumpsRemaining = maxJumps;
        }
        else
        {
            _fallTimer += Time.deltaTime;
            float downForce = downPull * _fallTimer * _fallTimer;
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _rigidbody2D.velocity.y - downForce);
        }
    }

    private void ContinueJump()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpVelocity);
        _fallTimer = 0;
    }

    private bool ShouldContinueJump()
    {
        return Input.GetButton("Fire1") && _jumpTimer <= maxJumpDuration;
    }

    private void Jump()
    {
        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpVelocity);
        _jumpsRemaining--;
        _fallTimer = 0;
        _jumpTimer = 0;
    }

    private bool ShouldStartJump()
    {
        return Input.GetButtonDown("Fire1") && _jumpsRemaining > 0;
    }

    private void MoveHorizontal()
    {
        if (Mathf.Abs(_horizontal) >= 1)
        {
            _rigidbody2D.velocity = new Vector2(_horizontal, _rigidbody2D.velocity.y);
        }
    }

    private void ReadHorizontalInput()
    {
        _horizontal = Input.GetAxis("Horizontal") * speed;
    }

    private void UpdateSpriteDirection()
    {
        if (_horizontal != 0)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.flipX = _horizontal < 0;
        }
    }

    private void UpdateAnimator()
    {
        bool walking = _horizontal != 0;
        _animator.SetBool(Walk, walking);
    }

    private void UpdateIsGrounded()
    {
        Collider2D hit = Physics2D.OverlapCircle(feet.position, 0.1f, LayerMask.GetMask("Default"));
        _isGrounded = hit != null;
    }

    public void ResetToStart()
    {
        transform.position = _startPosition;
        _rigidbody2D.velocity = Vector2.zero;
    }
}