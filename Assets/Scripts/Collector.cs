using System;
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
            c.OnPickedUp += ItemPickedUp;
        }

        _remainingText?.SetText(_collectibles.Count.ToString());
    }

    private void ItemPickedUp()
    {
        _countCollected++;
        int countRemaining = _collectibles.Count - _countCollected;

        _remainingText?.SetText(countRemaining.ToString());

        if (countRemaining > 0) return;

        onCollectionComplete?.Invoke();
    }

    private void OnDestroy()
    {
        foreach (Collectible c in _collectibles)
        {
            c.OnPickedUp -= ItemPickedUp;
        }
    }

    private void OnDrawGizmos()
    {
        _collectibles ??= new HashSet<Collectible>(GetComponentsInChildren<Collectible>());

        Gizmos.color = Color.gray;
        
        foreach (Collectible c in _collectibles)
        {
            Gizmos.DrawLine(transform.position, c.transform.position);
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        _collectibles ??= new HashSet<Collectible>(GetComponentsInChildren<Collectible>());

        Gizmos.color = Color.yellow;
        
        foreach (Collectible c in _collectibles)
        {
            Gizmos.DrawLine(transform.position, c.transform.position);
        }
    }
}