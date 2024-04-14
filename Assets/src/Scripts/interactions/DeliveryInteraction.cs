using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryInteraction : AbstractInteraction
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private OrderManager _orderManager;

    public override void MainInteraction(GameObject author)
    {
        Slot authorSlot = author.GetComponent<Slot>();
        if (slot.IsEmpty() && !authorSlot.IsEmpty())
        {
            if (authorSlot.GetObjectInSlot().GetComponent<Ingredient>().ingredientData.isDeliverable)
            {
                GameObject objectToDeliver = authorSlot.Get();
                if (!Deliver(objectToDeliver))
                    authorSlot.Put(objectToDeliver);
            }
        }
    }
    
    public override void SecondaryInteraction(GameObject author) { }

    bool Deliver(GameObject objectToDeliver)
    {
        Ingredient ingredient = objectToDeliver.GetComponent<Ingredient>();
        RecipeData recipe = ingredient.ingredientData.recipes[0];
        if (recipe is not null)
        {
            if (_orderManager.LoseOrderOfRecipe(recipe.title))
            {
                _gameManager.IncreaseScore(recipe.price);
                return true;
            }
        }
        return false;
    }
}
