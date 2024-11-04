using UnityEngine;

[CreateAssetMenu(fileName = "Collectible", menuName = "CollectibleData", order = 0)]

public class CollectibleData : ScriptableObject
{

    public string collectibleName;
    public int ID;

    public GameObject collectibleMesh;

}
