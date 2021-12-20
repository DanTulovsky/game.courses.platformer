using UnityEngine;
using UnityEngine.Events;

public class PushButtonSwitch : MonoBehaviour
{
    [SerializeField] private Sprite pressedSprite;
    [SerializeField] private UnityEvent onPressed;
    [SerializeField] private UnityEvent onReleased;
    [SerializeField] private int playerNumber = 1;

    private SpriteRenderer _spriteRenderer;
    private Sprite _releasedSprite;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _releasedSprite = _spriteRenderer.sprite;
        
        BecomeReleased();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Player player = col.GetComponent<Player>();
        if (player == null || player.PlayerNumber != playerNumber) return;

        BecomePressed();
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        if (player == null || player.PlayerNumber != playerNumber) return;

        BecomeReleased();
    }

    private void BecomePressed()
    {
        _spriteRenderer.sprite = pressedSprite;
        onPressed?.Invoke();
    }
    
    private void BecomeReleased()
    {
        if (onReleased.GetPersistentEventCount() != 0)
        {
            _spriteRenderer.sprite = _releasedSprite;
            onReleased?.Invoke();
        }
    }
}