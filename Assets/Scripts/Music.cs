using UnityEngine;

public class Music : MonoBehaviour
{
    public static Music Instance { get; private set; }


    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;

            GetComponent<AudioSource>().Play();

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}