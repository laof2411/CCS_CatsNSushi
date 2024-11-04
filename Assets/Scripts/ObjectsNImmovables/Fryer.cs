using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fryer : Interactable
{

    [SerializeField] private float totalFryingTime;
    private float currentFryingTime;

    public bool isFrying = false;

    public Recipes desiredRecipe = null;
    public Recipes currentRecipe = null;

    public Recipes[] possibleEnteringRecipes;
    public Recipes[] possibleOutgoingRecipes;

    [SerializeField] private AudioSource fryingSound;

    [SerializeField] private GameObject progressUIPrefab;
    [SerializeField] private GameObject singleSlotUIPrefab;

    [SerializeField] private GameObject progressUI;
    [SerializeField] private GameObject singleSlotUI;

    [SerializeField] private GameObject worldUI;

    protected override void Awake()
    {

        base.Awake();

        Setup();

    }


    private void Setup()
    {

        currentFryingTime = 0;

        progressUI = Instantiate(progressUIPrefab, worldUI.transform);
        progressUI.GetComponent<LookAtCamera>().targetToFollow = transform;
        singleSlotUI = Instantiate(singleSlotUIPrefab, worldUI.transform);
        singleSlotUI.GetComponent<LookAtCamera>().targetToFollow = transform;

    }

    public void StartFryingCoroutine()
    {

        StartCoroutine(FryingCoroutine());
        UpdateSlotUI();

    }

    private void UpdateBarUI()
    {

        if (isFrying)
        {

            progressUI.SetActive(true);
            float percentageCompleted = currentFryingTime / totalFryingTime;
            progressUI.transform.GetChild(1).GetComponent<Image>().fillAmount = percentageCompleted;

        }
        else
        {

            progressUI.SetActive(false);

        }

    }

    private IEnumerator FryingCoroutine()
    {
        fryingSound.Play();
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.235f, transform.position.z);
        isFrying = true;
        while (isFrying)
        {

            currentFryingTime += Time.deltaTime;
            UpdateBarUI();
            if (currentFryingTime >= totalFryingTime)
            {
                currentRecipe = desiredRecipe;
                desiredRecipe = null;
                Destroy(slot.GetChild(0).gameObject);
                GameObject obj = Instantiate(currentRecipe.recipePrefab, slot.transform.position, slot.transform.rotation);
                obj.transform.SetParent(slot.transform);
                isFrying = false;
                currentFryingTime = 0;
                transform.position = new Vector3(transform.position.x, transform.position.y + 0.235f, transform.position.z);
                fryingSound.Stop();
                UpdateBarUI();
                UpdateSlotUI();

            }

            yield return null;

        }

    }

    public void UpdateSlotUI()
    {

        if (currentRecipe != null)
        {

            singleSlotUI.SetActive(true);
            singleSlotUI.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = currentRecipe.recipeSprite;

        }
        else
        {

            singleSlotUI.SetActive(false);

        }

    }

    public bool ConfirmIsPossibleRecipe(Recipes recipe)
    {

        if(recipe == possibleEnteringRecipes[0])
        {

            return true;

        }
        if (recipe == possibleEnteringRecipes[1])
        {

            return true;

        }
        if (recipe == possibleEnteringRecipes[2])
        {

            return true;

        }
        return false;
    }

    public void GetDesiredRecipe()
    {

        if(currentRecipe == possibleEnteringRecipes[0])
        {

            desiredRecipe = possibleOutgoingRecipes[0];

        }
        else if (currentRecipe == possibleEnteringRecipes[1])
        {

            desiredRecipe = possibleOutgoingRecipes[1];

        }
        else if(currentRecipe == possibleEnteringRecipes[2])
        {

            desiredRecipe = possibleOutgoingRecipes[2];

        }
        
    }

}
