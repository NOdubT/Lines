using System;
using System.IO;
using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
    public string playerName { get; set; }
    public int playerScore { get; set; }

    public static string player = "Player";

    public static PlayerSettings instance {  get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [Serializable]
    class SaveData
    {
        public string name;
        public int score;
    }

    public void SaveScore()
    {
        SaveData sd = new SaveData();
        sd.name = playerName;
        sd.score = playerScore;

        string json = JsonUtility.ToJson(sd);
        File.WriteAllText(Application.persistentDataPath + "/bestscore.json", json);
    }

    public void LoadScore()
    {
        string path = Application.persistentDataPath + "/bestscore.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);
        }
    }
}
