using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float speed = 1;
    [SerializeField] private float slipFactor = 1;
    
    [Header("Jumping")]
    [SerializeField] private float jumpVelocity = 10;
    [SerializeField] private int maxJumps = 2;
    [SerializeField] private float downPull = 0.1f;
    [SerializeField] private float maxJumpDuration = 0.1f;
    
    [Header("Misc")]
    [SerializeField] private Transform feet;

    private Vector2 _startPosition;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private float _fallTimer;
    private float _jumpTimer;
    private int _jumpsRemaining;
    private float _horizontal;

    private static readonly int Walk = Animator.StringToHash("Walk");
    private bool _isGrounded;
    private bool _isOnSlipperySurface;

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
        
        if (_isOnSlipperySurface)
            SlipHorizontal();
        else
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
    
    private void ReadHorizontalInput()
    {
        _horizontal = Input.GetAxis("Horizontal") * speed;
    }

    private void MoveHorizontal()
    {
        _rigidbody2D.velocity = new Vector2(_horizontal, _rigidbody2D.velocity.y);
    }

    private void SlipHorizontal()
    {
        var desiredVelocity = new Vector2(_horizontal, _rigidbody2D.velocity.y);
        var smoothedVelocity = Vector2.Lerp(_rigidbody2D.velocity, desiredVelocity, Time.deltaTime / slipFactor);
        _rigidbody2D.velocity = smoothedVelocity;
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

        _isOnSlipperySurface = hit?.CompareTag("Slippery") ?? false;
    }

    public void ResetToStart()
    {
        transform.position = _startPosition;
        _rigidbody2D.velocity = Vector2.zero;
    }
}