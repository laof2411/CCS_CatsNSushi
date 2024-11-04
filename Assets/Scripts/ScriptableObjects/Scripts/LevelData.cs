using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Level", menuName = "LevelData", order = 0)]

public class LevelData : ScriptableObject
{

    public string levelName;

    public int levelSceneNumber;
    public float time;

    public Sprite levelImage;

    public Recipes[] recipesUsed;

    public int plateAmount;

}
