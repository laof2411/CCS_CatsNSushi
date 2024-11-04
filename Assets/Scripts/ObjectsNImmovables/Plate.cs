using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Plate : Interactable, IPickable
{
    [SerializeField] private GameObject[] ingredientUISlots;
    [SerializeField] private GameObject recipeSlot;
    [SerializeField] private GameObject progressUI;

    [SerializeField] private GameObject UISlotsPrefab;
    [SerializeField] private GameObject progressBarPrefab;
    [SerializeField] private GameObject worldUI;

    [SerializeField] private Material cleanMaterial;
    [SerializeField] private Material dirtyMaterial;
    [SerializeField] private MeshRenderer meshRenderer;

    private const int MaxNumberIngredients = 3;

    public bool isClean = true;

    public Ingredient[] currentIngredients = new Ingredient[MaxNumberIngredients];

    public Recipes currentRecipe;
    public Recipes desiredRecipe;

    public bool isMakingRecipe = false;

    [SerializeField] private float totalCreationTime;
    [SerializeField] private float currentCreationTime;

    [SerializeField] private AudioSource cookingSound;

    [SerializeField] private Recipes catamalgam;

    [SerializeField] private ParticleSystem cookingParticles;

    protected override void Awake()
    {
        base.Awake();

        Setup();

    }


    private void Update()
    {

        if (isMakingRecipe)
        {
            UpdateProgressBar();
            currentCreationTime += Time.deltaTime;
            if(currentCreationTime >= totalCreationTime) 
            {

                CreateFood(desiredRecipe);
                SetMakingToFalse();          
            
            }

        }

    }

    private void CreateFood(Recipes recipeToCreate)
    {

        currentRecipe = recipeToCreate;
        desiredRecipe = null;
        GameObject temp = Instantiate(recipeToCreate.recipePrefab, slot.transform.position,slot.transform.rotation);
        temp.transform.SetParent(slot.transform);
        currentCreationTime = 0;
        RemoveAllIngredients();
        cookingParticles.Stop();

        FindAnyObjectByType<TutorialManager>().ActivateTutorial(2);
        FindAnyObjectByType<LevelEndManager>().cooked_times++;
        UpdateProgressBar();

    }

    public void SetMakingToFalse()
    {

        isMakingRecipe = false;
        FindAnyObjectByType<PlayerMovementController>().isMaking = false;

    }

    public override bool IsFull()
    {

        if (currentIngredients[0] != null && currentIngredients[1] != null && currentIngredients[2] != null)
        {

            return true;

        }
        else
        {

            return false;

        }

    }

    private void Setup()
    {

        currentCreationTime = 0;

        GameObject temp = Instantiate(UISlotsPrefab, worldUI.transform);
        ingredientUISlots[0] = temp.transform.GetChild(0).gameObject;
        ingredientUISlots[1] = temp.transform.GetChild(1).gameObject;
        ingredientUISlots[2] = temp.transform.GetChild(2).gameObject;

        recipeSlot = temp.transform.GetChild(3).gameObject;

        temp.GetComponent<LookAtCamera>().targetToFollow = this.gameObject.transform;

        temp = Instantiate(progressBarPrefab, worldUI.transform);
        progressUI = temp;

        temp.GetComponent<LookAtCamera>().targetToFollow = this.gameObject.transform;


    }

    public override GameObject AddIngredient(Ingredient ingredientToAdd)
    {
        if (IsFull()) return ingredientToAdd.gameObject;
        if (ingredientToAdd.status == IngredientStatus.Raw) return ingredientToAdd.gameObject;

        if (currentIngredients[0] == null)
        {

            currentIngredients[0] = ingredientToAdd;

        }
        else if (currentIngredients[1] == null)
        {

            currentIngredients[1] = ingredientToAdd;
            cookingParticles.Play();

        }
        else
        {

            currentIngredients[2] = ingredientToAdd;

        }
        ingredientToAdd.transform.SetParent(slot);
        ingredientToAdd.transform.SetPositionAndRotation(slot.transform.position, Quaternion.identity);


        UpdateIconsUI();

        return null;
    }

    public void RemoveAllIngredients()
    {
        if (currentIngredients[0] != null) 
        {

            Destroy(currentIngredients[0].gameObject);

        }

        if (currentIngredients[1] != null)
        {

            Destroy(currentIngredients[1].gameObject);
        }

        if (currentIngredients[2] != null) 
        {

            Destroy(currentIngredients[2].gameObject);


        }

        currentIngredients[0] = null;
        currentIngredients[1] = null;
        currentIngredients[2] = null;

        cookingParticles.Stop();
        UpdateIconsUI();
        UpdateProgressBar();
    }

    public void RemoveRecipe()
    {

        currentRecipe = null;
        desiredRecipe = null;
        Destroy(slot.GetChild(0).gameObject);

        UpdateIconsUI();
        UpdateProgressBar();

    }

    public void UpdateIconsUI()
    {
      
        for(int i = 0; i < currentIngredients.Length; i++)
        {

            if (currentIngredients[i] != null)
            {
                ingredientUISlots[i].SetActive(true);
                ingredientUISlots[i].transform.GetChild(0).GetComponent<Image>().sprite = currentIngredients[i].data.sprite;

            }
            else
            {

                ingredientUISlots[i].SetActive(false);

            }

        }

        if(currentRecipe != null)
        {

            recipeSlot.SetActive(true);
            recipeSlot.transform.GetChild(0).GetComponent<Image>().sprite = currentRecipe.recipeSprite;

        }
        else
        {

            recipeSlot.SetActive(false);
            recipeSlot.transform.GetChild(0).GetComponent<Image>().sprite = null;

        }

    }

    private void UpdateProgressBar()
    {

        if (desiredRecipe != null)
        {

            progressUI.SetActive(true);
            float percentageCompleted = currentCreationTime / totalCreationTime;
            progressUI.transform.GetChild(1).GetComponent<Image>().fillAmount = percentageCompleted;

        }
        else
        {

            progressUI.SetActive(false);

        }

    }

    public void SetClean()
    {
        if (!isClean)
        {

            meshRenderer.material = cleanMaterial;
            isClean = true;
            FindAnyObjectByType<LevelEndManager>().cleaned_plates++;

        }

    }

    public void SetDirty()
    {
        if (isClean)
        {

            meshRenderer.material = dirtyMaterial;
            isClean = false;
            RemoveAllIngredients();
            RemoveRecipe();
            FindAnyObjectByType<LevelEndManager>().dirty_plates++;

        }

    }

    public GameObject Pick(Transform playerSlot)
    {

        transform.parent = playerSlot;
        transform.SetPositionAndRotation(playerSlot.position, playerSlot.rotation);
        FindAnyObjectByType<LevelEndManager>().picked_pickables++;
        return this.gameObject;

    }

    public GameObject Place(GameObject parentInteractable)
    {

        Transform interactableSlot = parentInteractable.GetComponent<Interactable>().slot;

        transform.SetParent(interactableSlot);
        transform.SetPositionAndRotation(interactableSlot.position, interactableSlot.rotation);
        parentInteractable.GetComponent<Interactable>().currentSonInteractable = this.gameObject;
        FindAnyObjectByType<LevelEndManager>().placed_pickables++;

        return null;
    }

    public override void Interact()
    {
        if (currentIngredients[0] != null && currentIngredients[1] != null)
        {

            if (desiredRecipe == null)
            {

                desiredRecipe = GameObject.FindAnyObjectByType<RecipesManager>().ReturnCorrectRecipe(currentIngredients);

            }
            if (desiredRecipe != null)
            {

                if(desiredRecipe == catamalgam)
                {

                    CreateFood(desiredRecipe);
                    FindAnyObjectByType<TutorialManager>().ActivateTutorial(5);

                }
                else
                {

                    isMakingRecipe = true;
                    FindAnyObjectByType<PlayerMovementController>().isMaking = true;

                    cookingSound.Play();
                    cookingSound.time = currentCreationTime;

                }

            }


        }

    }

    public void StopCookingSound()
    {

        cookingSound.Stop();

    }
}
