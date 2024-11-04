using UnityEngine;

public class LevelSelectionUI : MonoBehaviour
{

    [SerializeField] private GameObject levelSelectorUIObject;
    [SerializeField] private GameObject startingMenuUIObject;

    [SerializeField] private GameObject firstLevel;

    public LevelData currentLevelData;

    public Animator levelSelectionUIAnimator;

    public bool canMoveLevelSelector = true;

    public GameObject currentLevelSelected;

    public AnimatorClipInfo currentAnimationState;

    public void ResetAnimation()
    {

        levelSelectionUIAnimator.Play("02-01", 0, 1);
        currentLevelSelected = firstLevel;

    }

    public void MoveRightLevelSelector()
    {
        GetNewLevelData();
        if (currentLevelData.levelName == "Saturday" || !canMoveLevelSelector) { return; }
        levelSelectionUIAnimator.SetTrigger("MoveRight");
        Debug.Log("Derecha");

    }

    public void MoveLeftLevelSelector()
    {
        GetNewLevelData();
        if (currentLevelData.levelName == "Monday" || !canMoveLevelSelector) { return; }
        levelSelectionUIAnimator.SetTrigger("MoveLeft");
        Debug.Log("Izquierda");


    }

    public void DeactivateLevelSelector()
    {
        GetNewLevelData();
        if (!canMoveLevelSelector) { return; }
        levelSelectorUIObject.SetActive(false);
        startingMenuUIObject.SetActive(true);

    }

    public void CallLoadSceneMethod()
    {
        GetNewLevelData();
        GameManager.instance.LoadScenePublicMethod(currentLevelData);

    }

    public void GetNewLevelData()
    {

        currentLevelData = currentLevelSelected.GetComponent<LoadMainMenuElements>().levelData;

    }

}
