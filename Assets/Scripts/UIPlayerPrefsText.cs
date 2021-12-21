using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIPlayerPrefsText : MonoBehaviour
{
    [SerializeField] private ScoreSystem.PlayerPref key;

    private TMP_Text _tmpText;

    private void Start()
    {
        _tmpText = GetComponent<TMP_Text>();
        _tmpText.SetText(PlayerPrefs.GetInt(key.ToString()).ToString());
    }

    private void OnValidate()
    {
        _tmpText = GetComponent<TMP_Text>();
        _tmpText.SetText(PlayerPrefs.GetInt(key.ToString()).ToString());
    }
}
