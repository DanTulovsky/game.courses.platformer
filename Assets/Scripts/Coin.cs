using UnityEngine;

public class Coin : MonoBehaviour
{
    private static int _coinsCollected;

    private void OnTriggerEnter2D(Collider2D col)
    {
        
        Player player = col.GetComponent<Player>();
        if (player == null) return;
        
        gameObject.SetActive(false);
        _coinsCollected++;
    }
}
