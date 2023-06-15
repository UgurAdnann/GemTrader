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
    private GemPanelController gemPanelController;
    private ParticleSpawner particleSpawner;
    [HideInInspector] public GridManager gridManager;
    #endregion

    #region Variables for Gem
    public GemState gemState;
    private int followSiblingIndex, gemNum;
    private GameObject followObject;
    private bool isGrowUp, isCanRotate;
    private GameObject tempPrtc;
    #endregion


    void Start()
    {
        playerManager = ObjectManager.PlayerManager;
        canvasManager = ObjectManager.CanvasManager;
        gemPanelController = ObjectManager.GemPanelController;
        particleSpawner = ObjectManager.ParticleSpawner;
    }

    private void Update()
    {
        StackFollow();
        DoRotate();
    }

    public void StartEvents(GridManager tempGrid, int index) //Called from grid manager
    {
        gemNum = index;
        gridManager = tempGrid;
        transform.DOScale(Vector3.one, gemSO.growthTime).SetEase(Ease.Linear).SetId(0).OnStepComplete(() => SetGrowUpEvents());
        gemProperties = gemSO.gemProperties[index];
    }

    #region Collect Gem
    public void CollectGem()
    {
        if (transform.localScale.x >= gemSO.minCollectSize)
        {
            transform.GetComponent<Collider>().enabled = false;
            DOTween.Kill(this.transform, false);
            transform.SetParent(playerManager.stackedGems);
            followSiblingIndex = transform.GetSiblingIndex();
           
            if (followSiblingIndex != 0)
                followObject = transform.parent.GetChild(followSiblingIndex - 1).gameObject;
            else
                followObject = playerManager.stackPos;

            transform.DOJump(followObject.transform.position, 3, 0, 0.2f).OnStepComplete(() => gemState = GemState.Collectted);

            gridManager.CreateGem();
        }
    }

    private void StackFollow()
    {
        if (gemState.Equals(GemState.Collectted))
        {
            if (followSiblingIndex.Equals(0))
                transform.position = Vector3.Lerp(transform.position, followObject.transform.position, gemSO.followSpeed * Time.deltaTime);
            else
                transform.position = Vector3.Lerp(transform.position, followObject.transform.position + Vector3.up * followObject.transform.localScale.y, gemSO.followSpeed * Time.deltaTime);
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
        gemPanelController.transform.GetChild(0).GetChild(gemNum).GetComponent<GemUIController>().SetCountText(1);
    }
    #endregion

    private void SetGrowUpEvents()
    {
        if (!isGrowUp)
        {
            tempPrtc = particleSpawner.gemPrtcQue.Dequeue();
            tempPrtc.GetComponent<GemParticleController>().OpenParticle(transform);
            isCanRotate = true;
            isGrowUp = true;
        }
    }

    private void DoRotate()
    {
        if (isCanRotate && gemState.Equals(GemState.Grid))
            transform.GetChild(0).Rotate(Vector3.up * gemSO.rotateSpeed * Time.deltaTime);
    }
}
