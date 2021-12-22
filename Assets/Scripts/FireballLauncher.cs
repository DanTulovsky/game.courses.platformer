using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballLauncher : MonoBehaviour
{
    [SerializeField] private Fireball fireballPrefab;
    [SerializeField] private float fireRate = 0.25f;

    private Player _player;
    private string _fireButton;
    private float _nextFireTime;
    private string _horizontalAxis;


    private void Awake()
    {
        _player = GetComponent<Player>();
        _fireButton = $"Player{_player.PlayerNumber}Fire1";
        _horizontalAxis = $"P{_player.PlayerNumber}Horizontal";
    }

    private void Update()
    {
        if (Input.GetButtonDown(_fireButton) && Time.time >= _nextFireTime)
        {
            float horizontal = Input.GetAxis(_horizontalAxis);

            Fireball fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
            fireball.Direction = horizontal >= 0f ? 1f : -1f;
            _nextFireTime = Time.time + fireRate;
        }
    }

}