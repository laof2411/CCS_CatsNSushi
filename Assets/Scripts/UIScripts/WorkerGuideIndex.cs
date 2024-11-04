using UnityEngine;

public class WorkerGuideIndex : MonoBehaviour
{

    [SerializeField] private GameObject workersGuide;
    [SerializeField] private GameObject indexGameObject;
    [SerializeField] private GameObject recipesGameObject;
    [SerializeField] private GameObject ingredientsGameObject;
    [SerializeField] private GameObject immovablesGameObject;
    [SerializeField] private GameObject controlsGameObject;

    [SerializeField] private LevelData mainMenuData;

    public void OpenIndex()
    {

        indexGameObject.SetActive(true);
        recipesGameObject.SetActive(false);
        ingredientsGameObject.SetActive(false);
        immovablesGameObject.SetActive(false);
        controlsGameObject.SetActive(false);

    }

    public void OpenRecipes()
    {

        recipesGameObject.SetActive(true);
        indexGameObject.SetActive(false);

    }

    public void OpenIngredients()
    {

        ingredientsGameObject.SetActive(true);
        indexGameObject.SetActive(false);

    }

    public void OpenImmovables()
    {

        immovablesGameObject.SetActive(true);
        indexGameObject.SetActive(false);

    }

    public void OpenControls()
    {

        controlsGameObject.SetActive(true);
        indexGameObject.SetActive(false);

    }

    public void ExitWorkersGuide()
    {

        indexGameObject.SetActive(false);
        recipesGameObject.SetActive(false);
        ingredientsGameObject.SetActive(false);
        immovablesGameObject.SetActive(false);
        controlsGameObject.SetActive(false);
        workersGuide.SetActive(false);

        Time.timeScale = 1f;

    }

    public void ReturnToIndex()
    {

        indexGameObject.SetActive(true);
        recipesGameObject.SetActive(false);
        ingredientsGameObject.SetActive(false);
        immovablesGameObject.SetActive(false);
        controlsGameObject.SetActive(false);
        workersGuide.SetActive(true);

    }

    public void ReturnToMainMenu()
    {

        Time.timeScale = 1f;
        GameManager.instance.LoadScenePublicMethod(mainMenuData);

    }



}
