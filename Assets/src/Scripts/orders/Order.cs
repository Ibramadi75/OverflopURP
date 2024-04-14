using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order : MonoBehaviour
{
    private Countdown _countdown;
    private Recipe _recipe;
    public Recipe recipe => _recipe;
    
    // Start is called before the first frame update
    void Start()
    {
        _countdown = GetComponent<Countdown>();
        _recipe = GetComponent<Recipe>();

        _countdown.SetTime(_recipe.recipeData.baseExpiration);
    }

    public bool HasExpired() => _countdown.TimeLeft <= 0;

    public bool IsRecipeTitleIs(string title) => _recipe.recipeData.title.Equals(title);
}
