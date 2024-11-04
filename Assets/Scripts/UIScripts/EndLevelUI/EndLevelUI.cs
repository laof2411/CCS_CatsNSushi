using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelUI : MonoBehaviour
{

    [SerializeField] private LevelData[] levelDatas;

    [SerializeField] private GameObject GachaponObject;
    [SerializeField] private GameObject EndLevelUIObject;
    

    public void Replay()
    {

        GameManager.instance.LoadScenePublicMethod(GameManager.instance.currentLevelData);

    }

    public void BackToMenu()
    {

        GameManager.instance.LoadScenePublicMethod(levelDatas[0]);

    }

    public void NextLevel()
    {

        if (GameManager.instance.canPlayLevel[GameManager.instance.currentLevelData.levelSceneNumber])
        {

            switch (GameManager.instance.currentLevelData.name)
            {

                case "Level1":
                    {

                        GameManager.instance.LoadScenePublicMethod(levelDatas[2]);
                        break;
                    }
                case "Level2":
                    {

                        GameManager.instance.LoadScenePublicMethod(levelDatas[3]);
                        break;
                    }
                case "Level3":
                    {

                        GameManager.instance.LoadScenePublicMethod(levelDatas[4]);
                        break;
                    }
                case "Level4":
                    {

                        GameManager.instance.LoadScenePublicMethod(levelDatas[5]);
                        break;
                    }
                case "Level5":
                    {

                        GameManager.instance.LoadScenePublicMethod(levelDatas[6]);
                        break;
                    }
                case "Level6":
                    {

                        //No
                        break;
                    }


            }

        }
        

    }

    public void Gachapon()
    {

        EndLevelUIObject.SetActive(false);
        GachaponObject.SetActive(true);

    }

}
