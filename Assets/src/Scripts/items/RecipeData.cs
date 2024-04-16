using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "OverflopURP/RecipeData")]
public class RecipeData : ScriptableObject
{
    public string title;
    public List<IngredientData> ingredients;
    [Tooltip("In seconds")] public float baseExpiration;
    public float price;
}