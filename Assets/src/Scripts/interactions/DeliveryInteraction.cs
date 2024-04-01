using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryInteraction : AbstractInteraction
{
    public override void MainInteraction(GameObject author)
    {
        Slots authorSlot = author.GetComponent<Slots>();
        Slots slots = GetComponent<Slots>();
        
        if (slots.IsEmpty() && !authorSlot.IsEmpty())
        {
            Debug.Log("empty slot and not empty author");
            
            Debug.Log(authorSlot.slots[0]);

            if (authorSlot.slots[0].GetComponent<Ingredient>().ingredientData.isDeliverable)
                Place(author, gameObject);
        }
    }
 
    public override void SecondaryInteraction(GameObject author)
    {
        Debug.Log("tried");
        MainInteraction(author);
    }

    void Replace(GameObject objectToCut, GameObject newObject)
    {
        slots.Store(newObject);
        GameObject instantiatedObject = Instantiate(newObject, GetTopPosition(newObject, gameObject), Quaternion.identity);
        instantiatedObject.transform.parent = transform;
        Destroy(objectToCut.gameObject);
    }
}
