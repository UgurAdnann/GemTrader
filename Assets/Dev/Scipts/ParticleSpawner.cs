using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    public GemSO gemSO;
    private GameObject gemPrtc;
    public int gemPrtcPoolCount;
    public Queue<GameObject> gemPrtcQue = new Queue<GameObject>();

    private void Awake()
    {
        ObjectManager.ParticleSpawner = this;
    }

    void Start()
    {
        CreateGemParticle();
    }

    private void CreateGemParticle()
    {
        for (int i = 0; i < gemPrtcPoolCount; i++)
        {
            GameObject tempGemPrtc = Instantiate(gemSO.gemParticle);
            tempGemPrtc.transform.SetParent(transform.GetChild(0));
            tempGemPrtc.transform.localPosition = Vector3.zero;
            tempGemPrtc.GetComponent<GemParticleController>().particleSpawner = this;
            gemPrtcQue.Enqueue(tempGemPrtc);
        }
    }
}
