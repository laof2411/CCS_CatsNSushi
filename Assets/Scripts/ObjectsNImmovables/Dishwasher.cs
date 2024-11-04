using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dishwasher : Interactable
{

    [SerializeField] private List<Plate> platesInDishwasher;

    [SerializeField] private float timePerPlate = 4;

    public bool isWashing;

    [SerializeField] private Animator dishwasherAnimator;

    [SerializeField] private GameObject progressUIPrefab;

    [SerializeField] private GameObject progressUI;
    [SerializeField] private GameObject worldUI;

    private float currentWashingTime;
    private float totalWashingTime;

    protected override void Awake()
    {

        progressUI = Instantiate(progressUIPrefab, worldUI.transform);
        progressUI.GetComponent<LookAtCamera>().targetToFollow = transform;

    }

    public override void Interact()
    {
        base.Interact();

        if(NumberOfDirtyPlates() > 0)
        {

            isWashing = true;
            StartCoroutine(WashPlates());

        }

    }

    private void UpdateUI()
    {
      
        if (isWashing)
        {

            progressUI.SetActive(true);
            float percentageCompleted = currentWashingTime / totalWashingTime;
            progressUI.transform.GetChild(1).GetComponent<Image>().fillAmount = percentageCompleted;

        }
        else
        {

            progressUI.SetActive(false);

        }

    }

    public void AddDirtyPlate(GameObject plate)
    {

        platesInDishwasher.Add(plate.GetComponent<Plate>());
        plate.transform.SetParent(slot);
        plate.transform.SetPositionAndRotation(slot.position, slot.rotation);

    }

    public GameObject PickCleanPlate(Transform slot)
    {

        foreach(Plate p in platesInDishwasher)
        {

            if (p.isClean)
            {

                p.transform.SetParent(slot);
                p.transform.SetPositionAndRotation(slot.position, slot.rotation);
                platesInDishwasher.Remove(p);
                return p.gameObject;

            }

        }
        return null;

    }

    private int NumberOfDirtyPlates()
    {

        int number = 0;

        foreach(Plate p in  platesInDishwasher)
        {

            if (!p.isClean)
            {

                number++;

            }

        }

        return number;

    }

    private IEnumerator WashPlates()
    {

        dishwasherAnimator.Play("lavar");
        totalWashingTime = NumberOfDirtyPlates() * timePerPlate;
        while (isWashing)
        {

            currentWashingTime += Time.deltaTime;
            UpdateUI();
            if (currentWashingTime >= totalWashingTime)
            {

                isWashing = false;
                UpdateUI();
                currentWashingTime = 0;

            }
            yield return null;
        }
        dishwasherAnimator.Play("finished_washing");
        foreach (Plate p in platesInDishwasher)
        {

            p.SetClean();

        }

    }

}
