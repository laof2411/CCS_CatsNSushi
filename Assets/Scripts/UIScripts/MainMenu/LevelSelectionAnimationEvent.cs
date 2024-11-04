using UnityEngine;

public class LevelSelectionAnimationEvent : MonoBehaviour
{

    public GameObject[] levelSelectionBlocks;

    public LevelSelectionUI rightButton;
    public LevelSelectionUI leftButton;
    public LevelSelectionUI backButton;

    public Animator animator;

    

    public void GetCurrentLevelBlock()
    {

        switch (animator.GetCurrentAnimatorClipInfo(0)[0].clip.name)
        {

            case "01-02":
                {

                    rightButton.currentLevelSelected = levelSelectionBlocks[1];
                    leftButton.currentLevelSelected = levelSelectionBlocks[1];
                    backButton.currentLevelSelected = levelSelectionBlocks[1];
                    break;
                }
            case "02-03":
                {

                    rightButton.currentLevelSelected = levelSelectionBlocks[2];
                    leftButton.currentLevelSelected = levelSelectionBlocks[2];
                    backButton.currentLevelSelected = levelSelectionBlocks[2];
                    break;
                }
            case "03-04":
                {

                    rightButton.currentLevelSelected = levelSelectionBlocks[3];
                    leftButton.currentLevelSelected = levelSelectionBlocks[3];
                    backButton.currentLevelSelected = levelSelectionBlocks[3];
                    break;
                }
            case "04-05":
                {

                    rightButton.currentLevelSelected = levelSelectionBlocks[4];
                    leftButton.currentLevelSelected = levelSelectionBlocks[4];
                    backButton.currentLevelSelected = levelSelectionBlocks[4];
                    break;
                }
            case "05-06":
                {

                    rightButton.currentLevelSelected = levelSelectionBlocks[5];
                    leftButton.currentLevelSelected = levelSelectionBlocks[5];
                    backButton.currentLevelSelected = levelSelectionBlocks[5];
                    break;
                }
            case "06-05":
                {

                    rightButton.currentLevelSelected = levelSelectionBlocks[4];
                    leftButton.currentLevelSelected = levelSelectionBlocks[4];
                    backButton.currentLevelSelected = levelSelectionBlocks[4];
                    break;
                }
            case "05-04":
                {

                    rightButton.currentLevelSelected = levelSelectionBlocks[3];
                    leftButton.currentLevelSelected = levelSelectionBlocks[3];
                    backButton.currentLevelSelected = levelSelectionBlocks[3];
                    break;
                }
            case "04-03":
                {

                    rightButton.currentLevelSelected = levelSelectionBlocks[2];
                    leftButton.currentLevelSelected = levelSelectionBlocks[2];
                    backButton.currentLevelSelected = levelSelectionBlocks[2];
                    break;
                }
            case "03-02":
                {

                    rightButton.currentLevelSelected = levelSelectionBlocks[1];
                    leftButton.currentLevelSelected = levelSelectionBlocks[1];
                    backButton.currentLevelSelected = levelSelectionBlocks[1];
                    break;
                }
            case "02-01":
                {

                    rightButton.currentLevelSelected = levelSelectionBlocks[0];
                    leftButton.currentLevelSelected = levelSelectionBlocks[0];
                    backButton.currentLevelSelected = levelSelectionBlocks[0];
                    break;
                }


        }

    }

}
