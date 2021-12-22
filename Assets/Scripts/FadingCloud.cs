using System.Collections;
using UnityEngine;

public class FadingCloud : HittableFromBelow
{
    [SerializeField] private float resetDelay = 2f;

    protected override void Use()
    {
        SpriteRenderer.enabled = false;
        Collider2D.enabled = false;

        StartCoroutine(ResetAfterDelay(resetDelay));
    }

    private IEnumerator ResetAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        SpriteRenderer.enabled = true;
        Collider2D.enabled = true;
    }
}