using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIStartLevelButton : MonoBehaviour
{
    [SerializeField] private string levelName;

    public string LevelName => levelName;

    public void LoadLevel()
    {
        SceneManager.LoadScene(LevelName);
    }
}