using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GemUIController : MonoBehaviour
{
    public GemSO gemSO;
    private PlayerManager playerManager;
    public int collectCount;

    void Start()
    {
        playerManager = ObjectManager.PlayerManager;
    }

    void Update()
    {

    }

    public void SetGemInformation(int index)
    {
        transform.GetChild(0).GetComponent<Image>().sprite = gemSO.gemProperties[index].Icon;
        transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = "Gem Type: " + gemSO.gemProperties[index].name.ToString();
        transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = "Gem Count: " + collectCount;
    }

    public void SetCountText(int addValue)
    {
        collectCount += addValue;
        transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = "Gem Count: " + collectCount;

    }
}
