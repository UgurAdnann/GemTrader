using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    #region Variables for General
    public GemSO gemSO;
    #endregion
    #region Variables for Gem
    public GemSO.GemProperties gemProperties;
    private int rndGem;
    private Transform gems;
    #endregion

    void Start()
    {
        gems = GameObject.FindGameObjectWithTag("Gems").transform;
        CreateGem();
    }

    private void CreateGem()
    {
        rndGem = Random.Range(0, gemSO.gemProperties.Length);
        GameObject newGem = Instantiate(gemSO.gemProperties[rndGem].gemPrefab);
        newGem.transform.SetParent(gems);
        newGem.transform.position = transform.position;
        newGem.GetComponent<GemController>().StartEvents(gameObject, rndGem);
    }
}
