using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private int playerNumber = 1;

    [Header("Movement")] [SerializeField] private float speed = 1;
    [SerializeField] private float slipFactor = 1;

    [Header("Jumping")] [SerializeField] private float jumpVelocity = 10;
    [SerializeField] private int maxJumps = 2;
    [SerializeField] private float downPull = 0.1f;
    [SerializeField] private float maxJumpDuration = 0.1f;

    [Header("Misc")] [SerializeField] private Transform feet;

    private AudioSource _jumpAudio;

    private Vector2 _startPosition;
    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private Animator _animator;

    private float _fallTimer;
    private float _jumpTimer;
    private int _jumpsRemaining;
    private float _horizontal;

    private static readonly int WalkParam = Animator.StringToHash("Walk");
    private static readonly int JumpParam = Animator.StringToHash("Jump");
    private bool _isGrounded;
    private bool _isOnSlipperySurface;
    private string _jumpButton;
    private string _horizontalAxis;
    private LayerMask _defaultMask;

    public int PlayerNumber => playerNumber;

    private void Awake()
    {
        _jumpAudio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        _jumpButton = $"P{playerNumber}Jump";
        _horizontalAxis = $"P{playerNumber}Horizontal";
        _defaultMask = LayerMask.GetMask("Default");

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


    private void Jump()
    {
        if (_jumpAudio != null) _jumpAudio.Play();

        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpVelocity);
        _jumpsRemaining--;
        _fallTimer = 0;
        _jumpTimer = 0;
    }


    private void ReadHorizontalInput()
    {
        _horizontal = Input.GetAxis(_horizontalAxis) * speed;
    }

    private bool ShouldContinueJump()
    {
        return Input.GetButton(_jumpButton) && _jumpTimer <= maxJumpDuration;
    }

    private bool ShouldStartJump()
    {
        return Input.GetButtonDown(_jumpButton) && _jumpsRemaining > 0;
    }

    private void MoveHorizontal()
    {
        _rigidbody2D.velocity = new Vector2(_horizontal, _rigidbody2D.velocity.y);
    }

    private void SlipHorizontal()
    {
        Vector2 desiredVelocity = new Vector2(_horizontal, _rigidbody2D.velocity.y);
        Vector2 smoothedVelocity = Vector2.Lerp(_rigidbody2D.velocity, desiredVelocity, Time.deltaTime / slipFactor);
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
        _animator.SetBool(WalkParam, walking);
        _animator.SetBool(JumpParam, ShouldContinueJump());
    }

    private void UpdateIsGrounded()
    {
        Collider2D hit = Physics2D.OverlapCircle(feet.position, 0.1f, _defaultMask);
        _isGrounded = hit != null;

        _isOnSlipperySurface = hit != null && hit.CompareTag("Slippery");
    }

    public void Die()
    {
        ScoreSystem.ResetScore();
        SceneManager.LoadScene("MainMenu");
        // _rigidbody2D.position = _startPosition;
        // _rigidbody2D.velocity = Vector2.zero;
    }

    public void TeleportTo(Vector3 to)
    {
        _rigidbody2D.position = to;
        _rigidbody2D.velocity = Vector2.zero;
    }
}