using UnityEngine;
using System.IO;

/// <summary>
/// PlayerDataManager is a Singleton who manages player data.
/// </summary>
public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance;

    public string PlayerName {  get; set; }

    public string BestPlayerName { get; private set; }

    public int BestScore { get; private set; }
    

    private void Awake()
    {
        // PlayerDataManager is a Singleton
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        LoadPlayerData();
    }

    private class SaveData
    {
        public string BestPlayerName;
        public int BestScore;
    }

    public void SavePlayerData()
    {
        SaveData data = new()
        {
            BestPlayerName = BestPlayerName,
            BestScore = BestScore
        };

        string json = JsonUtility.ToJson(data);

        File.WriteAllText
            (Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadPlayerData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            BestPlayerName = data.BestPlayerName;
            BestScore = data.BestScore;
        }
    }

    public void UpdateBestScore(int score)
    {
        if (score > BestScore)
        {
            BestScore = score;

            if (BestPlayerName != PlayerName)
            {
                BestPlayerName = PlayerName;
            }
        }
    }
}
