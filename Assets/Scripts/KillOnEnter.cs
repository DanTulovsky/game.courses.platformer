using System;
using UnityEngine;

public class KillOnEnter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        ResetPlayer(col.GetComponent<Player>());
    }

    private void OnParticleCollision(GameObject other)
    {
        ResetPlayer(other.GetComponent<Player>());
    }

    private void ResetPlayer(Player player)
    {
        if (player == null) return;

        player.ResetToStart();
    }
}
