using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemPanelController : MonoBehaviour
{
    public GemSO gemSO;

    void Start()
    {
        CreateGemUI();
    }

    private void CreateGemUI()
    {
        transform.GetChild(0).localScale = new Vector3(1, gemSO.gemProperties.Length*0.5f, 1);
        for (int i = 0; i < gemSO.gemProperties.Length; i++)
        {
            GameObject newGemUI = Instantiate(gemSO.gemUIPrefab);
            newGemUI.transform.SetParent(transform);
            newGemUI.transform.localScale = Vector3.one;
            newGemUI.transform.localPosition = Vector3.up*(100)- (Vector3.up * i * (200));
            newGemUI.transform.SetParent(transform.GetChild(0));
        }
    }

    void Update()
    {
        
    }
}
