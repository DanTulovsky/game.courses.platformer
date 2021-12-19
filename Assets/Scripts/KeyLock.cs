using UnityEngine;
using UnityEngine.Events;

public class KeyLock : MonoBehaviour
{
    [SerializeField] private UnityEvent onUnlocked;

    public void Unlock()
    {
        onUnlocked.Invoke();
    }
}
