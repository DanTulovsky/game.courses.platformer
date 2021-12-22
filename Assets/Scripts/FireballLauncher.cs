using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballLauncher : MonoBehaviour
{
    [SerializeField] private Fireball fireballPrefab;

    private void Start()
    {
        Instantiate(fireballPrefab, transform.position, Quaternion.identity);
    }

    private void Update()
    {
        
    }
}
