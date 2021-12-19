using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private int requiredCoins = 3;
    [SerializeField] private Door exit;
    [SerializeField] private Canvas _canvas;

    private Animator _animator;
    private static readonly int OpenProp = Animator.StringToHash("Open");
    private bool _open;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    [ContextMenu("Open Door")]
    private void Open()
    {
        _animator.SetTrigger(OpenProp);
        _open = true;
        if (_canvas)
            _canvas.enabled = false;
        
    }

    private void Update()
    {
        if (!_open && Coin.CoinsCollected >= requiredCoins)
        {
            Open();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (exit == null || !_open) return;

        Player player = col.GetComponent<Player>();
        if (player == null) return;

        player.TeleportTo(exit.transform.position);
    }
}