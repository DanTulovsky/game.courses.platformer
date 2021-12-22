using UnityEngine;

public class HittableFromBelow : MonoBehaviour
{
    [SerializeField] protected Sprite usedSprite;

    private SpriteRenderer _spriteRenderer;
    private AudioSource _audioSource;
    protected virtual bool CanUse => true;

    private Animator _animator;
    private static readonly int UseTrigger = Animator.StringToHash("Use");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
    }

    protected virtual void Start()
    {
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!CanUse) return;
        
        Player player = col.collider.GetComponent<Player>();
        if (player == null) return;

        // Hit from below
        if (!(col.GetContact(0).normal.y > 0)) return;

        PlayAnimation();
        PlayAudio();
        Use();
        if (!CanUse)
            _spriteRenderer.sprite = usedSprite;
    }

    private void PlayAudio()
    {
        if (_audioSource != null) _audioSource.Play();
    }

    private void PlayAnimation()
    {
        if (_animator)
            _animator.SetTrigger(UseTrigger);
    }

    protected virtual void Use()
    {
    }
}