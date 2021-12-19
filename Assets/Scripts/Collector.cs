using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Collector : MonoBehaviour
{
    private HashSet<Collectible> _collectibles;
    private TMP_Text _remainingText;

    private void Awake()
    {
        _collectibles = new HashSet<Collectible>(GetComponentsInChildren<Collectible>());
    }

    // Start is called before the first frame update
    private void Start()
    {
        _remainingText = GetComponentInChildren<TMP_Text>();
    }

    // Update is called once per frame
    private void Update()
    {
        int countRemaining = 0;
        
        foreach (Collectible t in _collectibles)
        {
            if (t.gameObject.activeSelf)
            {
                countRemaining++;
            }
        }

        _remainingText?.SetText(countRemaining.ToString());
        
        if (countRemaining > 0 ) return;
    }
}