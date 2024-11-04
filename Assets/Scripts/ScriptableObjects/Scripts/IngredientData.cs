using UnityEngine;

[CreateAssetMenu(fileName = "IngredientData", menuName = "IngredientData", order = 0)]
public class IngredientData : ScriptableObject
{
    public IngredientType type;
    public IngredientStatus startingStatus;

    public GameObject ingredientPrefab;

    [Header("Visuals")]
    public Mesh un_ReadyMesh;
    public Mesh readyMesh;
    
    public Material un_readyMeshMaterial;
    public Material readyMeshMaterial;
    
    public Sprite sprite;
}
