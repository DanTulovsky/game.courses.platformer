using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        var player = col.GetComponent<Player>();
        if (player == null) return;

        // Play flag wave
        var animator = GetComponent<Animator>();
        animator.SetTrigger("Raise");

        // Load new level
    }
}
