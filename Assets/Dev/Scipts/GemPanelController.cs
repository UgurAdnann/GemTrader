using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class GemPanelController : MonoBehaviour
{
    private PlayerManager playerManager;
    private CanvasManager canvasManager;
    public GemSO gemSO;


    private void Awake()
    {
        ObjectManager.GemPanelController = this;
    }

    void Start()
    {
        playerManager = ObjectManager.PlayerManager;
        canvasManager=ObjectManager.CanvasManager;
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
            newGemUI.transform.localPosition = Vector3.up*(200)- (Vector3.up * i * (170));
            newGemUI.transform.SetParent(transform.GetChild(0));
            newGemUI.GetComponent<GemUIController>().SetGemInformation(i);
        }
        transform.localScale = Vector3.zero;
        gameObject.SetActive(false);
    }

    public void CloseGemPanel()
    {
        StartCoroutine(WaitCloseGemPanel());
    }

    IEnumerator WaitCloseGemPanel()
    {
        transform.DOScale(Vector3.zero, 0.3f);
        yield return new WaitForSeconds(0.3f);
        DOTween.Complete(this);
        gameObject.SetActive(false);
        canvasManager.gemUIBUtton.SetActive(true);
    }

    public void OpenGemPanel()
    {
        StartCoroutine(WaitOpenGemPanel());
    }
    IEnumerator WaitOpenGemPanel()
    {
        canvasManager.gemUIBUtton.SetActive(false);
        transform.DOScale(Vector3.one*1.2f, 0.5f).SetId(3);
        yield return new WaitForSeconds(0.5f);
        DOTween.Complete(3);
        transform.DOScale(Vector3.one, 0.2f);
        print("Open");
    }
}
