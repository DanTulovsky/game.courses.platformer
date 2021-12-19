using UnityEngine;

public class Collectible : MonoBehaviour
{
    private Collector _collector;
    
    private void OnTriggerEnter2D(Collider2D col)
    {
        Player player = col.GetComponent<Player>();
        if (player == null) return;
        
        gameObject.SetActive(false);
        _collector.ItemPickedUp(this);
    }

    public void SetCollector(Collector collector)
    {
        _collector = collector;
    }
}