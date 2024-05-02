using UnityEngine;

public class AssembleInteraction : AbstractInteraction
{
    public override void MainInteraction(GameObject author)
    {
        var authorSlot = author.GetComponent<Slot>();
        if (slot.IsEmpty() && !authorSlot.IsEmpty())
        {
            if (authorSlot.GetObjectInSlot().GetComponent<Ingredient>().ingredientData.isBase)
                slot.Put(authorSlot.Get());
        }
        else if (!slot.IsEmpty())
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
            var storedObject = slot.GetObjectInSlot();
            var playerObject = authorSlot.Get();

            var storedIngredient = storedObject.GetComponent<Ingredient>();
            var toAssembleIngredient = playerObject.GetComponent<Ingredient>();

            if (storedObject.CompareTag("Ingredient") && storedIngredient is not null &&
                storedIngredient.ingredientData.isAssemblable)
            {
                var assembleResult =
                    storedIngredient.ingredientData.GetAssembledIngredient(toAssembleIngredient.ingredientData);

                if (assembleResult is not null)
                {
                    slot.Clear();
                    slot.Put(assembleResult);
                }
            }
            else
            {
                authorSlot.Put(playerObject);
            }
        }
    }

    public override void SecondaryInteraction()
    {
    }

    protected override void OnCountdownComplete()
    {
    }

    protected override void WhileCountdownRunning()
    {
    }
}