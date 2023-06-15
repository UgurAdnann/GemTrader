using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GemProperties")]
public class GemSO : ScriptableObject
{
    [System.Serializable]
    public struct GemProperties
    {
        public string name;
        public float startPrice;
        public Sprite Icon;
        public GameObject gemPrefab;
        public int CollectCount;
    }
    public GemProperties[] gemProperties;
    public float minCollectSize, followSpeed, growthTime;
    public float reCreateDelay, sellDelay, sellMoveDelay,rotateSpeed;
    public GameObject gemUIPrefab, gemParticle;
    [Header("Click R for Reset Data")]
    public float money;
}
