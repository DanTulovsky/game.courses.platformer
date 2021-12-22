using UnityEngine;
using UnityEngine.Events;

public class KeyLock : MonoBehaviour
{
    [SerializeField] private UnityEvent onUnlocked;

    public void Unlock()
    {
        onUnlocked.Invoke();
    }

    public void Disable()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }
}