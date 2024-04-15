using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(fileName = "Ingredients", menuName = "OverflopURP/IngredientData")]
public class IngredientData : ScriptableObject
{
    public string title;
    public List<RecipeData> recipes;
    public float time;
    public bool isDeliverable;
    public bool isBase;

    #region cut
    public bool isCuttable;
    public GameObject cutPrefab;
    #endregion
    #region assemble
    public bool isAssemblable;
    public IngredientData canAssembleWith;
    public GameObject assembledPrefab;
    
    #endregion
    #region melt
    public bool isMeltable;
    public GameObject meltedPrefab;
    #endregion
    
    private bool CanAssembleWith(IngredientData ingredientData)
    {
        return isAssemblable && ingredientData == canAssembleWith;
    }

    public GameObject GetAssembledIngredient(IngredientData ingredientData)
    {
        if (ingredientData is not null && CanAssembleWith(ingredientData))
            return assembledPrefab;

        Debug.Log(ingredientData.title + " cannot be assembled with " + title);;
        return null;
    }
}
