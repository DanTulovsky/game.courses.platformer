using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIStartLevelButton : MonoBehaviour
{
    [SerializeField] private string levelName;

    public void LoadLevel()
    {
        SceneManager.LoadScene(levelName);
    }
}
