using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{

    public GameObject tutorialUI;
    
    public void ExitTutorial()
    {

        Time.timeScale = 1.0f;
        tutorialUI.SetActive(false);

    }

}
