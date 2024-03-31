using UnityEngine;

public class WorkTableInteraction : AbstractInteraction
{
    public override void MainInteraction(GameObject author)
    {
        Slots authorSlot = author.GetComponent<Slots>();
        Slots slots = GetComponent<Slots>();
        if (slots.IsEmpty() && !authorSlot.IsEmpty())
        {
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
            GameObject objectToCut = slots.slots[0];
            Ingredient ingredient = objectToCut.GetComponent<Ingredient>();
            Countdown countdown = objectToCut.GetComponentInChildren<Countdown>();

            if (objectToCut.CompareTag("Ingredient") 
            && ingredient is not null 
            && ingredient.ingredientData.isCuttable)
            {
                if (countdown is not null)
                    countdown.InteractOn();
                else
                {
                    GameObject cutObject = ingredient.ingredientData.cutPrefab;
                    slots.ClearSlots();
                    Replace(objectToCut, cutObject);
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
