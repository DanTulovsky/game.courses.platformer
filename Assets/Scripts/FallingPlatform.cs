using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public bool playerInside;

    private readonly HashSet<Player> _playersInTrigger = new();

    private Coroutine _wiggleAndFallCoroutine;
    private Vector3 _initialPosition;

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
        float wiggleTimer = 0;
        while (wiggleTimer < 1f)
        {
            // ReSharper disable once RedundantNameQualifier
            float randomX = UnityEngine.Random.Range(-0.01f, 0.01f);
            // ReSharper disable once RedundantNameQualifier
            float randomY = UnityEngine.Random.Range(-0.01f, 0.01f);
            
            transform.position = _initialPosition + new Vector3(randomX, randomY);
            float randomDelay = UnityEngine.Random.Range(0.005f, 0.01f);
            yield return new WaitForSeconds(randomDelay);

            wiggleTimer += randomDelay;
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
        var player = col.GetComponent<Player>();
        if (player == null) return;

        _playersInTrigger.Remove(player);

        if (_playersInTrigger.Count == 0)
        {
            playerInside = false;
            StopCoroutine(_wiggleAndFallCoroutine);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}