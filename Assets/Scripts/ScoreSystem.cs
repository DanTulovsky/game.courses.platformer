using System;

public static class ScoreSystem
{
    private static int _score;
    public static event Action<int> OnScoreChanged;

    public static int Score => _score;

    public static void Add(int points)
    {
        _score += points;
        if (OnScoreChanged != null) OnScoreChanged.Invoke(_score);
    }
}
