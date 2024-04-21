using System.Collections.Generic;
using UnityEngine;

public class Recipe : MonoBehaviour
{
    [SerializeField] private RecipeData recipeData;

    public string GetTitle() => recipeData.title;
    public List<IngredientData> GetIngredients() => recipeData.ingredients;
    public float GetBaseExpiration() => recipeData.baseExpiration;
    public float GetPrice() => recipeData.price;
}