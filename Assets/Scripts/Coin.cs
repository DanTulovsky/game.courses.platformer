using UnityEngine;

public class Coin : MonoBehaviour
{
    public static int CoinsCollected { get; set; }

    private void OnTriggerEnter2D(Collider2D col)
    {
        
        Player player = col.GetComponent<Player>();
        if (player == null) return;
        
        gameObject.SetActive(false);
        CoinsCollected++;

        ScoreSystem.Add(100);
    }
}
