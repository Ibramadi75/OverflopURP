using UnityEngine;

public class WorkTableInteraction : AbstractInteraction
{
    public override void execute(GameObject author)
    {
        if (slots.IsEmpty() && !author.GetComponent<Slots>().IsEmpty())
            Place(author, gameObject);
        
        else if (!slots.IsEmpty() && author.GetComponent<Slots>().IsEmpty())
        {
            GameObject objectToCut = slots.Retrieve();
            Ingredient ingredient = objectToCut.GetComponent<Ingredient>();
            
            if (!ingredient.ingredientData.isCuttable)
            {
                Give(author, objectToCut, author.transform.position);
                Destroy(GetComponentInChildren<Ingredient>().gameObject);
                return;
            }

            GameObject cutObject = ingredient.ingredientData.cutPrefab;
            Replace(objectToCut, cutObject);
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
