using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveSystem 
{
    public static int[] HighScores;
    public static string[] Scorers;
    private static string SaveFolder = Application.dataPath+"/Saves/";

    public static void CheckAndCreateSaveDir()
    {
        if (!Directory.Exists(SaveFolder))
        {
            Directory.CreateDirectory(SaveFolder);
        }
        
    }

    public static void SaveGame()
    {
        string jsondata = JsonUtility.ToJson(new SaveObject(HighScores, Scorers));
        File.WriteAllText(SaveFolder + "save.txt", jsondata);
    }

    public static void LoadGame()
    {
        string loadFile="";
        if(File.Exists(SaveFolder + "save.txt"))
        {
            loadFile = File.ReadAllText(SaveFolder + "save.txt");
        }
        
        if (loadFile!="")
        {
            SaveObject load = JsonUtility.FromJson<SaveObject>(loadFile);
            HighScores = load.highScores;
            Scorers = load.scorers;
        }
        else
        {
            HighScores = new int[5];
            Scorers = new string[5];
            for(int i=0;i<Scorers.Length;i++)
            {
                Scorers[i] = "Player";
            }
        }
        
    }
}

public class SaveObject
{
    public int[] highScores;
    public string[] scorers;

    public SaveObject(int[] highScores,string[] scorers)
    {
        this.highScores = highScores;
        this.scorers = scorers;

    }
}
