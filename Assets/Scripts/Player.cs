using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed = 1;
    [SerializeField] private float jumpForce = 200;
    [SerializeField] private int maxJumps = 2;
    [SerializeField] private Transform feet;

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

    void Update()
    {
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
            _rigidbody2D.AddForce(Vector2.up * jumpForce);
            _jumpsRemaining--;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        var hit = Physics2D.OverlapCircle(feet.position, 0.1f, LayerMask.GetMask("Default"));
        if (hit != null)
        {
            _jumpsRemaining = maxJumps;
        }
    }

    public void ResetToStart()
    {
        transform.position = _startPosition;
        _rigidbody2D.velocity = Vector2.zero;
    }
}