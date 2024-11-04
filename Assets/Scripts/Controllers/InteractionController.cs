using UnityEngine;
using UnityEngine.InputSystem;
public class InteractionController : MonoBehaviour
{

    private PlayerControls playerControls;
    private InputAction interactWithInteractable;
    private InputAction pickAndLeaveObject;

    [SerializeField] private Transform slot;

    public GameObject currentInteractable;
    public GameObject pickableInHand;

    private void Awake()
    {

        playerControls = new PlayerControls();

    }

    private void OnEnable()
    {

        playerControls.Enable();

        interactWithInteractable = playerControls.InteractionSystem.UseInteractable;
        interactWithInteractable.performed += HandleInteract;

        pickAndLeaveObject = playerControls.InteractionSystem.PickAndLeaveObject;
        pickAndLeaveObject.performed += HandlePickAndPlace;

    }

    private void HandlePickAndPlace(InputAction.CallbackContext context)
    {

        if (currentInteractable == null) { return; }

        if (pickableInHand != null)
        {

            if (currentInteractable.GetComponent<Interactable>().currentSonInteractable == null && !currentInteractable.TryGetComponent<Dishwasher>(out Dishwasher _dishwasher) && !currentInteractable.TryGetComponent<TrashCan>(out TrashCan _trashcan) && !currentInteractable.TryGetComponent<Fryer>(out Fryer _fryer))
            {

                pickableInHand = pickableInHand.GetComponent<IPickable>().Place(currentInteractable);

            }
            else if (pickableInHand.GetComponent<Interactable>() is Ingredient)
            {
                if (currentInteractable.GetComponent<Interactable>() is TrashCan)
                {

                    Destroy(pickableInHand);
                    pickableInHand = null;
                    FindAnyObjectByType<LevelEndManager>().IncreaseFoodThrown();

                }
                else if (currentInteractable.GetComponent<Interactable>().currentSonInteractable.GetComponent<Interactable>() is Plate)
                {

                    if (!currentInteractable.GetComponent<Interactable>().currentSonInteractable.GetComponent<Plate>().IsFull() && pickableInHand.GetComponent<Ingredient>().status != IngredientStatus.Raw && currentInteractable.GetComponent<Interactable>().currentSonInteractable.GetComponent<Plate>().isClean)
                    {

                        pickableInHand = pickableInHand.GetComponent<IPickable>().Place(currentInteractable.GetComponent<Interactable>().currentSonInteractable);

                    }

                }
                else if (currentInteractable.GetComponent<Interactable>().currentSonInteractable.GetComponent<Interactable>() is RiceCooker)
                {

                    if (pickableInHand.GetComponent<Ingredient>().type == IngredientType.Rice && pickableInHand.GetComponent<Ingredient>().status == IngredientStatus.Raw && currentInteractable.GetComponent<Interactable>().currentSonInteractable.GetComponent<RiceCooker>().currentSonInteractable == null)
                    {

                        pickableInHand = pickableInHand.GetComponent<IPickable>().Place(currentInteractable.GetComponent<Interactable>().currentSonInteractable);
                        currentInteractable.GetComponent<Interactable>().currentSonInteractable.GetComponent<RiceCooker>().StartCookingCoroutine();

                    }

                }
                else if (currentInteractable.GetComponent<Interactable>().currentSonInteractable.GetComponent<Interactable>() is CuttingTable)
                {

                    if (pickableInHand.GetComponent<Ingredient>().type == IngredientType.Salmon || pickableInHand.GetComponent<Ingredient>().type == IngredientType.Octopus || pickableInHand.GetComponent<Ingredient>().type == IngredientType.Shrimp && pickableInHand.GetComponent<Ingredient>().status == IngredientStatus.Raw && currentInteractable.GetComponent<Interactable>().currentSonInteractable.GetComponent<CuttingTable>().currentSonInteractable == null)
                    {

                        pickableInHand = pickableInHand.GetComponent<IPickable>().Place(currentInteractable.GetComponent<Interactable>().currentSonInteractable);
                        currentInteractable.GetComponent<Interactable>().currentSonInteractable.GetComponent<CuttingTable>().UpdateSlotUI();

                    }

                }

            }
            else if (pickableInHand.GetComponent<Interactable>() is Plate)
            {

                if (currentInteractable.GetComponent<Interactable>() is Dishwasher && !pickableInHand.GetComponent<Plate>().isClean)
                {

                    currentInteractable.GetComponent<Dishwasher>().AddDirtyPlate(pickableInHand);
                    pickableInHand = null;

                }
                else if (currentInteractable.GetComponent<Interactable>() is TrashCan)
                {

                    pickableInHand.GetComponent<Plate>().RemoveAllIngredients();
                    pickableInHand.GetComponent<Plate>().RemoveRecipe();
                    FindAnyObjectByType<LevelEndManager>().IncreaseFoodThrown();

                }
                else if(currentInteractable.GetComponent<Interactable>() is Fryer)
                {
                    
                    if (currentInteractable.GetComponent<Fryer>().ConfirmIsPossibleRecipe(pickableInHand.GetComponent<Plate>().currentRecipe) && !currentInteractable.GetComponent<Fryer>().isFrying && currentInteractable.GetComponent<Fryer>().currentRecipe == null)
                    {
                        
                        currentInteractable.GetComponent<Fryer>().currentRecipe = pickableInHand.GetComponent<Plate>().currentRecipe;
                        pickableInHand.GetComponent<Plate>().slot.GetChild(0).SetParent(currentInteractable.GetComponent<Fryer>().slot);
                        currentInteractable.GetComponent<Fryer>().slot.GetChild(0).SetLocalPositionAndRotation(currentInteractable.GetComponent<Fryer>().slot.position, currentInteractable.GetComponent<Fryer>().slot.rotation);
                        currentInteractable.GetComponent<Fryer>().GetDesiredRecipe();
                        currentInteractable.GetComponent<Fryer>().StartFryingCoroutine();
                        pickableInHand.GetComponent<Plate>().currentRecipe = null;
                        pickableInHand.GetComponent<Plate>().UpdateIconsUI();


                    }
                    else if(pickableInHand.GetComponent<Plate>().currentRecipe == null && !currentInteractable.GetComponent<Fryer>().isFrying && currentInteractable.GetComponent<Fryer>().currentRecipe != null)
                    {

                        pickableInHand.GetComponent<Plate>().currentRecipe = currentInteractable.GetComponent<Fryer>().currentRecipe;
                        currentInteractable.GetComponent<Fryer>().slot.GetChild(0).SetParent(pickableInHand.GetComponent<Plate>().slot);
                        pickableInHand.GetComponent<Plate>().slot.GetChild(0).SetPositionAndRotation(pickableInHand.GetComponent<Plate>().slot.position, pickableInHand.GetComponent<Plate>().slot.rotation);
                        currentInteractable.GetComponent<Fryer>().currentRecipe = null;
                        currentInteractable.GetComponent<Fryer>().UpdateSlotUI();
                        pickableInHand.GetComponent<Plate>().UpdateIconsUI();

                    }

                }

            }

        }
        else if (pickableInHand == null)
        {

            if (currentInteractable.GetComponent<Interactable>().currentSonInteractable != null)
            {
                if (currentInteractable.GetComponent<Interactable>().currentSonInteractable.GetComponent<Interactable>() is RiceCooker)
                {

                    if (currentInteractable.GetComponent<Interactable>().currentSonInteractable.GetComponent<RiceCooker>().isCooking != true && currentInteractable.GetComponent<Interactable>().currentSonInteractable.GetComponent<RiceCooker>().currentSonInteractable != null)
                    {

                        pickableInHand = currentInteractable.GetComponent<Interactable>().currentSonInteractable.GetComponent<RiceCooker>().currentSonInteractable.GetComponent<IPickable>().Pick(slot);
                        currentInteractable.GetComponent<Interactable>().currentSonInteractable.GetComponent<RiceCooker>().currentSonInteractable = null;
                        pickableInHand.transform.GetChild(0).gameObject.SetActive(true);   
                        currentInteractable.GetComponent<Interactable>().currentSonInteractable.GetComponent<RiceCooker>().DeactivateSmoke();
                        currentInteractable.GetComponent<Interactable>().currentSonInteractable.GetComponent<RiceCooker>().UpdateSlotUI();

                    }

                }
                else if (currentInteractable.GetComponent<Interactable>().currentSonInteractable.GetComponent<Interactable>() is CuttingTable)
                {

                    if (currentInteractable.GetComponent<Interactable>().currentSonInteractable.GetComponent<CuttingTable>().hasStartedCutting != true && currentInteractable.GetComponent<Interactable>().currentSonInteractable.GetComponent<CuttingTable>().currentSonInteractable != null)
                    {

                        pickableInHand = currentInteractable.GetComponent<Interactable>().currentSonInteractable.GetComponent<CuttingTable>().currentSonInteractable.GetComponent<IPickable>().Pick(slot);
                        currentInteractable.GetComponent<Interactable>().currentSonInteractable.GetComponent<CuttingTable>().currentSonInteractable = null;
                        currentInteractable.GetComponent<Interactable>().currentSonInteractable.GetComponent<CuttingTable>().UpdateSlotUI();
                        Debug.Log("Si lo agarra2");

                    }

                }else if (currentInteractable.GetComponent<Interactable>().currentSonInteractable.GetComponent<Interactable>() is WorkerGuide)
                {

                    //Do nothing

                }
                else
                {

                    pickableInHand = currentInteractable.GetComponent<Interactable>().currentSonInteractable.GetComponent<IPickable>().Pick(slot);
                    currentInteractable.GetComponent<Interactable>().currentSonInteractable = null;

                }

            }
            else if (currentInteractable.GetComponent<Interactable>() is Fridge)
            {

                GameObject ingredient = currentInteractable.GetComponent<Fridge>().SpawnIngredient();
                pickableInHand = ingredient.GetComponent<IPickable>().Pick(slot);

            }
            else if (currentInteractable.GetComponent<Interactable>() is Dishwasher)
            {

                pickableInHand = currentInteractable.GetComponent<Dishwasher>().PickCleanPlate(slot);

            }


        }

    }

    private void HandleInteract(InputAction.CallbackContext context)
    {
        currentInteractable.GetComponent<Interactable>().Interact();
    }
}







