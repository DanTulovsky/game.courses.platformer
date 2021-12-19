using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        
        Player player = col.GetComponent<Player>();
        if (player == null) return;
        
        transform.SetParent(player.transform);
        transform.localPosition = Vector3.up;
    }
}
