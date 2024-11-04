using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecipesManager : MonoBehaviour
{
    public List<Recipes> recipesList = new List<Recipes>();

    public Recipes catmalgamRecipe;

    public void Awake()
    {

        for(int i = 0; i < GameManager.instance.currentLevelData.recipesUsed.Length; i++)
        {

            recipesList.Add(GameManager.instance.currentLevelData.recipesUsed[i]);

        }        

    }


    public Recipes ReturnCorrectRecipe(Ingredient[] ingredients)
    {

        IngredientType[] ingredientArrays = new IngredientType[3];

        if (ingredients[0] == null)
        {
            ingredientArrays[0] = IngredientType.NoIngredient;
        }
        else
        {
            ingredientArrays[0] = ingredients[0].GetComponent<Ingredient>().type;
        }
        if (ingredients[1] == null)
        {
            ingredientArrays[1] = IngredientType.NoIngredient;
        }
        else
        {
            ingredientArrays[1] = ingredients[1].GetComponent<Ingredient>().type;
        }
        if (ingredients[2] == null)
        {
            ingredientArrays[2] = IngredientType.NoIngredient;
        }
        else
        {
            ingredientArrays[2] = ingredients[2].GetComponent<Ingredient>().type;
        }

        foreach (var recipe in recipesList)
        {

            int i = 0;
            if (ingredientArrays[0] == recipe.ingredientsArray[0]) 
            {
                i++;      
            }
            if (ingredientArrays[1] == recipe.ingredientsArray[1])
            {
                i++;
            }
            if (ingredientArrays[2] == recipe.ingredientsArray[2])
            {
                i++;
            }
            if(i == 3)
            {
                return recipe;
            }

        }
        return catmalgamRecipe;
    }

}
