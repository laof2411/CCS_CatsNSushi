using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class RiceCooker : Interactable
{

    [SerializeField] private GameObject progressUIPrefab;
    [SerializeField] private GameObject singleSlotUIPrefab;

    [SerializeField] private GameObject progressUI;
    [SerializeField] private GameObject singleSlotUI;
    [SerializeField] private GameObject worldUI;

    [SerializeField] private float totalCookTime;
    private float currentCookTime;

    public bool isCooking;

    [SerializeField] private AudioSource cookSound;
    [SerializeField] private GameObject cookingParticles;

    protected override void Awake()
    {

        base.Awake();

        Setup();

    }

    private void Setup()
    {

        currentCookTime = 0;
        progressUI = Instantiate(progressUIPrefab, worldUI.transform);
        progressUI.GetComponent<LookAtCamera>().targetToFollow = transform;
        singleSlotUI = Instantiate(singleSlotUIPrefab, worldUI.transform);
        singleSlotUI.GetComponent<LookAtCamera>().targetToFollow = transform;


    }


    public void UpdateSlotUI()
    {

        if(currentSonInteractable != null)
        {

            singleSlotUI.SetActive(true);
            singleSlotUI.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = currentSonInteractable.GetComponent<Ingredient>().data.sprite;

        }
        else
        {

            singleSlotUI.SetActive(false);

        }

    }

    private void UpdateBarUI()
    {

        if (isCooking)
        {

            progressUI.SetActive(true);
            float percentageCompleted = currentCookTime / totalCookTime;
            progressUI.transform.GetChild(1).GetComponent<Image>().fillAmount = percentageCompleted;

        }
        else
        {

            progressUI.SetActive(false);

        }

    }

    public void StartCookingCoroutine()
    {

        isCooking = true;
        UpdateSlotUI();
        DeactivateSmoke();
        StartCoroutine(CookRice());

    }

    private IEnumerator CookRice()
    {
        cookSound.Play();
        currentSonInteractable.transform.GetChild(0).gameObject.SetActive(false);
        
        while (isCooking)
        {

            currentCookTime += Time.deltaTime;
            UpdateBarUI();
            if(currentCookTime >= totalCookTime)
            {

                isCooking = false;
                UpdateBarUI();
                currentSonInteractable.GetComponent<Ingredient>().status = IngredientStatus.Cooked;
                Destroy(currentSonInteractable.transform.GetChild(0).gameObject);
                GameObject temp = Instantiate(currentSonInteractable.GetComponent<Ingredient>().preparedGameObject, currentSonInteractable.transform.position, currentSonInteractable.transform.rotation);
                temp.transform.SetParent(currentSonInteractable.transform);
                temp.SetActive(false);
                currentCookTime = 0;
                cookSound.Stop();
                cookingParticles.SetActive(true);

            }

            yield return null;

        } 

    }

    public void DeactivateSmoke()
    {

        cookingParticles.SetActive(false);

    }
 

}
