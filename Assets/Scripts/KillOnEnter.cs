using UnityEngine;

public class KillOnEnter : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        Player player = col.GetComponent<Player>();
        ResetPlayer(player);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.collider.GetComponent<Player>();
        ResetPlayer(player);
    }

    private void OnParticleCollision(GameObject other)
    {
        ResetPlayer(other.GetComponent<Player>());
    }

    private void ResetPlayer(Player player)
    {
        if (player == null) return;

        player.Die();
    }
}
