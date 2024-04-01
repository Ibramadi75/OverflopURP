using System.Collections;
using UnityEngine;

public class MeltInteraction : AbstractInteraction
{
    private Countdown _countdown;
    public void Start()
    {
        GetComponent<Slots>().ClearSlots();
        _countdown = GetComponent<Countdown>();

        if (_countdown is null)
            _countdown = GetComponentInChildren<Countdown>();

        if (_countdown is null)
            Debug.Log("no countdown for this cooking ?");
    }

    public override void MainInteraction(GameObject author)
    {
        Slots authorSlot = author.GetComponent<Slots>();
        Slots slots = GetComponent<Slots>();

        if (slots.IsEmpty() && !authorSlot.IsEmpty())
        {
            Debug.Log("pose");
            Place(author, gameObject);
            GameObject placed = slots.Retrieve();
            Ingredient ingredient = placed.GetComponentInChildren<Ingredient>();
            
            if (ingredient is not null && ingredient.ingredientData.isMeltable)
            {
                // if (_countdown is null)
                //     Cook(ingredient)
                StartCoroutine(Cook(ingredient));
            }
                
            
        }else if (!slots.IsEmpty() && authorSlot.IsEmpty() && _countdown is null)
        {
            GameObject taken = slots.Retrieve();
            Give(author, taken, author.transform.position);
            foreach (Transform child in transform)
            {
                Destroy(child.gameObject);
            }
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
        GetComponent<Slots>().ClearSlots();
        GetComponent<Slots>().Store(newObject);
        GameObject instantiatedObject = Instantiate(newObject, GetTopPosition(newObject, gameObject), Quaternion.identity);
        instantiatedObject.transform.parent = transform;
        Destroy(objectToMelt.gameObject);
    }
}
