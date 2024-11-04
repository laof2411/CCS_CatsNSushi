using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CuttingTable : Interactable
{

    [SerializeField] private float totalCuttingTime;
    public float currentCuttingTime;

    public bool isCutting;
    public bool hasStartedCutting;

    [SerializeField] private AudioSource cuttingSound;

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

        progressUI = Instantiate(progressUIPrefab, worldUI.transform);
        progressUI.GetComponent<LookAtCamera>().targetToFollow = transform;
        singleSlotUI = Instantiate(singleSlotUIPrefab, worldUI.transform);
        singleSlotUI.GetComponent<LookAtCamera>().targetToFollow = transform;
        RestartTotalCuttingTime();

    }



    public override void Interact()
    {

        base.Interact();
        if(currentSonInteractable != null)
        {

            if(currentSonInteractable.GetComponent<Ingredient>().status == IngredientStatus.Raw)
            {

                hasStartedCutting = true;
                isCutting = true;
                FindAnyObjectByType<PlayerMovementController>().isCutting = true;
                cuttingSound.Play();
                UpdateSlotUI();

            }

        }       

    }

    private void UpdateBarUI()
    {

        if (hasStartedCutting)
        {

            progressUI.SetActive(true);
            float percentageCompleted = currentCuttingTime / totalCuttingTime;
            progressUI.transform.GetChild(1).GetComponent<Image>().fillAmount = percentageCompleted;

        }
        else
        {

            progressUI.SetActive(false);

        }

    }

    public void UpdateSlotUI()
    {

        if (currentSonInteractable != null)
        {

            singleSlotUI.SetActive(true);
            singleSlotUI.transform.GetChild(0).transform.GetChild(0).GetComponent<Image>().sprite = currentSonInteractable.GetComponent<Ingredient>().data.sprite;

        }
        else
        {

            singleSlotUI.SetActive(false);

        }

    }

    private void RestartTotalCuttingTime()
    {

        currentCuttingTime = 0;
        hasStartedCutting = false;
        UpdateBarUI();

    }

    public void SetCuttingFalse()
    {

        isCutting = false;
        FindAnyObjectByType<PlayerMovementController>().isCutting = false;
        cuttingSound.Stop();
        UpdateBarUI();

    }

    private void Update()
    {
        
        if(isCutting)
        {
            UpdateBarUI();

            currentCuttingTime += Time.deltaTime;

            if(currentCuttingTime >= totalCuttingTime)
            {

                SetCuttingFalse();
                RestartTotalCuttingTime();
                currentSonInteractable.GetComponent<Ingredient>().status = IngredientStatus.Cut;

                Destroy(currentSonInteractable.transform.GetChild(0).gameObject);
                GameObject temp = Instantiate(currentSonInteractable.GetComponent<Ingredient>().preparedGameObject, currentSonInteractable.transform.position, currentSonInteractable.transform.rotation);
                temp.transform.SetParent(currentSonInteractable.transform);

            }

        }

    }

}
