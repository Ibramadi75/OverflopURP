using System.Collections.Generic;
using UnityEngine;

public class Recipe : MonoBehaviour
{
    [SerializeField] private RecipeData recipeData;

    public string GetTitle()
    {
        return recipeData.title;
    }

    public List<IngredientData> GetIngredients()
    {
        return recipeData.ingredients;
    }

    public float GetBaseExpiration()
    {
        return recipeData.baseExpiration;
    }

    public float GetPrice()
    {
        return recipeData.price;
    }
}