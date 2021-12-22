using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballLauncher : MonoBehaviour
{
    [SerializeField] private Fireball fireballPrefab;

    private Player _player;
    private string _fireButton;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _fireButton = $"Player{_player.PlayerNumber}Fire1";
    }

    private void Update()
    {
        if (Input.GetButtonDown(_fireButton))
        {
            Instantiate(fireballPrefab, transform.position, Quaternion.identity);
        }
    }

}