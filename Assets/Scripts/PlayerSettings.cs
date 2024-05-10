using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
    public Player player;

    private int maxPlayersNumber = 10;

    public static PlayerSettings instance { get; private set; }

    public List<Player> bestScore { get; private set; }

    [Serializable]
    public struct Player
    {
        public string Name;
        public int Score;
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            player.Name = "";
            player.Score = 0;
            LoadScore();
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SetPlayerName(string name)
    {
        player.Name = name;
    }

    public void CheckScore()
    {
        bestScore.Add(player);
        bestScore.Sort((a, b) => b.Score.CompareTo(a.Score));
        if (bestScore.Count > maxPlayersNumber)
        {
            bestScore.RemoveAt(bestScore.Count);
        }
        SaveScore();
    }

    [Serializable]
    class SaveData
    {
        public List<Player> data;
    }

    private void SaveScore()
    {
        SaveData sd = new SaveData();
        sd.data = bestScore;

        string json = JsonUtility.ToJson(sd);
        File.WriteAllText(Application.persistentDataPath + "/bestscore.json", json);
    }

    private void LoadScore()
    {
        string path = Application.persistentDataPath + "/bestscore.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            bestScore = JsonUtility.FromJson<SaveData>(json).data;
        }
        if (bestScore == null)
        {
            bestScore = new List<Player>();
        }
    }
}
