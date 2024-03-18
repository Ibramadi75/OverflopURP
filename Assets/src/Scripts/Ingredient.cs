using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Ingredients", menuName = "OverflopURP/Ingredient")]
public class Ingredient : ScriptableObject
{
    public string name;
    public List<Recipe> recipes;
    public bool isBase;
}
