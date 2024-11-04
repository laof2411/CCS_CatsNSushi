using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fridge : Interactable
{

    public IngredientData ingredientData;
    public SpriteRenderer spriteRenderer;

    protected override void Awake()
    {
        
        base.Awake();
        spriteRenderer.sprite = ingredientData.sprite;

    }

    public GameObject SpawnIngredient()
    {

        GameObject ingredient = Instantiate(ingredientData.ingredientPrefab);
        return ingredient;

    }

}
