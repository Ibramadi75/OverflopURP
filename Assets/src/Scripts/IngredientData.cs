using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ingredients", menuName = "OverflopURP/IngredientData")]
public class IngredientData : ScriptableObject
{
    public string title;
    public List<RecipeData> recipes;
    public bool isBase;
}
