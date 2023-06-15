using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemParticleController : MonoBehaviour
{
    [HideInInspector] public ParticleSpawner particleSpawner;

    public void OpenParticle(Transform target)
    {
        transform.position = target.position;
        gameObject.SetActive(true);
        StartCoroutine(WaitCloseParticle());
    }

    IEnumerator WaitCloseParticle()
    {
        yield return new WaitForSeconds(0.5f);
        CloseParticle();
    }

    public void CloseParticle()
    {
        gameObject.SetActive(false);
        transform.localPosition = Vector3.zero;
        particleSpawner.gemPrtcQue.Enqueue(gameObject);
    }
}
