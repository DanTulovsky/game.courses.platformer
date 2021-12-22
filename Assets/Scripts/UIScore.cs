using System;
using TMPro;
using UnityEngine;

public class UIScore : MonoBehaviour
{
    private TMP_Text _text;

    private void Start()
    {
        _text = GetComponent<TMP_Text>();
        ScoreSystem.OnScoreChanged += UpdateScoreText;
        _text.SetText(ScoreSystem.Score.ToString());
    }

    private void UpdateScoreText(int score)
    {
        _text.SetText(score.ToString());
    }

    private void OnDestroy()
    {
        ScoreSystem.OnScoreChanged -= UpdateScoreText;
    }
}