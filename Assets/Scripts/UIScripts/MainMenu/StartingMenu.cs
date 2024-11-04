using UnityEngine;

public class StartingMenu : MonoBehaviour
{

    [SerializeField] private GameObject LevelSelectorUIObject;
    [SerializeField] private GameObject StartingMenuUIObject;    
    [SerializeField] private GameObject GachaponUIObject;    
    [SerializeField] private GameObject CreditsUIObject;    


    
    public void ActivateLevelSelector()
    {

        LevelSelectorUIObject.SetActive(true);
        StartingMenuUIObject.SetActive(false);
        LevelSelectorUIObject.transform.GetChild(8).GetComponent<LevelSelectionUI>().ResetAnimation();

    }


    public void ActivateGachapon()
    {

        GachaponUIObject.SetActive(true);
        StartingMenuUIObject.SetActive(false);

    }

    public void ActivateCredits()
    {

        CreditsUIObject.SetActive(true);
        StartingMenuUIObject.SetActive(false);

    }

    public void ExitGame()
    {

        Application.Quit();

    }

}
