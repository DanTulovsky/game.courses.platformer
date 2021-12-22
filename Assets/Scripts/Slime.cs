using System.Collections;
using UnityEngine;

public class Slime : MonoBehaviour
{
    [SerializeField] private Transform leftSensor;
    [SerializeField] private Transform rightSensor;
    [SerializeField] private Sprite deadSprite;

    private Rigidbody2D _rigidbody2D;
    private SpriteRenderer _spriteRenderer;
    private AudioSource _audioSource;

    private float _direction = -1f;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
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
        RaycastHit2D sideResult = Physics2D.Raycast(position, new Vector2(_direction, 0), 0.1f);
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
        // Coming from above
        if (normal.y <= -0.6)
        {
            StartCoroutine(Die());
        }
        else
        {
            player.Die();
        }
    }

    private IEnumerator Die()
    {
        GetComponent<Animator>().enabled = false;
        GetComponent<Collider2D>().enabled = false;

        _audioSource.Play();

        _rigidbody2D.simulated = false;
        _spriteRenderer.sprite = deadSprite;

        enabled = false;

        float alpha = 1;

        while (alpha > 0)
        {
            yield return new WaitForFixedUpdate();
            
            alpha -= Time.deltaTime;
            _spriteRenderer.color = new Color(1, 1, 1, alpha);
        }
    }
}