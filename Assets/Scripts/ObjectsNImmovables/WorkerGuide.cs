using UnityEngine;

public class WorkerGuide : Interactable
{

    [SerializeField] private GameObject workerGuideUI;
    [SerializeField] private GameObject indexUI;
    [SerializeField] private GameObject recipesUI;
    [SerializeField] private GameObject ingredientsUI;
    [SerializeField] private GameObject immovablesUI;
    [SerializeField] private GameObject controlsUI;    

    public override void Interact()
    {
        
        workerGuideUI.SetActive(true);
        indexUI.SetActive(true);
        recipesUI.SetActive(false);
        ingredientsUI.SetActive(false);
        immovablesUI.SetActive(false);
        controlsUI.SetActive(false);

        Time.timeScale = 0f;

    }

}
