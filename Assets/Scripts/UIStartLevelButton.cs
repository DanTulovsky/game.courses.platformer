using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIStartLevelButton : MonoBehaviour
{
    [SerializeField] private string levelName;

    public void LoadLevel()
    {
        SceneManager.LoadScene(levelName);
    }

    private void OnValidate()
    {
        GetComponentInChildren<TMP_Text>()?.SetText(levelName);
    }
}
