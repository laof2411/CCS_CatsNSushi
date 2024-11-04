using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public bool[] collectiblesArray;
    public bool[] canPlayLevel;

    public static GameManager instance;

    public int nekoins = 0;

    public LevelData currentLevelData;

    public bool isChangingScene;

    void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    public void LoadScenePublicMethod(LevelData levelData)
    {

        if (!isChangingScene)
        {

            FindAnyObjectByType<BlackCanvasManager>().IncreaseSize();
            Invoke(nameof(LoadScene), 2.1f);
            Time.timeScale = 1.0f;
            currentLevelData = levelData;
            isChangingScene = true;

        }


    }

    private void LoadScene()
    {

        isChangingScene = false;
        SceneManager.LoadScene(currentLevelData.levelSceneNumber);

    }

}
