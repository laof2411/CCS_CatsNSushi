using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevelRequirements : MonoBehaviour
{

    [SerializeField] private LevelCanvas levelCanvas;

    [SerializeField] private GameObject[] decorations;

    public void Start()
    {
        LevelCanvasRequirements();

        for(int i = 0; i < decorations.Length; i++)
        {

            if (GameManager.instance.collectiblesArray[i] == true)
            {

                decorations[i].SetActive(true);

            }

        }

        GameObject.FindAnyObjectByType<Spawner>().CallSpawnerCoroutine();


        FindAnyObjectByType<TutorialManager>().ActivateTutorial(0);

    }

    private void LevelCanvasRequirements()
    {
        levelCanvas.UpdateCoinsUI(FindAnyObjectByType<LevelEndManager>().nekoinsGained);
    }
}
