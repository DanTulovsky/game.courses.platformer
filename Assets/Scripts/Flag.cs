using UnityEngine;
using UnityEngine.SceneManagement;

public class Flag : MonoBehaviour
{
    [SerializeField] private string sceneName;

    private void OnTriggerEnter2D(Collider2D col)
    {
        var player = col.GetComponent<Player>();
        if (player == null) return;

        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("Raise");

        SceneManager.LoadScene(sceneName);
    }
}
