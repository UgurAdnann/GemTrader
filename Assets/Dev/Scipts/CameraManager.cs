using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    #region Variable for Follow
    private Vector3 targetDistance;
    public float followSpeed;
    private Transform target;
    #endregion

    private void Awake()
    {
        ObjectManager.CameraManager = this;
    }
    void Start()
    {
        target = ObjectManager.PlayerManager.transform;
        targetDistance = transform.position - target.transform.position;
    }

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, targetDistance + target.transform.position, followSpeed * Time.deltaTime);
    }
}
