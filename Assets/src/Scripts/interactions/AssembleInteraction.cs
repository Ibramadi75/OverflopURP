using UnityEngine;

public class AssembleInteraction : AbstractInteraction
{
    public override void MainInteraction(GameObject author)
    {
        Slot authorSlot = author.GetComponent<Slot>();
        if (slot.IsEmpty() && !authorSlot.IsEmpty())
        {
            if (authorSlot.GetObjectInSlot().GetComponent<Ingredient>().ingredientData.isBase)
                slot.Put(authorSlot.Get());
            
        } else if (!slot.IsEmpty())
        {
            if (authorSlot.IsEmpty())
                authorSlot.Put(slot.Get());
            else
                Assemble(authorSlot);
        }
    }

    private void Assemble(Slot authorSlot)
    {
        if (slot.GetMaxCapacity() == 1 && !slot.IsEmpty())
        {
            GameObject storedObject = slot.GetObjectInSlot();
            GameObject playerObject = authorSlot.Get();
        
            Ingredient storedIngredient = storedObject.GetComponent<Ingredient>();
            Ingredient toAssembleIngredient = playerObject.GetComponent<Ingredient>();

            if (storedObject.CompareTag("Ingredient") && storedIngredient is not null && storedIngredient.ingredientData.isAssemblable)
            {
                GameObject assembleResult =
                    storedIngredient.ingredientData.GetAssembledIngredient(toAssembleIngredient.ingredientData);
            
                if (assembleResult is not null)
                {
                    slot.Clear();
                    slot.Put(assembleResult);
                }
            }
            else authorSlot.Put(playerObject);
        }
    }

    public override void SecondaryInteraction(GameObject author) { }
}
