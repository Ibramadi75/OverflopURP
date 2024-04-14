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
            if (authorSlot.slots[0].GetComponent<Ingredient>().ingredientData.isDeliverable)
            {
                GameObject objectToDeliver = authorSlot.Retrieve();
                slots.Store(objectToDeliver);
                if (Deliver(objectToDeliver))
                {
                    slots.ClearSlots();
                }
            }
        }
    }

    void Update()
    {
        GameObject objectToDeliver = slots.slots[0];
        if (objectToDeliver is not null && Deliver(objectToDeliver))
            slots.ClearSlots();
    }
 
    public override void SecondaryInteraction(GameObject author)
    {
        MainInteraction(author);
    }

    bool Deliver(GameObject objectToDeliver)
    {
        Ingredient ingredient = objectToDeliver.GetComponent<Ingredient>();
        RecipeData recipe = ingredient.ingredientData.recipes[0];
        if (recipe is not null)
        {
            if (_orderManager.LoseOrderOfRecipe(recipe.title))
            {
                Destroy(ingredient.gameObject);
                _gameManager.IncreaseScore(recipe.price);
                return true;
            }
        }
        return false;
    }
}
