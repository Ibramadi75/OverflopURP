using System.Collections;
using UnityEngine;

public class MeltInteraction : AbstractInteraction
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

    public override void SecondaryInteraction(GameObject author) {}
    
    void Replace(GameObject objectToMelt, GameObject newObject)
    {
        slots.Store(newObject);
        GameObject instantiatedObject = Instantiate(newObject, GetTopPosition(newObject, gameObject), Quaternion.identity);
        instantiatedObject.transform.parent = transform;
        Destroy(objectToMelt.gameObject);
    }
}
