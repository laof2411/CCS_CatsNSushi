using UnityEngine;
using UnityEngine.UI;

public class StressManager : MonoBehaviour
{

    private float currentStressAmount;
    [SerializeField] Image stressUI;

    [SerializeField] GameObject endLevelUI;

    public void AddStress(float stressIncrease)
    {

        currentStressAmount += stressIncrease;

        if(currentStressAmount > 1)
        {

            currentStressAmount = 1;
            endLevelUI.SetActive(true);

        }

        ModifyStressUI(currentStressAmount);

    }

    private void ModifyStressUI(float stressAmount)
    {

        stressUI.fillAmount = stressAmount;


    }

}
