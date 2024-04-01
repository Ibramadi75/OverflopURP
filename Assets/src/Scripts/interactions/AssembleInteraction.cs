using UnityEngine;

public class AssembleInteraction : AbstractInteraction
{
    public override void MainInteraction(GameObject author)
    {
        Slots authorSlot = author.GetComponent<Slots>();
        Debug.Log(authorSlot.IsEmpty());

        Slots slots = GetComponent<Slots>();
        if (slots.IsEmpty() && !authorSlot.IsEmpty())
        {
            if (authorSlot.slots[0].GetComponent<Ingredient>().ingredientData.isBase)
                Place(author, gameObject);
        }else if (!slots.IsEmpty() && authorSlot.IsEmpty())
        {
            Place(gameObject, author);
        }
    }
 
    public override void SecondaryInteraction(GameObject author)
    {
        if (slots.GetCapacity() == 1 && !slots.IsEmpty())
        {
            GameObject storedObject = slots.slots[0];
            Ingredient storedIngredient = storedObject.GetComponent<Ingredient>();
            Ingredient toAssembleIngredient = author.GetComponent<Slots>().Retrieve().GetComponent<Ingredient>();
            Countdown countdown = storedObject.GetComponentInChildren<Countdown>();
            
            if (storedObject.CompareTag("Ingredient") 
                && storedIngredient is not null 
                && storedIngredient.ingredientData is not null
                && storedIngredient.ingredientData.isAssemblable
            )
            {
                if (countdown is not null)
                    countdown.InteractOn();
                else
                {
                    GameObject assembleageResult = storedIngredient.ingredientData.GetAssembledIngredient(toAssembleIngredient.ingredientData);
                    if (assembleageResult is not null)
                    {
                        slots.ClearSlots();
                        Destroy(toAssembleIngredient.gameObject);
                        Replace(storedIngredient.gameObject, assembleageResult);
                    }
                }
            }
        }
    }

    void Replace(GameObject objectToCut, GameObject newObject)
    {
        slots.Store(newObject);
        GameObject instantiatedObject = Instantiate(newObject, GetTopPosition(newObject, gameObject), Quaternion.identity);
        instantiatedObject.transform.parent = transform;
        Destroy(objectToCut.gameObject);
    }
}
