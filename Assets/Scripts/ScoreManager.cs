using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ScoreManager
{
    private  static int score;
    public static string playerName;

    public static void UpdateScore(int add)
    {
        if (add <= 0)
        {
            score = 0;
        }
        else
        {
            score += add;
            UpdateSave();
        }
        Debug.Log(score);
    }

    private static void UpdateSave()
    {
        for(int i = 0; i < SaveSystem.HighScores.Length; i++)
        {
            if (score >= SaveSystem.HighScores[i])
            {
                for(int j= SaveSystem.HighScores.Length - 1; j > i; j--)
                {
                    SaveSystem.HighScores[j] = SaveSystem.HighScores[j - 1];
                    SaveSystem.Scorers[j] = SaveSystem.Scorers[j - 1];
                }
                SaveSystem.HighScores[i] = score;
                SaveSystem.Scorers[i] = playerName;
                break;
            }
        }
        SaveSystem.SaveGame();
    }
}
