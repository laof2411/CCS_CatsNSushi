using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoadMainMenuElements : MonoBehaviour
{
    public LevelData levelData;

    [SerializeField] private Image levelImageObject;
    [SerializeField] private Image[] recipesImages;

    [SerializeField] private TextMeshProUGUI timeTextMesh;
    [SerializeField] private TextMeshProUGUI levelTextMesh;

    [SerializeField] private LevelSelectionUI LeftButton;
    [SerializeField] private LevelSelectionUI RightButton;

    [SerializeField] private GameObject chains;

    private void Awake()
    {
        SetLevelBlockVariables();

    }

    private void SetLevelBlockVariables()
    {
        levelImageObject.sprite = levelData.levelImage;
        timeTextMesh.text = "" + levelData.time + " seconds";
        levelTextMesh.text = levelData.levelName;

        if (GameManager.instance.canPlayLevel[levelData.levelSceneNumber - 1])
        {

            chains.SetActive(false);

        }

        int i = 0;

        foreach (var recipe in levelData.recipesUsed)
        {

            recipesImages[i].sprite = levelData.recipesUsed[i].recipeSprite;
            i++;

        }
    }

}
