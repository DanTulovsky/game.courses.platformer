using System.Linq;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [SerializeField] private Collectible[] collectibles;
    
    // Start is called before the first frame update
    private void Start()
    { 
        
    }

    // Update is called once per frame
    private void Update()
    {
        foreach (Collectible t in collectibles)
        {
            if (t.gameObject.activeSelf) return;
        }

        Debug.Log("Got all gems!");
    }
}