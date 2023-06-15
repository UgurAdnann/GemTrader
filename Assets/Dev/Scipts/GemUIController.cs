using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GemUIController : MonoBehaviour
{
    #region Variables for General
    public GemSO gemSO;
    #endregion

    #region Variables for Gem
    public int collectCount;
    private int index;
    #endregion

    public void SetGemInformation(int gemNum)
    {
        index = gemNum;
        transform.GetChild(0).GetComponent<Image>().sprite = gemSO.gemProperties[index].Icon;
        transform.GetChild(1).GetComponent<TMPro.TextMeshProUGUI>().text = "Type: " + gemSO.gemProperties[index].name.ToString();
        collectCount = gemSO.gemProperties[index].CollectCount;
        transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = "Gem Count: " + collectCount;
    }

    public void SetCountText(int addValue)
    {
        collectCount += addValue;
        gemSO.gemProperties[index].CollectCount = collectCount;
        transform.GetChild(2).GetComponent<TMPro.TextMeshProUGUI>().text = "Gem Count: " + collectCount;
    }
}
