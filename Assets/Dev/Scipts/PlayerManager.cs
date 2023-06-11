using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Variables for General
    private CharacterManager chrManager;
    private Animator chrAnimator;
    private Transform currentGem;
    [HideInInspector] public GameObject stackPos;
    #endregion

    #region Variables for Input
    private bool isTouch, isWalking;
    [HideInInspector] public float forwardSpeed, rotateSpeed;
    [SerializeField] private float initialForwardSpeed, initialRotateSpeed;
    [SerializeField] private VariableJoystick joyStick;
    private CharacterController chrController;
    #endregion


    private void Awake()
    {
        ObjectManager.PlayerManager = this;
    }
    void Start()
    {
        chrManager = ObjectManager.ChrManager;
        chrAnimator = chrManager.animator;
        chrController = GetComponent<CharacterController>();

        stackPos = transform.GetChild(1).gameObject;
        forwardSpeed = initialForwardSpeed;
        rotateSpeed = initialRotateSpeed;
    }

    void Update()
    {
        InputListener();
    }

    #region Input System
    private void InputListener()
    {
        if (Input.GetMouseButtonUp(0))
        {
            isTouch = false;
            isWalking = false;
            chrAnimator.SetBool("Run", false);
        }

        if (isTouch)
        {
            chrController.Move(new Vector3(joyStick.Direction.x, 0, joyStick.Direction.y) * forwardSpeed * Time.deltaTime);
            SetRot();
        }
        if (Input.GetMouseButton(0))
        {
            isTouch = true;
        }
    }

    private void SetRot()
    {
        transform.forward = Vector3.Lerp(transform.forward, new Vector3(joyStick.Direction.x, 0, joyStick.Direction.y), rotateSpeed);

        //Hareket yoksa walk anim oynamamasý için eklendi(titriyor)
        if (!isWalking)
        {
        if (joyStick.Direction.x != 0 || joyStick.Direction.y != 0)
            {
                chrAnimator.SetBool("Run", true);
                isWalking = true;
            }
        else if (joyStick.Direction.x == 0 && joyStick.Direction.y == 0)
                chrAnimator.SetBool("Run", false);

        }
    }

    #endregion

    #region Collisions
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Gem"))
        {
            other.GetComponent<GemController>().CollectGem();
        }
    }
    #endregion
}
