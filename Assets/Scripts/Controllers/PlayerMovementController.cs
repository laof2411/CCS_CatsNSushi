using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerMovementController : MonoBehaviour
{

    private PlayerControls playerControls;
    private InputAction walkAction;

    [SerializeField] private Transform cameraPivotReference;

    [SerializeField] private CharacterController characterController;

    [SerializeField] private float movementSpeed;

    private float turnVelocity;
    [SerializeField] private float turnTime;

    [SerializeField] private Animator playerAnimator;

    [SerializeField] private InteractionController interactionController;
    [SerializeField] private BenchController benchController;

    public bool canMove = true;

    public bool isCutting = false;
    public bool isMaking = false;

    [SerializeField] private AudioSource walkingSound;

    private void Awake()
    {

        playerControls = new PlayerControls();
        canMove = true;

    }

    private void OnEnable()
    {
        
        playerControls.Enable();

        walkAction = playerControls.PlayerMovement.Walking;
        
    }

    private void OnDisable()
    {

        playerControls.Disable();

    }

    private void Update()
    {

        Move();

    }

    private void Move()
    {
        if(canMove)
        {
            Vector3 direction = new(walkAction.ReadValue<Vector2>().x, 0, walkAction.ReadValue<Vector2>().y);

            if (direction.magnitude >= 0.1f)
            {
                if (!walkingSound.isPlaying)
                {

                    walkingSound.Play();

                }
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cameraPivotReference.eulerAngles.y;
                float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnVelocity, turnTime);
                transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                characterController.Move(movementSpeed * Time.deltaTime * moveDir);

                FindFirstObjectByType<CuttingTable>().SetCuttingFalse();
                Plate[] plates = GameObject.FindObjectsOfType<Plate>();
                for (int i = 0; i < plates.Length; i++)
                {

                    plates[i].SetMakingToFalse();
                    plates[i].StopCookingSound();

                }
                
            }

            if (interactionController.pickableInHand == null && direction.magnitude >= 0.1f && !benchController.inBench)
            {

                playerAnimator.Play("Run_no_object");

            }
            if (interactionController.pickableInHand != null && direction.magnitude >= 0.1f && !benchController.inBench)
            {

                playerAnimator.Play("Run_object");

            }
            if (direction.magnitude < 0.1f && !isCutting && !isMaking && interactionController.pickableInHand == null || benchController.inBench && !isCutting && !isMaking && interactionController.pickableInHand == null)
            {

                playerAnimator.Play("Idle_cocinero");
                walkingSound.Stop();

            }
            if(direction.magnitude < 0.1f && !isCutting && !isMaking && interactionController.pickableInHand != null || benchController.inBench && !isCutting && !isMaking && interactionController.pickableInHand != null)
            {

                playerAnimator.Play("idle_with_pickable");
                walkingSound.Stop();

            }
            if (isCutting)
            {

                playerAnimator.Play("cut");
                walkingSound.Stop();

            }           
            if (isMaking)
            {

                playerAnimator.Play("Make");
                walkingSound.Stop();

            }
        }

    }

    public void PlayIdle()
    {

        playerAnimator.Play("Idle_cocinero");


    }


}
