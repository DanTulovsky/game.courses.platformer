using UnityEngine;

public class HittableFromBelow : MonoBehaviour
{
    [SerializeField] protected Sprite usedSprite;

    private SpriteRenderer _spriteRenderer;
    protected virtual bool CanUse => true;

    protected virtual void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!CanUse) return;
        
        Player player = col.collider.GetComponent<Player>();
        if (player == null) return;

        // Hit from below
        if (!(col.GetContact(0).normal.y > 0)) return;
        
        Use();
        if (!CanUse)
            _spriteRenderer.sprite = usedSprite;
    }

    protected virtual void Use()
    {
    }
}