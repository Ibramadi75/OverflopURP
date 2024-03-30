using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public IngredientData ingredientData;
    
    void Start()
    {
        if (GetComponent<Countdown>() is null)
        {
            Debug.Log("A countdown script must be attached.");
        }
    }

    void Update()
    {
        
    }
}
