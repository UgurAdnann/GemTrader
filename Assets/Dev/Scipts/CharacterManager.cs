using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    #region Variables for General
    [HideInInspector] public Animator animator;
    #endregion

    private void Awake()
    {
        animator = GetComponent<Animator>();
        ObjectManager.ChrManager = this;
    }
}
