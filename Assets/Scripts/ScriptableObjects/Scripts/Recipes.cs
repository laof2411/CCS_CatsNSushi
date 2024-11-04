using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "RecipeData", order = 0)]
public class Recipes : ScriptableObject
{

    public IngredientType[] ingredientsArray;

    public GameObject recipePrefab;
    public Sprite recipeSprite;

}
