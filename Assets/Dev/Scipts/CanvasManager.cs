using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    #region Variables for General
    private GemPanelController gemPanelController;
    public GemSO gemSO;
    #endregion

    #region Variables for UI
    public float money;
    public TMPro.TextMeshProUGUI moneyText;
    public GameObject gemUIBUtton;
    #endregion

    private void Awake()
    {
        ObjectManager.CanvasManager = this;
    }
    void Start()
    {
        money = gemSO.money;
        gemPanelController = ObjectManager.GemPanelController;

        SetMoney();
    }

    public void  SetMoney()
    {
        gemSO.money = money;

        if(money==0)
            moneyText.text = "Money: " + 0 + "$";
        else
        moneyText.text = "Money: " + money.ToString(".0") + "$";
    }

    public void OpenGempanel()
    {
        gemPanelController.gameObject.SetActive(true);
        gemPanelController.OpenGemPanel();
    }

    public void CloseGemPanel()
    {
        gemPanelController.CloseGemPanel();
    }
}
