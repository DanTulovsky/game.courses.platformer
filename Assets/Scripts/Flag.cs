using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flag : MonoBehaviour
{
    [SerializeField] private string sceneName;
    private Animator _animator;
    private static readonly int Raise = Animator.StringToHash("Raise");

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        Player player = col.GetComponent<Player>();
        if (player == null) return;

        _animator.SetTrigger(Raise);

        StartCoroutine(LoadAfterDelay());
    }

    private IEnumerator LoadAfterDelay()
    {
        string key = sceneName + "Unlocked";
        PlayerPrefs.SetInt(key, 1);

        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(sceneName);
    }
}
