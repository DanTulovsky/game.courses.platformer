using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.GetComponent<Player>() == null) return;

        if (col.GetContact(0).normal.y > 0)
        {
            // Hit from below
            TakeHit();
        }
        
    }

    private void TakeHit()
    {
        gameObject.SetActive(false);
    }
}
