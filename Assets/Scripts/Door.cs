using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private int requiredCoins = 3;
    [SerializeField] private Door exit;
    
    private Animator _animator;
    private static readonly int OpenProp = Animator.StringToHash("Open");

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    [ContextMenu("Open Door")]
    private void Open()
    {
        _animator.SetTrigger(OpenProp);
    }

    private void Update()
    {
        if (Coin.CoinsCollected >= requiredCoins)
        {
            Open();
        } 
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        
        if (exit == null) return;
        
        var player = col.GetComponent<Player>();
        if (player == null) return;

        player.TeleportTo(exit.transform.position);
    }
}
