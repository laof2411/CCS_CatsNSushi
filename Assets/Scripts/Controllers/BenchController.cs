using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BenchController : MonoBehaviour
{

    private PlayerControls playerControls;
    private InputAction useBenchAction;

    public bool inBench = false;

    [SerializeField] private GameObject benchPhysicalAppearance;
    [SerializeField] private GameObject playerPhysicalAppearance;
    [SerializeField] private Transform slot;

    [SerializeField] private GameObject getClosestObjectGameObject;

    [SerializeField] private PlayerMovementController playerMovementController;

    [SerializeField] private ParticleSystem benchParticles;

    private void Awake()
    {
        
        playerControls = new PlayerControls();

    }

    private void OnEnable()
    {

        playerControls.Enable();

        useBenchAction = playerControls.PlayerMovement.UseBench;
        useBenchAction.performed += UseBench;

    }

    private void OnDisable()
    {

        playerControls.Disable();

    }

    private void UseBench(InputAction.CallbackContext context)
    {

        if (inBench)
        {

            inBench = false;

            benchPhysicalAppearance.SetActive(false);
            playerPhysicalAppearance.transform.Translate(0f, -0.55f, 0f);
            slot.transform.Translate(0f, -0.53f, 0f);
            getClosestObjectGameObject.transform.Translate(0f, 0f, -0.95f);

            playerMovementController.PlayIdle();
            playerMovementController.canMove = true;
            
            benchParticles.Play();
            
        }
        else
        {

            inBench = true;

            benchPhysicalAppearance.SetActive(true);
            playerPhysicalAppearance.transform.Translate(0f, +0.55f, 0f);
            slot.transform.Translate(0f, +0.53f, 0f);
            getClosestObjectGameObject.transform.Translate(0f, 0f, +0.95f);

            playerMovementController.PlayIdle();
            playerMovementController.canMove = false;

            benchParticles.Play();
            
        }


    }

}
