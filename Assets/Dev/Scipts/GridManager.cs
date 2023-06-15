using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    #region Variables for General
    public GemSO gemSO;
    #endregion

    #region Variables for Gem
    private int rndGem;
    private Transform gems;
    #endregion

    void Start()
    {
        gems = GameObject.FindGameObjectWithTag("Gems").transform;
        CreateGem();
    }

    public void CreateGem()
    {
        StartCoroutine(WaitCreateGem());
    }
    private IEnumerator WaitCreateGem()
    {
        yield return new WaitForSeconds(Random.Range(0.1f, 1.5f));
        rndGem = Random.Range(0, gemSO.gemProperties.Length);
        GameObject newGem = Instantiate(gemSO.gemProperties[rndGem].gemPrefab);
        newGem.transform.SetParent(gems);
        newGem.transform.position = transform.position;
        newGem.GetComponent<GemController>().StartEvents(this, rndGem);
    }
}
