using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    private Vector2 _startPosition;
    private Vector2 _direction = Vector2.up;

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        transform.Translate(_direction * Time.deltaTime);
        var distance = Vector2.Distance(_startPosition, transform.position);

        if (distance >= 2)
        {
            _direction *= -1;
        }
    }
}