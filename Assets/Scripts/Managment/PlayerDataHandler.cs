using UnityEngine;
using System.IO;

public static class PlayerDataHandler
{
    [System.Serializable]
    class SaveData 
    {
        public int bestScore;
    }

    public static void SaveBestScore (int playerBestScore)
    {
        SaveData data = new SaveData { bestScore = playerBestScore };

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }
    public static int LoadBestScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";

        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            return data.bestScore;
        }
        return 0;
    }
}
