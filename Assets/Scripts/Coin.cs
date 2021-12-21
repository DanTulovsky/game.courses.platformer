using UnityEngine;

public class Coin : MonoBehaviour
{
    public static int CoinsCollected { get; set; }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Player player = col.GetComponent<Player>();
        if (player == null) return;

        PlaySound();
        DisableCoin();

        CoinsCollected++;
        ScoreSystem.Add(100);
    }

    private void DisableCoin()
    {
        GetComponent<Collider2D>().enabled = false;
        GetComponent<SpriteRenderer>().enabled = false;
    }

    private void PlaySound()
    {
        GetComponent<AudioSource>().Play();
    }
}
