using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;


public class CamerRotationController : MonoBehaviour
{


    private PlayerControls playerControls;
    private bool isRotating;

    private InputAction rotateRightAction;
    private InputAction rotateLeftAction;

    [SerializeField] private Animator cameraAnimator;

    private void Awake()
    {
        
        playerControls = new PlayerControls();

    }

    private void OnEnable()
    {
        
        playerControls.Enable();

        rotateRightAction = playerControls.CameraRotation.RotateRight;
        rotateRightAction.performed += RotateRight;

        rotateLeftAction = playerControls.CameraRotation.RotateLeft;
        rotateLeftAction.performed += RotateLeft;

    }

    private void OnDisable()
    {

        playerControls.Disable();

    }

    private void RotateRight(InputAction.CallbackContext context)
    {

        if(isRotating) { return; }
        isRotating = true;

        cameraAnimator.SetTrigger("RotateRightTrigger");

    }

    private void RotateLeft(InputAction.CallbackContext context)
    {

        if (isRotating) { return; }
        isRotating = true;

        cameraAnimator.SetTrigger("RotateLeftTrigger");

    }

    public void MakeIsRotatingFalse()
    {

        isRotating = false;

    }

}
