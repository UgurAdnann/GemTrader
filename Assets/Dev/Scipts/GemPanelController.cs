using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GemPanelController : MonoBehaviour
{
    private PlayerManager playerManager;
    public GemSO gemSO;


    private void Awake()
    {
        ObjectManager.GemPanelController = this;
    }

    void Start()
    {
        playerManager = ObjectManager.PlayerManager;
        CreateGemUI();
    }

    private void CreateGemUI()
    {
        transform.GetChild(0).localScale = new Vector3(1, gemSO.gemProperties.Length*0.5f, 1);
        for (int i = 0; i < gemSO.gemProperties.Length; i++)
        {
            GameObject newGemUI = Instantiate(gemSO.gemUIPrefab);
            newGemUI.transform.SetParent(transform.parent);
            newGemUI.transform.localScale = Vector3.one;
            newGemUI.transform.localPosition = Vector3.up*(250)- (Vector3.up * i * (200));
            newGemUI.transform.SetParent(transform.GetChild(0));
            newGemUI.GetComponent<GemUIController>().SetGemInformation(i);
        }
    }

    void Update()
    {
        
    }
}
