using System;
using UnityEngine;

public static class ScoreSystem
{
    private static int Score { get; set; }
    private static int HighScore { get; set; }

    public static event Action<int> OnScoreChanged;

    public enum  PlayerPref
    {
        HighScore
    }

    public static void Add(int points)
    {
        Score += points;
        if (OnScoreChanged != null) OnScoreChanged.Invoke(Score);

        if (Score > HighScore)
        {
            HighScore = Score;
            PlayerPrefs.SetInt(PlayerPref.HighScore.ToString(), HighScore);
        }
    }
}
