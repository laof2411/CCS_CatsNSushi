using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{

    [SerializeField] public Sprite[] tutorialImages;
    [SerializeField] public bool[] tutorialSeen;
    
    [SerializeField] GameObject tutorialCanvas;

    public void ActivateTutorial(int tutorialID)
    {

        if (tutorialImages[tutorialID] != null && !tutorialSeen[tutorialID])
        {

            tutorialCanvas.SetActive(true);
            tutorialCanvas.GetComponent<Animator>().Play("GoDown");
            tutorialCanvas.transform.GetChild(1).GetComponent<Image>().sprite = tutorialImages[tutorialID];
            tutorialSeen[tutorialID] = true;
            Time.timeScale = 0f;

        }


    }

}
