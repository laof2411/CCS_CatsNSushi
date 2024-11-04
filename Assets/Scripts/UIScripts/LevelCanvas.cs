using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelCanvas : MonoBehaviour
{

    [SerializeField] private Image stressBar;
    [SerializeField] private TextMeshProUGUI timeTextMeshPro;
    [SerializeField] private float startingTime;
    [SerializeField] private TextMeshProUGUI coinsTextMeshPro;

    [SerializeField] private GameObject[] mainUI;

    private void Start()
    {

        startingTime = GameManager.instance.currentLevelData.time;


    }

    private void Update()
    {
        
        startingTime -= Time.deltaTime;
        if(startingTime <= 0 && !FindAnyObjectByType<LevelEndManager>().wonLevel)
        {

            timeTextMeshPro.text = 0f.ToString();
            GameManager.instance.canPlayLevel[GameManager.instance.currentLevelData.levelSceneNumber] = true;
            mainUI[1].SetActive(false);
            mainUI[0].SetActive(false);
            ActivateEndingScreen();

        }
        else
        {

            timeTextMeshPro.text = Mathf.Round(startingTime).ToString();
            

        }
        
    }

    private void ActivateEndingScreen()
    {
        FindAnyObjectByType<LevelEndManager>().WinLevel();
        Time.timeScale = 0f;
    }

    public void UpdateCoinsUI(int nekoins)
    {

        coinsTextMeshPro.text = nekoins + " Nekoins";

    }

}
