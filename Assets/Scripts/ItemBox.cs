using UnityEngine;

public class ItemBox : HittableFromBelow
{
    [SerializeField] private Vector2 itemLaunchVelocity = Vector2.up;
    [SerializeField] private GameObject itemPrefab;
    
     private GameObject item;
    private bool _used;
    protected override bool CanUse => _used == false;

    protected override void Use()
    {
        item = Instantiate(itemPrefab, transform.position + Vector3.up, Quaternion.identity, transform);
        
        if (!item) return;
        
        base.Use();
        
        _used = true;
        item.SetActive(true);
            
        if (item.TryGetComponent(out Rigidbody2D rb))
        {
            rb.velocity = itemLaunchVelocity;
        }
    }

    protected override void Start()
    {
        base.Start();
        
        if (item)
            item.SetActive(false);
    }
}