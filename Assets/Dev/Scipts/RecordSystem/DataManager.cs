using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager
{
    public static void SaveData(GemSO gameData)
    {
        string dataString = JsonUtility.ToJson(gameData);
        Debug.Log("Save");
        PlayerPrefs.SetString("data", dataString);
    }
    public static void LoadData(GemSO gameData)
    {
        if (!PlayerPrefs.HasKey("data"))
        {
            Debug.Log("Load");
            SaveData(gameData);
            return;
        }

        string dataString = PlayerPrefs.GetString("data");
        JsonUtility.FromJsonOverwrite(dataString, gameData);
    }
}
