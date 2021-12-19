using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{

    [SerializeField] private KeyLock keyLock;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        
        Player player = col.GetComponent<Player>();
        if (player != null)
        {
            transform.SetParent(player.transform);
            transform.localPosition = Vector3.up;
        }
        
        KeyLock keyLock = col.GetComponent<KeyLock>();
        if (keyLock != null && keyLock == this.keyLock)
        {
            keyLock.Unlock();
            Destroy(gameObject);
        }
    }
}
