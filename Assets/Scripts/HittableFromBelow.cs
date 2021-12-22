using UnityEngine;

public abstract class HittableFromBelow : MonoBehaviour
{
    [SerializeField] protected Sprite usedSprite;

    protected SpriteRenderer SpriteRenderer;
    protected Collider2D Collider2D;

    private AudioSource _audioSource;
    protected virtual bool CanUse => true;

    private Animator _animator;
    private static readonly int UseTrigger = Animator.StringToHash("Use");

     protected void Awake()
    {
        _animator = GetComponent<Animator>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        _audioSource = GetComponent<AudioSource>();
        Collider2D = GetComponent<Collider2D>();
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
            SpriteRenderer.sprite = usedSprite;
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

    protected abstract void Use();
}