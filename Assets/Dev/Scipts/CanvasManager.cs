using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    public float money;
    public TMPro.TextMeshProUGUI moneyText;

    private void Awake()
    {
        ObjectManager.CanvasManager = this;
    }
    void Start()
    {
        SetMoney();
    }

    void Update()
    {
        
    }

    public void  SetMoney()
    {
        moneyText.text = "Money: " + money.ToString(".0") + "$";
    }
}
