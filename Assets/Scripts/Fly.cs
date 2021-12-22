using System;
using UnityEngine;

public class Fly : MonoBehaviour
{
    private Vector2 _startPosition;
    [SerializeField] private Vector2 direction = Vector2.up;
    [SerializeField] private float maxDistance = 2;
    [SerializeField] private float speed = 1;

    private AudioSource _audioSource;

    private void Start()
    {
        _startPosition = transform.position;
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        transform.Translate(direction.normalized * (speed * Time.deltaTime));
        float distance = Vector2.Distance(_startPosition, transform.position);

        if (distance >= maxDistance)
        {
            transform.position = _startPosition + (direction.normalized * maxDistance);
            direction *= -1;
        }
    }

    private void OnTriggerEnter2D( Collider2D col)
    {
        Player player = col.GetComponent<Player>();
        if (!player) return;

        if (_audioSource != null) _audioSource.Play();
    }
}
