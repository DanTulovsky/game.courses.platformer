using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class Collector : MonoBehaviour
{
    [SerializeField] private UnityEvent onCollectionComplete;

    private static readonly int OpenProp = Animator.StringToHash("Open");

    private HashSet<Collectible> _collectibles;
    private TMP_Text _remainingText;
    private int _countCollected;

    private void Awake()
    {
        _collectibles = new HashSet<Collectible>(GetComponentsInChildren<Collectible>());
    }

    // Start is called before the first frame update
    private void Start()
    {
        _remainingText = GetComponentInChildren<TMP_Text>();

        foreach (Collectible c in _collectibles)
        {
            c.SetCollector(this);
        }
        
        _remainingText?.SetText(_collectibles.Count.ToString());
    }

    public void ItemPickedUp(Collectible collectible)
    {
        _countCollected++;
        int countRemaining = _collectibles.Count - _countCollected;

        _remainingText?.SetText(countRemaining.ToString());

        if (countRemaining > 0) return;

        onCollectionComplete?.Invoke();
    }
}