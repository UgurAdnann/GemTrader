using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GemSO gemSO;

    private void Awake()
    {
        DataManager.LoadData(gemSO);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetData();
            //DataManager.SaveData(gemSO);
        }
    }

    private void ResetData()
    {
        for (int i = 0; i < gemSO.gemProperties.Length; i++)
        {
            gemSO.gemProperties[i].CollectCount = 0;
        }
            gemSO.money = 0.0f;
        PlayerPrefs.DeleteKey("data");
        print("Reset Data");
    }
}
