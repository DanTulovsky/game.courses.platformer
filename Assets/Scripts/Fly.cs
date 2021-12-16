using System;
using UnityEngine;

public class Fly : MonoBehaviour
{
    private Vector2 _startPosition;
    [SerializeField] private Vector2 direction = Vector2.up;
    [SerializeField] private float maxDistance = 2;
    [SerializeField] private float speed = 1;



    private void Start()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        transform.Translate(direction.normalized * (speed * Time.deltaTime));
        var distance = Vector2.Distance(_startPosition, transform.position);

        if (distance >= maxDistance)
        {
            transform.position = _startPosition + (direction.normalized * maxDistance);
            direction *= -1;
        }
    }
}
