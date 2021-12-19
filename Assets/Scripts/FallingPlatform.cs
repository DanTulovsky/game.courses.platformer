using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public bool playerInside;

    [Range(0.1f, 5f)] [SerializeField] private float fallAfterSeconds = 3;

    [Range(0.005f, 0.01f)]
    [SerializeField]
    private float shakeY = 0.005f;

    [Range(0.005f, 0.01f)]
    [SerializeField]
    private float shakeX = 0.005f;

    [Tooltip("Reset platform timer when nobody is on it.")]
    [SerializeField]
    private bool resetOnEmpty;

    private readonly HashSet<Player> _playersInTrigger = new();

    private Coroutine _wiggleAndFallCoroutine;
    private Vector3 _initialPosition;
    private float _wiggleTimer;

    private void Start()
    {
        _initialPosition = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Player player = col.GetComponent<Player>();
        if (player == null) return;

        _playersInTrigger.Add(player);
        playerInside = true;

        if (_playersInTrigger.Count == 1)
            _wiggleAndFallCoroutine = StartCoroutine(WiggleAndFall());
    }

    private IEnumerator WiggleAndFall()
    {
        // waiting to wiggle
        yield return new WaitForSeconds(0.25f);

        // wiggle
        // _wiggleTimer = 0;
        while (_wiggleTimer < fallAfterSeconds)
        {
            // ReSharper disable once RedundantNameQualifier
            float randomX = UnityEngine.Random.Range(-shakeX, shakeX);
            // ReSharper disable once RedundantNameQualifier
            float randomY = UnityEngine.Random.Range(-shakeY, shakeY);

            transform.position = _initialPosition + new Vector3(randomX, randomY);
            float randomDelay = UnityEngine.Random.Range(0.005f, 0.01f);
            yield return new WaitForSeconds(randomDelay);

            _wiggleTimer += randomDelay;
        }

        // fall
        foreach (Collider2D col in GetComponents<Collider2D>())
        {
            col.enabled = false;
        }

        gameObject.AddComponent<Rigidbody2D>();
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        Player player = col.GetComponent<Player>();
        if (player == null) return;

        _playersInTrigger.Remove(player);

        if (_playersInTrigger.Count == 0)
        {
            playerInside = false;
            StopCoroutine(_wiggleAndFallCoroutine);

            if (resetOnEmpty)
                _wiggleTimer = 0;
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}