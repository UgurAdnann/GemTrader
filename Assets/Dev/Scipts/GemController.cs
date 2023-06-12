using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class GemController : MonoBehaviour
{
    #region Variables for General
    public GemSO gemSO;
    public GemSO.GemProperties gemProperties;
    private PlayerManager playerManager;
    private CanvasManager canvasManager;
    [HideInInspector] public GridManager gridManager;
    #endregion

    #region Variables for Gem
    public GemState gemState;
    private int followSiblingIndex;
    private GameObject followObject;
    #endregion


    void Start()
    {
        playerManager = ObjectManager.PlayerManager;
        canvasManager = ObjectManager.CanvasManager;
    }

    private void Update()
    {
        StackFollow();
    }

    public void StartEvents(GridManager tempGrid, int gemNum) //Called from grid manager
    {
        gridManager = tempGrid;
        transform.DOScale(Vector3.one, gemSO.growthTime).SetEase(Ease.Linear).SetId(0);
        gemProperties = gemSO.gemProperties[gemNum];
    }

    #region Collect Gem
    public void CollectGem()
    {
        if (transform.localScale.x >= gemSO.minCollectSize)
            StartCoroutine(WaitCollectGem());
    }

    IEnumerator WaitCollectGem()
    {
        DOTween.Kill(this.transform, false);
        transform.SetParent(playerManager. stackedGems);
        followSiblingIndex = transform.GetSiblingIndex();
        if (followSiblingIndex != 0)
            followObject = transform.parent.GetChild(followSiblingIndex - 1).gameObject;
        else
            followObject = playerManager.stackPos;

        GetComponent<Collider>().enabled = false;
        transform.DOJump(followObject.transform.position, 3, 0, 0.2f).OnStepComplete(() => gemState = GemState.Collectted);
        
        yield return new WaitForSeconds(gemSO.reCreateDelay);
        gridManager.CreateGem();
    }

    private void StackFollow()
    {
        if (gemState.Equals(GemState.Collectted))
        {
            if(followSiblingIndex.Equals(0))
                transform.position = Vector3.Lerp(transform.position, followObject.transform.position, gemSO.followSpeed * Time.deltaTime);
            else
                transform.position = Vector3.Lerp(transform.position, followObject.transform.position+Vector3.up* followObject.transform.localScale.y, gemSO.followSpeed * Time.deltaTime);
        }
    }
    #endregion

    #region Sell Gem

    public void SellGem(Transform sellArea)
    {
        StartCoroutine(WaitSellGem(sellArea));
    }

    IEnumerator WaitSellGem(Transform sellArea)
    {
        gemState = GemState.Sold;
        transform.SetParent(null);
        transform.DOJump(sellArea.position, 1, 0, gemSO.sellMoveDelay);
        yield return new WaitForSeconds(gemSO.sellMoveDelay);
        DOTween.Kill(this);
        Destroy(this.gameObject);
        canvasManager.money += gemProperties.startPrice * transform.localScale.x;
        canvasManager.SetMoney();
    }
    #endregion
}
