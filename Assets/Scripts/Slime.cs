using UnityEngine;
using UnityEngine.UI;

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

        ScanSensor(_direction < 0 ? leftSensor : rightSensor);
    }

    private void ScanSensor(Transform sensor)
    {
        Vector3 position = sensor.position;

        Debug.DrawRay(position, Vector2.down * 0.1f, Color.red);
        RaycastHit2D downResult = Physics2D.Raycast(position, Vector2.down, 0.1f);
        if (downResult.collider == null)
            TurnAround();

        Debug.DrawRay(sensor.position, new Vector2(_direction, 0) * 0.1f, Color.red);
        var sideResult = Physics2D.Raycast(position, new Vector2(_direction, 0), 0.1f);
        if (sideResult.collider != null)
            TurnAround();
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

        Vector2 normal = col.GetContact(0).normal;
        if (normal.y <= -0.5)
        {
            Die();
        }
        else
        {
            player.ResetToStart();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}