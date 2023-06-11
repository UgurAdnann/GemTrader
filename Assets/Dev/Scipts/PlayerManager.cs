using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Variables for General
    private CharacterManager chrManager;
    private Animator chrAnimator;
    #endregion

    #region Variables for Input
    private bool isTouch, isMove = true;
    [HideInInspector] public float forwardSpeed, rotateSpeed;
    [SerializeField] private float initialForwardSpeed, initialRotateSpeed;
    private Rigidbody rb;
    [SerializeField] private VariableJoystick joyStick; //Karakter içerisine yazýlýr. Canvastaki variable joystick içine atýlýr
    //Rot
    private float PreviousXPos, PreviousZPos, nextXPos, nextZPos;
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
        if (isMove)
        {
            if (Input.GetMouseButtonUp(0))
            {
                isTouch = false;

                chrAnimator.SetBool("Run",false);
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
            if (Input.GetMouseButtonDown(0))
            {
                chrAnimator.SetBool("Run", true);

            }

        }
    }

    private void SetRot()
    {
        PreviousXPos = transform.position.x;
        PreviousZPos = transform.position.z;

        transform.forward = Vector3.Lerp(transform.forward, new Vector3(PreviousXPos - nextXPos, 0, PreviousZPos - nextZPos), rotateSpeed);

        //Hareket yoksa walk anim oynamamasý için eklendi(titriyor)
         if (PreviousXPos - nextXPos != 0 || PreviousZPos - nextZPos != 0)
             chrAnimator.SetBool("Run", true);
         else
             chrAnimator.SetBool("Run", false);
        nextXPos = transform.position.x;
        nextZPos = transform.position.z;

    }

    #endregion
}
