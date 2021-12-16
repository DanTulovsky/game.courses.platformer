using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private float jumpVelocity = 10;
    [SerializeField] private int maxJumps = 2;
    [SerializeField] private Transform feet;
    [SerializeField] private float downPull = 0.1f;

    private float _fallTimer;
    private Vector2 _startPosition;
    private Rigidbody2D _rigidbody2D;
    private int _jumpsRemaining;
    private static readonly int Walk = Animator.StringToHash("Walk");

    private void Start()
    {
        _startPosition = transform.position;
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _jumpsRemaining = maxJumps;
    }

    private void Update()
    {
        var hit = Physics2D.OverlapCircle(feet.position, 0.1f, LayerMask.GetMask("Default"));
        bool isGrounded = hit != null;

        var horizontal = Input.GetAxis("Horizontal") * speed;

        if (Mathf.Abs(horizontal) >= 1)
        {
            _rigidbody2D.velocity = new Vector2(horizontal, _rigidbody2D.velocity.y);
        }

        var animator = GetComponent<Animator>();
        bool walking = horizontal != 0;
        animator.SetBool(Walk, walking);

        if (horizontal != 0)
        {
            var spriteRenderer = GetComponent<SpriteRenderer>();
            spriteRenderer.flipX = horizontal < 0;
        }

        if (Input.GetButtonDown("Fire1") && _jumpsRemaining > 0)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpVelocity);
            _jumpsRemaining--;
            _fallTimer = 0;
        }

        if (isGrounded)
        {
            _fallTimer = 0;
            _jumpsRemaining = maxJumps;
        }
        else
        {
            _fallTimer += Time.deltaTime;
            var downForce = downPull * _fallTimer * _fallTimer;
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _rigidbody2D.velocity.y - downForce);
        }
    }

    public void ResetToStart()
    {
        transform.position = _startPosition;
        _rigidbody2D.velocity = Vector2.zero;
    }
}