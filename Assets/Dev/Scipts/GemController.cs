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
    [HideInInspector] public GridManager gridManager;
    private Transform stackedGems;
    #endregion

    #region Variables for Gem
    public GemState gemState;
    private int followSiblingIndex;
    private GameObject followObject;
    #endregion


    void Start()
    {
        playerManager = ObjectManager.PlayerManager;
        stackedGems = GameObject.FindGameObjectWithTag("StackedGems").transform;
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

    public void CollectGem()
    {
        if (transform.localScale.x >= gemSO.minCollectSize)
            StartCoroutine(WaitCollectGem());
    }

    IEnumerator WaitCollectGem()
    {
        DOTween.Kill(this.transform, false);
        transform.SetParent(stackedGems);
        followSiblingIndex = transform.GetSiblingIndex();

        if (followSiblingIndex != 0)
            followObject = transform.parent.GetChild(followSiblingIndex - 1).gameObject;

        GetComponent<Collider>().enabled = false;
        transform.DOJump(playerManager.stackPos.transform.position, 3, 0, 0.2f).OnStepComplete(() => gemState = GemState.Collectted);
        
        yield return new WaitForSeconds(gemSO.reCreateDelay);
        gridManager.CreateGem();
    }

    private void StackFollow()
    {
        if (gemState.Equals(GemState.Collectted))
        {
            if(followSiblingIndex.Equals(0))
                transform.position = Vector3.Lerp(transform.position, playerManager.stackPos.transform.position, gemSO.followSpeed * Time.deltaTime);
            else
                transform.position = Vector3.Lerp(transform.position, followObject.transform.position+Vector3.up* followObject.transform.localScale.y, gemSO.followSpeed * Time.deltaTime);
        }
    }

}
