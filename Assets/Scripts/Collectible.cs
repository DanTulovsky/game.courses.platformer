using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    private readonly List<Collector> _collectors = new();
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        Player player = col.GetComponent<Player>();
        if (player == null) return;
        
        gameObject.SetActive(false);
        foreach (Collector collector in _collectors)
        {
            collector.ItemPickedUp();
        }
    }

    public void AddCollector(Collector collector)
    {
        _collectors.Add(collector);
    }
}