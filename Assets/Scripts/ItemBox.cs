using UnityEngine;

public class ItemBox : HittableFromBelow
{
    [SerializeField] private GameObject item;
    [SerializeField] private Vector2 itemLaunchVelocity = Vector2.up;

    private bool _used;
    protected override bool CanUse => _used == false && item != null;

    protected override void Use()
    {
        if (!item) return;
        
        base.Use();
        
        if (_used) return;
        
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