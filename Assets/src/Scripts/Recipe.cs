using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "OverflopURP/Recipe")]
public class Recipe : ScriptableObject
{
    public string title;
    public List<Ingredient> ingredients;
    [Tooltip("In seconds")] public float baseExpiration;
}