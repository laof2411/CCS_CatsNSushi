using UnityEngine;


public abstract class Interactable : MonoBehaviour
{

    public Transform slot;

    public GameObject currentSonInteractable;

    public bool canPlaceInSlot;


    protected virtual void Awake()
    {

        CheckSlotOccupied();
    }

    private void CheckSlotOccupied()
    {

        if (slot == null) return;
        foreach(Transform child in slot)
        {

            currentSonInteractable = child.gameObject;
            if (currentSonInteractable != null) return;

        }

    }

    public virtual bool IsFull()
    {

        return false;

    }

    public virtual void Interact()
    {

    }

    public virtual GameObject AddIngredient(Ingredient ingredientToAdd)
    {
        return null;

    }

}
