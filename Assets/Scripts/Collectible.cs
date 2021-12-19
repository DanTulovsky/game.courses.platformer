using System;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public event Action OnPickedUp;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        Player player = col.GetComponent<Player>();
        if (player == null) return;
        
        gameObject.SetActive(false);
        
        OnPickedUp?.Invoke();
    }

}