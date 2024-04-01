using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryInteraction : AbstractInteraction
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private OrderManager _orderManager;
    
    public override void MainInteraction(GameObject author)
    {
        Slots authorSlot = author.GetComponent<Slots>();
        Slots slots = GetComponent<Slots>();
        
        if (slots.IsEmpty() && !authorSlot.IsEmpty())
        {
            Debug.Log("empty slot and not empty author");
            
            Debug.Log(authorSlot.slots[0]);

            if (authorSlot.slots[0].GetComponent<Ingredient>().ingredientData.isDeliverable) {}
                Deliver(authorSlot.Retrieve());
        }
    }
 
    public override void SecondaryInteraction(GameObject author)
    {
        Debug.Log("tried");
        MainInteraction(author);
    }

    void Deliver(GameObject objectToDeliver)
    {
        Ingredient ingredient = objectToDeliver.GetComponent<Ingredient>();
        RecipeData recipe = ingredient.ingredientData.recipes[0];
        if (recipe is not null)
        {
            if (_orderManager.LoseOrderOfRecipe(recipe.title))
            {
                Destroy(ingredient.gameObject);
                _gameManager.AddTime();
            }
        }
        
    }
}
