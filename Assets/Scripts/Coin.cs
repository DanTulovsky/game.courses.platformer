using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public static int CoinsCollected { get; set; }

    [SerializeField] private List<AudioClip> clips;

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
        if (clips.Count == 0) return;

        int randomIndex = UnityEngine.Random.Range(0, clips.Count-1);
        GetComponent<AudioSource>().PlayOneShot(clips[randomIndex]);
    }
}
