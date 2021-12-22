using System;
using UnityEngine;

public class Key : MonoBehaviour
{

    [SerializeField] private KeyLock keyLock;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        
        Player player = col.GetComponent<Player>();
        if (player != null)
        {
            transform.SetParent(player.transform);
            transform.localPosition = Vector3.up;
            if (_audioSource != null) _audioSource.Play();
        }
        
        KeyLock keyLock = col.GetComponent<KeyLock>();
        if (keyLock != null && keyLock == this.keyLock)
        {
            keyLock.Unlock();
            Destroy(gameObject);
        }
    }
}
