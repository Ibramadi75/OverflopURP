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
            GameObject placed = slots.Retrieve();
            Ingredient ingredient = placed.GetComponent<Ingredient>();
            
            if (ingredient is not null && ingredient.ingredientData.isMeltable)
                StartCoroutine(Cook(ingredient));
            
        }else if (!slots.IsEmpty() && authorSlot.IsEmpty())
        {
            Give(author, slots.Retrieve(), author.transform.position);
            Destroy(gameObject.GetComponentInChildren<Ingredient>().gameObject);
        }
    }

    public override void SecondaryInteraction(GameObject author) {}

    IEnumerator Cook(Ingredient ingredient)
    {
        float time = ingredient.ingredientData.time;
        while (time > 0)
        {
            yield return null;
            time -= Time.deltaTime;
            Debug.Log(time);
        }
        Replace(ingredient.gameObject, ingredient.ingredientData.meltedPrefab);
    }
    
    void Replace(GameObject objectToMelt, GameObject newObject)
    {
        slots.Store(newObject);
        GameObject instantiatedObject = Instantiate(newObject, GetTopPosition(newObject, gameObject), Quaternion.identity);
        instantiatedObject.transform.parent = transform;
        Destroy(objectToMelt.gameObject);
    }
}
