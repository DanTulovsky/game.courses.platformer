using System;
using UnityEngine;

public static class ScoreSystem
{
    public static int Score { get; private set; }
    private static int HighScore { get; set; }

    public static event Action<int> OnScoreChanged;

    public enum  PlayerPref
    {
        HighScore
    }

    public static void ResetScore()
    {
        Score = 0;
    }

    public static void Add(int points)
    {
        Score += points;
        OnScoreChanged?.Invoke(Score);

        if (Score > HighScore)
        {
            HighScore = Score;
            PlayerPrefs.SetInt(PlayerPref.HighScore.ToString(), HighScore);
        }
    }
}
